using Framework.Context;
using Framework.Models.QoutationManagement;
using System;
namespace Framework.Repositories.QoutationManagement
{
    public interface IQoutationStatusRepository : IRepository<QoutationStatus>
    {
        
    }
    public class QoutationStatusRepository : BaseRepository<QoutationStatus>, IQoutationStatusRepository
    {
        public QoutationStatusRepository(FrameworkDbContext dbContext) :
            base(dbContext)
        {
        }

        public override QoutationStatus Add(QoutationStatus entity)
        {
            entity.CreationUserName = GetLoginedUserName();
            entity.CreationTime = DateTime.Now;
            entity.Active = true;
            return base.Add(entity);
        }
        public override void Update(QoutationStatus entity)
        {
            entity.ModifiedUserName = GetLoginedUserName();
            entity.ModifiedTime = DateTime.Now;
            base.Update(entity);
        }
    }
}