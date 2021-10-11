using API.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;


namespace API.Persistence.Seeding
{
    public static class MovieSeed
    {
        public static void Seed(ModelBuilder modelBuilder,Guid UserId, Guid FantasticGuid, Guid musicalGuid)
        {
            modelBuilder.Entity<Movie>().HasData(new Movie
            {
                Id = Guid.NewGuid(),
                Name = "A fantastic movie",
                Description = "A fantastic description",
                OwnerId = UserId,
                CategoryId = FantasticGuid
            });

            modelBuilder.Entity<Movie>().HasData(new Movie
            {
                Id = Guid.NewGuid(),
                Name = "A musical movie",
                Description = "A musical description",
                OwnerId = UserId,
                CategoryId = musicalGuid
            });
        }
    }
}
