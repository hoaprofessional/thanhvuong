namespace WebCore.Services.Share.Admins.Users
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Dto;
    using Framework.Models.UserManagement;
    using Framework.Repositories.UserManagement;
    using Framework.Services;
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using WebCore.Services.Impl.Commons;
    using WebCore.Services.Share.Commons.Permissions;
    using WebCore.Utils.CollectionHelper;
    using WebCore.Utils.FilterHelper;
    using WebCore.Utils.ModelHelper;

    public interface IUserService
    {
        Task<bool> Add(UserInfoInput addInput);
        Task<bool> UpdateInfo(UserInfoInput updateInput);
        Task<bool> Delete(EntityId<string> entityId);
        Task<bool> SetActiveAsync(EntityId<string> entityId, bool active);
        PagingResultDto<UserDto> GetAllByPaging(UserFilterInput filterInput);
        ApplicationUser GetById(EntityId<string> entityId);
        UserInfoInput GetInputById(EntityId<string> entityId);
        UserResetPasswordInput GetResetPasswordInputById(EntityId<string> entityId);
        Task<string[]> GetAllClaimsAsync(EntityId<string> userId);
        Task<List<RoleDto>> GetAllRolesAsync(EntityId<string> userId);

        Task<bool> UpdatePermissionsAsync(AssignPermissionInput assignPermissionInput);
    }

    public class UserService : BaseWebService, IUserService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IApplicationUserRepository userRepository;
        private readonly IIdentityRoleRepository roleRepository;
        private readonly IMapper mapper;
        private readonly IPermissionService permissionService;



        public UserService(IServiceProvider serviceProvider, UserManager<ApplicationUser> userManager, IApplicationUserRepository userRepository,
            IPermissionService permissionService,
            IIdentityRoleRepository roleRepository, IMapper mapper)
            : base(serviceProvider)
        {
            this.userManager = userManager;
            this.userRepository = userRepository;
            this.roleRepository = roleRepository;
            this.permissionService = permissionService;
            this.mapper = mapper;
        }

        public async Task<bool> Add(UserInfoInput addInput)
        {
            ApplicationUser entity = mapper.Map<ApplicationUser>(addInput);

            SetAuditForInsert(entity);

            IdentityResult result = await userManager.CreateAsync(entity);
            return result.Succeeded;
        }

        public async Task<bool> Delete(EntityId<string> entityId)
        {
            ApplicationUser entity = userRepository.GetSingleById(entityId.Id);

            SetAuditForDelete(entity);

            IdentityResult result = await userManager.UpdateAsync(entity);
            return result.Succeeded;
        }

        public async Task<bool> SetActiveAsync(EntityId<string> entityId, bool active)
        {
            ApplicationUser entity = userRepository.GetSingleById(entityId.Id);

            SetAuditForUpdate(entity);
            entity.Active = active;

            IdentityResult result = await userManager.UpdateAsync(entity);
            return result.Succeeded;
        }

        public PagingResultDto<UserDto> GetAllByPaging(UserFilterInput filterInput)
        {
            SetDefaultPageSize(filterInput);

            IQueryable<ApplicationUser> userQuery = userRepository.GetAll();

            userQuery = userQuery.Filter(filterInput);

            PagingResultDto<UserDto> userResult = userQuery
                .ProjectTo<UserDto>(mapper.ConfigurationProvider)
                .PagedQuery(filterInput);

            return userResult;
        }


        public async Task<bool> UpdateInfo(UserInfoInput updateInput)
        {
            ApplicationUser entity = userRepository.GetSingleById(updateInput.Id);

            if (entity == null)
            {
                return false;
            }

            mapper.Map(updateInput, entity);

            SetAuditForUpdate(entity);
            IdentityResult result = await userManager.UpdateAsync(entity);

            return result.Succeeded;
        }

        public UserInfoInput GetInputById(EntityId<string> entityId)
        {
            ApplicationUser entity = userRepository.GetSingleById(entityId.Id);

            UserInfoInput updateInput = new UserInfoInput();

            if (entity == null)
            {
                return null;
            }

            updateInput = mapper.Map<UserInfoInput>(entity);

            return updateInput;
        }

        public UserResetPasswordInput GetResetPasswordInputById(EntityId<string> entityId)
        {
            ApplicationUser entity = userRepository.GetSingleById(entityId.Id);

            UserResetPasswordInput updateInput = new UserResetPasswordInput();

            if (entity == null)
            {
                return null;
            }

            updateInput = mapper.Map<UserResetPasswordInput>(entity);

            return updateInput;
        }


        public ApplicationUser GetById(EntityId<string> entityId)
        {
            return userRepository.GetSingleById(entityId.Id);
        }

        public async Task<string[]> GetAllClaimsAsync(EntityId<string> userId)
        {
            ApplicationUser user = await userManager.FindByIdAsync(userId.Id);
            IList<Claim> claims = await userManager.GetClaimsAsync(user);
            return claims.Select(x => x.Value).ToArray();
        }

        public async Task<List<RoleDto>> GetAllRolesAsync(EntityId<string> userId)
        {
            List<RoleDto> roles = roleRepository.GetAll().Select(x => new RoleDto()
            {
                RoleName = x.Name,
                IsChecked = false
            }).ToList();

            ApplicationUser user = userRepository.GetSingleById(userId.Id);
            IList<string> allRolesOfUser = await userManager.GetRolesAsync(user);

            foreach (RoleDto role in roles)
            {
                if (allRolesOfUser.Any(x => x == role.RoleName))
                {
                    role.IsChecked = true;
                }
            }

            return roles;
        }

        public async Task<bool> UpdatePermissionsAsync(AssignPermissionInput assignPermissionInput)
        {
            ApplicationUser user = userRepository.GetSingleById(assignPermissionInput.UserId);
            string[] viewRoles = assignPermissionInput.Roles;

            EntityId<string> userIdModel = new EntityId<string>() { Id = assignPermissionInput.UserId };

            List<RoleDto> allRoles = await GetAllRolesAsync(userIdModel);

            string[] allUserRoles = allRoles.Where(x => x.IsChecked).Select(x => x.RoleName).ToArray();


            HashSet<string> allClaims = permissionService.GetAllPermissions();
            IList<Claim> allClaimsOfUser = await userManager.GetClaimsAsync(user);
            string[] viewClaims = assignPermissionInput.Permissions;

            if (viewRoles.Any(vr => allRoles.Count(r => r.RoleName == vr) == 0))
            {
                return false;
            }

            if (viewClaims.Any(vc => allClaims.Count(ac => ac == vc) == 0))
            {
                return false;
            }

            // roles need delete
            List<string> rolesNeedDelete = allUserRoles
                                .Where(ur => viewRoles.Count(vr => vr == ur) == 0)
                                .ToList();

            // roles need add
            List<string> rolesNeedAdd = viewRoles
                                .Where(vr => allUserRoles.Count(ur => ur == vr) == 0)
                                .ToList();

            foreach (string roleName in rolesNeedDelete)
            {
                await userManager.RemoveFromRoleAsync(user, roleName);
            }

            foreach (string roleName in rolesNeedAdd)
            {
                await userManager.AddToRoleAsync(user, roleName);
            }



            // claims need delete
            IList<Claim> claimsNeedDelete = allClaimsOfUser
                                            .Where(uc => viewClaims.Count(vc => vc == uc.Value) == 0)
                                            .ToList();

            // claims need add
            List<string> claimsNeedAdd = viewClaims
                                            .Where(vc => allClaimsOfUser.Count(uc => vc == uc.Value) == 0)
                                            .ToList();


            foreach (string claim in claimsNeedAdd)
            {
                await userManager.AddClaimAsync(user, new Claim("Permission", claim));
            }

            foreach (var claim in claimsNeedDelete)
            {
                await userManager.RemoveClaimAsync(user, claim);
            }

            return true;
        }
    }
}
