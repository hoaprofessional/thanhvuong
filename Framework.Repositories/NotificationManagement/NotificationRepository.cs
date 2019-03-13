using Framework.Context;
using Framework.Models.NotificationManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Repositories.NotificationManagement
{
    public interface INotificationRepository : IRepository<Notification>
    {

    }
    public class NotificationRepository : BaseRepository<Notification>, INotificationRepository
    {
        public NotificationRepository(FrameworkDbContext dbContext) :
            base(dbContext)
        {
        }

        public override Notification Add(Notification entity)
        {
            entity.Id = GenerateUniqueId();
            entity.CreationUserName = GetLoginedUserName();
            entity.CreationTime = DateTime.Now;
            entity.Active = true;
            return base.Add(entity);
        }
        public override void Update(Notification entity)
        {
            entity.ModifiedUserName = GetLoginedUserName();
            entity.ModifiedTime = DateTime.Now;
            base.Update(entity);
        }
    }
}
