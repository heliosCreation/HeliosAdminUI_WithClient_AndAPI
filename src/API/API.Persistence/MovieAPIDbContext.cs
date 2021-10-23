using API.Application.Contracts.Identity;
using API.Domain.Common;
using API.Domain.Entities;
using API.Persistence.Seeding;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace API.Persistence
{
    public class MovieAPIDbContext : DbContext
    {
        private readonly ILoggedInUserService _loggedInUserService;

        public MovieAPIDbContext(DbContextOptions<MovieAPIDbContext> options)
           : base(options)
        {
        }

        public MovieAPIDbContext(DbContextOptions<MovieAPIDbContext> options, ILoggedInUserService loggedInUserService)
            : base(options)
        {
            _loggedInUserService = loggedInUserService;
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ApplicationUserProfile> UserProfiles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MovieAPIDbContext).Assembly);

            //seed data, added through migrations
            var FantasticGuid = Guid.Parse("{B0788D2F-8003-43C1-92A4-EDC76A7C5DDE}");
            var musicalGuid = Guid.Parse("{6313179F-7837-473A-A4D5-A5571B43E6A6}");
            var DramaGuid = Guid.Parse("{BF3F3002-7E53-441E-8B76-F6280BE284AA}");
            var ActionGuid = Guid.Parse("{FE98F549-E790-4E9F-AA16-18C2292A2EE9}");

            CategorySeed.Seed(modelBuilder, FantasticGuid, musicalGuid, DramaGuid, ActionGuid);
        }


        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.Now;
                        entry.Entity.CreatedBy = _loggedInUserService.UserId;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedDate = DateTime.Now;
                        entry.Entity.LastModifiedBy = _loggedInUserService.UserId;
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
