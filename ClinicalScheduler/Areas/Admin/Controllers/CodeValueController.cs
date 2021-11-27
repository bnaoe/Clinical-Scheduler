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
            IEnumerable<CodeSet> codeSetList = _unitOfWork.CodeSet.GetAll();
            return View(codeSetList);
        }

       
        //get
        public IActionResult Upsert(int? id)
        {
            CodeValueVM codeValueVM = new()
            {
                CodeValue = new(),
                CodeSetList = _unitOfWork.CodeSet.GetAll().Select(c => new SelectListItem
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

            }

            return View(codeValueVM);
        }

        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(CodeValueVM obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.CodeValue.Add(obj.CodeValue);
                _unitOfWork.Save();
                TempData["Success"] = "Code Set edited successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //get
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0) return NotFound();

            var codeSetInDb = _unitOfWork.CodeSet.GetFirstOrDefault(c => c.Id == id);

            if (codeSetInDb == null) return NotFound();

            return View(codeSetInDb);
        }

        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {

            var codeSetInDb = _unitOfWork.CodeSet.GetFirstOrDefault(c => c.Id == id);

            if (codeSetInDb == null) return NotFound();

            _unitOfWork.CodeSet.Remove(codeSetInDb);
            _unitOfWork.Save();
            TempData["Success"] = "Code Set deleted successfully";

            return RedirectToAction("Index");
        }

    }
}
