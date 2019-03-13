using Framework.Context;
using Framework.Models;
using Microsoft.AspNetCore.Identity;

namespace Framework.Repositories.UserManagement
{
    public interface IIdentityRoleRepository : IRepository<IdentityRole>
    {

    }
    public class IdentityRoleRepository : BaseRepository<IdentityRole>, IIdentityRoleRepository
    {
        public IdentityRoleRepository(FrameworkDbContext dbContext) :
            base(dbContext)
        {
        }
    }
}