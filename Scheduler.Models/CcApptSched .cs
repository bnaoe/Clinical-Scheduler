using Scheduler.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Models
{
    public class CcApptSched
    {
        public PatientVM patientVM { get; set; }
        public SchApptVM schApptVM { get; set; }
        public EncounterVM encounterVM { get; set; }
        public ProviderScheduleProfileVM providerScheduleProfileVM { get; set; }


    }
}
