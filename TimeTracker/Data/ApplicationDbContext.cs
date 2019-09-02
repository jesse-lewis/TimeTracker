using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TimeTracker.Models;

namespace TimeTracker.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<TimeSheetEntry> Entries { get; set; }
        public DbSet<Job> Jobs { get; set; }

        public async Task<Job> GetJobById(Guid id)
        {
            return await Jobs.FindAsync(id);
        }

        public async Task<TimeSheetEntry> GetEntryById(long id, string userId)
        {
            if (await GetUserByGuid(userId) is User user)
            {
                var entry = await Entries.Where(e => e.Id == id).Include(e => e.Job).FirstOrDefaultAsync();
                if (entry?.User == user)
                {
                    return entry;
                }
            }
            return null;
        }

        public async Task<List<TimeSheetEntry>> GetEntriesForUser(string userId)
        {
            if (await GetUserByGuid(userId) is User user)
            {
                return await GetEntriesForUser(user);
            }
            return null;
        }

        public Task<List<TimeSheetEntry>> GetEntriesForUser(User user)
        {
            return Entries.Where(e => e.User == user).Include(e => e.Job).ToListAsync();
        }

        public async Task<User> GetUserByGuid(string userId)
        {
            return await Users.FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task AddTimeSheetEntry(TimeSheetEntry entry)
        {
            Entries.Add(entry);
            await SaveChangesAsync();
        }

        public async Task RemoveTimeSheetEntry(TimeSheetEntry entry)
        {
            Entries.Remove(entry);
            await SaveChangesAsync();
        }

        public async Task UpdateTimeSheetEntry(TimeSheetEntry entry)
        {
            Attach(entry).State = EntityState.Modified;
            await SaveChangesAsync();
        }
    }
}
