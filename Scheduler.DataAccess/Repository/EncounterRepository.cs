using Scheduler.DataAccess.Repository.IRepository;
using Scheduler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.DataAccess.Repository
{
    public class EncounterRepository : RepositoryAsync<Encounter>, IEncounterRepository
    {
        private ApplicationDbContext _db;

        public EncounterRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(Encounter obj)
        {
            //Another way to update class to update specific properties
            var encounterInDb = _db.Encounters.FirstOrDefault(e => e.Id == obj.Id);
            if (encounterInDb != null)
            {
                encounterInDb.PatientId = obj.PatientId;
                encounterInDb.ProviderUserId = obj.ProviderUserId;
                encounterInDb.SchApptId = obj.SchApptId;
                encounterInDb.InsuranceId = obj.InsuranceId;
                encounterInDb.HealthPlanName = obj.HealthPlanName;
                encounterInDb.MemberNo = obj.MemberNo;
                encounterInDb.GroupNo = obj.GroupNo;
                encounterInDb.InsDate = obj.InsDate;
                encounterInDb.LocationId = obj.LocationId;
                encounterInDb.AdmitDateTime = obj.AdmitDateTime;
                encounterInDb.DschDateTime =  obj.DschDateTime;
                encounterInDb.ReasonForVisit = obj.ReasonForVisit;
                encounterInDb.ConsentGiven = obj.ConsentGiven;
                encounterInDb.PrivacyNotice = obj.PrivacyNotice;
                encounterInDb.GuarantorName = obj.GuarantorName;
                encounterInDb.IsDeleted=obj.IsDeleted;
            }
        }
    }
}
