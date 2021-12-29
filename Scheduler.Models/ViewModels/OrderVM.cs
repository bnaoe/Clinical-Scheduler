using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Models.ViewModels
{
    public class OrderVM
    {
        public Order Order { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> OrderTypeList { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> AdminRouteList { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> AdminFreqList { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> AdminTimeList { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> OrderStatusList { get; set; }

        [ValidateNever]
        public EncounterVM EncounterVM { get; set; }

    }
}
