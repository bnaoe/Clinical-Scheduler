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
    public class ProviderScheduleProfile
    {
        [Key]
        public int Id { get; set; }

        public string ProviderUserId { get; set; }
        [ForeignKey("ProviderUserId")]
        
        [ValidateNever]
        public ApplicationUser ProviderUser { get; set; }
        
        [Required]
        public int LocationId { get; set; }

        [ValidateNever]
        public Location Location { get; set; }

        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }
        public bool Saturday { get; set; }
        public bool Sunday { get; set; }

        [Required]
        public TimeSpan StartTime { get; set; }
        
        [Required]
        public TimeSpan EndTime { get; set; }

        public bool isDeleted { get; set; } = false;

    }
}
