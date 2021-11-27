using Scheduler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.DataAccess.Repository.IRepository
{
    public interface ICodeValueRepository : IRepository<CodeValue>
    {
        void Update(CodeValue obj);

    }
}
