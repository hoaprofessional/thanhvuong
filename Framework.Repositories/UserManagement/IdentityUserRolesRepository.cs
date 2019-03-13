using Framework.Context;
using Framework.Models;
using Microsoft.AspNetCore.Identity;
using System;

namespace Framework.Repositories.UserManagement
{
    public interface IIdentityUserRolesRepository : IRepository<IdentityUserRole<String>>
    {
        
    }
    public class IdentityUserRolesRepository : BaseRepository<IdentityUserRole<String>>, IIdentityUserRolesRepository
    {
        public IdentityUserRolesRepository(FrameworkDbContext dbContext) :
            base(dbContext)
        {
        }
    }
}