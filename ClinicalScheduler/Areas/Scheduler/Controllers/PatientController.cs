using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Scheduler.DataAccess;
using Scheduler.DataAccess.Repository.IRepository;
using Scheduler.Models;
using Scheduler.Models.ViewModels;
using Scheduler.Utility;

namespace ClinicalScheduler.Controllers
{
    [Area("Scheduler")]
    [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Scheduler)]
    public class PatientController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public PatientController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        //get
        public async Task<IActionResult> Upsert(int? id)
        {

            var GenderCSId = await _unitOfWork.CodeSet.GetFirstOrDefaultAsync(c => c.Name == SD.Gender);
            var GenderCVs = await _unitOfWork.CodeValue.GetAllAsync(c => c.CodeSetId == GenderCSId.Id && c.IsDeleted == false);

            var RaceCSId = await _unitOfWork.CodeSet.GetFirstOrDefaultAsync(c => c.Name == SD.Race);
            var RaceCVs = await _unitOfWork.CodeValue.GetAllAsync(c => c.CodeSetId == RaceCSId.Id && c.IsDeleted == false);

            var EthnicityCSId = await _unitOfWork.CodeSet.GetFirstOrDefaultAsync(c => c.Name == SD.Ethnicity);
            var EthnicityCVs = await _unitOfWork.CodeValue.GetAllAsync(c => c.CodeSetId == EthnicityCSId.Id && c.IsDeleted == false);

            PatientVM patientVM = new()
            {
                Patient = new(),
                GenderList = GenderCVs.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                RaceList = RaceCVs.Select(i => new SelectListItem
                {
                    Text= i.Name,
                    Value = i.Id.ToString(),
                }),
                EthnicityList = EthnicityCVs.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value= i.Id.ToString(),
                })
            };
            if (id==null || id ==0)
            {
                return View(patientVM);
            } else
            {
                //Update Patient
                patientVM.Patient = await _unitOfWork.Patient.GetFirstOrDefaultAsync(p => p.Id == id);
                return View(patientVM);
            }   
        }

        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(PatientVM obj)
        {
            if (ModelState.IsValid)
            {
                if (obj.Patient.Id==0) {
                    await _unitOfWork.Patient.AddAsync(obj.Patient);
                    TempData["Success"] = "Added successfully";
                } else
                {
                    _unitOfWork.Patient.Update(obj.Patient);
                    TempData["Success"] = "Updated successfully";
                }

                _unitOfWork.Save();
                return RedirectToAction("Upsert", new { id = obj.Patient.Id});
            }
            return View(obj);
        }

        #region API CALLS
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var patientList = await _unitOfWork.Patient.GetAllAsync();
            return Json(new { patientList });
        }

        //post
        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {

            var patientInDb = await _unitOfWork.Patient.GetFirstOrDefaultAsync(p => p.Id == id);
            if (patientInDb == null)
            {
                return Json(new { Success = false, message = "Error while deleting" });
            }

            patientInDb.IsDeleted = true;
            _unitOfWork.Save();

            return Json(new { Success = true, message = "Delete successful" });

        }
        #endregion
    }
}
