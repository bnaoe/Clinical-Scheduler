using Microsoft.AspNetCore.Mvc;
using Scheduler.DataAccess.Repository.IRepository;
using System.Security.Claims;

namespace ClinicalScheduler.ViewComponents
{
    public class UserNameViewComponent:ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserNameViewComponent(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var userInDb = await _unitOfWork.ApplicationUser.GetFirstOrDefaultAsync(u => u.Id == claims.Value);
        
            return View(userInDb);
        }
    }
}
