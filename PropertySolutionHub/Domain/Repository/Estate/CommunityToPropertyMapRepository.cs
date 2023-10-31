using AutoMapper;
using Azure.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using PropertySolutionHub.Api.Dto.Estate;
using PropertySolutionHub.Application.Estate.CommunityComponent.Command;
using PropertySolutionHub.Domain.Entities.Estate;
using PropertySolutionHub.Domain.Helper;
using PropertySolutionHub.Domain.Repository;
using PropertySolutionHub.Infrastructure.DataAccess;

namespace PropertySolutionHub.Domain.Repository.Estate
{
    public interface ICommunityToPropertyMapRepository : IDynamicDbRepository
    {
        Task<int> CreateCommunityToPropertyMap(CommunityToPropertyMap community);
        Task<Community> DeleteCommunityToPropertyMap(int Id);
        Task<List<CommunityToPropertyMap>> GetPropertyListForCommunity(int Id);
        Task<List<CommunityToPropertyMap>> GetAllCommunityToPropertyMaps();
        Task<CommunityToPropertyMap> UpdateCommunityToPropertyMap(CommunityToPropertyMap community);
        Task<Community> UpdateCommunitySummaryDetails(int Id);
    }

    public class CommunityToPropertyMapRepository : AbstractDynamicDbRepository, ICommunityToPropertyMapRepository
    {
        IMemoryCache _cache;
        ILocalDbContext db;
        IAuthDbContext _authDb;
        IHttpHelper _httpHelper;
        private readonly string DomainKey = string.Empty;
        IMapper _mapper;

        public CommunityToPropertyMapRepository(DapperContext dapperContext, IMemoryCache cache, IAuthDbContext authDb, IHttpHelper httpHelper, IMapper mapper) : base(cache, authDb)
        {
            _cache = cache;
            _authDb = authDb;
            this.db = new DbFactory<LocalDbContext>(GetConnectionString()).CreateDbContext();
            _httpHelper = httpHelper;
            DomainKey = GetDomainKey();
            _mapper = mapper;
        }

        public void Validate(CommunityToPropertyMap communityToPropertyMap)
        {
            ValidationHelper.CheckIsNull(communityToPropertyMap);
            ValidationHelper.CheckException(communityToPropertyMap.PropertyId <= 0, "Property is required.");
        }

        public async Task<int> CreateCommunityToPropertyMap(CommunityToPropertyMap communityToPropertyMap)
        {

            try
            {
                Validate(communityToPropertyMap);

                if (db.CommunityToPropertyMaps.Any(m => m.CommunityId == communityToPropertyMap.CommunityId && m.PropertyId == communityToPropertyMap.PropertyId && m.ArchiveDate == null))
                {
                    throw new Exception("Duplicate property found.");
                }

                communityToPropertyMap.CreatedDate = DateTime.Now; 
                db.CommunityToPropertyMaps.Add(communityToPropertyMap);
                await db.SaveChangesAsync();
                return communityToPropertyMap.Id;
            }
            catch (Exception ex)
            {
                throw new Exception("Error " + ex.Message);
            }
        }

        public async Task<CommunityToPropertyMap> UpdateCommunityToPropertyMap(CommunityToPropertyMap communityToPropertyMap)
        {
            try
            {
                Validate(communityToPropertyMap);

                if (db.CommunityToPropertyMaps.Any(m => m.CommunityId == communityToPropertyMap.CommunityId && m.PropertyId == communityToPropertyMap.PropertyId && m.Id != communityToPropertyMap.Id && m.ArchiveDate == null))
                {
                    throw new Exception("Duplicate property found.");
                }

                communityToPropertyMap.ModifiedDate = DateTime.Now;
                db.SetStateAsModified(communityToPropertyMap);
                await db.SaveChangesAsync();
                return communityToPropertyMap;
            }
            catch (Exception ex)
            {
                throw new Exception("Error " + ex.Message);
            }
        }

        public async Task<Community> UpdateCommunitySummaryDetails(int Id)
        {
            try
            {
                Community community = db.Communities.FirstOrDefault(m => m.Id == Id && m.ArchiveDate == null);

                if (community == null)
                    return null;

                List<CommunityToPropertyMap> communityToPropertyMapList = db.CommunityToPropertyMaps
                    .Where(m => m.CommunityId == Id && m.ArchiveDate == null)
                    .Include(m => m.Property)
                    .ToList();

                if (communityToPropertyMapList.Count == 0)
                {
                    community.PriceFrom = 0;
                    community.PriceTo = 0;
                    community.BedFrom = 0;
                    community.BedTo = 0;
                    community.BathFrom = 0;
                    community.BathTo = 0;
                    community.AreaFrom = 0;
                    community.AreaTo = 0;
                    community.NumberOfUnits = 0;
                }
                else
                {
                    community.PriceFrom = communityToPropertyMapList.Min(item => item.Property.Price);
                    community.PriceTo = communityToPropertyMapList.Max(item => item.Property.Price);
                    community.BedFrom = communityToPropertyMapList.Min(item => item.Property.Bedrooms);
                    community.BedTo = communityToPropertyMapList.Max(item => item.Property.Bedrooms);
                    community.BathFrom = communityToPropertyMapList.Min(item => item.Property.Bathrooms);
                    community.BathTo = communityToPropertyMapList.Max(item => item.Property.Bathrooms);
                    community.AreaFrom = (int)communityToPropertyMapList.Min(item => item.Property.Area);
                    community.AreaTo = (int)communityToPropertyMapList.Max(item => item.Property.Area);
                    community.NumberOfUnits = communityToPropertyMapList.Count;
                }
                await db.SaveChangesAsync();

                return community;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<Community> DeleteCommunityToPropertyMap(int Id)
        {
            try
            {
                CommunityToPropertyMap communityToPropertyMap = db.CommunityToPropertyMaps.Where(m => m.Id == Id && m.ArchiveDate == null).FirstOrDefault();

                if (communityToPropertyMap == null)
                    throw new Exception("property does not exist.");

                Community community = db.Communities.Where(m => m.Id == communityToPropertyMap.CommunityId && m.ArchiveDate == null).FirstOrDefault();

                if (IsLastItem(community.Id))
                    community.IsPublished = false;

                communityToPropertyMap.ArchiveDate = DateTime.Now;
                await db.SaveChangesAsync();

                return await UpdateCommunitySummaryDetails(community.Id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting property. " + ex.Message);
            }
        }


        private bool IsLastItem(int communityId)
        {
            int itemCount = db.CommunityToPropertyMaps
                .Count(m => m.CommunityId == communityId && m.ArchiveDate == null);

            return itemCount == 1;
        }

        public async Task<List<CommunityToPropertyMap>> GetAllCommunityToPropertyMaps()
        {
            return await db.CommunityToPropertyMaps.Where(m => m.ArchiveDate == null).ToListAsync();
        }

        public async Task<List<CommunityToPropertyMap>> GetPropertyListForCommunity(int Id)
        {
            return await db.CommunityToPropertyMaps.Where(m => m.CommunityId == Id && m.ArchiveDate == null).ToListAsync();
        }
    }
}
