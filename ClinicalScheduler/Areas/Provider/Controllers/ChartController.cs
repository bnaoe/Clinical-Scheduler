using Microsoft.AspNetCore.Mvc;
using Scheduler.DataAccess.Repository.IRepository;
using Scheduler.Models;
using Scheduler.Models.ViewModels;

namespace ClinicalScheduler.Areas.Provider.Controllers
{
    [Area("Provider")]

    public class ChartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ChartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        //get
        public async Task<IActionResult> EncounterSchAppt(int enctrId)
        {
            IEnumerable<Document> docList;

            docList = await _unitOfWork.Document.GetAllAsync(d => d.EncounterId == enctrId, orderBy: d => d.OrderByDescending(x => x.CreateDateTime)
            , includeProperties: "ProviderUser,DocType,DocStatus");

            EncounterVM encounterVM = new()
            {
                Encounter = await _unitOfWork.Encounter.GetFirstOrDefaultAsync(e => e.Id == enctrId, includeProperties: "Patient,SchAppt.ApptType,SchAppt.ApptStatus,FinancialNumAlias,Insurance,ProviderUser,Location"),
                LastDocument = docList.FirstOrDefault(),

            };

            return View(encounterVM);
        }

    }
}