using Framework.Context;
using Framework.Models.UserManagement;

namespace Framework.Repositories.UserManagement
{
    public interface IApplicationUserRepository : IRepository<ApplicationUser>
    {
        string GetLoginedUserName();
        FrameworkDbContext DbContext { get; }
    }
    public class ApplicationUserRepository : BaseRepository<ApplicationUser>, IApplicationUserRepository
    {
        public ApplicationUserRepository(FrameworkDbContext dbContext) :
            base(dbContext)
        {
        }
    }
}