using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Models.ViewModels
{
    public class SchApptVM
    {
        public SchAppt SchAppt { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> ApptTypeList { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> ApptStatusList { get; set; }

        [ValidateNever]
        public Patient Patient { get; set; }

        [ValidateNever]
        public ProviderScheduleProfile ProviderScheduleProfile { get; set; }
    }
}
