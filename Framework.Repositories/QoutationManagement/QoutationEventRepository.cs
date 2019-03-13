using Framework.Context;
using Framework.Models.QoutationManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Repositories.QoutationManagement
{
    public interface IQoutationEventRepository : IRepository<QoutationEvent>
    {

    }
    public class QoutationEventRepository : BaseRepository<QoutationEvent>, IQoutationEventRepository
    {
        public QoutationEventRepository(FrameworkDbContext dbContext) :
            base(dbContext)
        {
        }

        public override QoutationEvent Add(QoutationEvent entity)
        {
            entity.Id = GenerateUniqueId();
            entity.CreationUserName = GetLoginedUserName();
            entity.CreationTime = DateTime.Now;
            entity.Active = true;
            return base.Add(entity);
        }
        public override void Update(QoutationEvent entity)
        {
            entity.ModifiedUserName = GetLoginedUserName();
            entity.ModifiedTime = DateTime.Now;
            base.Update(entity);
        }
    }
}
