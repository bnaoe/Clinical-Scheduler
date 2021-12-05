using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Models
{
    public class ApplicationUser:IdentityUser
    {
        [Required]
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string? Suffix { get; set; }
        public string? Specialization { get; set; }

        public int LocationId { get; set; }
        [ForeignKey("LocationId")]
        [ValidateNever]

        public Location Location { get; set; }

        [NotMapped]
        public string Role { get; set; }
    }
}
