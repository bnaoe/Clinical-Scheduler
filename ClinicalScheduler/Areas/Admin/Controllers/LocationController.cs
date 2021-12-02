using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Scheduler.DataAccess;
using Scheduler.DataAccess.Repository.IRepository;
using Scheduler.Models;
using Scheduler.Models.ViewModels;

namespace ClinicalScheduler.Controllers
{
    [Area("Admin")]
    public class LocationController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public LocationController(IUnitOfWork unitOfWork)
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
            LocationVM locationVM = new()
            {
                Location = new(),
            };
            if (id==null || id ==0)
            {
                return View(locationVM);
            } else
            {
                //Update Location
                locationVM.Location = _unitOfWork.Location.GetFirstOrDefault(l => l.Id == id);
                return View(locationVM);
            }   
        }

        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(LocationVM obj)
        {
            if (ModelState.IsValid)
            {
                if (obj.Location.Id==0) {
                    _unitOfWork.Location.Add(obj.Location);
                    TempData["Success"] = "Added successfully";
                } else
                {
                    _unitOfWork.Location.Update(obj.Location);
                    TempData["Success"] = "Updated successfully";
                }

                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var locationList = _unitOfWork.Location.GetAll();
            return Json(new { locationList });
        }

        //post
        [HttpDelete]
        public IActionResult Delete(int? id)
        {

            var locationInDb = _unitOfWork.Location.GetFirstOrDefault(l => l.Id == id);
            if (locationInDb == null)
            {
                return Json(new { Success = false, message = "Error while deleting" });
            }

            //_unitOfWork.Location.Remove(locationInDb);
            locationInDb.IsDeleted = true;
            _unitOfWork.Save();

            return Json(new { Success = true, message = "Delete successful" });

        }
        #endregion
    }
}
