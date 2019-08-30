using Microsoft.AspNetCore.Identity;
using System;

namespace TimeTracker.Models
{
    public class User : IdentityUser
    {
        [PersonalData]
        public string Name { get; set; }

        public override bool Equals(object obj)
        {
            return obj is User user &&
                   Id == user.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }
}
