using Framework.Context;
using Framework.Models.QoutationManagement;
using System;
namespace Framework.Repositories.QoutationManagement
{
    public interface IOrderStatusWaitingProcessInterestedRepository : IRepository<OrderStatusWaitingProcessInterested>
    {

    }
    public class OrderStatusWaitingProcessInterestedRepository : BaseRepository<OrderStatusWaitingProcessInterested>, IOrderStatusWaitingProcessInterestedRepository
    {
        public OrderStatusWaitingProcessInterestedRepository(FrameworkDbContext dbContext) :
            base(dbContext)
        {
        }

        public override OrderStatusWaitingProcessInterested Add(OrderStatusWaitingProcessInterested entity)
        {
            entity.Id = GenerateUniqueId();
            entity.CreationUserName = GetLoginedUserName();
            entity.CreationTime = DateTime.Now;
            entity.Active = true;
            return base.Add(entity);
        }
        public override void Update(OrderStatusWaitingProcessInterested entity)
        {
            entity.ModifiedUserName = GetLoginedUserName();
            entity.ModifiedTime = DateTime.Now;
            base.Update(entity);
        }
    }
}