using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Scheduler.DataAccess;
using Scheduler.DataAccess.Repository.IRepository;
using Scheduler.Models;
using Scheduler.Models.ViewModels;
using Scheduler.Utility;

namespace ClinicalScheduler.Controllers
{
    [Area("Provider")]
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;

        public OrderController(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        //get
        public async Task<IActionResult> Upsert(int? orderId, int encntrId)
        {
            var AdminRouteId = await _unitOfWork.CodeSet.GetFirstOrDefaultAsync(c => c.Name == SD.AdminRoute);
            var AdminRouteCVs = await _unitOfWork.CodeValue.GetAllAsync(c => c.CodeSetId == AdminRouteId.Id && c.IsDeleted == false);

            var AdminFreqId = await _unitOfWork.CodeSet.GetFirstOrDefaultAsync(c => c.Name == SD.AdminFreq);
            var AdminFreqCVs = await _unitOfWork.CodeValue.GetAllAsync(c => c.CodeSetId == AdminFreqId.Id && c.IsDeleted == false);

            var AdminTimeCSId = await _unitOfWork.CodeSet.GetFirstOrDefaultAsync(c => c.Name == SD.AdminTime);
            var AdminTimeCVs = await _unitOfWork.CodeValue.GetAllAsync(c => c.CodeSetId == AdminTimeCSId.Id && c.IsDeleted == false);

            var OrderStatusCSId = await _unitOfWork.CodeSet.GetFirstOrDefaultAsync(c => c.Name == SD.OrdetStatus);
            var OrderStatusCVs = await _unitOfWork.CodeValue.GetAllAsync(c => c.CodeSetId == OrderStatusCSId.Id && c.IsDeleted == false);

            OrderVM orderVM = new()
            {
                Order = new()
                {
                    EncounterId = encntrId
                },
                AdminRouteList = AdminRouteCVs.Select(o => new SelectListItem
                {
                    Text = o.Name,
                    Value = o.Id.ToString()
                }),
                AdminFreqList = AdminFreqCVs.Select(o => new SelectListItem
                {
                    Text = o.Name,
                    Value = o.Id.ToString()
                }),
                AdminTimeList = AdminTimeCVs.Select(o => new SelectListItem
                {
                    Text = o.Name,
                    Value = o.Id.ToString()
                }),
                OrderStatusList = OrderStatusCVs.Select(o => new SelectListItem
                {
                    Text = o.Name,
                    Value = o.Id.ToString()
                }),
                EncounterVM = new()
                {
                    Encounter = await _unitOfWork.Encounter.GetFirstOrDefaultAsync(e=>e.Id == encntrId, includeProperties: "Patient,SchAppt.ApptType,SchAppt.ApptStatus,FinancialNumAlias,Insurance,ProviderUser,Location")
                }
            };


            if (orderId == null || orderId == 0)
            {
                //Create
                orderVM.Order.OrderingUserId = _userManager.GetUserId(User);
                orderVM.Order.PatientId = orderVM.EncounterVM.Encounter.PatientId;
                return View(orderVM);
            } else
            {
                //Update 
                //orderVM.Order.PatientId = orderVM.EncounterVM.Encounter.PatientId;
                orderVM.Order = await _unitOfWork.Order.GetFirstOrDefaultAsync(o => o.Id == orderId, includeProperties: "Encounter,Patient,OrderCatalog");
                return View(orderVM);
            }
        }

        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(OrderVM obj)
        {
            if (ModelState.IsValid)
            {
                obj.Order.OrderCatalog=null;
                obj.Order.OrderingUserId = "83af9bcc-4112-49fb-9bda-3e9d4b715e9e";
                if (obj.Order.Id==0) {
                    await _unitOfWork.Order.AddAsync(obj.Order);
                    TempData["Success"] = "Added successfully";
                } else
                {
                    _unitOfWork.Order.Update(obj.Order);
                    TempData["Success"] = "Updated successfully";
                }

                _unitOfWork.Save();
                return RedirectToAction("EncounterSchAppt", "Chart", new { enctrId = obj.Order.EncounterId, Area = "Provider" });

            }
            return View(obj);
        }

        #region API CALLS        
        [HttpGet]
        public async Task<JsonResult> GetList(string name)
        {
            var orderList = await _unitOfWork.OrderCatalog.GetAllAsync(x => x.Name.StartsWith(name) && x.IsDeleted == false);
            return Json(new { orderList });

        }
        #endregion
    }
}
