using Microsoft.EntityFrameworkCore;
using NZWalksApi.Models.Domain;

namespace NZWalksApi.Data
{
    public class NZWaksDbContext: DbContext
    {
        public NZWaksDbContext(DbContextOptions<NZWaksDbContext> options): base(options)
        {

        }

        public DbSet<Region> Regions { get; set; }

        public DbSet<Walk> Walks { get; set; }

        public DbSet<WalkDifficulty> WalkDifficulty { get; set; }

    }
}
