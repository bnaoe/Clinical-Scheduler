using Scheduler.DataAccess.Repository.IRepository;
using Scheduler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.DataAccess.Repository
{
    public class OrderCatalogRepository : RepositoryAsync<OrderCatalog>, IOrderCatalogRepository
    {
        private ApplicationDbContext _db;

        public OrderCatalogRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(OrderCatalog obj)
        {
            //Another way to update class to update specific properties
            var orderCatalogInDb = _db.OrderCatalogs.FirstOrDefault(o => o.Id == obj.Id);
            if (orderCatalogInDb != null)
            {
                orderCatalogInDb.Name = obj.Name;
                orderCatalogInDb.Description = obj.Description;
                orderCatalogInDb.CodeValueId = obj.CodeValueId;
                orderCatalogInDb.IsDeleted = obj.IsDeleted;
            }
        }
    }
}
