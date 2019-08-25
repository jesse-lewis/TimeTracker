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
    public class IndexModel : PageModel
    {
        private readonly TimeTracker.Data.ApplicationDbContext _context;

        public IndexModel(TimeTracker.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<TimeSheetEntry> TimeSheetEntry { get;set; }

        public async Task OnGetAsync()
        {
            TimeSheetEntry = await _context.Entries.ToListAsync();
        }
    }
}
