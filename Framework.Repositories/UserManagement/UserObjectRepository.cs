using Framework.Context;
using Framework.Models;
using Framework.Models.UserManagement;
using Microsoft.AspNetCore.Identity;
using System;

namespace Framework.Repositories.UserManagement
{
    public interface IUserObjectRepository : IRepository<UserObject>
    {
        
    }
    public class UserObjectRepository : BaseRepository<UserObject>, IUserObjectRepository
    {
        public UserObjectRepository(FrameworkDbContext dbContext) :
            base(dbContext)
        {
        }
    }
}