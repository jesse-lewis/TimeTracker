using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimeTracker.Data;
using TimeTracker.Models;

namespace TimeTracker.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntriesController : ControllerBase
    {
        private readonly IApplicationDbContext _context;

        public EntriesController(IApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<TimeSheetEntry>> GetEntries()
        {
            return await _context.GetEntriesForUser(GetUserGuid());
        }

        private string GetUserGuid()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}