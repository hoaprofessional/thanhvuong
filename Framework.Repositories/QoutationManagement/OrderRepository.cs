using Framework.Context;
using Framework.Models.QoutationManagement;
using System;
namespace Framework.Repositories.QoutationManagement
{
    public interface IOrderRepository : IRepository<Order>
    {

    }
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(FrameworkDbContext dbContext) :
            base(dbContext)
        {
        }

        public override Order Add(Order entity)
        {
            entity.CreationUserName = GetLoginedUserName();
            entity.CreationTime = DateTime.Now;
            entity.ModifiedTime = entity.CreationTime;
            entity.Active = true;
            return base.Add(entity);
        }
        public override void Update(Order entity)
        {
            entity.ModifiedUserName = GetLoginedUserName();
            entity.ModifiedTime = DateTime.Now;
            base.Update(entity);
        }
    }
}