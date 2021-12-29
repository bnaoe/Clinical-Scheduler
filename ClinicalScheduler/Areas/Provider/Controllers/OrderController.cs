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
            var OrderTypeCSId = await _unitOfWork.CodeSet.GetFirstOrDefaultAsync(c => c.Name == SD.OrderType);
            var OrderTypeCVs = await _unitOfWork.CodeValue.GetAllAsync(c => c.CodeSetId == OrderTypeCSId.Id && c.IsDeleted == false);

            var AdminRouteId = await _unitOfWork.CodeSet.GetFirstOrDefaultAsync(c => c.Name == SD.AdminRoute);
            var AdminRouteCVs = await _unitOfWork.CodeValue.GetAllAsync(c => c.CodeSetId == AdminRouteId.Id && c.IsDeleted == false);

            var AdminFreqId = await _unitOfWork.CodeSet.GetFirstOrDefaultAsync(c => c.Name == SD.AdminFreq);
            var AdminFreqCVs = await _unitOfWork.CodeValue.GetAllAsync(c => c.CodeSetId == AdminFreqId.Id && c.IsDeleted == false);

            var AdminTimeCSId = await _unitOfWork.CodeSet.GetFirstOrDefaultAsync(c => c.Name == SD.AdminTime);
            var AdminTimeCVs = await _unitOfWork.CodeValue.GetAllAsync(c => c.CodeSetId == AdminTimeCSId.Id && c.IsDeleted == false);

            var OrderStatusCSId = await _unitOfWork.CodeSet.GetFirstOrDefaultAsync(c => c.Name == SD.OrdetStatus);
            var OrderStatusCVs = await _unitOfWork.CodeValue.GetAllAsync(c => c.CodeSetId == OrderTypeCSId.Id && c.IsDeleted == false);

            OrderVM orderVM = new()
            {
                Order = new()
                {
                    EncounterId = encntrId
                },
                OrderTypeList = OrderTypeCVs.Select(o => new SelectListItem
                {
                    Text = o.Name,
                    Value = o.Id.ToString()
                }),
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
                return View(orderVM);
            } else
            {
                //Update 
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
        public async Task<IActionResult> GetAllOrders(int encntrId)
        {
            IEnumerable<Order> orderList;

            orderList = await _unitOfWork.Order.GetAllAsync(o => o.EncounterId == encntrId, orderBy: o => o.OrderByDescending(x => x.OrderingDtTm)
            , includeProperties: "OrderingUser,OrderType,OrderStatus,OrderCatalog");

            var encounterOrderList = orderList.Select(async i => new
            {
                i.Id,
                i.OrderingDtTm,
                i.OrderCatalog.Name, 
                i.OrderingUser.LastName,
                i.OrderingUser.FirstName,
                i.OrderingUser.MiddleName,
                i.OrderStatus,
                i.IsActive
            });

            return Json(new { encounterOrderList });
        }

        //post
        [HttpPost]
        public async Task<IActionResult> IsActive(int? id)
        {

            var documentInDb = await _unitOfWork.Document.GetFirstOrDefaultAsync(d => d.Id == id);
            if (documentInDb == null)
            {
                return Json(new { Success = false, message = "Error while deleting" });
            }

            documentInDb.InError = true;
            _unitOfWork.Save();

            return Json(new { Success = true, message = "Delete successful" });

        }
        #endregion
    }
}
