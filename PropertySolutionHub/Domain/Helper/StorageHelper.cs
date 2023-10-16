using System;
using System.Text.RegularExpressions;

namespace PropertySolutionHub.Domain.Helper
{
    public interface IStorageHelper
    {
        string GetStoragePath(string domainkey, string typeName, string modelName);
        string GetUrlPath(HttpContext httpContext, string domainkey, string typeName, string modelName);
        void UploadImage(int Id, string fileStoragePath, string fileUrlPath, string modelName, IFormFile postedImageFile, bool replaceExistitng, ref string uploadedImageFileUrl);
        bool DeleteFromStorage(string storagePath, string fileName);
    }

    public class StorageHelper : IStorageHelper
    {
        IWebHostEnvironment _environment;
        protected const string imageExtensionPattern = "\\.(?i)(jpg|jpeg|png|gif|bmp|webp)$";

        public StorageHelper(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public static bool IsValidImageFile(IFormFile postedImageFile)
        {
            if (postedImageFile == null || postedImageFile.Length == 0)
                return false;

            try
            {
                if (!Regex.IsMatch(postedImageFile.FileName, imageExtensionPattern))
                    return false;

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    postedImageFile.CopyTo(memoryStream);

                    Image image = Image.Load(memoryStream.ToArray());

                    return image != null;
                }
            }
            catch (Exception e)
            {
                throw new Exception("An error occurred while validating the uploaded image file.", e);
            }
        }

        public string GetStoragePath(string domainkey, string typeName, string modelName)
        {
            return string.Format("/files/{0}/{1}/{2}/", domainkey, typeName, modelName);
        }

        public string GetUrlPath(HttpContext httpContext, string domainkey, string typeName, string modelName)
        {
            return string.Format("{0}://{1}:{2}/files/{3}/{4}/{5}", httpContext.Request.Scheme, httpContext.Request.Host.Host, httpContext.Request.Host.Port, domainkey, typeName, modelName);
        }

        public bool DeleteFromStorage(string storagePath, string fileName)
        {
            try
            {
                File.Delete(Path.Combine(_environment.ContentRootPath + storagePath + fileName));
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        private void ReplaceExistingImageFromStorage(string fileDirectory, IFormFile postedImageFile, string modelName, ref string path)
        {
            DirectoryInfo directory = new DirectoryInfo(fileDirectory);
            FileInfo[] allFiles = directory.GetFiles();
            FileInfo existingImageFile = allFiles.Where(file => file.ToString().Split('_')[0] == postedImageFile.FileName.Split('_')[0]).FirstOrDefault();

            if (existingImageFile != null)
            {
                try
                {
                    using MemoryStream memoryStream = new MemoryStream();
                    postedImageFile.CopyTo(memoryStream);
                    byte[] data = memoryStream.ToArray();
                    File.WriteAllBytes(fileDirectory + existingImageFile.Name, data);
                    string newName = existingImageFile.Name.Replace(Path.GetExtension(existingImageFile.Name), Path.GetExtension(postedImageFile.FileName));
                    File.Move(fileDirectory + existingImageFile.Name, fileDirectory + newName);
                    path = Path.GetFileName(fileDirectory + newName);
                }
                catch (Exception e)
                {
                    throw new Exception("Failed to update file.");
                }
            }
        }

        public void UploadImage(int Id, string fileStoragePath, string fileUrlPath, string modelName, IFormFile postedImageFile, bool replaceExistitng, ref string uploadedImageFileUrl)
        {
            if (Id <= 0)
                throw new Exception("Id associated with this image file is invalid.");

            if (string.IsNullOrEmpty(fileStoragePath))
                throw new Exception("File storage path cannot be null or empty.");

            if (string.IsNullOrEmpty(fileUrlPath))
                throw new Exception("File URL path cannot be null or empty.");

            if (string.IsNullOrEmpty(modelName))
                throw new Exception("Model name cannot be null or empty.");

            try
            {
                string fileDirectory = Path.Combine(_environment.ContentRootPath + fileStoragePath);

                if (string.IsNullOrEmpty(fileDirectory))
                    throw new Exception("Invalid directory! The file directory path not found.");

                Directory.CreateDirectory(fileDirectory);

                if (!IsValidImageFile(postedImageFile))
                    throw new Exception("uploaded file is not a valid image file.");
                else
                {
                    string replacedImageFileName = string.Empty;

                    if (string.IsNullOrEmpty(replacedImageFileName))
                    {
                        var newImageFileName = string.Format("{0}-{1}_{2}{3}", modelName, Id, Guid.NewGuid(), Path.GetExtension(postedImageFile.FileName));

                        var newFilePath = Path.Combine(fileDirectory, newImageFileName);
                        using (var stream = new FileStream(newFilePath, FileMode.Create))
                        {
                            postedImageFile.CopyTo(stream);
                        }

                        uploadedImageFileUrl = Path.Combine(fileUrlPath, newImageFileName);
                    }
                    else
                        uploadedImageFileUrl = Path.Combine(fileUrlPath, replacedImageFileName);
                }
            }
            catch (Exception e)
            {
                throw new Exception("An error occurred while uploading the image file.");
            }
        }

    }
}
