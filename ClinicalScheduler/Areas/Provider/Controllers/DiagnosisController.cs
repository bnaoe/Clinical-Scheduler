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
    public class DiagnosisController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;

        public DiagnosisController(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }


        //get
        public async Task<IActionResult> Upsert(int? diagnosisId, int encntrId)
        {

            DiagnosisVM diagnosisVM = new()
            {
                EncounterVM = new()
                {
                    Encounter = await _unitOfWork.Encounter.GetFirstOrDefaultAsync(e => e.Id == encntrId, includeProperties: "Patient,SchAppt.ApptType,SchAppt.ApptStatus,FinancialNumAlias,Insurance,ProviderUser,Location")
                },
                Diagnosis = new()
                {
                    EncounterId = encntrId

                }
            };


            if (diagnosisId == null || diagnosisId == 0)
            {
                //Create
                diagnosisVM.Diagnosis.ProviderUserId = _userManager.GetUserId(User);
                diagnosisVM.Diagnosis.PatientId = diagnosisVM.EncounterVM.Encounter.PatientId;
                return View(diagnosisVM);
            } else
            {
                //Update 
                //orderVM.Order.PatientId = orderVM.EncounterVM.Encounter.PatientId;
                diagnosisVM.Diagnosis = await _unitOfWork.Diagnosis.GetFirstOrDefaultAsync(d => d.Id == diagnosisId, includeProperties: "Encounter,Patient,DxCode");
                return View(diagnosisVM);
            }
        }

        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(DiagnosisVM obj)
        {
            if (obj.Diagnosis.DxCodeId == 0)
            {
                ModelState["Diagnosis.DxCode.Description"].ValidationState = ModelValidationState.Invalid;
                ModelState.AddModelError("Diagnosis.DxCode.Description", "Diagnosis Not Found");
            }
            if (obj.Diagnosis.IsActive == false)
            {
                obj.Diagnosis.EndDtTm = DateTime.Now;
            }

            obj.Diagnosis.DxCode = null;

            if (ModelState.IsValid)
            {
                obj.Diagnosis.ProviderUserId = _userManager.GetUserId(User);

                if (obj.Diagnosis.Id==0) {
                    await _unitOfWork.Diagnosis.AddAsync(obj.Diagnosis);
                    TempData["Success"] = "Added successfully";
                } else
                {
                    _unitOfWork.Diagnosis.Update(obj.Diagnosis);
                    TempData["Success"] = "Updated successfully";
                }

                _unitOfWork.Save();
                return RedirectToAction("EncounterSchAppt", "Chart", new { enctrId = obj.Diagnosis.EncounterId, Area = "Provider" });

            }

            DiagnosisVM diagnosisVM = new()
            {
                Diagnosis = obj.Diagnosis,
                EncounterVM = new()
                {
                    Encounter = await _unitOfWork.Encounter.GetFirstOrDefaultAsync(e => e.Id == obj.Diagnosis.EncounterId, includeProperties: "Patient,SchAppt.ApptType,SchAppt.ApptStatus,FinancialNumAlias,Insurance,ProviderUser,Location")
                }
            };

            return View(diagnosisVM);
        }

        #region API CALLS        
        [HttpPost]
        public async Task<JsonResult> IsActive(int diagnosisId)
        {
            var diagnisInDb = await _unitOfWork.Diagnosis.GetFirstOrDefaultAsync(d => d.Id == diagnosisId);
            if (diagnisInDb == null)
            {
                return Json(new { Success = false, message = "Error while inactivating" });
            }

            diagnisInDb.IsActive = false;
            _unitOfWork.Save();

            return Json(new { Success = true, message = "InActivate successful" });

        }
        #endregion
    }
}
