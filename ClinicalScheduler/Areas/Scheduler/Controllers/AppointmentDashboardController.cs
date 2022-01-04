using Microsoft.AspNetCore.Mvc;

namespace ClinicalScheduler.Areas.Scheduler.Controllers
{
    public class AppointmentDashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
