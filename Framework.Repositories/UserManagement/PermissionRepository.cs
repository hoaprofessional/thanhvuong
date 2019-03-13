using Framework.Context;
using Framework.Models;
using Framework.Models.UserManagement;
using Microsoft.AspNetCore.Identity;
using System;

namespace Framework.Repositories.UserManagement
{
    public interface IPermissionRepository : IRepository<Permission>
    {
        
    }
    public class PermissionRepository : BaseRepository<Permission>, IPermissionRepository
    {
        public PermissionRepository(FrameworkDbContext dbContext) :
            base(dbContext)
        {
        }
    }
}