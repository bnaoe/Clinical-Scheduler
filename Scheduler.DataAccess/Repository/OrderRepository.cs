using Scheduler.DataAccess.Repository.IRepository;
using Scheduler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.DataAccess.Repository
{
    public class OrderRepository : RepositoryAsync<Order>, IOrderRepository
    {
        private ApplicationDbContext _db;

        public OrderRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(Order obj)
        {
            //Another way to update class to update specific properties
            var orderInDb = _db.Orders.FirstOrDefault(o => o.Id == obj.Id);
            if (orderInDb != null)
            {
                orderInDb.IsActive = obj.IsActive;
                orderInDb.Narrative = obj.Narrative;
                orderInDb.OrderCatalogId = obj.OrderCatalogId;
                orderInDb.OrderDetails = obj.OrderDetails;
                orderInDb.OrderingDtTm = obj.OrderingDtTm;
                orderInDb.OrderingUserId = obj.OrderingUserId;
                orderInDb.OrderStatusId = obj.OrderStatusId;
                orderInDb.OrderTypeId = obj.OrderTypeId;
                orderInDb.AdminRouteId = obj.AdminRouteId;
                orderInDb.AdminFreqId = obj.AdminFreqId;
                orderInDb.AdminTimeId = obj.AdminTimeId;
                orderInDb.EncounterId = obj.EncounterId;
                orderInDb.OrderingDtTm = obj.OrderingDtTm;
            }
        }
    }
}
