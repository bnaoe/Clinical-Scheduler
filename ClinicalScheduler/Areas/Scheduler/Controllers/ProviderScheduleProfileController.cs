using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Scheduler.DataAccess;
using Scheduler.DataAccess.Repository.IRepository;
using Scheduler.Models;
using Scheduler.Models.ViewModels;

namespace ClinicalScheduler.Controllers
{
    [Area("Scheduler")]
    public class ProviderScheduleProfileController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;

        public ProviderScheduleProfileController(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public IActionResult Index()
        {

            return View();
        }

        //get
        public async Task<IActionResult> Upsert(int? id,string userId)
        {
            IEnumerable<Location> LocationList = await _unitOfWork.Location.GetAllAsync();

            ProviderScheduleProfileVM providerScheduleProfileVM = new()
            {
                ProviderScheduleProfile = new(),
                LocationList = LocationList.Where(l => l.IsDeleted == false).OrderBy(l => l.Name).Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                })
            };

            ApplicationUser applicationUser = await _unitOfWork.ApplicationUser.GetFirstOrDefaultAsync(a => a.Id == userId);
            applicationUser.Role = _userManager.GetRolesAsync(applicationUser).Result.FirstOrDefault();
            CollectionClassScheduleProfile collectionModel = new CollectionClassScheduleProfile();
            collectionModel.applicationUser = applicationUser;

            if (id==null || id ==0)
            {
                providerScheduleProfileVM.ProviderScheduleProfile.ApplicationUserId = userId;
                collectionModel.providerScheduleProfileVM = providerScheduleProfileVM;

                return View(collectionModel);

            } else
            {
                providerScheduleProfileVM.ProviderScheduleProfile = await _unitOfWork.ProviderScheduleProfile.GetFirstOrDefaultAsync(p => p.Id == id);
                collectionModel.providerScheduleProfileVM = providerScheduleProfileVM;

                return View(collectionModel);

            }         
        }

        //get
        public async Task<IActionResult> GetProviderDetails(string id)
        {
            ApplicationUser applicationUser = new();
            {
                applicationUser = await _unitOfWork.ApplicationUser.GetFirstOrDefaultAsync(a => a.Id == id);
                if (applicationUser == null)
                {
                    TempData["Error"] = "Not Found";
                }
                
                return View("ProviderScheduleProfile", applicationUser);
                
            }
        }

        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(CollectionClassScheduleProfile obj)
        {
            if (ModelState.IsValid)
            {
                if (obj.providerScheduleProfileVM.ProviderScheduleProfile.Id==0) {
                    await _unitOfWork.ProviderScheduleProfile.AddAsync(obj.providerScheduleProfileVM.ProviderScheduleProfile);
                    TempData["Success"] = "Added successfully";
                } else
                {
                    _unitOfWork.ProviderScheduleProfile.Update(obj.providerScheduleProfileVM.ProviderScheduleProfile);
                    TempData["Success"] = "Updated successfully";
                }

                _unitOfWork.Save();
            }
            return RedirectToAction("GetProviderDetails", new { id = obj.providerScheduleProfileVM.ProviderScheduleProfile.ApplicationUserId } );
        }

        #region API CALLS
        [HttpGet]
        public async Task<IActionResult> GetAll(string id)
        {
            var providerScheduleProfileList = await _unitOfWork.ProviderScheduleProfile.GetAllAsync(p=>p.ApplicationUserId == id, includeProperties:"Location");
            return Json(new { providerScheduleProfileList });
        }

        //post
        [HttpDelete]
        public async Task<IActionResult> Delete(int? id)
        {

            var scheduleProfileInDb = await _unitOfWork.ProviderScheduleProfile.GetFirstOrDefaultAsync(p => p.Id == id);
            if (scheduleProfileInDb == null)
            {
                return Json(new { Success = false, message = "Error while deleting" });
            }

            //_unitOfWork.CodeValue.Remove(codeValueInDb);
            scheduleProfileInDb.isDeleted = true;
            _unitOfWork.Save();

            return Json(new { Success = true, message = "Delete successful" });

        }
        #endregion
    }
}
