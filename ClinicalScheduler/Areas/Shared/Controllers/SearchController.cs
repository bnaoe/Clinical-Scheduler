using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Scheduler.DataAccess;
using Scheduler.DataAccess.Repository.IRepository;
using Scheduler.Models;
using Scheduler.Models.ViewModels;
using Scheduler.Utility;
using System.Security.Claims;

namespace ClinicalScheduler.Controllers
{
    [Area("Shared")]
    [Authorize]
    public class SearchController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;

        public SearchController(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public IActionResult PatientSearch()
        {
            return View();
        }

        public IActionResult ProviderSearch()
        {
            return View();
        }

        public async Task<IActionResult> ApptDashboard()
        {
            IEnumerable<Location> locationList = await _unitOfWork.Location.GetAllAsync(l=>l.IsDeleted== false, orderBy: l => l.OrderBy(x=>x.Name));

            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var currentUserDefaultLocId = await _unitOfWork.ApplicationUser.GetFirstOrDefaultAsync(a => a.Id == claim.Value);

            ApptDashboardVM apptDashboardVM = new()
            {
                LocationList = locationList.Select(l => new SelectListItem
                {
                    Text = l.Name,
                    Value = l.Id.ToString(),
                    Selected = l.Id == currentUserDefaultLocId.LocationId? true:l.Id==locationList.FirstOrDefault().Id

                })
            };

            
            return View(apptDashboardVM);
        }

        #region API CALLS
        [HttpGet]
        public async Task<JsonResult> GetDxList(string description)
        {
            var dxList = await _unitOfWork.DxCode.GetAllAsync(d => d.Description.Contains(description) && d.IsActive == true);
            var diagnosisList = dxList.Take(10);
            return Json(new { diagnosisList });

        }

        [HttpGet]
        public async Task<JsonResult> GetInsuranceList(string name)
        {
            var insuranceList = await _unitOfWork.Insurance.GetAllAsync(x => x.Name.StartsWith(name) && x.IsDeleted == false);
            return Json(new { insuranceList });

        }

        [HttpGet]
        public async Task<JsonResult> GetOrderList(string name)
        {
            var orderList = await _unitOfWork.OrderCatalog.GetAllAsync(o => o.Name.StartsWith(name) && o.IsDeleted == false);
            return Json(new { orderList });

        }

        [HttpGet]
        public async Task<IActionResult> GetAllPatients(String firstName, String lastName, DateTime birthDate)
        {

            IEnumerable<Patient> patients;

            if (firstName != null && lastName != null && birthDate!= DateTime.MinValue)
            {
                patients = await _unitOfWork.Patient.GetAllAsync(p => p.LastName.Contains(lastName)
               && p.FirstName.Contains(firstName) && p.BirthDate == birthDate);
            }
            else if (firstName != null && lastName != null)
            {
                patients = await _unitOfWork.Patient.GetAllAsync(p => p.LastName.Contains(lastName)
                && p.FirstName.Contains(firstName));
            }
            else if (lastName != null && birthDate!= DateTime.MinValue) {
                patients = await _unitOfWork.Patient.GetAllAsync(p => p.LastName.Contains(lastName)
               && p.BirthDate == birthDate);
            }
            else
            {
                patients = await _unitOfWork.Patient.GetAllAsync(p => p.LastName.Contains(lastName)
                || p.FirstName.Contains(firstName) || p.BirthDate == birthDate);

            }
            patients = patients.Where(p => p.IsDeleted == false);

            var patientList = patients.Select(async i => new
            {
                i.Id,
                i.FirstName,
                i.MiddleName,
                i.LastName,
                i.BirthDate,
                i.IsDeleted
            });

            return Json(new { patientList });
        }


        [HttpGet]
        public async Task<IActionResult> GetAllDiagnosisEncntr(int encntrId)
        {
            IEnumerable<Diagnosis> diagnosisList;

            diagnosisList = await _unitOfWork.Diagnosis.GetAllAsync(d => d.EncounterId == encntrId, orderBy: d => d.OrderByDescending(x => x.Id)
            , includeProperties: "Encounter,DxCode,Patient,ProviderUser,Encounter.FinancialNumAlias");

            var encounterDiagnosisList = diagnosisList.Select(async i => new
            {
                i.Id,
                i.DxCode.Code,
                i.DxCode.Description,
                i.Encounter.FinancialNumAlias.Fin,
                i.EncounterId,
                i.ProviderUser.FirstName,
                i.ProviderUser.MiddleName,
                i.ProviderUser.LastName,
                i.ProviderUser.Suffix,
                i.Encounter.AdmitDateTime,
                i.IsActive,
                i.ActiveDtTm,
                i.EndDtTm,
                i.PatientId,
                i.ProviderUserId
            });

            return Json(new { encounterDiagnosisList });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDiagnosisPatient(int patientId)
        {
            IEnumerable<Diagnosis> diagnosisList;

            diagnosisList = await _unitOfWork.Diagnosis.GetAllAsync(d => d.PatientId == patientId, orderBy: d => d.OrderBy(x => x.IsActive)
            , includeProperties: "Patient,Encounter,DxCode,ProviderUser,Encounter.FinancialNumAlias");

            var patientDiagnosisList = diagnosisList.Select(async i => new
            {
                i.Id,
                i.DxCode.Code,
                i.DxCode.Description,
                i.Encounter.FinancialNumAlias.Fin,
                i.EncounterId,
                i.ProviderUser.FirstName,
                i.ProviderUser.MiddleName,
                i.ProviderUser.LastName,
                i.ProviderUser.Suffix,
                i.Encounter.AdmitDateTime,
                i.IsActive,
                i.ActiveDtTm,
                i.EndDtTm,
                i.PatientId,
                i.ProviderUserId
            });

            return Json(new { patientDiagnosisList });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPrescriptions(int patientId)
        {
            IEnumerable<Order> orderList;

            orderList = await _unitOfWork.Order.GetAllAsync(o => o.PatientId == patientId && o.OrderCatalog.CodeValue.Name==SD.Presciption
            , includeProperties: "Patient,OrderingUser,OrderStatus,OrderCatalog,OrderCatalog.CodeValue,OrderCatalog.CodeValue.CodeSet,Encounter,AdminRoute,AdminFreq,AdminTime");

            var prescriptionList =  orderList.Select(async i => new
            {
                i.Id,
                i.OrderingDtTm,
                i.EncounterId,
                OrderTypeName = i.OrderCatalog.CodeValue.Name,
                i.OrderCatalog.Name,
                i.OrderDetails,
                i.OrderingUser.LastName,
                i.OrderingUser.FirstName,
                i.OrderingUser.MiddleName,
                i.OrderingUser.Suffix,
                Route = i.AdminRoute.Name,
                Freq = i.AdminFreq.Name,
                Time = i.AdminTime.Name,
                OrderStatusName = i.OrderStatus.Name,
                i.IsActive
            });

            return Json(new { prescriptionList });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders(int encntrId)
        {
            IEnumerable<Order> orderList;

            orderList = await _unitOfWork.Order.GetAllAsync(o => o.EncounterId == encntrId, orderBy: o => o.OrderByDescending(x => x.OrderingDtTm)
            , includeProperties: "AdminRoute,AdminFreq,AdminTime,OrderingUser,OrderStatus,OrderCatalog,OrderCatalog.CodeValue,Encounter");

            var encounterOrderList = orderList.Select(async i => new
            {
                i.Id,
                i.OrderingDtTm,
                i.EncounterId,
                OrderTypeName = i.OrderCatalog.CodeValue.Name,
                i.OrderCatalog.Name,
                i.OrderDetails,
                i.OrderingUser.LastName,
                i.OrderingUser.FirstName,
                i.OrderingUser.MiddleName,
                i.OrderingUser.Suffix,
                i.AdminRoute,
                i.AdminFreq,
                i.AdminTime,
                OrderStatusName = i.OrderStatus.Name,
                i.IsActive
            });

            return Json(new { encounterOrderList });
        }


        [HttpGet]
        public async Task<IActionResult> GetAllDocuments(int encntrId)
        {
            IEnumerable<Document> docList;

            docList = await _unitOfWork.Document.GetAllAsync(d => d.EncounterId == encntrId, orderBy: d=>d.OrderByDescending(x => x.CreateDateTime)
            ,includeProperties: "ProviderUser,DocType,DocStatus") ;

            var encounterDocList = docList.Select(async i => new
            {
                i.Id,
                i.Title,
                i.EncounterId,
                i.CreateDateTime,
                i.ModifiedDateTime,
                i.ProviderUser.LastName,
                i.ProviderUser.FirstName,
                i.ProviderUser.MiddleName,
                i.ProviderUser.Suffix,
                i.DocType,
                i.DocStatus,
                i.InError
            });
            
            return Json(new { encounterDocList });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEncounters(int id)
        {
            IEnumerable<Encounter> encounterSchApptList;
          

            encounterSchApptList = await _unitOfWork.Encounter.GetAllAsync(e=>e.PatientId== id,includeProperties: "ProviderUser,SchAppt,Location,FinancialNumAlias,,SchAppt.ApptType,SchAppt.ApptStatus");

            var patientEncounterSchApptList = encounterSchApptList.Select(async i => new
            {
                i.SchApptId,
                i.Id,
                i.PatientId,
                i.SchAppt.ProviderScheduleProfileId,
                i.FinancialNumAlias.Fin,
                i.ProviderUser.FirstName,
                i.ProviderUser.LastName,
                i.ProviderUser.MiddleName,
                i.SchAppt.start_date,
                i.SchAppt.end_date,
                i.Location.Name,
                i.SchAppt.ApptType,
                i.SchAppt.ApptStatus
            });

            return Json(new { patientEncounterSchApptList });

        }

        public async Task<IActionResult> GetAllAppointments(int locId, DateTime apptDT, string? firstName, string? lastName)
        {
            IEnumerable<Encounter> ApptList;

            if (firstName != null && lastName != null)
            {
                ApptList = await _unitOfWork.Encounter.GetAllAsync(e => e.LocationId == locId && e.SchAppt.start_date.Date == apptDT.Date
                    && e.ProviderUser.FirstName.Contains(firstName) && e.ProviderUser.LastName.Contains(lastName)
                        , includeProperties: "ProviderUser,SchAppt,Location,FinancialNumAlias,SchAppt.ApptType,SchAppt.ApptStatus");
            }
            else if (firstName != null || lastName != null)
            {
                ApptList = await _unitOfWork.Encounter.GetAllAsync(e => e.LocationId == locId && e.SchAppt.start_date.Date == apptDT.Date
                        && (e.ProviderUser.FirstName.Contains(firstName) || e.ProviderUser.LastName.Contains(lastName))
                        , includeProperties: "ProviderUser,SchAppt,Location,FinancialNumAlias,SchAppt.ApptType,SchAppt.ApptStatus");
            } else 
            {
                ApptList = await _unitOfWork.Encounter.GetAllAsync(e => e.LocationId == locId && e.SchAppt.start_date.Date == apptDT.Date
                           , includeProperties: "Patient,ProviderUser,SchAppt,Location,FinancialNumAlias,SchAppt.ApptType,SchAppt.ApptStatus");
            }

            var dashboardApptList = ApptList.Select(async i => new
            {
                i.SchApptId,
                i.Id,
                i.PatientId,
                i.SchAppt.ProviderScheduleProfileId,
                i.FinancialNumAlias.Fin,
                ProvName = i.ProviderUser.LastName + ", " + i.ProviderUser.FirstName + " " + i.ProviderUser.MiddleName + " " + i.ProviderUser.Suffix,
                PtName = i.Patient.LastName + ", " + i.Patient.FirstName + " " + i.Patient.MiddleName, 
                i.SchAppt.start_date,
                i.SchAppt.end_date,
                i.SchAppt.ApptType,
                i.SchAppt.ApptStatus
            });;

            return Json(new { dashboardApptList });

        }

        [HttpGet]
        public async Task<IActionResult> GetAllProviders(string firstName, string lastName)
        {
            IEnumerable<ApplicationUser> users;

            if (firstName != null && lastName != null)
            {
                users = await _unitOfWork.ApplicationUser.GetAllAsync(a => a.LastName.Contains(lastName) 
                && a.FirstName.Contains(firstName), includeProperties: "Location");
            }
            else 
            { 
                users = await _unitOfWork.ApplicationUser.GetAllAsync(a => a.LastName.Contains(lastName) 
                || a.FirstName.Contains(firstName), includeProperties: "Location");
            }

            foreach (var user in users)
            {
                user.Role = _userManager.GetRolesAsync(user).Result.FirstOrDefault();
            }

            users = users.Where(u => u.Role == SD.Role_Physician||u.Role== SD.Role_NP);

            var providerList = users.Select(async i => new
            {
                i.Id,
                i.FirstName,
                i.MiddleName,
                i.LastName,
                i.Suffix,
                i.Specialization,
                locName = i.Location.Name
            });
            
            return Json(new { providerList });
        }
        #endregion
    }
}
