using Framework.Context;
using Framework.Models.QoutationManagement;
using System;
namespace Framework.Repositories.QoutationManagement
{
    public interface IQoutationRepository : IRepository<Qoutation>
    {

    }
    public class QoutationRepository : BaseRepository<Qoutation>, IQoutationRepository
    {
        public QoutationRepository(FrameworkDbContext dbContext) :
            base(dbContext)
        {
        }

        public override Qoutation Add(Qoutation entity)
        {
            entity.CreationUserName = GetLoginedUserName();
            entity.CreationTime = DateTime.Now;
            entity.ModifiedTime = entity.CreationTime;
            entity.Active = true;
            return base.Add(entity);
        }
        public override void Update(Qoutation entity)
        {
            entity.ModifiedUserName = GetLoginedUserName();
            entity.ModifiedTime = DateTime.Now;
            base.Update(entity);
        }
    }
}