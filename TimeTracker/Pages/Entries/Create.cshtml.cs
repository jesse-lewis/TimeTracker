using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TimeTracker.Data;
using TimeTracker.Models;

namespace TimeTracker.Pages.Entries
{
    public class CreateModel : PageModel
    {
        private readonly TimeTracker.Data.ApplicationDbContext _context;

        public CreateModel(TimeTracker.Data.ApplicationDbContext context)
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

            _context.Entries.Add(TimeSheetEntry);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}