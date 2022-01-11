using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scheduler.DataAccess;
using Scheduler.DataAccess.Repository.IRepository;
using Scheduler.Models;
using Scheduler.Utility;

namespace ClinicalScheduler.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CodeSetController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CodeSetController(IUnitOfWork unitOfWork)
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
            CodeSet codeSet = new();

            if (id == null || id == 0)
            {
                return View(codeSet);
            }
            else
            {
                //Update Code Set
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

        #region API CALLS
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var codeSetList = await _unitOfWork.CodeSet.GetAllAsync(orderBy: c=>c.OrderBy(x=>x.Name));
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
