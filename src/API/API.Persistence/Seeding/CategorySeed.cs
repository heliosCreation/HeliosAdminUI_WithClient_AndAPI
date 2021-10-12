using API.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace API.Persistence.Seeding
{
    public static class CategorySeed
    {
        public static void Seed(ModelBuilder modelBuilder, Guid FantasticGuid, Guid musicalGuid, Guid DramaGuid, Guid ActionGuid)
        {
            modelBuilder.Entity<Category>().HasData(new Category
            {
                Id = FantasticGuid,
                Name = "Fantastic"
            });
            modelBuilder.Entity<Category>().HasData(new Category
            {
                Id = musicalGuid,
                Name = "Musicals"
            });
            modelBuilder.Entity<Category>().HasData(new Category
            {
                Id = DramaGuid,
                Name = "Drama"
            });
            modelBuilder.Entity<Category>().HasData(new Category
            {
                Id = ActionGuid,
                Name = "Action"
            });
        }
    }
}

