using Scheduler.DataAccess.Repository.IRepository;
using Scheduler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.DataAccess.Repository
{
    public class DxCodeRepository : RepositoryAsync<DxCode>, IDxCodeRepository
    {
        private ApplicationDbContext _db;

        public DxCodeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(DxCode obj)
        {
            _db.DxCodes.Update(obj);

        }
    }
}
