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
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]

    public class InsuranceController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public InsuranceController(IUnitOfWork unitOfWork)
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
            InsuranceVM insuranceVM = new()
            {
                Insurance = new(),
            };
            if (id==null || id ==0)
            {
                return View(insuranceVM);
            } else
            {
                //Update Insurance
                insuranceVM.Insurance = await _unitOfWork.Insurance.GetFirstOrDefaultAsync(i => i.Id == id);
                return View(insuranceVM);
            }   
        }

        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(InsuranceVM obj)
        {
            if (ModelState.IsValid)
            {
                if (obj.Insurance.Id==0) {
                    await _unitOfWork.Insurance.AddAsync(obj.Insurance);
                    TempData["Success"] = "Added successfully";
                } else
                {
                    _unitOfWork.Insurance.Update(obj.Insurance);
                    TempData["Success"] = "Updated successfully";
                }

                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        #region API CALLS
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var insuranceList = await _unitOfWork.Insurance.GetAllAsync();
            return Json(new { insuranceList });
        }

        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {

            var insuranceInDb = await _unitOfWork.Insurance.GetFirstOrDefaultAsync(i => i.Id == id);
            if (insuranceInDb == null)
            {
                return Json(new { Success = false, message = "Error while inactivating" });
            }

            insuranceInDb.IsDeleted = true;
            _unitOfWork.Save();

            return Json(new { Success = true, message = "Successfully inactivated" });

        }
        #endregion
    }
}
