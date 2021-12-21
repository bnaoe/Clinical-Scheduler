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

        public async Task<IActionResult> GetAllEncounters(int id)
        {
            IEnumerable<Encounter> encounterSchApptList;
            var ApptTypeCSId = await _unitOfWork.CodeSet.GetFirstOrDefaultAsync(c => c.Name == SD.ApptType);
            var ApptTypeCVs = await _unitOfWork.CodeValue.GetAllAsync(c => c.CodeSetId == ApptTypeCSId.Id && c.IsDeleted == false);

            var ApptStatusCSId = await _unitOfWork.CodeSet.GetFirstOrDefaultAsync(c => c.Name == SD.ApptStatus);
            var ApptStatusCVs = await _unitOfWork.CodeValue.GetAllAsync(c => c.CodeSetId == ApptStatusCSId.Id && c.IsDeleted == false);

            encounterSchApptList = await _unitOfWork.Encounter.GetAllAsync(e=>e.PatientId== id,includeProperties: "ProviderUser,SchAppt,Location,FinancialNumAlias");

            // encounterSchApptList.Select(p => { p.SchAppt.ApptType.Name = "TEST" ; return p; });
            var patientEncounterSchApptList = encounterSchApptList.Select(async i => new
            {
                i.SchApptId,
                i.Id,
                i.PatientId,
                i.SchAppt.ProviderScheduleProfileId,
                i.FinancialNumAlias.Fin,
                i.ProviderUser.FirstName,
                i.ProviderUser.LastName,
                i.ProviderUser.MiddleName,
                i.SchAppt.start_date,
                i.SchAppt.end_date,
                i.Location.Name,
                i.SchAppt.ApptType,
                i.SchAppt.ApptStatus
            });

            return Json(new { patientEncounterSchApptList });

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
