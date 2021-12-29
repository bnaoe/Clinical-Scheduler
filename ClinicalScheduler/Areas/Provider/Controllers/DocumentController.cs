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
    public class DocumentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;

        public DocumentController(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        //get
        public async Task<IActionResult> Upsert(int? docId, int encntrId)
        {
            var DocTypeCSId = await _unitOfWork.CodeSet.GetFirstOrDefaultAsync(c => c.Name == SD.DocType);
            var DocTypeCVs = await _unitOfWork.CodeValue.GetAllAsync(c => c.CodeSetId == DocTypeCSId.Id && c.IsDeleted == false);

            var DocStatusCV = await _unitOfWork.CodeValue.GetFirstOrDefaultAsync(c => c.Name == SD.InProgressDocStatus && c.IsDeleted == false);

            DocumentVM documentVM = new()
            {
                Document = new()
                {
                    EncounterId = encntrId
                },
                DocTypeList = DocTypeCVs.Select(d => new SelectListItem
                {
                    Text = d.Name,
                    Value = d.Id.ToString()
                }),
                EncounterVM = new()
                {
                    Encounter = await _unitOfWork.Encounter.GetFirstOrDefaultAsync(e=>e.Id == encntrId, includeProperties: "Patient,SchAppt.ApptType,SchAppt.ApptStatus,FinancialNumAlias,Insurance,ProviderUser,Location")
                }
            };
            if (docId == null || docId == 0)
            {
                //Create
                documentVM.Document.ProviderUserId = _userManager.GetUserId(User);
                documentVM.Document.DocStatusId = DocStatusCV.Id;
                documentVM.Document.DocStatus = DocStatusCV;
                return View(documentVM);
            } else
            {
                //Update 
                documentVM.Document = await _unitOfWork.Document.GetFirstOrDefaultAsync(d => d.Id == docId,includeProperties: "DocStatus");
                documentVM.Document.ProviderUserId = _userManager.GetUserId(User);
                documentVM.Document.ModifiedDateTime = DateTime.Now;
                return View(documentVM);
            }
        }

        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(DocumentVM obj,string? save, string? final)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(final))
                {
                    var DocStatusCV = await _unitOfWork.CodeValue.GetFirstOrDefaultAsync(c => c.Name == SD.FinalDocStatus && c.IsDeleted == false);
                    obj.Document.DocStatusId = DocStatusCV.Id;
                    obj.Document.DocStatus = DocStatusCV;
                }

                if (obj.Document.Id==0) {
                    await _unitOfWork.Document.AddAsync(obj.Document);
                    TempData["Success"] = "Added successfully";
                } else
                {
                    _unitOfWork.Document.Update(obj.Document);
                    TempData["Success"] = "Updated successfully";
                }

                _unitOfWork.Save();
                return RedirectToAction("EncounterSchAppt", "Chart", new { enctrId = obj.Document.EncounterId, Area = "Provider" });

            }
            return View(obj);
        }

        #region API CALLS
        [HttpGet]
        public async Task<IActionResult> GetAllDocuments(int encntrId)
        {
            IEnumerable<Document> docList;

            docList = await _unitOfWork.Document.GetAllAsync(d => d.EncounterId == encntrId, orderBy: d => d.OrderByDescending(x => x.CreateDateTime)
            , includeProperties: "ProviderUser,DocType,DocStatus");

            var encounterDocList = docList.Select(async i => new
            {
                i.Id,
                i.Title,
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

        //post
        [HttpPost]
        public async Task<IActionResult> InError(int? id)
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
