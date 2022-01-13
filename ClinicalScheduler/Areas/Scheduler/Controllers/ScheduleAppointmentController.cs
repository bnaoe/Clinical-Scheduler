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
using System.Globalization;

namespace ClinicalScheduler.Controllers
{
    [Area("Scheduler")]
    [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Scheduler)]
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
            var ApptTypeCSId = await _unitOfWork.CodeSet.GetFirstOrDefaultAsync(c => c.Name == SD.ApptType);
            var ApptTypeCVs = await _unitOfWork.CodeValue.GetAllAsync(c => c.CodeSetId == ApptTypeCSId.Id && c.IsDeleted == false);

            var ApptStatusCSId = await _unitOfWork.CodeSet.GetFirstOrDefaultAsync(c => c.Name == SD.ApptStatus);
            var ApptStatusCVs = await _unitOfWork.CodeValue.GetAllAsync(c => c.CodeSetId == ApptStatusCSId.Id && c.IsDeleted == false);

            //Declare Collection Class
            CcApptSched cCApptSched = new()
            {
                patientVM = new()
                {
                    Patient = await _unitOfWork.Patient.GetFirstOrDefaultAsync(p => p.Id == patientId, includeProperties: "Gender")
                }, // _PatientDetails View
                schApptVM = new() 
                {
                    SchAppt = new(),
                    ApptTypeList = ApptTypeCVs.Select(c => new SelectListItem
                    {
                        Text = c.Name,
                        Value = c.Id.ToString()
                    }),
                    ApptStatusList = ApptStatusCVs.Select(c => new SelectListItem
                    {
                        Text = c.Name,
                        Value = c.Id.ToString()
                    }),
                },
                encounterVM = new() 
                {
                    Encounter = new(),
                },
                providerScheduleProfileVM = new()
                {
                    ProviderScheduleProfile = await _unitOfWork.ProviderScheduleProfile.GetFirstOrDefaultAsync(p => p.Id == providerScheduleProfileId,includeProperties:"ProviderUser,Location")
                } 
            };

            if ((schApptId == null || schApptId == 0) && (enctrId == null || enctrId == 0))
            { 
                // Assign Ids needed
                // Prefill Data needed for Encounter and SchAppt for repository operations
                
                cCApptSched.schApptVM.SchAppt.PatientId = patientId;
                cCApptSched.schApptVM.SchAppt.ProviderScheduleProfileId = providerScheduleProfileId;                
                cCApptSched.schApptVM.SchAppt.RegistrarUserId = _userManager.GetUserId(User);
                cCApptSched.schApptVM.SchAppt.start_date = DateTime.Now;
                cCApptSched.schApptVM.SchAppt.end_date = DateTime.Now;
                cCApptSched.encounterVM.Encounter.PatientId = patientId;

                cCApptSched.encounterVM.Encounter.ProviderUserId = cCApptSched.providerScheduleProfileVM.ProviderScheduleProfile.ProviderUserId;
                cCApptSched.encounterVM.Encounter.LocationId = cCApptSched.providerScheduleProfileVM.ProviderScheduleProfile.LocationId;

                return View(cCApptSched);
            } else
            {
                cCApptSched.patientVM.Patient = await _unitOfWork.Patient.GetFirstOrDefaultAsync(p => p.Id == patientId);
                cCApptSched.schApptVM.SchAppt = await _unitOfWork.SchAppt.GetFirstOrDefaultAsync(s => s.Id == schApptId);
                cCApptSched.encounterVM.Encounter = await _unitOfWork.Encounter.GetFirstOrDefaultAsync(e => e.Id == enctrId);
                cCApptSched.providerScheduleProfileVM.ProviderScheduleProfile = await _unitOfWork.ProviderScheduleProfile.GetFirstOrDefaultAsync(p=>p.Id== providerScheduleProfileId);
                cCApptSched.encounterVM.Insurance = await _unitOfWork.Insurance.GetFirstOrDefaultAsync(i => i.Id == cCApptSched.encounterVM.Encounter.InsuranceId);

                return View(cCApptSched);
            }
        }

        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert([Bind(Prefix = "encounterVM")] EncounterVM encntrObj, [Bind(Prefix = "schApptVM")] SchApptVM schApptObj, 
            [Bind(Prefix = "providerScheduleProfileVM")] ProviderScheduleProfileVM providerScheduleProfileObj)
        {

            var apptType = await _unitOfWork.CodeValue.GetFirstOrDefaultAsync(c => c.Id == schApptObj.SchAppt.ApptTypeId);
            var patient = await _unitOfWork.Patient.GetFirstOrDefaultAsync(p => p.Id == encntrObj.Encounter.PatientId,includeProperties:"Gender");
            var loction = providerScheduleProfileObj.ProviderScheduleProfile.Location;

            //Update schAppt.color property
            if (apptType.Name.Contains("Initial"))
            {
                schApptObj.SchAppt.color = "#9575CD";
            }
            else if (apptType.Name.Contains("Follow-Up"))
            {
                schApptObj.SchAppt.color = "#FF9633";
            }
            else 
            { 
                //ModelState.SetModelValue("schApptVM.SchAppt.color", new ValueProviderResult("CadetBlue", CultureInfo.InvariantCulture));
                schApptObj.SchAppt.color = "#CadetBlue";
            }
            
            ModelState["schApptVM.SchAppt.color"].ValidationState = ModelValidationState.Valid;

            //Update schAppt.text property
            schApptObj.SchAppt.text = patient.FirstName + " " + patient.LastName + "\n" + loction.Name + " " + apptType.Name;
            


            ModelState["schApptVM.SchAppt.text"].ValidationState = ModelValidationState.Valid;

            if (encntrObj.Encounter.InsuranceId==0)
            {
                ModelState["encounterVM.Encounter.Insurance.Name"].ValidationState = ModelValidationState.Invalid;
                ModelState.AddModelError("encounterVM.Encounter.Insurance.Name", "Insurance Not Found");
            }

            var admittedStatusId = await _unitOfWork.CodeValue.GetFirstOrDefaultAsync(c => c.Name == SD.Admitted);

            if (schApptObj.SchAppt.ApptStatusId == admittedStatusId.Id)
            {
                encntrObj.Encounter.AdmitDateTime = schApptObj.SchAppt.start_date;
            }

            if (ModelState.IsValid)
            {
                if (schApptObj.SchAppt.Id == 0) {
                    await _unitOfWork.SchAppt.AddAsync(schApptObj.SchAppt);
                } else
                {
                    _unitOfWork.SchAppt.Update(schApptObj.SchAppt);
                }

                _unitOfWork.Save();

                //Create Financial Number for the Encounter
                FinancialNumAlias financialNumAlias = new FinancialNumAlias();
                financialNumAlias.Fin = DateTime.Now.Year.ToString() + schApptObj.SchAppt.Id + DateTime.Now.Millisecond.ToString();

                await _unitOfWork.FinancialNumAlias.AddAsync(financialNumAlias);
                _unitOfWork.Save();

                encntrObj.Encounter.SchApptId = schApptObj.SchAppt.Id;
                encntrObj.Encounter.Insurance = null;
                encntrObj.Encounter.FinancialNumAliasId = financialNumAlias.Id;

                if (encntrObj.Encounter.Id == 0)
                {
                    await _unitOfWork.Encounter.AddAsync(encntrObj.Encounter);
                    TempData["Success"] = "Added successfully";
                }
                else
                {
                    _unitOfWork.Encounter.Update(encntrObj.Encounter);
                    TempData["Success"] = "Updated successfully";
                }

                _unitOfWork.Save();


                return RedirectToAction("Upsert", new
                {
                    schApptId = schApptObj.SchAppt.Id,
                    enctrId = encntrObj.Encounter.Id,
                    patientId = encntrObj.Encounter.PatientId,
                    providerScheduleProfileId = schApptObj.SchAppt.ProviderScheduleProfileId

                });
            }

            encntrObj.Encounter.Insurance = await _unitOfWork.Insurance.GetFirstOrDefaultAsync(i => i.Id == encntrObj.Encounter.InsuranceId);

            var ApptTypeCSId = await _unitOfWork.CodeSet.GetFirstOrDefaultAsync(c => c.Name == SD.ApptType);
            var ApptTypeCVs = await _unitOfWork.CodeValue.GetAllAsync(c=>c.CodeSetId == ApptTypeCSId.Id && c.IsDeleted==false);

            var ApptStatusCSId = await _unitOfWork.CodeSet.GetFirstOrDefaultAsync(c => c.Name == SD.ApptStatus);
            var ApptStatusCVs = await _unitOfWork.CodeValue.GetAllAsync(c=>c.CodeSetId == ApptStatusCSId.Id && c.IsDeleted==false);

            CcApptSched cCApptSched = new()
            {
                patientVM = new()
                {
                    Patient = patient
                },
                schApptVM = new()
                {
                    SchAppt = schApptObj.SchAppt,
                    ApptTypeList = ApptTypeCVs.Select(c => new SelectListItem
                    {
                        Text = c.Name,
                        Value = c.Id.ToString()
                    }),
                    ApptStatusList = ApptStatusCVs.Select(c => new SelectListItem
                    {
                        Text = c.Name,
                        Value = c.Id.ToString()
                    })
                },
                encounterVM = encntrObj,
                providerScheduleProfileVM = new()
                {
                    ProviderScheduleProfile = await _unitOfWork.ProviderScheduleProfile.GetFirstOrDefaultAsync(p => p.Id == schApptObj.SchAppt.ProviderScheduleProfileId, includeProperties: "ProviderUser,Location")
                }
            };
 
            return View(cCApptSched);
        }

        #region API CALLS
        
        [HttpGet]
        public async Task<IActionResult> GetProviderAppointments(string providerId)
        {
            var schApptList = await _unitOfWork.SchAppt.GetAllAsync(c=>c.ProviderScheduleProfile.ProviderUserId==providerId);
            var appointmentList = schApptList.Select(a => new { a.start_date, a.end_date, a.text, a.color });
            return Ok(appointmentList );
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CancelAppt(int schApptId)
        {
            var schApptInDb = await _unitOfWork.SchAppt.GetFirstOrDefaultAsync(s => s.Id == schApptId);

            if (schApptInDb == null)
            {
                return Json(new { Success = false, message = "Error while cancelling appointment" });
            }
            
            var cancelledApptId = await _unitOfWork.CodeValue.GetFirstOrDefaultAsync(c => c.Name == SD.CancelledAppt && c.IsDeleted == false);
            schApptInDb.ApptStatusId = cancelledApptId.Id;

            _unitOfWork.Save();
            
            return Json(new { Success = true, message = "Cancel successful" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DischEncounter(int schApptId, int encntrId)
        {
            var schApptInDb = await _unitOfWork.SchAppt.GetFirstOrDefaultAsync(s => s.Id == schApptId);
            var encntrInDb = await _unitOfWork.Encounter.GetFirstOrDefaultAsync(e=>e.Id == encntrId, includeProperties: "SchAppt");
            var dischargeApptId = await _unitOfWork.CodeValue.GetFirstOrDefaultAsync(c => c.Name == SD.Discharged && c.IsDeleted == false);

            if (schApptInDb.ApptStatusId == dischargeApptId.Id)
            {
                return Json(new { Success = false, message = "Appointment had already been discharged" });
            }

            if (schApptInDb == null|| encntrInDb == null)
            {
                return Json(new { Success = false, message = "Error while discharging appointment" });
            }

            schApptInDb.ApptStatusId = dischargeApptId.Id;
            _unitOfWork.Save();

            encntrInDb.DschDateTime = encntrInDb.SchAppt.end_date;
            _unitOfWork.Save();

            return Json(new { Success = true, message = "Discharge successful" });
        }

        #endregion
    }
}
