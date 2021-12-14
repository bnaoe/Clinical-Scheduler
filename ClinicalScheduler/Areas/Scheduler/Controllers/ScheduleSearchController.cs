using Microsoft.AspNetCore.Mvc;
using Scheduler.DataAccess.Repository.IRepository;
using Scheduler.Models;
using Scheduler.Models.ViewModels;

namespace ClinicalScheduler.Areas.Scheduler.Controllers
{
    [Area("Scheduler")]
    public class ScheduleSearch : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ScheduleSearch(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> GetPatientDetails(int id)
        {
            PatientVM patientVM = new();
            patientVM.Patient = await _unitOfWork.Patient.GetFirstOrDefaultAsync(p => p.Id == id);

            if (patientVM == null) {
                TempData["Error"] = "Not Found";
                return RedirectToAction("PatientSearch", "Search", new { area = "Shared" });
            }

            return View("ScheduleProfileSearch", patientVM);
        }

        #region API CALLS
        [HttpGet]
        public async Task<IActionResult> GetAllScheduleProfiles(string location, string firstName, string lastName)
        {
            IEnumerable<ProviderScheduleProfile> providerScheduleProfileList;

            if (location != null && firstName != null && lastName != null)
            {
                providerScheduleProfileList = await _unitOfWork.ProviderScheduleProfile.GetAllAsync(p => p.Location.Name.Contains(location)
                && p.ProviderUser.FirstName.Contains(firstName) && p.ProviderUser.LastName.Contains(lastName), includeProperties: "Location,ProviderUser");
            }
            else
            {
                providerScheduleProfileList = await _unitOfWork.ProviderScheduleProfile.GetAllAsync(p => p.Location.Name.Contains(location)
                || p.ProviderUser.FirstName.Contains(firstName) || p.ProviderUser.LastName.Contains(lastName), includeProperties: "Location,ProviderUser");
            }

            providerScheduleProfileList = providerScheduleProfileList.OrderBy(p => p.Location.Name);

            return Json(new { providerScheduleProfileList });

        }
        #endregion


    }
}
