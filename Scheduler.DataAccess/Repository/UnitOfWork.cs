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
            Location = new LocationRepository(_db);
            Patient = new PatientRepository(_db);
            OrderCatalog = new OrderCatalogRepository(_db);
            ApplicationUser = new ApplicationUserRepository(_db);
            ProviderScheduleProfile = new ProviderScheduleProfileRepository(_db);
            Insurance = new InsuranceRepository(_db);
            Encounter = new EncounterRepository(_db);
            SchAppt = new SchApptRepository(_db);
            FinancialNumAlias = new FinancialNumAliasRepository(_db);
            Document = new DocumentRepository(_db);
            Order = new OrderRepository(_db);
        }

        public ICodeSetRepository CodeSet { get; private set; }

        public ICodeValueRepository CodeValue { get; private set; }

        public ILocationRepository Location { get; private set; }

        public IPatientRepository Patient { get; private set; }

        public IOrderCatalogRepository OrderCatalog { get; private set; }

        public IApplicationUserRepository ApplicationUser { get; private set; }

        public IProviderScheduleProfileRepository ProviderScheduleProfile { get; private set; }
        public IInsuranceRepository Insurance { get; private set; }

        public IEncounterRepository Encounter { get; private set; }
        
        public ISchApptRepository SchAppt { get; private set; }

        public IFinancialNumAliasRepository FinancialNumAlias { get; private set; }

        public IDocumentRepository Document { get; private set; }

        public IOrderRepository Order { get; set; }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
