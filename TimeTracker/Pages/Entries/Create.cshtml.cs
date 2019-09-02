using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TimeTracker.Data;
using TimeTracker.Models;

namespace TimeTracker.Pages.Entries
{
    public class CreateModel : PageModel
    {
        private readonly IApplicationDbContext _context;

        public CreateModel(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGet()
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

            if(!(await GetJobOrNull() is Job job))
            {
                return Page();
            }

            if (!(await GetUserOrNull() is User user))
            {
                return Page();
            }

            TimeSheetEntry.Job = job;

            TimeSheetEntry.User = user;

            await _context.AddTimeSheetEntry(TimeSheetEntry);

            return RedirectToPage("./Index");
        }

        private async Task<User> GetUserOrNull()
        {
            return await _context.GetUserByGuid(GetUserGuid());
        }

        private async Task<Job> GetJobOrNull()
        {
            return await _context.GetJobById(TimeSheetEntry.Job.Id);
        }

        private string GetUserGuid()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}