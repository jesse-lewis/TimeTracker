using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TimeTracker.Data;
using TimeTracker.Models;

namespace TimeTracker.Pages.Entries
{
    public class EditModel : PageModel
    {
        private readonly IApplicationDbContext _context;

        public EditModel(IApplicationDbContext context)
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

            TimeSheetEntry = await _context.GetEntryById(id.Value, GetUserGuid());

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

            try
            {
                await _context.UpdateTimeSheetEntry(TimeSheetEntry);
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
            return _context.GetEntryById(id, GetUserGuid()) != null;
        }

        private string GetUserGuid()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
