using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Scheduler.DataAccess;
using Scheduler.DataAccess.Repository.IRepository;
using Scheduler.Models;
using Scheduler.Models.ViewModels;

namespace ClinicalScheduler.Controllers
{
    [Area("Scheduler")]
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
        public IActionResult Upsert(int? id)
        {
            PatientVM patientVM = new()
            {
                Patient = new(),
            };
            if (id==null || id ==0)
            {
                return View(patientVM);
            } else
            {
                //Update Patient
                patientVM.Patient = _unitOfWork.Patient.GetFirstOrDefault(p => p.Id == id);
                return View(patientVM);
            }   
        }

        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(PatientVM obj)
        {
            if (ModelState.IsValid)
            {
                if (obj.Patient.Id==0) {
                    _unitOfWork.Patient.Add(obj.Patient);
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
        public IActionResult GetAll()
        {
            var patientList = _unitOfWork.Patient.GetAll();
            return Json(new { patientList });
        }

        //post
        [HttpDelete]
        public IActionResult Delete(int? id)
        {

            var patientInDb = _unitOfWork.Patient.GetFirstOrDefault(p => p.Id == id);
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
