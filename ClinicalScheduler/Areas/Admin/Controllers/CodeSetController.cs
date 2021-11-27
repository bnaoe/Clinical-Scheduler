using Microsoft.AspNetCore.Mvc;
using Scheduler.DataAccess;
using Scheduler.DataAccess.Repository.IRepository;
using Scheduler.Models;

namespace ClinicalScheduler.Controllers
{
    [Area("Admin")]
    public class CodeSetController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CodeSetController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<CodeSet> codeSetList = _unitOfWork.CodeSet.GetAll();
            return View(codeSetList);
        }

        //get
        public IActionResult Create()
        {
            
            return View();
        }

        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CodeSet codeSet)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.CodeSet.Add(codeSet);
                _unitOfWork.Save();
                TempData["Success"] = "Code Set created successfully";
                return RedirectToAction("Index");
            }
            return View(codeSet);
        }

        //get
        public IActionResult Edit(int? id)
        {
            if (id==null || id ==0) return NotFound();

            var codeSetInDb = _unitOfWork.CodeSet.GetFirstOrDefault(c => c.Id == id);

            if (codeSetInDb == null) return NotFound();

            return View(codeSetInDb);
        }

        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CodeSet codeSet)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.CodeSet.Update(codeSet);
                _unitOfWork.Save();
                TempData["Success"] = "Code Set edited successfully";
                return RedirectToAction("Index");
            }
            return View(codeSet);
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
