using Scheduler.DataAccess.Repository.IRepository;
using Scheduler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.DataAccess.Repository
{
    public class InsuranceRepository : RepositoryAsync<Insurance>, IInsuranceRepository
    {
        private ApplicationDbContext _db;

        public InsuranceRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(Insurance obj)
        {
            _db.Insurances.Update(obj);
        }
    }
}
