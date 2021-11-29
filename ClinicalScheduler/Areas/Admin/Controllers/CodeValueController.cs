using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Scheduler.DataAccess;
using Scheduler.DataAccess.Repository.IRepository;
using Scheduler.Models;
using Scheduler.Models.ViewModels;

namespace ClinicalScheduler.Controllers
{
    [Area("Admin")]
    public class CodeValueController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CodeValueController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            //IEnumerable<CodeSet> codeSetList = _unitOfWork.CodeSet.GetAll();
            //return View(codeSetList);
            return View();
        }

        //get
        public IActionResult Upsert(int? id)
        {
            CodeValueVM codeValueVM = new()
            {
                CodeValue = new(),
                CodeSetList = _unitOfWork.CodeSet.GetAll().Where(c=>c.IsDeleted==false).Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                })
            };
            if (id==null || id ==0)
            {
                //Create Code Value
                //ViewBag.CodeSetList = CodeSetList; //This is used to pass temp data if data is not available form the model 
                return View(codeValueVM);
            } else
            {
                //Update Code Value
                codeValueVM.CodeValue = _unitOfWork.CodeValue.GetFirstOrDefault(c => c.Id == id);
                return View(codeValueVM);
            }

            
        }

        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(CodeValueVM obj)
        {
            if (ModelState.IsValid)
            {
                if (obj.CodeValue.Id==0) {
                    _unitOfWork.CodeValue.Add(obj.CodeValue);
                    TempData["Success"] = "Added successfully";
                } else
                {
                    _unitOfWork.CodeValue.Update(obj.CodeValue);
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
            var codeValueList = _unitOfWork.CodeValue.GetAll(includeProperties:"CodeSet");
            return Json(new { codeValueList });
        }

        //post
        [HttpDelete]
        public IActionResult Delete(int? id)
        {

            var codeValueInDb = _unitOfWork.CodeValue.GetFirstOrDefault(c => c.Id == id);
            if (codeValueInDb == null)
            {
                return Json(new { Success = false, message = "Error while deleting" });
            }

            //_unitOfWork.CodeValue.Remove(codeValueInDb);
            codeValueInDb.IsDeleted = true;
            _unitOfWork.Save();

            return Json(new { Success = true, message = "Delete successful" });

        }
        #endregion
    }
}
