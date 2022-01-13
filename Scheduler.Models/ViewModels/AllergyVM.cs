using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Models.ViewModels
{
    public class AllergyVM
    {
        public Allergy Allergy { get; set; }

        [ValidateNever]
        public EncounterVM EncounterVM { get; set; }

    }
}
