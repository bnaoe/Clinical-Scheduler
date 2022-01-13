using Microsoft.AspNetCore.Mvc;

namespace ClinicalScheduler.Areas.Shared.Controllers
{
    [Area("Shared")]
    public class HomePageController : Controller
    {
        public ActionResult Go()
        {
            if (User.IsInRole("Resgistration"))
            {
                return RedirectToAction("ApptDashboard", "Search", new { area= "Shared" });
            }
            else
            {
                return RedirectToAction("PatientSearch", "Search", new { area = "Shared" });
            }
        }
    }
}
