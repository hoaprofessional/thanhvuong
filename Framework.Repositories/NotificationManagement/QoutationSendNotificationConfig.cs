using Framework.Context;
using Framework.Models.NotificationManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Repositories.NotificationManagement
{
    public interface IQoutationSendNotificationConfigRepository : IRepository<QoutationSendNotificationConfig>
    {

    }
    public class QoutationSendNotificationConfigRepository : BaseRepository<QoutationSendNotificationConfig>, IQoutationSendNotificationConfigRepository
    {
        public QoutationSendNotificationConfigRepository(FrameworkDbContext dbContext) :
            base(dbContext)
        {
        }

        public override QoutationSendNotificationConfig Add(QoutationSendNotificationConfig entity)
        {
            entity.Id = GenerateUniqueId();
            entity.CreationUserName = GetLoginedUserName();
            entity.CreationTime = DateTime.Now;
            entity.Active = true;
            return base.Add(entity);
        }
        public override void Update(QoutationSendNotificationConfig entity)
        {
            entity.ModifiedUserName = GetLoginedUserName();
            entity.ModifiedTime = DateTime.Now;
            base.Update(entity);
        }
    }
}
