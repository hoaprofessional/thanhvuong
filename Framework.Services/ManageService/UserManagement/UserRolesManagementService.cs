using Framework.Context;
using Framework.DTOs.Areas.Management.UserRolesManagement;
using Framework.Models.UserManagement;
using Framework.Repositories.UserManagement;
using Framework.Utils;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.Services.ManageService.UserManagement
{
    public interface IUserRolesManagementService
    {
        List<UserDto> GetUsers();
        List<RoleDto> GetRoles();
        ApplicationUser GetUserById(string userId);

    }
    public class UserRolesManagementService : IUserRolesManagementService
    {

        readonly IIdentityUserRolesRepository identityUserRolesRepository;
        readonly IApplicationUserRepository applicationUserRepository;
        readonly IIdentityRoleRepository identityRoleRepository;

        public UserRolesManagementService(IIdentityUserRolesRepository identityUserRolesRepository,
            IApplicationUserRepository applicationUserRepository,
            IIdentityRoleRepository identityRoleRepository)
        {
            this.identityUserRolesRepository = identityUserRolesRepository;
            this.applicationUserRepository = applicationUserRepository;
            this.identityRoleRepository = identityRoleRepository;
        }
        public List<RoleDto> GetRoles()
        {
            return identityRoleRepository.GetMultiBySelectClassType<RoleDto>(x => true).ToList();
        }

        private List<UserRolesDto> GetUserRoles()
        {
            IQueryable<ApplicationUser> users = applicationUserRepository.GetMulti(x => x.Active == true);
            IQueryable<IdentityRole> roles = identityRoleRepository.GetAll();
            IQueryable<IdentityUserRole<String>> userRoles = identityUserRolesRepository.GetAll();

            var query = users
                    .Join(userRoles,
                                    (user => user.Id),
                                    (userRole => userRole.UserId),
                                    ExpressionHelper.JoinSelectResulExpression<ApplicationUser, IdentityUserRole<String>, UserRolesDto>())
                    .Join(roles,
                                    (userRole => userRole.RoleId),
                                    (role => role.Id),
                                    ExpressionHelper.JoinSelectResulExpression<UserRolesDto, IdentityRole>());

            return query.ToList();

        }

        public List<UserDto> GetUsers()
        {
            //get users
            var users = applicationUserRepository.GetMulti(x => x.Active == true).Select<ApplicationUser, UserDto>().ToList();
            //get roles
            var roles = GetRoles();
            //get user roles
            var userRoles = GetUserRoles();
            //set role per user
            foreach (var user in users)
            {
                user.Roles = userRoles.Where(x => x.UserId == user.Id).Select(x => x.RoleName).ToList();
            }

            return users;
        }

        public ApplicationUser GetUserById(string userId)
        {
            return applicationUserRepository.GetSingleByCondition(x => x.Id == userId);
        }
    }
}
