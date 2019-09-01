using Microsoft.AspNetCore.Identity;
using System;

namespace TimeTracker.Models
{

    public class Job
    {
        public Guid Id { get; set; }
        public string JobNumber { get; set; }

        public override string ToString()
        {
            return JobNumber;
        }
    }
}
