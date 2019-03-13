using Framework.Context;
using Framework.Models.TaskManagement;
using System;
namespace Framework.Repositories.TaskManagement
{
    public interface ITaskStatusRepository : IRepository<TaskStatus>
    {
        
    }
    public class TaskStatusRepository : BaseRepository<TaskStatus>, ITaskStatusRepository
    {
        public TaskStatusRepository(FrameworkDbContext dbContext) :
            base(dbContext)
        {
        }

        public override TaskStatus Add(TaskStatus entity)
        {
            entity.Id = GenerateUniqueId();
            entity.CreationUserName = GetLoginedUserName();
            entity.CreationTime = DateTime.Now;
            entity.ModifiedTime = entity.CreationTime;
            entity.Active = true;
            return base.Add(entity);
        }
        public override void Update(TaskStatus entity)
        {
            entity.ModifiedUserName = GetLoginedUserName();
            entity.ModifiedTime = DateTime.Now;
            base.Update(entity);
        }
    }
}