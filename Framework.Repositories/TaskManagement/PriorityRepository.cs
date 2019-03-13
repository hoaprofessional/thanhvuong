using Framework.Context;
using Framework.Models.TaskManagement;
using System;
namespace Framework.Repositories.TaskManagement
{
    public interface IPriorityRepository : IRepository<Priority>
    {
        
    }
    public class PriorityRepository : BaseRepository<Priority>, IPriorityRepository
    {
        public PriorityRepository(FrameworkDbContext dbContext) :
            base(dbContext)
        {
        }

        public override Priority Add(Priority entity)
        {
            entity.Id = GenerateUniqueId();
            entity.CreationUserName = GetLoginedUserName();
            entity.CreationTime = DateTime.Now;
            entity.ModifiedTime = entity.CreationTime;
            entity.Active = true;
            return base.Add(entity);
        }
        public override void Update(Priority entity)
        {
            entity.ModifiedUserName = GetLoginedUserName();
            entity.ModifiedTime = DateTime.Now;
            base.Update(entity);
        }
    }
}