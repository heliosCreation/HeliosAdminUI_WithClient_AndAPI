using API.Application.Enum;
using API.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace API.Persistence.Seeding
{
    public static class ApplicationUserProfileSeed
    {
        public static void Seed(ModelBuilder modelBuilder, Guid userId)
        {
            modelBuilder.Entity<ApplicationUserProfile>().HasData(new ApplicationUserProfile
            {
                Id = userId,
                Subject = Guid.NewGuid().ToString(),
                SubscriptionLevel = SubscriptionLevel.FreeUser.ToString(),
                Role = Roles.Basic.ToString()
            });
        }
    }
}
