using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TimeTracker.Data;
using TimeTracker.Models;

namespace TimeTracker.Pages.Entries
{
    public class EditModel : PageModel
    {
        private readonly TimeTracker.Data.ApplicationDbContext _context;

        public EditModel(TimeTracker.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(TimeSheetEntry).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TimeSheetEntryExists(TimeSheetEntry.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool TimeSheetEntryExists(long id)
        {
            return _context.Entries.Any(e => e.Id == id);
        }
    }
}
