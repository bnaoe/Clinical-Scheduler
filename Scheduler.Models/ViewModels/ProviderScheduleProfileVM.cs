using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Models.ViewModels
{
    public class ProviderScheduleProfileVM
    {
        public ProviderScheduleProfile ProviderScheduleProfile { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> LocationList { get; set; }
        
    }
}
