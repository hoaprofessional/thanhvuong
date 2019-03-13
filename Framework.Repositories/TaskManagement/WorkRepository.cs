using Framework.Context;
using Framework.Models.TaskManagement;
using Framework.Repositories.Utils;
using System;
namespace Framework.Repositories.TaskManagement
{
    public interface IWorkRepository : IRepository<Work>
    {
        
    }
    public class WorkRepository : BaseRepository<Work>, IWorkRepository
    {
        ILoggerRepository loggerRepository;
        public WorkRepository(FrameworkDbContext dbContext, ILoggerRepository loggerRepository) :
            base(dbContext)
        {
            this.loggerRepository = loggerRepository;
        }

        public override Work Add(Work entity)
        {
            entity.Id = GenerateUniqueId();
            entity.CreationUserName = GetLoginedUserName();
            entity.CreationTime = DateTime.Now;
            entity.ModifiedTime = entity.CreationTime;
            entity.Active = true;
            return base.Add(entity);
        }
        public override void Update(Work entity)
        {
            entity.ModifiedUserName = GetLoginedUserName();
            entity.ModifiedTime = DateTime.Now;
            base.Update(entity);
        }
    }
}