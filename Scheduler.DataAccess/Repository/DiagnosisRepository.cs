using Scheduler.DataAccess.Repository.IRepository;
using Scheduler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.DataAccess.Repository
{
    public class DiagnosisRepository : RepositoryAsync<Diagnosis>, IDiagnosisRepository
    {
        private ApplicationDbContext _db;

        public DiagnosisRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(Diagnosis obj)
        {
            //Another way to update class to update specific properties
            var diagnosisInDb = _db.Diagnoses.FirstOrDefault(d => d.Id == obj.Id);
            if (diagnosisInDb != null)
            {
                diagnosisInDb.PatientId = obj.PatientId;
                diagnosisInDb.EncounterId = obj.EncounterId;
                diagnosisInDb.DxCodeId = obj.DxCodeId;
                diagnosisInDb.ActiveDtTm = obj.ActiveDtTm;
                diagnosisInDb.EndDtTm = obj.EndDtTm;
                diagnosisInDb.ProviderUserId = obj.ProviderUserId;
                diagnosisInDb.IsActive = obj.IsActive;

            }
        }
    }
}
