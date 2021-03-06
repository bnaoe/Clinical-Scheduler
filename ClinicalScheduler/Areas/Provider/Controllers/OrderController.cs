using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Scheduler.DataAccess;
using Scheduler.DataAccess.Repository.IRepository;
using Scheduler.Models;
using Scheduler.Models.ViewModels;
using Scheduler.Utility;

namespace ClinicalScheduler.Controllers
{
    [Area("Provider")]
    [Authorize(Roles = SD.Role_Admin + "," + "," + SD.Role_NP + "," + SD.Role_PA + "," + SD.Role_Physician)]
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;


        public OrderController(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }
        

        public async Task<IActionResult> Preview(int orderId, int encntrId)
        {

            var AdminFreqId = await _unitOfWork.CodeSet.GetFirstOrDefaultAsync(c => c.Name == SD.AdminFreq);
            var AdminFreqCVs = await _unitOfWork.CodeValue.GetAllAsync(c => c.CodeSetId == AdminFreqId.Id && c.IsDeleted == false);

            var AdminTimeCSId = await _unitOfWork.CodeSet.GetFirstOrDefaultAsync(c => c.Name == SD.AdminTime);
            var AdminTimeCVs = await _unitOfWork.CodeValue.GetAllAsync(c => c.CodeSetId == AdminTimeCSId.Id && c.IsDeleted == false);

            var OrderStatusCSId = await _unitOfWork.CodeSet.GetFirstOrDefaultAsync(c => c.Name == SD.OrdetStatus);
            var OrderStatusCVs = await _unitOfWork.CodeValue.GetAllAsync(c => c.CodeSetId == OrderStatusCSId.Id && c.IsDeleted == false);

            OrderVM orderVM = new()
            {
                EncounterVM = new()
                {
                    Encounter = await _unitOfWork.Encounter.GetFirstOrDefaultAsync(e => e.Id == encntrId, includeProperties: "Patient,SchAppt.ApptType,SchAppt.ApptStatus,FinancialNumAlias,Insurance,ProviderUser,Location")
                }
            };
            
            orderVM.Order = await _unitOfWork.Order.GetFirstOrDefaultAsync(o => o.Id == orderId, includeProperties: "Encounter,Encounter.Patient,OrderCatalog,AdminRoute,AdminFreq,AdminTime,OrderStatus");
            return View(orderVM);
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
            if (obj.Order.OrderCatalogId == 0)
            {
                ModelState["Order.OrderCatalog.Name"].ValidationState = ModelValidationState.Invalid;
                ModelState.AddModelError("Order.OrderCatalog.Name", "Order Not Found");
            }

            if (ModelState.IsValid)
            {
                obj.Order.OrderCatalog = null;
                obj.Order.OrderingUserId = _userManager.GetUserId(User);
                var orderDetails="";

                if (obj.Order.AdminRouteId != null)
                {
                    var adminRoute = await _unitOfWork.CodeValue.GetFirstOrDefaultAsync(c => c.Id == obj.Order.AdminRouteId);
                    orderDetails += adminRoute.Name + ' ';
                }

                if (obj.Order.AdminFreqId != null)
                {
                    var adminFreq = await _unitOfWork.CodeValue.GetFirstOrDefaultAsync(c => c.Id == obj.Order.AdminFreqId);
                    orderDetails += adminFreq.Name + ' ';
                }

                if (obj.Order.AdminTimeId != null)
                {
                    var adminTime = await _unitOfWork.CodeValue.GetFirstOrDefaultAsync(c => c.Id == obj.Order.AdminTimeId);
                    orderDetails += adminTime.Name + ' ';
                }
                obj.Order.OrderDetails = orderDetails;

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
                Order = obj.Order,
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
                    Encounter = await _unitOfWork.Encounter.GetFirstOrDefaultAsync(e => e.Id == obj.Order.EncounterId, includeProperties: "Patient,SchAppt.ApptType,SchAppt.ApptStatus,FinancialNumAlias,Insurance,ProviderUser,Location")
                }
            };

            return View(orderVM);
        }

        #region API CALLS        
        [HttpGet]
        public async Task<JsonResult> OrderExists(int orderCatalogId, int encntrId)
        {
            var orderExists = await _unitOfWork.Order.GetFirstOrDefaultAsync(o => o.OrderCatalogId==orderCatalogId && o.IsActive==true && o.EncounterId==encntrId);
            return Json(new { orderExists });

        }
        #endregion
    }
}
