using Framework.Context;
using Framework.Models.QoutationManagement;
using System;
namespace Framework.Repositories.QoutationManagement
{
    public interface IOrderStatusWaitingApprovalInterestedRepository : IRepository<OrderStatusWaitingApprovalInterested>
    {

    }
    public class OrderStatusWaitingApprovalInterestedRepository : BaseRepository<OrderStatusWaitingApprovalInterested>, IOrderStatusWaitingApprovalInterestedRepository
    {
        public OrderStatusWaitingApprovalInterestedRepository(FrameworkDbContext dbContext) :
            base(dbContext)
        {
        }

        public override OrderStatusWaitingApprovalInterested Add(OrderStatusWaitingApprovalInterested entity)
        {
            entity.Id = GenerateUniqueId();
            entity.CreationUserName = GetLoginedUserName();
            entity.CreationTime = DateTime.Now;
            entity.Active = true;
            return base.Add(entity);
        }
        public override void Update(OrderStatusWaitingApprovalInterested entity)
        {
            entity.ModifiedUserName = GetLoginedUserName();
            entity.ModifiedTime = DateTime.Now;
            base.Update(entity);
        }
    }
}