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
    public class Encounter
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public int PatientId { get; set; }
        [ForeignKey("PatientId")]

        [ValidateNever]
        public Patient Patient { get; set; }

        [Required]
        public string ProviderUserId { get; set; }
        [ForeignKey("ProviderUserId")]

        [ValidateNever]
        public ApplicationUser ProviderUser { get; set; }

        [Required]
        public int SchApptId { get; set; }
        [ForeignKey("SchApptId")]

        [ValidateNever]
        public SchAppt SchAppt { get; set; }

        [Required]
        public int InsuranceId { get; set; }
        [ForeignKey("InsuranceId")]

        [ValidateNever]
        public Insurance Insurance { get; set; }

        public int? FinancialNumAliasId { get; set; }
        [ForeignKey("FinancialNumAliasId")]

        [ValidateNever]
        public FinancialNumAlias? FinancialNumAlias { get; set; }

        [Display(Name ="Health Plan Name")]
        public string HealthPlanName { get; set; }

        [Display(Name = "Member No.")]
        public string MemberNo { get; set; }

        [Display(Name ="Group No.")]
        public string GroupNo { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime InsDate { get; set; } = DateTime.Now;

        [Required]
        public int LocationId { get; set; }
        [ForeignKey("LocationId")]

        [ValidateNever]
        public Location Location { get; set; }
        
        public DateTime? AdmitDateTime { get; set; }

        public DateTime? DschDateTime { get; set; }

        [Required]
        [Display(Name = "Reason for Visit")]
        public string ReasonForVisit { get; set; }

        [Display(Name = "Consent given")]
        public bool ConsentGiven { get; set; } = false;

        [Display(Name = "Provacy Notice")]
        public bool PrivacyNotice { get; set; } = false;

        [Display(Name = "Guarantor Name")]
        public String GuarantorName { get; set; }

        public bool IsDeleted { get; set; }=false;


    }
}
