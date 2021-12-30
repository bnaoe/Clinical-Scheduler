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
    public class Document
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("EncounterId")]
        public int EncounterId { get; set; }

        [ValidateNever]
        public Encounter Encounter { get; set; }

        public string? ProviderUserId { get; set; }
        [ForeignKey("ProviderUserId")]

        [ValidateNever]
        public ApplicationUser? ProviderUser { get; set; }

        [Required]
        [ForeignKey("DocTypeId")]
        [InverseProperty("DocTypeCodeValues")]
        public int DocTypeId { get; set; }

        [ValidateNever]
        public CodeValue DocType { get; set; }

        [Required]
        [ForeignKey("DocStatusId")]
        [InverseProperty("DocStatusCodeValues")]
        public int DocStatusId { get; set; }

        [ValidateNever]
        public CodeValue DocStatus { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public int Age { get; set; }

        public int? HeightFt { get; set; }

        [Range(1, 11, ErrorMessage = "Height in inches must be between 1-11.")]
        public float? HeightIn { get; set; }

        public float? Weight { get; set; }

        public int? BMI { get; set; }

        public int? Systolic { get; set; }

        public int? Diastolic { get; set; }

        public int? PulseRate { get; set; }

        public int? OxygenSaturation { get; set; }

        public float? Temperature { get; set; }

        [Range(0, 10, ErrorMessage = "Pain Scale must be 0-10.")]
        public int? PainScale { get; set; }

        public string? PainLocation { get; set; }

        public string? Narrative { get; set; }

        public DateTime CreateDateTime { get; set; } = DateTime.Now;

        public DateTime? ModifiedDateTime { get; set; } = null;

        public bool InError { get; set; }
    }
}
