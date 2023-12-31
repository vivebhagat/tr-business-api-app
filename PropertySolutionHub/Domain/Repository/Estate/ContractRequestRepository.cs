﻿using Azure.Core;
using Castle.Core.Resource;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using PropertySolutionHub.Application.Estate.ContractRequestComponent.Command;
using PropertySolutionHub.Domain.Entities.Auth;
using PropertySolutionHub.Domain.Entities.Estate;
using PropertySolutionHub.Domain.Entities.Users;
using PropertySolutionHub.Domain.Helper;
using PropertySolutionHub.Domain.Repository;
using PropertySolutionHub.Domain.Service.Email;
using PropertySolutionHub.Infrastructure.DataAccess;

namespace ContractRequestSolutionHub.Domain.Repository.Estate
{
    public interface IContractRequestRepository : IDynamicDbRepository
    {
        Task<int> CreateContractRequest(ContractRequest contractRequest);
        Task<int> DeleteContractRequest(int Id);
        Task<bool> DeleteRemoteContractRequest(int Id);
        Task<ContractRequest> GetContractRequestById(int Id);
        Task<List<ContractRequest>> GetAllContractRequests();
        Task<ContractRequest> UpdateContractRequest(ContractRequest contractRequest);
        Task<bool> UpdateRemoteContractRequest(string postData);
        Task<bool> WithdrawContractRequest(int Id);
    }

    public class ContractRequestRepository : AbstractDynamicDbRepository, IContractRequestRepository
    {
        IMemoryCache _cache;
        ILocalDbContext db;
        IAuthDbContext _authDb;
        IHttpHelper _httpHelper;
        ILogger<ContractRequest> _logger;
        IWebHostEnvironment _environment;
        private readonly string DomainKey = string.Empty;
        IAzureEmailService _azureEmailService;

        public ContractRequestRepository(DapperContext dapperContext, IMemoryCache cache, IAuthDbContext authDb, IHttpHelper httpHelper, ILogger<ContractRequest> logger, IWebHostEnvironment environment, IAzureEmailService azureEmailService) : base(cache, authDb)
        {
            _cache = cache;
            _authDb = authDb;
            this.db = new DbFactory<LocalDbContext>(GetConnectionString()).CreateDbContext();
            DomainKey = GetDomainKey();
            _httpHelper = httpHelper;
            _logger = logger;
            _azureEmailService = azureEmailService;
            _environment = environment;
        }


        public void Validate(ContractRequest contractRequest)
        {
            ValidationHelper.CheckIsNull(contractRequest);
            ValidationHelper.CheckException(contractRequest.PropertyId == 0, "Property is required.");
            ValidationHelper.CheckException(contractRequest.CustomerId == 0, "Customer is required.");
            ValidationHelper.CheckException(contractRequest.ProposedPurchasePrice == 0, "Proposed price should be positive.");
        }

        public async Task<int> CreateContractRequest(ContractRequest contractRequest)
        {

            try
            {
                Validate(contractRequest);
                Property property = db.Property.Where(m => m.RemoteId == contractRequest.PropertyId && m.ArchiveDate == null).FirstOrDefault();

                bool exists = db.ContractRequests.Any(m => m.CustomerId == contractRequest.CustomerId && m.PropertyId == contractRequest.PropertyId
                        && m.Status != ContractRequestStatus.Rejected && m.Status != ContractRequestStatus.Withdraw && m.ArchiveDate == null);

                if (exists)
                    throw new Exception("Duplicate application found.");

                contractRequest.CreatedDate = DateTime.Now;
                contractRequest.PropertyId = property.Id;
                db.ContractRequests.Add(contractRequest);
                await db.SaveChangesAsync();
                await SendNewApplicationEmail(contractRequest.Id, property.Id);

                return contractRequest.Id;
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding Request. " + ex.Message);
            }
        }

        public async Task<bool> SendNewApplicationEmail(int contractRequestId, int propertyId)
        {
            try
            {
                ContractRequest contractRequest = db.ContractRequests.Where(m => m.Id == contractRequestId && m.ArchiveDate == null).FirstOrDefault();

                if (contractRequest == null)
                    return false;

                Property property = db.Property.Where(m => m.Id == propertyId && m.ArchiveDate == null).FirstOrDefault();


                if (property == null)
                    return false;

                BusinessUser businessUser = db.BusinessUsers.Where(m => m.Id == property.PropertyManagerId && m.ArchiveDate == null).FirstOrDefault();

                if (businessUser == null)
                    return false;

                Customer customer = await _httpHelper.GetAsync<Customer>("/api/CustomerDefault/GetCustomerById/" + contractRequest.CustomerId, DomainKey);

                if (customer == null)
                    return false;


                string emailBody = LoadEmailTemplate("BizApplicationTemplate.html");

                if (string.IsNullOrEmpty(emailBody))
                    return false;

                emailBody = ReplaceNewApplicationTokens(emailBody, customer, businessUser, property, contractRequest);

                if (string.IsNullOrEmpty(emailBody))
                    return false;

                if (!await Send(customer.Email, "New Application", emailBody))
                    return false;

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error  sending email. " + ex.Message);
            }

        }

        private string ReplaceNewApplicationTokens(string emailBody, Customer customer, BusinessUser businessUser, Property property, ContractRequest request)
        {
            try
            {
                emailBody = emailBody.Replace("{User}", businessUser.FirstName);
                emailBody = emailBody.Replace("{CustomerName}", customer.FirstName + ' ' + customer.LastName);
                emailBody = emailBody.Replace("{Name}", property.Name);
                emailBody = emailBody.Replace("{Url}", property.Url);
                emailBody = emailBody.Replace("{Price}", String.Format("{0:C}", property.Price));
                emailBody = emailBody.Replace("{Description}", property.Description);
                emailBody = emailBody.Replace("{Bedrooms}", property.Bedrooms.ToString());
                emailBody = emailBody.Replace("{Bathrooms}", property.Bathrooms.ToString());
                emailBody = emailBody.Replace("{Area}", property.Area.ToString("N0"));
                emailBody = emailBody.Replace("{Status}", request.Status.ToString());

                return emailBody;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<bool> SendApplicationStatusUpdateEmail(int contractRequestId, int customerId, int propertyId)
        {
            try
            {
                ContractRequest contractRequest = db.ContractRequests.Where(m => m.Id == contractRequestId && m.ArchiveDate == null).FirstOrDefault();
                Property property = db.Property.Where(m => m.Id == propertyId && m.ArchiveDate == null).FirstOrDefault();

                Customer customer = await _httpHelper.GetAsync<Customer>("/api/CustomerDefault/GetCustomerById/" + customerId, DomainKey);

                string emailBody = LoadEmailTemplate("ApplicationStatusUpdateTemplate.html");

                if (string.IsNullOrEmpty(emailBody))
                    return false;

                emailBody = ReplaceApplicationUpdateTokens(emailBody, customer, property, contractRequest);

                if (string.IsNullOrEmpty(emailBody))
                    return false;

                if (!await Send(customer.Email, "Trilineas Application Update", emailBody))
                    return false;

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error sending email. " + ex.Message);
            }
        }

        private string ReplaceApplicationUpdateTokens(string emailBody, Customer customer, Property property, ContractRequest request)
        {
            try
            {
                emailBody = emailBody.Replace("{User}", customer.FirstName);
                emailBody = emailBody.Replace("{Name}", property.Name);
                emailBody = emailBody.Replace("{Url}", property.Url);
                emailBody = emailBody.Replace("{Price}", String.Format("{0:C}", property.Price));
                emailBody = emailBody.Replace("{Description}", property.Description);
                emailBody = emailBody.Replace("{Bedrooms}", property.Bedrooms.ToString());
                emailBody = emailBody.Replace("{Bathrooms}", property.Bathrooms.ToString());
                emailBody = emailBody.Replace("{Area}", property.Area.ToString("N0"));
                emailBody = emailBody.Replace("{Status}", request.Status.ToString());

                return emailBody;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        private string LoadEmailTemplate(string templateFileName)
        {
            try
            {
                string templatePath = Path.Combine(_environment.ContentRootPath, "Files", templateFileName);
                using StreamReader reader = new(templatePath);
                return reader.ReadToEnd();
            }
            catch (Exception e)
            {
                return null;
            }
        }

        private async Task<bool> Send(string email, string subject, string emailBody)
        {
            try
            {
                var emailRequest = new EmailRequest
                {
                    RecieverAddresses = new List<string>(),
                    PrimaryRecieverAddress = email,
                    Subject = subject,
                    Body = emailBody
                };

                await _azureEmailService.SendEmail(emailRequest);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<ContractRequest> UpdateContractRequest(ContractRequest contractRequest)
        {
            try
            {
                Validate(contractRequest);
                contractRequest.ModifiedDate = DateTime.Now;

                if(contractRequest.Status == ContractRequestStatus.Approved)
                {
                    contractRequest.ApprovalDate = DateTime.Now;
                    contractRequest.IsApproved = true;
                }
                else
                {
                    contractRequest.ApprovalDate = null;
                    contractRequest.IsApproved = false;
                }

                db.SetStateAsModified(contractRequest);
                await db.SaveChangesAsync();

                var req = await db.ContractRequests.Where(m => m.Id == contractRequest.Id && m.ArchiveDate == null).Include(m => m.Property).FirstOrDefaultAsync();

                await SendApplicationStatusUpdateEmail(req.Id, (int)req.Property.PropertyManagerId, req.PropertyId);

                return contractRequest;
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating request. " + ex.Message);
            }
        }

        public async Task<int> DeleteContractRequest(int Id)
        {
            try
            {
                ContractRequest contractRequest = db.ContractRequests.Where(m => m.Id == Id && m.ArchiveDate == null).FirstOrDefault();

                if (contractRequest == null)
                    throw new Exception("Request does not exist.");

                contractRequest.ArchiveDate = DateTime.Now;
                await db.SaveChangesAsync();
                return contractRequest.Id;
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting Request. " + ex.Message);
            }
        }

        public async Task<List<ContractRequest>> GetAllContractRequests()
        {
            return await db.ContractRequests.Where(m => m.ArchiveDate == null).OrderByDescending(m => m.Id).ToListAsync();
        }

        public async Task<ContractRequest> GetContractRequestById(int Id)
        {
            return await db.ContractRequests.Where(m => m.Id == Id && m.ArchiveDate == null).FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateRemoteContractRequest(string postData)
        {
            var result = await _httpHelper.PostAsync<dynamic>(postData, DomainKey, "/api/ContractRequestExternal/editcontractrequest");
            return result != null;
        }

        public async Task<bool> WithdrawContractRequest(int Id)
        {
            ContractRequest contractRequest =  await db.ContractRequests.Where(m => m.Id == Id && m.ArchiveDate == null).FirstOrDefaultAsync();
            contractRequest.Status = ContractRequestStatus.Withdraw;
            db.SaveChanges();
            return true;
        }

        public async Task<bool> DeleteRemoteContractRequest(int Id)
        {
            var deleted = await _httpHelper.GetAsync<bool>("/api/ContractRequestExternal/deletecontractrequest/" + Id, DomainKey);
            return deleted;
        }
    }
}
