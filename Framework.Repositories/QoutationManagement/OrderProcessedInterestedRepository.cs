using Framework.Context;
using Framework.Models.QoutationManagement;
using System;
namespace Framework.Repositories.QoutationManagement
{
    public interface IOrderProcessedInterestedRepository : IRepository<OrderProcessedInterested>
    {

    }
    public class OrderProcessedInterestedRepository : BaseRepository<OrderProcessedInterested>, IOrderProcessedInterestedRepository
    {
        public OrderProcessedInterestedRepository(FrameworkDbContext dbContext) :
            base(dbContext)
        {
        }

        public override OrderProcessedInterested Add(OrderProcessedInterested entity)
        {
            entity.Id = GenerateUniqueId();
            entity.CreationUserName = GetLoginedUserName();
            entity.CreationTime = DateTime.Now;
            entity.Active = true;
            return base.Add(entity);
        }
        public override void Update(OrderProcessedInterested entity)
        {
            entity.ModifiedUserName = GetLoginedUserName();
            entity.ModifiedTime = DateTime.Now;
            base.Update(entity);
        }
    }
}