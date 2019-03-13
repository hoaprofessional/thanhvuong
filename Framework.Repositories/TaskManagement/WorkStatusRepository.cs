using Framework.Context;
using Framework.Models.TaskManagement;
using System;
namespace Framework.Repositories.TaskManagement
{
    public interface IWorkStatusRepository : IRepository<WorkStatus>
    {
        
    }
    public class WorkStatusRepository : BaseRepository<WorkStatus>, IWorkStatusRepository
    {
        public WorkStatusRepository(FrameworkDbContext dbContext) :
            base(dbContext)
        {
        }

        public override WorkStatus Add(WorkStatus entity)
        {
            entity.Id = GenerateUniqueId();
            entity.CreationUserName = GetLoginedUserName();
            entity.CreationTime = DateTime.Now;
            entity.ModifiedTime = entity.CreationTime;
            entity.Active = true;
            return base.Add(entity);
        }
        public override void Update(WorkStatus entity)
        {
            entity.ModifiedUserName = GetLoginedUserName();
            entity.ModifiedTime = DateTime.Now;
            base.Update(entity);
        }
    }
}