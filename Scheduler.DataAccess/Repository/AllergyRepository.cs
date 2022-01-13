using Scheduler.DataAccess.Repository.IRepository;
using Scheduler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.DataAccess.Repository
{
    public class AllergyRepository : RepositoryAsync<Allergy>, IAllergyRepository
    {
        private ApplicationDbContext _db;

        public AllergyRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(Allergy obj)
        {
            //Another way to update class to update specific properties
            var allergyInDb = _db.Allergies.FirstOrDefault(a => a.Id == obj.Id);
            if (allergyInDb != null)
            {
                allergyInDb.PatientId = obj.PatientId;
                allergyInDb.ProviderUserId = obj.ProviderUserId;
                allergyInDb.AllergyName = obj.AllergyName;
                allergyInDb.IsActive = obj.IsActive;
                allergyInDb.ActiveDtTm = obj.ActiveDtTm;
                allergyInDb.EndDtTm = obj.EndDtTm;
            }
        }
    }
}
