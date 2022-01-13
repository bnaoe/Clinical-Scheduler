using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Scheduler.DataAccess;
using Scheduler.DataAccess.Repository.IRepository;
using Scheduler.Models;
using Scheduler.Models.ViewModels;
using Scheduler.Utility;

namespace ClinicalScheduler.Controllers
{
    [Area("Provider")]
    [Authorize(Roles = SD.Role_Admin + "," + "," + SD.Role_NP + "," + SD.Role_PA + "," + SD.Role_Physician)]
    public class AllergyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;

        public AllergyController(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }


        //get
        public async Task<IActionResult> Upsert(int? allergyId, int encntrId)
        {
            AllergyVM allergyVM = new()
            {
                EncounterVM = new()
                {
                    Encounter = await _unitOfWork.Encounter.GetFirstOrDefaultAsync(e => e.Id == encntrId, includeProperties: "Patient,SchAppt.ApptType,SchAppt.ApptStatus,FinancialNumAlias,Insurance,ProviderUser,Location")
                },
                Allergy = new()

            };

            if (allergyId == null || allergyId == 0)
            {
                allergyVM.Allergy.PatientId = allergyVM.EncounterVM.Encounter.PatientId;
                allergyVM.Allergy.ProviderUserId = _userManager.GetUserId(User);
                return View(allergyVM);
            } else
            {
                allergyVM.Allergy = await _unitOfWork.Allergy.GetFirstOrDefaultAsync(a => a.Id == allergyId, includeProperties: "Patient");
                return View(allergyVM);
            }

        }

        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(AllergyVM obj)
        {

            if (obj.Allergy.IsActive == false)
            {
                obj.Allergy.EndDtTm = DateTime.Now;
            }

            if (ModelState.IsValid)
            {
                obj.Allergy.ProviderUserId = _userManager.GetUserId(User);

                if (obj.Allergy.Id==0) {
                    await _unitOfWork.Allergy.AddAsync(obj.Allergy);
                    TempData["Success"] = "Added successfully";
                } else
                {
                    _unitOfWork.Allergy.Update(obj.Allergy);
                    TempData["Success"] = "Updated successfully";
                }

                _unitOfWork.Save();
                return RedirectToAction("EncounterSchAppt", "Chart", new { enctrId = obj.EncounterVM.Encounter.Id, Area = "Provider" });

            }

            AllergyVM allergyVM = new()
            {
                Allergy = obj.Allergy,
                EncounterVM = new()
                {
                    Encounter = await _unitOfWork.Encounter.GetFirstOrDefaultAsync(e => e.Id == obj.EncounterVM.Encounter.Id, includeProperties: "Patient,SchAppt.ApptType,SchAppt.ApptStatus,FinancialNumAlias,Insurance,ProviderUser,Location")
                }
            };

            return View(allergyVM);
        }

        #region API CALLS        
        [HttpPost]
        public async Task<JsonResult> IsActive(int allergyId)
        {
            var allergyInDb = await _unitOfWork.Allergy.GetFirstOrDefaultAsync(a => a.Id == allergyId);
            if (allergyInDb == null)
            {
                return Json(new { Success = false, message = "Error while inactivating" });
            }

            allergyInDb.IsActive = false;
            _unitOfWork.Save();

            return Json(new { Success = true, message = "InActivate successful" });

        }
        #endregion
    }
}
