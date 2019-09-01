using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using TimeTracker.Data;

namespace TimeTracker.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using(var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                if (!context.Jobs.Any())
                {
                    SeedJobs(context);
                }
            }
        }

        private static void SeedJobs(ApplicationDbContext context)
        {
            context.Jobs.AddRange(
                new Job
                {
                    Id = Guid.NewGuid(),
                    JobNumber = "19-1"
                },
                new Job
                {
                    Id = Guid.NewGuid(),
                    JobNumber = "19-2"
                });
            context.SaveChanges();
        }
    }
}
