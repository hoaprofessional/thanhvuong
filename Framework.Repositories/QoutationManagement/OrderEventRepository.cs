using Framework.Context;
using Framework.Models.QoutationManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Repositories.QoutationManagement
{
    public interface IOrderEventRepository : IRepository<OrderEvent>
    {

    }
    public class OrderEventRepository : BaseRepository<OrderEvent>, IOrderEventRepository
    {
        public OrderEventRepository(FrameworkDbContext dbContext) :
            base(dbContext)
        {
        }

        public override OrderEvent Add(OrderEvent entity)
        {
            entity.Id = GenerateUniqueId();
            entity.CreationUserName = GetLoginedUserName();
            entity.CreationTime = DateTime.Now;
            entity.Active = true;
            return base.Add(entity);
        }
        public override void Update(OrderEvent entity)
        {
            entity.ModifiedUserName = GetLoginedUserName();
            entity.ModifiedTime = DateTime.Now;
            base.Update(entity);
        }
    }
}
