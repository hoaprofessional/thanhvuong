using Framework.Context;
using Framework.Models.TaskManagement;
using Framework.Repositories.Utils;
using System;
using System.Linq.Expressions;

namespace Framework.Repositories.TaskManagement
{
    public interface IAssignWorkUserRepository : IRepository<AssignWorkUser>
    {
        
    }
    public class AssignWorkUserRepository : BaseRepository<AssignWorkUser>, IAssignWorkUserRepository
    {
        public AssignWorkUserRepository(FrameworkDbContext dbContext) :
            base(dbContext)
        {
        }

        public override AssignWorkUser Add(AssignWorkUser entity)
        {
            entity.Id = GenerateUniqueId();
            entity.CreationUserName = GetLoginedUserName();
            entity.CreationTime = DateTime.Now;
            entity.ModifiedTime = entity.CreationTime;
            entity.Active = true;
            return base.Add(entity);
        }
        public override void Update(AssignWorkUser entity)
        {
            entity.ModifiedUserName = GetLoginedUserName();
            entity.ModifiedTime = DateTime.Now;
            base.Update(entity);
        }
    }
}