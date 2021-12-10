using Scheduler.DataAccess.Repository.IRepository;
using Scheduler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.DataAccess.Repository
{
    public class PatientRepository : RepositoryAsync<Patient>, IPatientRepository
    {
        private ApplicationDbContext _db;

        public PatientRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(Patient obj)
        {
            //Another way to update class to update specific properties
            var patientInDb = _db.Patients.FirstOrDefault(p => p.Id == obj.Id);
            if (patientInDb != null)
            {
                patientInDb.FirstName = obj.FirstName;
                patientInDb.LastName = obj.LastName;
                patientInDb.MiddleName = obj.MiddleName;
                patientInDb.BirthDate = obj.BirthDate;
                patientInDb.StreetAddress = obj.StreetAddress;
                patientInDb.City = obj.City;
                patientInDb.State = obj.State;
                patientInDb.Zip = obj.Zip;
                patientInDb.BirthDate = obj.BirthDate;
                patientInDb.PhoneNumber = obj.PhoneNumber;
                patientInDb.WorkNumber = obj.WorkNumber;
                patientInDb.MobileNumber = obj.MobileNumber;
                patientInDb.IsDeleted = obj.IsDeleted;
            }
        }
    }
}
