using Scheduler.DataAccess.Repository.IRepository;
using Scheduler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.DataAccess.Repository
{
    public class LocationRepository : RepositoryAsync<Location>, ILocationRepository
    {
        private ApplicationDbContext _db;

        public LocationRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(Location obj)
        {
            //Another way to update class to update specific properties
            var locationInDb = _db.Locations.FirstOrDefault(l => l.Id == obj.Id);
            if (locationInDb != null)
            {
                locationInDb.Name = obj.Name;
                locationInDb.Street = obj.Street;
                locationInDb.City = obj.City;
                locationInDb.State = obj.State;
                locationInDb.Zip = obj.Zip;
                locationInDb.IsDeleted = obj.IsDeleted;
            }
        }
    }
}
