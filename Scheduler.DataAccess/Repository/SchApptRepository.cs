using Scheduler.DataAccess.Repository.IRepository;
using Scheduler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.DataAccess.Repository
{
    public class SchApptRepository : RepositoryAsync<SchAppt>, ISchApptRepository
    {
        private ApplicationDbContext _db;

        public SchApptRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(SchAppt obj)
        {
            //Another way to update class to update specific properties
            var schApptInDb = _db.SchAppts.FirstOrDefault(s => s.Id == obj.Id);
            if (schApptInDb != null)
            {
                schApptInDb.PatientId = obj.PatientId;
                schApptInDb.ProviderScheduleProfileId = obj.ProviderScheduleProfileId;
                schApptInDb.RegistrarUserId = obj.RegistrarUserId;
                schApptInDb.ApptTypeId = obj.ApptTypeId;
                schApptInDb.ApptStatusId = obj.ApptStatusId;
                schApptInDb.start_date = obj.start_date;
                schApptInDb.end_date = obj.end_date;

            }
        }
    }
}
