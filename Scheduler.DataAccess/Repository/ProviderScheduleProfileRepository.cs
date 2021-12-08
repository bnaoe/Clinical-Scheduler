using Scheduler.DataAccess.Repository.IRepository;
using Scheduler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.DataAccess.Repository
{
    public class ProviderScheduleProfileRepository : RepositoryAsync<ProviderScheduleProfile>, IProviderScheduleProfileRepository
    {
        private ApplicationDbContext _db;

        public ProviderScheduleProfileRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(ProviderScheduleProfile obj)
        {
            //Another way to update class to update specific properties
            var scheduleProfileInDb = _db.ProviderScheduleProfiles.FirstOrDefault(p => p.Id == obj.Id);
            if (scheduleProfileInDb != null)
            {
                scheduleProfileInDb.ApplicationUserId = obj.ApplicationUserId;
                scheduleProfileInDb.LocationId = obj.LocationId;
                scheduleProfileInDb.Monday = obj.Monday;
                scheduleProfileInDb.Tuesday = obj.Tuesday;
                scheduleProfileInDb.Wednesday = obj.Wednesday;
                scheduleProfileInDb.Thursday = obj.Thursday;
                scheduleProfileInDb.Friday = obj.Friday;
                scheduleProfileInDb.Saturday = obj.Saturday;
                scheduleProfileInDb.Sunday = obj.Sunday;
                scheduleProfileInDb.StartTime = obj.StartTime;
                scheduleProfileInDb.EndTime = obj.EndTime;
                scheduleProfileInDb.isDeleted = obj.isDeleted;
            }
        }
    }
}
