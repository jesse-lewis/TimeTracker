using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TimeTracker.Data;
using TimeTracker.Models;

namespace TimeTracker.Pages.Entries
{
    public class DetailsModel : PageModel
    {
        private readonly TimeTracker.Data.ApplicationDbContext _context;

        public DetailsModel(TimeTracker.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public TimeSheetEntry TimeSheetEntry { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TimeSheetEntry = await _context.Entries.FirstOrDefaultAsync(m => m.Id == id);

            if (TimeSheetEntry == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
