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
    public class Patient
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string FirstName { get; set; }
        
        public string? MiddleName { get; set; }
        
        [Required]
        public string LastName { get; set; }
        
        [Required]
        public string StreetAddress { get; set; }
        
        [Required]
        public string City { get; set; }
        
        [Required]
        public string State { get; set; }
        
        public string Zip { get; set; }

        public string? ReferringPhysician { get; set; }

        public string? PrimaryPhysician { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime BirthDate { get; set; }

        [Display(Name ="Phone Number")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:###-##-####}")]
        [RegularExpression(@"^(\([0-9]{3}\) |[0-9]{3}-)[0-9]{3}-[0-9]{4}$", ErrorMessage = "The Phone Number field is not a valid phone number")]
        public string? PhoneNumber { get; set; }

        [Display(Name = "Mobile Number")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:###-##-####}")]

        [RegularExpression(@"^(\([0-9]{3}\) |[0-9]{3}-)[0-9]{3}-[0-9]{4}$", ErrorMessage = "The Mobile Number field is not a valid phone number")]
        public string? MobileNumber { get; set; }
        
        [Display(Name = "Work Number")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:###-##-####}")]
        [RegularExpression(@"^(\([0-9]{3}\) |[0-9]{3}-)[0-9]{3}-[0-9]{4}$", ErrorMessage = "The Work Number field is not a valid phone number")]
        public string?  WorkNumber { get; set; }

        [Required(ErrorMessage = "SSN is Required")]
        [RegularExpression(@"^\d{9}|\d{3}-\d{2}-\d{4}$", ErrorMessage = "Invalid Social Security Number")]
        public string SSN { get; set; }

        [Required]
        [ForeignKey("GenderId")]
        [InverseProperty("GenderCodeValues")]
        public int GenderId { get; set; }

        [ValidateNever]
        public CodeValue Gender { get; set; }

        [Required]
        [ForeignKey("RaceId")]
        [InverseProperty("RaceCodeValues")]
        public int RaceId { get; set; }

        [ValidateNever]
        public CodeValue Race { get; set; }

        [Required]
        [ForeignKey("EthnicityId")]
        [InverseProperty("EthnicityCodeValues")]
        public int EthnicityId { get; set; }

        [ValidateNever]
        public CodeValue Ethnicity { get; set; }


        public bool IsDeleted { get; set; } = false;
    }
}
