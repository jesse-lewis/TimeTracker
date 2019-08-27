using Microsoft.AspNetCore.Identity;

namespace TimeTracker.Models
{
    public class User : IdentityUser
    {
        [PersonalData]
        public string Name { get; set; }
    }
}
