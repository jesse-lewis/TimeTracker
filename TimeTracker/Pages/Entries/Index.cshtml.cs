using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TimeTracker.Data;
using TimeTracker.Models;

namespace TimeTracker.Pages.Entries
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<TimeSheetEntry> TimeSheetEntries { get;set; }

        public async Task OnGetAsync()
        {
            var users = await _context.Users.ToListAsync();
            if (!(users.Find(u => u.Id == GetUserGuid()) is User user))
            {
                return;
            }
            TimeSheetEntries = await _context.Entries.Where(e=>e.User == user).ToListAsync();
        }

        private string GetUserGuid()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
