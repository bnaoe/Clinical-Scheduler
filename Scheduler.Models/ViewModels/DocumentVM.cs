using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Models.ViewModels
{
    public class DocumentVM
    {
        public Document Document { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> DocTypeList { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> DocStatusList { get; set; }

        [ValidateNever]
        public EncounterVM EncounterVM { get; set; }

    }
}
