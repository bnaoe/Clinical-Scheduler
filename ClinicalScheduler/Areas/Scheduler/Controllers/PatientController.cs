﻿using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> Upsert(int? id)
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
