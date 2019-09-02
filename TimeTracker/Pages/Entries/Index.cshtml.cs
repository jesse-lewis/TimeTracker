using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TimeTracker.Data;
using TimeTracker.Models;

namespace TimeTracker.Pages.Entries
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IApplicationDbContext _context;

        public IndexModel(IApplicationDbContext context)
        {
            _context = context;
        }

        public IList<TimeSheetEntry> TimeSheetEntries { get;set; }

        public async Task OnGetAsync()
        {
            TimeSheetEntries = await _context.GetEntriesForUser(GetUserGuid());
        }

        private string GetUserGuid()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}
