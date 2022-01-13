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
    public class Allergy
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int PatientId { get; set; }
        [ForeignKey("PatientId")]

        [ValidateNever]
        public Patient Patient { get; set; }

        [ForeignKey("ProviderUserId")]
        public string? ProviderUserId { get; set; }

        [ValidateNever]
        public ApplicationUser? ProviderUser { get; set; }

        [Required]
        public string AllergyName { get; set; }

        public bool IsActive { get; set; } = true;

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0: MM/dd/yyyy}")]
        public DateTime ActiveDtTm { get; set; } = DateTime.Now;

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0: MM/dd/yyyy}")]
        public DateTime? EndDtTm { get; set; }

    }
}
