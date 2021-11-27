using Scheduler.DataAccess.Repository.IRepository;
using Scheduler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.DataAccess.Repository
{
    public class CodeSetRepository : Repository<CodeSet>, ICodeSetRepository
    {
        private ApplicationDbContext _db;

        public CodeSetRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(CodeSet codeSet)
        {
            _db.CodeSets.Update(codeSet);
        }
    }
}
