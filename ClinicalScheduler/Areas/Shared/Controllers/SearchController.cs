using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Scheduler.DataAccess;
using Scheduler.DataAccess.Repository.IRepository;
using Scheduler.Models;
using Scheduler.Models.ViewModels;
using Scheduler.Utility;

namespace ClinicalScheduler.Controllers
{
    [Area("Shared")]
    public class SearchController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;

        public SearchController(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public IActionResult PatientSearch()
        {
            return View();
        }

        public IActionResult ProviderSearch()
        {
            return View();
        }

       #region API CALLS
        [HttpGet]
        public async Task<IActionResult> GetAllPatients(String firstName, String lastName, DateTime birthDate)
        {

            IEnumerable<Patient> patientList;

            if (firstName != null && lastName != null && birthDate!= DateTime.MinValue)
            {
                patientList = await _unitOfWork.Patient.GetAllAsync(p => p.LastName.Contains(lastName)
               && p.FirstName.Contains(firstName) && p.BirthDate == birthDate);
            }
            else if (firstName != null && lastName != null)
            {
                patientList = await _unitOfWork.Patient.GetAllAsync(p => p.LastName.Contains(lastName)
                && p.FirstName.Contains(firstName));
            }
            else if (lastName != null && birthDate!= DateTime.MinValue) {
                patientList = await _unitOfWork.Patient.GetAllAsync(p => p.LastName.Contains(lastName)
               && p.BirthDate == birthDate);
            }
            else
            {
                patientList = await _unitOfWork.Patient.GetAllAsync(p => p.LastName.Contains(lastName)
                || p.FirstName.Contains(firstName) || p.BirthDate == birthDate);

            }
            patientList = patientList.Where(p => p.IsDeleted == false);

            return Json(new { patientList });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProviders(string firstName, string lastName)
        {
            IEnumerable<ApplicationUser> users;

            if (firstName != null && lastName != null)
            {
                users = await _unitOfWork.ApplicationUser.GetAllAsync(a => a.LastName.Contains(lastName) 
                && a.FirstName.Contains(firstName), includeProperties: "Location");
            }
            else 
            { 
                users = await _unitOfWork.ApplicationUser.GetAllAsync(a => a.LastName.Contains(lastName) 
                || a.FirstName.Contains(firstName), includeProperties: "Location");
            }

            foreach (var user in users)
            {
                user.Role = _userManager.GetRolesAsync(user).Result.FirstOrDefault();
            }

            var providerList = users.Where(u => u.Role == SD.Role_Physician||u.Role== SD.Role_NP);
            return Json(new { providerList });
        }



        #endregion
    }
}
