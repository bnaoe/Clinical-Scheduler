using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Models.ViewModels
{
    public class EncounterVM
    {
        public Encounter Encounter { get; set; }

        [ValidateNever]
        public Patient Patient { get; set; }
        
        [ValidateNever]
        public ApplicationUser ProviderUser { get; set; }
        
        [ValidateNever]
        public SchAppt SchAppt { get; set; }
        
        [ValidateNever]
        public Insurance Insurance { get; set; }    

        [ValidateNever]
        public Location Location { get; set; }

        [ValidateNever]
        public Document LastDocument { get; set; }
    }
}
