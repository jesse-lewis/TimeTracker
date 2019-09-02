using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TimeTracker.Models;

namespace TimeTracker.Data
{
    public interface IApplicationDbContext
    {
        Task<List<TimeSheetEntry>> GetEntriesForUser(string userId);
        Task<List<TimeSheetEntry>> GetEntriesForUser(User user);
        Task<TimeSheetEntry> GetEntryById(long id, string userId);
        Task<Job> GetJobById(Guid id);
        Task<User> GetUserByGuid(string userId);
        Task AddTimeSheetEntry(TimeSheetEntry entry);
        Task RemoveTimeSheetEntry(TimeSheetEntry entry);
        Task UpdateTimeSheetEntry(TimeSheetEntry timeSheetEntry);
    }
}