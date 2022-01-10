using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Models.ViewModels
{
    public class PatientVM
    {
        public Patient Patient { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> GenderList { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> RaceList { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> EthnicityList { get; set; }
    }
}
