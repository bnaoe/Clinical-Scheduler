using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClinicalScheduler.Areas.Scheduler.Controllers
{
    public class AppointmentDashboardController : Controller
    {
        [Area("Scheduler")]
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
    }
}
