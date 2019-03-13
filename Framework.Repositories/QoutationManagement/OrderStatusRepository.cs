using Framework.Context;
using Framework.Models.QoutationManagement;
using System;
namespace Framework.Repositories.QoutationManagement
{
    public interface IOrderStatusRepository : IRepository<OrderStatus>
    {
        
    }
    public class OrderStatusRepository : BaseRepository<OrderStatus>, IOrderStatusRepository
    {
        public OrderStatusRepository(FrameworkDbContext dbContext) :
            base(dbContext)
        {

        }

        public override OrderStatus Add(OrderStatus entity)
        {
            entity.CreationUserName = GetLoginedUserName();
            entity.CreationTime = DateTime.Now;
            entity.Active = true;
            return base.Add(entity);
        }
        public override void Update(OrderStatus entity)
        {
            entity.ModifiedUserName = GetLoginedUserName();
            entity.ModifiedTime = DateTime.Now;
            base.Update(entity);
        }
    }
}