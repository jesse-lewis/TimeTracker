using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TimeTracker.Data;
using TimeTracker.Models;

namespace TimeTracker.Pages.Entries
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGet()
        {
            return Page();
        }

        [BindProperty]
        public TimeSheetEntry TimeSheetEntries { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if(!(await GetJobOrNull() is Job job))
            {
                return Page();
            }

            if (!(await GetUserOrNull() is User user))
            {
                return Page();
            }

            TimeSheetEntries.Job = job;

            TimeSheetEntries.User = user;

            _context.Entries.Add(TimeSheetEntries);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        private async Task<User> GetUserOrNull()
        {
            var users = await _context.Users.ToListAsync();
            return users.Find(u => u.Id == GetUserGuid());
        }

        private async Task<Job> GetJobOrNull()
        {
            var jobs = await _context.Jobs.ToListAsync();
            return jobs.Find(u => u.Id == TimeSheetEntries.Job.Id);
        }

        private string GetUserGuid()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}