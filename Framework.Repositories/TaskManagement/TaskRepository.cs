using Framework.Context;
using Framework.Models.TaskManagement;
using Framework.Repositories.Utils;
using System;
using System.Linq.Expressions;

namespace Framework.Repositories.TaskManagement
{
    public interface ITaskRepository : IRepository<Task>
    {
        
    }
    public class TaskRepository : BaseRepository<Task>, ITaskRepository
    {
        ILoggerRepository loggerRepository;
        public TaskRepository(FrameworkDbContext dbContext, ILoggerRepository loggerRepository) :
            base(dbContext)
        {
            this.loggerRepository = loggerRepository;
        }

        public override Task Add(Task entity)
        {
            entity.Id = GenerateUniqueId();
            entity.CreationUserName = GetLoginedUserName();
            entity.CreationTime = DateTime.Now;
            entity.ModifiedTime = entity.CreationTime;
            entity.Active = true;
            return base.Add(entity);
        }
        public override void Update(Task entity)
        {
            entity.ModifiedUserName = GetLoginedUserName();
            entity.ModifiedTime = DateTime.Now;
            base.Update(entity);
        }
    }
}