using Scheduler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.DataAccess.Repository.IRepository
{
    public interface IEncounterRepository : IRepositoryAsync<Encounter>
    {
        void Update(Encounter obj);
    }
}
