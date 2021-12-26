using Scheduler.DataAccess.Repository.IRepository;
using Scheduler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.DataAccess.Repository
{
    public class DocumentRepository : RepositoryAsync<Document>, IDocumentRepository
    {
        private ApplicationDbContext _db;

        public DocumentRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(Document obj)
        {
            //Another way to update class to update specific properties
            var documentInDb = _db.Documents.FirstOrDefault(d => d.Id == obj.Id);
            if (documentInDb != null)
            {
                documentInDb.Age = obj.Age;
                documentInDb.BMI = obj.BMI;
                documentInDb.Diastolic = obj.Diastolic;
                documentInDb.DocStatusId = obj.DocStatusId;
                documentInDb.DocTypeId = obj.DocTypeId;
                documentInDb.EncounterId = obj.EncounterId;
                documentInDb.HeightFt = obj.HeightFt;
                documentInDb.HeightIn = obj.HeightIn;
                documentInDb.InError = obj.InError;
                documentInDb.ModifiedDateTime = obj.ModifiedDateTime;
                documentInDb.Narrative = obj.Narrative;
                documentInDb.OxygenSaturation = obj.OxygenSaturation;
                documentInDb.PainLocation = obj.PainLocation;
                documentInDb.PainScale = obj.PainScale;
                documentInDb.ProviderUserId = obj.ProviderUserId;
                documentInDb.PulseRate = obj.PulseRate;
                documentInDb.Systolic = obj.Systolic;
                documentInDb.Temperature = obj.Temperature;
                documentInDb.Title = obj.Title;
                documentInDb.Weight = obj.Weight;

            }
        }
    }
}
