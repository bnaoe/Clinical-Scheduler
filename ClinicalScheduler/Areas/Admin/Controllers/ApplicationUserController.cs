using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Scheduler.DataAccess;
using Scheduler.DataAccess.Repository.IRepository;
using Scheduler.Models;
using Scheduler.Models.ViewModels;

namespace ClinicalScheduler.Controllers
{
    [Area("Admin")]
    public class ApplicationUserController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;

        public ApplicationUserController(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public IActionResult Index()
        {

            return View();
        }


        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var users =  _unitOfWork.ApplicationUser.GetAll(includeProperties: "Location");
            foreach (var user in users)
            {
                user.Role = _userManager.GetRolesAsync(user).Result.FirstOrDefault();

                if (user.Location == null)
                {
                    user.Location = new Location()
                    {
                        Name = ""
                    };
                }
            }
            return Json(new { data = users });
        }

        //post
        [HttpPost]
        public IActionResult LockUnlock([FromBody] string id)
        {
            var objInDb = _unitOfWork.ApplicationUser.GetFirstOrDefault(a=>a.Id==id);
            if (objInDb == null)
            {
                return Json(new { success = false, message = "Error while locking/unlocking." });
            }

            if (objInDb.LockoutEnd != null && objInDb.LockoutEnd > DateTime.Now)
            {
                //user is currently locked, and will be unlocked
                objInDb.LockoutEnd = DateTime.Now;
            }
            else
            {
                objInDb.LockoutEnd = DateTime.Now.AddYears(1000);
            }
            _unitOfWork.Save();
            return Json(new { success = true, message = "Success!" });
        }
        #endregion
    }
}
