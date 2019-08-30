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

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public TimeSheetEntry TimeSheetEntry { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var users = await _context.Users.ToListAsync();
            if (!(users.Find(u => u.Id == GetUserGuid()) is User user))
            {
                return Page();
            }
            TimeSheetEntry.User = user;

            _context.Entries.Add(TimeSheetEntry);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        private string GetUserGuid()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}