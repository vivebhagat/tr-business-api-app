using Azure.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using PropertySolutionHub.Application.Estate.ContractRequestComponent.Command;
using PropertySolutionHub.Domain.Entities.Auth;
using PropertySolutionHub.Domain.Entities.Estate;
using PropertySolutionHub.Domain.Helper;
using PropertySolutionHub.Domain.Repository;
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
        private readonly string DomainKey = string.Empty;

        public ContractRequestRepository(DapperContext dapperContext, IMemoryCache cache, IAuthDbContext authDb, IHttpHelper httpHelper) : base(cache, authDb)
        {
            _cache = cache;
            _authDb = authDb;
            this.db = new DbFactory<LocalDbContext>(GetConnectionString()).CreateDbContext();
            DomainKey = GetDomainKey();
            _httpHelper = httpHelper;
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
                int propertyId = db.Property.Where(m => m.RemoteId == contractRequest.PropertyId && m.ArchiveDate == null).Select(m => m.Id).FirstOrDefault();
                contractRequest.CreatedDate = DateTime.Now;
                contractRequest.PropertyId = propertyId;
                db.ContractRequests.Add(contractRequest);
                await db.SaveChangesAsync();
                return contractRequest.Id;
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding Request. " + ex.Message);
            }
        }

        public async Task<ContractRequest> UpdateContractRequest(ContractRequest contractRequest)
        {
            try
            {
                Validate(contractRequest);
                contractRequest.ModifiedDate = DateTime.Now;
                db.SetStateAsModified(contractRequest);
                await db.SaveChangesAsync();
                var req = await db.ContractRequests.Where(m => m.Id == contractRequest.Id && m.ArchiveDate == null).Include(m => m.Property).FirstOrDefaultAsync();
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
            return await db.ContractRequests.Where(m => m.ArchiveDate == null).ToListAsync();
        }

        public async Task<ContractRequest> GetContractRequestById(int Id)
        {
            return await db.ContractRequests.Where(m => m.Id == Id && m.ArchiveDate == null).FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateRemoteContractRequest(string postData)
        {
            bool result = await _httpHelper.PostAsync<dynamic>(postData, DomainKey, "/api/ContractRequestExternal/editcontractrequest");
            return result;
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
