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
    [Area("Scheduler")]
    public class ScheduleAppointmentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;

        public ScheduleAppointmentController(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        //get
        public async Task<IActionResult> Upsert(int? schApptId, int? enctrId, int patientId,int providerScheduleProfileId)
        {
            //Load data for Drop Downs
            var ApptTypeCSId = await _unitOfWork.CodeSet.GetFirstOrDefaultAsync(c => c.Name == SD.ApptType);
            var ApptTypeCodeSetId = ApptTypeCSId.Id;
            var ApptTypeCVs = await _unitOfWork.CodeValue.GetAllAsync();

            var ApptStatusCSId = await _unitOfWork.CodeSet.GetFirstOrDefaultAsync(c => c.Name == SD.ApptStatus);
            var ApptStatusCodeSetId = ApptStatusCSId.Id;
            var ApptStatusCVs = await _unitOfWork.CodeValue.GetAllAsync();

            //Declare Collection Class
            CcApptSched cCApptSched = new()
            {
                patientVM = new()
                {
                    Patient = await _unitOfWork.Patient.GetFirstOrDefaultAsync(p => p.Id == patientId)
                }, // _PatientDetails View
                schApptVM = new() //SchAppt View
                {
                    SchAppt = new(),
                    ApptTypeList = ApptTypeCVs.Where(c => c.IsDeleted == false && c.CodeSetId == ApptTypeCodeSetId).Select(c => new SelectListItem
                    {
                        Text = c.Name,
                        Value = c.Id.ToString()
                    }),
                    ApptStatusList = ApptStatusCVs.Where(c => c.IsDeleted == false && c.CodeSetId == ApptStatusCodeSetId).Select(c => new SelectListItem
                    {
                        Text = c.Name,
                        Value = c.Id.ToString()
                    }),
                    Patient = await _unitOfWork.Patient.GetFirstOrDefaultAsync(p => p.Id == patientId),
                    ProviderScheduleProfile = await _unitOfWork.ProviderScheduleProfile.GetFirstOrDefaultAsync(p => p.Id == providerScheduleProfileId)
                },
                encounterVM = new() // Encounter View ///////////////////////>>>>>>>>> institiate properties
                {
                    Encounter = new(),
                    Patient = await _unitOfWork.Patient.GetFirstOrDefaultAsync(p => p.Id == patientId),
                    ProviderUser = new(),
                    SchAppt =new(),
                    Insurance = new(),
                    Location = new()
                },
                providerScheduleProfileVM = new()
                {
                    ProviderScheduleProfile = await _unitOfWork.ProviderScheduleProfile.GetFirstOrDefaultAsync(p => p.Id == providerScheduleProfileId,includeProperties:"ProviderUser,Location")
                } // May not need this?
            };

            // Prepare for view 
            ///_PatientDetails uneditable
            //cCApptSched.patientVM.Patient = await _unitOfWork.Patient.GetFirstOrDefaultAsync(p => p.Id == patientId);

            /// Schedule Profile details uneditable
            //cCApptSched.providerScheduleProfileVM.ProviderScheduleProfile =
           //     await _unitOfWork.ProviderScheduleProfile.GetFirstOrDefaultAsync(p => p.Id == providerScheduleProfileId);

            if ((schApptId == null || schApptId == 0) && (enctrId == null || enctrId == 0))
            {
               
                // Assign Ids needed
                /// Prefill Data needed for Encounter and SchAppt
                //cCApptSched.schApptVM.SchAppt.PatientId = patientId;
               // cCApptSched.schApptVM.SchAppt.ProviderScheduleProfileId = providerScheduleProfileId;

                
                cCApptSched.schApptVM.SchAppt.RegistrarUserId = _userManager.GetUserId(User);
                cCApptSched.schApptVM.SchAppt.start_date = DateTime.Now;
                cCApptSched.schApptVM.SchAppt.end_date = DateTime.Now;

                //cCApptSched.encounterVM.Encounter.PatientId = patientId;
                cCApptSched.encounterVM.Encounter.ProviderUserId = cCApptSched.providerScheduleProfileVM.ProviderScheduleProfile.ProviderUserId;
                cCApptSched.encounterVM.Encounter.LocationId = cCApptSched.providerScheduleProfileVM.ProviderScheduleProfile.LocationId;

                return View(cCApptSched);
            } else
            {
                cCApptSched.schApptVM.SchAppt = await _unitOfWork.SchAppt.GetFirstOrDefaultAsync(s => s.Id == schApptId);
                cCApptSched.encounterVM.Encounter = await _unitOfWork.Encounter.GetFirstOrDefaultAsync(e => e.Id == enctrId);
                return View(cCApptSched);
            }
        }

        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(PatientVM obj)
        {
            if (ModelState.IsValid)
            {
                if (obj.Patient.Id==0) {
                    await _unitOfWork.Patient.AddAsync(obj.Patient);
                    TempData["Success"] = "Added successfully";
                } else
                {
                    _unitOfWork.Patient.Update(obj.Patient);
                    TempData["Success"] = "Updated successfully";
                }

                _unitOfWork.Save();
                return RedirectToAction("Upsert", new { id = obj.Patient.Id});
            }
            return View(obj);
        }

        #region API CALLS
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var patientList = await _unitOfWork.Patient.GetAllAsync();
            return Json(new { patientList });
        }
        
        [HttpGet]
        public async Task<IActionResult> GetProviderAppointments(string providerId)
        {
            var schApptList = await _unitOfWork.SchAppt.GetAllAsync(c=>c.ProviderScheduleProfile.ProviderUserId==providerId);
            var appointmentList = schApptList.Select(a => new { a.start_date, a.end_date, a.text });
            return Ok(appointmentList );
        }


        //post
        [HttpDelete]
        public async Task<IActionResult> Delete(int? id)
        {

            var patientInDb = await _unitOfWork.Patient.GetFirstOrDefaultAsync(p => p.Id == id);
            if (patientInDb == null)
            {
                return Json(new { Success = false, message = "Error while deleting" });
            }

            patientInDb.IsDeleted = true;
            _unitOfWork.Save();

            return Json(new { Success = true, message = "Delete successful" });

        }
        #endregion
    }
}
