using Scheduler.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            CodeSet = new CodeSetRepository(_db);
            CodeValue = new CodeValueRepository(_db);
        }

        public ICodeSetRepository CodeSet { get; private set; }

        public ICodeValueRepository CodeValue { get; private set; }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
