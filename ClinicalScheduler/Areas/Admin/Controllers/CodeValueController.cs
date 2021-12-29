﻿using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> Upsert(int? id)
        {
            IEnumerable<CodeSet> CodeSetList = await _unitOfWork.CodeSet.GetAllAsync();

            CodeValueVM codeValueVM = new()
            {
                CodeValue = new(),
                CodeSetList = CodeSetList.Where(c => c.IsDeleted == false).OrderBy(c => c.Name).Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                })
            };
            if (id==null || id ==0)
            {
                //Create Code Value
                return View(codeValueVM);
            } else
            {
                //Update Code Value
                codeValueVM.CodeValue = await _unitOfWork.CodeValue.GetFirstOrDefaultAsync(c => c.Id == id);
                return View(codeValueVM);
            }

            
        }

        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(CodeValueVM obj)
        {
            if (ModelState.IsValid)
            {
                if (obj.CodeValue.Id==0) {
                    await _unitOfWork.CodeValue.AddAsync(obj.CodeValue);
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
        public async Task<IActionResult> GetAll()
        {
            var codeValueList = await _unitOfWork.CodeValue.GetAllAsync(includeProperties:"CodeSet");
            return Json(new { codeValueList });
        }

        //post
        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {

            var codeValueInDb = await _unitOfWork.CodeValue.GetFirstOrDefaultAsync(c => c.Id == id);
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
