using Scheduler.DataAccess.Repository.IRepository;
using Scheduler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.DataAccess.Repository
{
    public class FinancialNumAliasRepository : RepositoryAsync<FinancialNumAlias>, IFinancialNumAliasRepository
    {
        private ApplicationDbContext _db;

        public FinancialNumAliasRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(FinancialNumAlias obj)
        {
            //Another way to update class to update specific properties
            var FinancialNumAliasInDb = _db.FinancialNumAliases.FirstOrDefault(f => f.Id == obj.Id);
            //anything to do here?
        }
    }
}
