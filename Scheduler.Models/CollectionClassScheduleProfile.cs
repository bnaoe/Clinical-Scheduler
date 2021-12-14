using Scheduler.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Models
{
    public class CollectionClassScheduleProfile
    {
        public ApplicationUser providerUser { get; set; }
        public ProviderScheduleProfileVM providerScheduleProfileVM { get; set; } 
    }
}
