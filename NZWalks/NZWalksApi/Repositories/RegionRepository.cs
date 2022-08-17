using Microsoft.EntityFrameworkCore;
using NZWalksApi.Data;
using NZWalksApi.Models.Domain;

namespace NZWalksApi.Repositories
{
    public class RegionRepository : IRegionRepository
        {
        private readonly NZWaksDbContext nZWaksDbContext;

        public RegionRepository(NZWaksDbContext nZWaksDbContext)
        {
            this.nZWaksDbContext = nZWaksDbContext;
        }

        public async Task<Region> AddAsync(Region region)
        {
            region.Id = Guid.NewGuid();
           await  nZWaksDbContext.AddAsync(region);
            await nZWaksDbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region> DeleteAsync(Guid id)
        {
            var region = await nZWaksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if(region == null)
            {
                return null;
            }

            nZWaksDbContext.Regions.Remove(region);
            await nZWaksDbContext.SaveChangesAsync();
            return region;
        }

        public async Task<IEnumerable<Region>> GetAllAsync()
        {
            return await  nZWaksDbContext.Regions.ToListAsync();
        }


        public async Task<Region> GetAsync(Guid id)
        {
            return await  nZWaksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Region> UpdateAsync(Guid id, Region region)
        {
            var existingRegion = await nZWaksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (existingRegion == null)
            {
                return null;
            }
           existingRegion.Code = region.Code;
            existingRegion.Area = region.Area;
            existingRegion.Name = region.Name;
            existingRegion.Lat = region.Lat;
            existingRegion.Long = region.Long;
            existingRegion.Population = region.Population;

            await nZWaksDbContext.SaveChangesAsync();

            return existingRegion;
        }
    }
}
