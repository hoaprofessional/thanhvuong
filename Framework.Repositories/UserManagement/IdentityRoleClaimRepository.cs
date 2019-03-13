using Framework.Context;
using Framework.Models;
using Microsoft.AspNetCore.Identity;

namespace Framework.Repositories.UserManagement
{
    public interface IIdentityRoleClaimRepository : IRepository<IdentityRoleClaim<string>>
    {

    }
    public class IdentityRoleClaimRepository : BaseRepository<IdentityRoleClaim<string>>, IIdentityRoleClaimRepository
    {
        public IdentityRoleClaimRepository(FrameworkDbContext dbContext) :
            base(dbContext)
        {
        }
    }
}