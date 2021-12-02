using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Scheduler.DataAccess;
using Scheduler.DataAccess.Repository.IRepository;
using Scheduler.Models;
using Scheduler.Models.ViewModels;

namespace ClinicalScheduler.Controllers
{
    [Area("Shared")]
    public class SearchController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public SearchController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult PatientSearch()
        {
            return View();
        }

       

        #region API CALLS
        [HttpGet]
        public IActionResult GetAllPatients(String firstName, String lastName, DateTime birthDate)
        {
            var patientList = _unitOfWork.Patient.GetAll(p=> p.LastName==lastName 
            || p.FirstName==firstName || p.BirthDate==birthDate);
            return Json(new { patientList });
        }
        #endregion
    }
}
