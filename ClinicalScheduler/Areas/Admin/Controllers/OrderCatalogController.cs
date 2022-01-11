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
    public class OrderCatalogController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderCatalogController(IUnitOfWork unitOfWork)
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
            var OrderTypeCodeSetId = await _unitOfWork.CodeSet.GetFirstOrDefaultAsync(c => c.Name == SD.OrderType);
            var CodeValues = await _unitOfWork.CodeValue.GetAllAsync(c => c.CodeSetId == OrderTypeCodeSetId.Id && c.IsDeleted==false, orderBy: c => c.OrderBy(x => x.Name));

            OrderCatalogVM orderCatalogVM = new()
            {
                OrderCatalog = new(),
                CodeValueList = CodeValues.Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                })
            };
            if (id==null || id ==0)
            {
                //Create OrderCatalog
                return View(orderCatalogVM);
            } else
            {
                //Update OrderCatalog
                orderCatalogVM.OrderCatalog = await _unitOfWork.OrderCatalog.GetFirstOrDefaultAsync(c => c.Id == id);
                return View(orderCatalogVM);
            }

            
        }

        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(OrderCatalogVM obj)
        {
            if (ModelState.IsValid)
            {
                if (obj.OrderCatalog.Id==0) {
                    await _unitOfWork.OrderCatalog.AddAsync(obj.OrderCatalog);
                    TempData["Success"] = "Added successfully";
                } else
                {
                    _unitOfWork.OrderCatalog.Update(obj.OrderCatalog);
                    TempData["Success"] = "Updated successfully";
                }

                _unitOfWork.Save();
                return RedirectToAction("Index");
            }

            var OrderTypeCodeSetId = await _unitOfWork.CodeSet.GetFirstOrDefaultAsync(c => c.Name == SD.OrderType);
            var CodeValues = await _unitOfWork.CodeValue.GetAllAsync(c => c.CodeSetId == OrderTypeCodeSetId.Id && c.IsDeleted==false, orderBy: c => c.OrderBy(x => x.Name));

            obj.CodeValueList = CodeValues.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            });

            return View(obj);
        }

        #region API CALLS
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var orderCatalogList = await _unitOfWork.OrderCatalog.GetAllAsync(includeProperties:"CodeValue");
            return Json(new { orderCatalogList });
        }

        //post
        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {

            var orderCatalogInDb = await _unitOfWork.OrderCatalog.GetFirstOrDefaultAsync(c => c.Id == id);
            if (orderCatalogInDb == null)
            {
                return Json(new { Success = false, message = "Error while deleting" });
            }

            orderCatalogInDb.IsDeleted = true;
            _unitOfWork.Save();

            return Json(new { Success = true, message = "Delete successful" });

        }
        #endregion
    }
}
