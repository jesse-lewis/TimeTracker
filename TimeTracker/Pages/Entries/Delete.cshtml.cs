using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TimeTracker.Data;
using TimeTracker.Models;

namespace TimeTracker.Pages.Entries
{
    public class DeleteModel : PageModel
    {
        private readonly IApplicationDbContext _context;

        public DeleteModel(IApplicationDbContext context)
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

            TimeSheetEntry = await GetEntry(id);

            if (TimeSheetEntry == null)
            {
                return NotFound();
            }
            return Page();
        }

        private Task<TimeSheetEntry> GetEntry(long? id)
        {
            return _context.GetEntryById(id.Value, GetUserGuid());
        }

        private string GetUserGuid()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public async Task<IActionResult> OnPostAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TimeSheetEntry = await GetEntry(id);

            if (TimeSheetEntry != null)
            {
                await _context.RemoveTimeSheetEntry(TimeSheetEntry);
            }

            return RedirectToPage("./Index");
        }
    }
}
