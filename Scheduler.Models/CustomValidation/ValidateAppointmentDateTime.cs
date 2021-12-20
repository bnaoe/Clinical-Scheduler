using Scheduler.Models;
using System.ComponentModel.DataAnnotations;

namespace ClinicalScheduler.CustomValidation
{
    public class ValidateAppointmentDateTime : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var schAppt = (SchAppt)validationContext.ObjectInstance;

            if (schAppt.start_valid == false || schAppt.end_valid == false) 
            {
                return new ValidationResult("Appointment date and time is outside providers operational hours.");
            };

            int[] minList = { 0, 15, 30, 45 };

            foreach (var min in minList)
            {
                if (schAppt.start_date.Minute == min)
                    return ValidationResult.Success;
            }

            foreach (var min in minList)
            {
                if (schAppt.end_date.Minute == min)
                    return ValidationResult.Success;
            }

            return new ValidationResult("Minutes must be in 0, 15, 30, 45 min values.");
        }
    }
}
