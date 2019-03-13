using Framework.Context;
using Framework.Models.QoutationManagement;
using System;
namespace Framework.Repositories.QoutationManagement
{
    public interface IQoutationDetailRepository : IRepository<QoutationDetail>
    {
        
    }
    public class QoutationDetailRepository : BaseRepository<QoutationDetail>, IQoutationDetailRepository
    {
        public QoutationDetailRepository(FrameworkDbContext dbContext) :
            base(dbContext)
        {
        }

        public override QoutationDetail Add(QoutationDetail entity)
        {
            entity.Id = GenerateUniqueId();
            entity.CreationUserName = GetLoginedUserName();
            entity.CreationTime = DateTime.Now;
            entity.Active = true;
            return base.Add(entity);
        }
        public override void Update(QoutationDetail entity)
        {
            entity.ModifiedUserName = GetLoginedUserName();
            entity.ModifiedTime = DateTime.Now;
            base.Update(entity);
        }
    }
}