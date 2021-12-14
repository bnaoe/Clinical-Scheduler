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
    public class SchAppt
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("PatientId")]
        public int PatientId { get; set; }

        [ValidateNever]
        public Patient Patient { get; set; }

        [Required]
        [ForeignKey("ProviderScheduleProfileId")]
        public int ProviderScheduleProfileId { get; set; }

        [ValidateNever]
        public ProviderScheduleProfile ProviderScheduleProfile { get; set; }

        [Required]
        [ForeignKey("RegistrarUserId")]
        public string RegistrarUserId { get; set; }

        [ValidateNever]
        public ApplicationUser RegistrarUser { get; set; }

        [Required]
        [ForeignKey("ApptTypeId")]
        [InverseProperty("ApptTypeCodeValues")]
        public int ApptTypeId { get; set; }

        [ValidateNever]
        public CodeValue ApptType { get; set; }

        [Required]
        [ForeignKey("ApptStatusId")]
        [InverseProperty("ApptStatusCodeValues")]
        public int ApptStatusId { get; set; }

        [ValidateNever]
        public CodeValue ApptStatus { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = @"{0:yyyy-MM-ddTHH:mm}", ApplyFormatInEditMode = true)]
        public DateTime start_date { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = @"{0:yyyy-MM-ddTHH:mm}", ApplyFormatInEditMode = true)]
        public DateTime end_date { get; set; }

        public string text { get; set; }
    }
}
