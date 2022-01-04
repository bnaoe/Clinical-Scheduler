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
    public class Diagnosis
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("PatientId")]
        public int PatientId { get; set; }

        [ValidateNever]
        public Patient Patient { get; set; }

        [Required]
        [ForeignKey("EncounterId")]
        public int EncounterId { get; set; }

        [ValidateNever]
        public Encounter Encounter { get; set; }

        [ForeignKey("ProviderUserId")]
        public string? ProviderUserId { get; set; }

        [ValidateNever]
        public ApplicationUser? ProviderUser { get; set; }

        [Required]
        [ForeignKey("DxCodeId")]
        public int DxCodeId { get; set; }

        [ValidateNever]
        public DxCode DxCode { get; set; }

        public bool IsActive { get; set; } = true;

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0: MM/dd/yyyy}")]
        public DateTime ActiveDtTm { get; set; } = DateTime.Now;


        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0: MM/dd/yyyy}")]
        public DateTime? EndDtTm { get; set; }
    }
}
