using Framework.Context;
using Framework.Models.QoutationManagement;
using System;
namespace Framework.Repositories.QoutationManagement
{
    public interface IOrderDetailRepository : IRepository<OrderDetail>
    {
        
    }
    public class OrderDetailRepository : BaseRepository<OrderDetail>, IOrderDetailRepository
    {
        public OrderDetailRepository(FrameworkDbContext dbContext) :
            base(dbContext)
        {
        }

        public override OrderDetail Add(OrderDetail entity)
        {
            entity.Id = GenerateUniqueId();
            entity.CreationUserName = GetLoginedUserName();
            entity.CreationTime = DateTime.Now;
            entity.Active = true;
            return base.Add(entity);
        }
        public override void Update(OrderDetail entity)
        {
            entity.ModifiedUserName = GetLoginedUserName();
            entity.ModifiedTime = DateTime.Now;
            base.Update(entity);
        }
    }
}