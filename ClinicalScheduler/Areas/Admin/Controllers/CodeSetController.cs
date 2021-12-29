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
            //IEnumerable<CodeSet> codeSetList = _unitOfWork.CodeSet.GetAll();
            //return View(codeSetList);
            return View();
        }

        //get
        //public IActionResult Create()
        //{

        //    return View();
        //}

        //post
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Create(CodeSet codeSet)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _unitOfWork.CodeSet.Add(codeSet);
        //        _unitOfWork.Save();
        //        TempData["Success"] = "Code Set created successfully";
        //        return RedirectToAction("Index");
        //    }
        //    return View(codeSet);
        //}

        //get
        //public IActionResult Edit(int? id)
        //{
        //    if (id==null || id ==0) return NotFound();

        //    var codeSetInDb = _unitOfWork.CodeSet.GetFirstOrDefault(c => c.Id == id);

        //    if (codeSetInDb == null) return NotFound();

        //    return View(codeSetInDb);
        //}

        //get
        public async Task<IActionResult> Upsert(int? id)
        {
            CodeSet codeSet = new();

            if (id == null || id == 0)
            {
                return View(codeSet);
            }
            else
            {
                //Update Code Value
                codeSet = await _unitOfWork.CodeSet.GetFirstOrDefaultAsync(l => l.Id == id);
                return View(codeSet);
            }
        }

        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(CodeSet obj)
        {
            if (ModelState.IsValid)
            {
                if (obj.Id == 0)
                {
                    await _unitOfWork.CodeSet.AddAsync(obj);
                    TempData["Success"] = "Added successfully";
                }
                else
                {
                    _unitOfWork.CodeSet.Update(obj);
                    TempData["Success"] = "Updated successfully";
                }

                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //post
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Edit(CodeSet codeSet)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _unitOfWork.CodeSet.Update(codeSet);
        //        _unitOfWork.Save();
        //        TempData["Success"] = "Code Set edited successfully";
        //        return RedirectToAction("Index");
        //    }
        //    return View(codeSet);
        //}

        //get
        //public IActionResult Delete(int? id)
        //{
        //    if (id == null || id == 0) return NotFound();

        //    var codeSetInDb = _unitOfWork.CodeSet.GetFirstOrDefault(c => c.Id == id);

        //    if (codeSetInDb == null) return NotFound();

        //    return View(codeSetInDb);
        //}

        ////post
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult DeletePOST(int? id)
        //{

        //    var codeSetInDb = _unitOfWork.CodeSet.GetFirstOrDefault(c => c.Id == id);

        //    if (codeSetInDb == null) return NotFound();

        //    _unitOfWork.CodeSet.Remove(codeSetInDb);
        //    _unitOfWork.Save();
        //    TempData["Success"] = "Code Set deleted successfully";

        //    return RedirectToAction("Index");
        //}

        #region API CALLS
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var codeSetList = await _unitOfWork.CodeSet.GetAllAsync();
            return Json(new { codeSetList });
        }

        //post
        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {

            var codeSetInDb = await _unitOfWork.CodeSet.GetFirstOrDefaultAsync(c => c.Id == id);
            if (codeSetInDb == null)
            {
                return Json(new { Success = false, message = "Error while deleting" });
            }

            //_unitOfWork.CodeSet.Remove(codeSetInDb);

            codeSetInDb.IsDeleted = true;
            _unitOfWork.Save();

            return Json(new { Success = true, message = "Delete successful" });
        }
        #endregion

    }
}
