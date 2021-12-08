﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ICodeSetRepository CodeSet { get; }
        ICodeValueRepository CodeValue { get; }
        ILocationRepository Location { get; }
        IPatientRepository Patient { get; }
        IOrderCatalogRepository OrderCatalog { get; }
        IApplicationUserRepository ApplicationUser { get; }
        IProviderScheduleProfileRepository ProviderScheduleProfile { get; }

        void Save();
    }
}
