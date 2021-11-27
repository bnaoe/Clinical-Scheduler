using Scheduler.DataAccess.Repository.IRepository;
using Scheduler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.DataAccess.Repository
{
    public class CodeValueRepository : Repository<CodeValue>, ICodeValueRepository
    {
        private ApplicationDbContext _db;

        public CodeValueRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(CodeValue codeValue)
        {
            //Another way to update class to update specific properties
            var codeValueInDb = _db.CodeValues.FirstOrDefault(c => c.Id == codeValue.Id);
            if (codeValueInDb != null)
            {
                codeValue.Name = codeValueInDb.Name;
                codeValue.Description = codeValueInDb.Description;
            }
        }
    }
}
