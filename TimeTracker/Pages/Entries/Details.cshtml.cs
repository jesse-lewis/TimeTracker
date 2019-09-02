using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TimeTracker.Data;
using TimeTracker.Models;

namespace TimeTracker.Pages.Entries
{
    public class DetailsModel : PageModel
    {
        private readonly IApplicationDbContext _context;

        public DetailsModel(IApplicationDbContext context)
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

            TimeSheetEntry = await _context.GetEntryById(id.Value, GetUserGuid());

            if (TimeSheetEntry == null)
            {
                return NotFound();
            }
            return Page();
        }

        private string GetUserGuid()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}
