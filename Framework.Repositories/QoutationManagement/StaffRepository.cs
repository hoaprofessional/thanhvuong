using Framework.Context;
using Framework.Models.QoutationManagement;
using System;
namespace Framework.Repositories.QoutationManagement
{
    public interface IStaffRepository : IRepository<Staff>
    {
        
    }
    public class StaffRepository : BaseRepository<Staff>, IStaffRepository
    {
        public StaffRepository(FrameworkDbContext dbContext) :
            base(dbContext)
        {
        }

        public override Staff Add(Staff entity)
        {
            var ticks = new DateTime(2016, 1, 1).Ticks;
            var ans = DateTime.Now.Ticks - ticks;
            entity.Id = ans.ToString("x");
            entity.CreationUserName = GetLoginedUserName();
            entity.CreationTime = DateTime.Now;
            entity.Active = true;
            return base.Add(entity);
        }
        public override void Update(Staff entity)
        {
            entity.ModifiedUserName = GetLoginedUserName();
            entity.ModifiedTime = DateTime.Now;
            base.Update(entity);
        }
    }
}