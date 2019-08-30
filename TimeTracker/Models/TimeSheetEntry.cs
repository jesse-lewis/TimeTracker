using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TimeTracker.Models
{
    public class TimeSheetEntry
    {
        public long Id { get; set; }
        public User User { get; set; }

        [Range(0,24)]
        public byte Hours { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
    }
}
