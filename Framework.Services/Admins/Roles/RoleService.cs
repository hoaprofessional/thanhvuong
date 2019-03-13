namespace WebCore.Services.Share.Admins.Roles
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Dto;
    using Framework.Repositories.Infrastructor;
    using Framework.Repositories.UserManagement;
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using WebCore.Services.Impl.Commons;
    using WebCore.Utils.CollectionHelper;
    using WebCore.Utils.FilterHelper;
    using WebCore.Utils.ModelHelper;

    public interface IRoleService
    {
        Task<bool> Add(RoleInput addInput);
        Task<bool> UpdateInfoAsync(RoleInput updateInput);
        Task<bool> Delete(EntityId<string> entityId);
        Task<bool> Active(EntityId<string> entityId);
        PagingResultDto<RoleDto> GetAllByPaging(RoleFilterInput filterInput);
        IdentityRole GetById(EntityId<string> entityId);
        RoleInput GetInputById(EntityId<string> entityId);
        Task<string[]> GetAllClaimsAsync(EntityId<string> idModel);
        Task UpdateClaimsAsync(EntityId<string> roleId, List<string> permissions);
    }

    public class RoleService : BaseWebService, IRoleService
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IIdentityRoleRepository roleRepository;
        private readonly IIdentityRoleClaimRepository roleClaimRepository;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public RoleService(IServiceProvider serviceProvider,
            RoleManager<IdentityRole> roleManager,
            IIdentityRoleRepository roleRepository,
            IUnitOfWork unitOfWork,
            IIdentityRoleClaimRepository roleClaimRepository, IMapper mapper)
            : base(serviceProvider)
        {
            this.roleManager = roleManager;
            this.roleRepository = roleRepository;
            this.mapper = mapper;
            this.roleClaimRepository = roleClaimRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<bool> Add(RoleInput addInput)
        {
            IdentityRole entity = mapper.Map<IdentityRole>(addInput);

            IdentityResult result = await roleManager.CreateAsync(entity);
            return result.Succeeded;
        }

        public async Task<bool> Active(EntityId<string> entityId)
        {
            IdentityRole entity = roleRepository.GetSingleById(entityId.Id);

            IdentityResult result = await roleManager.UpdateAsync(entity);

            return result.Succeeded;
        }

        public async Task<bool> Delete(EntityId<string> entityId)
        {
            IdentityRole entity = roleRepository.GetSingleById(entityId.Id);

            IdentityResult result = await roleManager.UpdateAsync(entity);
            return result.Succeeded;
        }

        public PagingResultDto<RoleDto> GetAllByPaging(RoleFilterInput filterInput)
        {
            SetDefaultPageSize(filterInput);

            IQueryable<IdentityRole> roleQuery = roleRepository.GetAll();

            roleQuery = roleQuery.Filter(filterInput);

            PagingResultDto<RoleDto> roleResult = roleQuery
                .ProjectTo<RoleDto>(mapper.ConfigurationProvider)
                .PagedQuery(filterInput);

            return roleResult;
        }


        public async Task<bool> UpdateInfoAsync(RoleInput updateInput)
        {
            IdentityRole entity = roleRepository.GetSingleById(updateInput.Id);

            if (entity == null)
            {
                return false;
            }

            mapper.Map(updateInput, entity);

            IdentityResult result = await roleManager.UpdateAsync(entity);

            return result.Succeeded;
        }

        public RoleInput GetInputById(EntityId<string> entityId)
        {
            IdentityRole entity = roleRepository.GetSingleById(entityId.Id);

            RoleInput updateInput = new RoleInput();

            if (entity == null)
            {
                return null;
            }

            updateInput = mapper.Map<RoleInput>(entity);

            return updateInput;
        }

        public IdentityRole GetById(EntityId<string> entityId)
        {
            return roleRepository.GetSingleById(entityId.Id);
        }

        public async Task<string[]> GetAllClaimsAsync(EntityId<string> roleId)
        {
            IdentityRole role = await roleManager.FindByIdAsync(roleId.Id);
            IList<Claim> claims = await roleManager.GetClaimsAsync(role);
            return claims.Select(x => x.Value).ToArray();
        }

        public async Task UpdateClaimsAsync(EntityId<string> roleId, List<string> permissions)
        {
            try
            {
                IdentityRole role = await roleManager.FindByIdAsync(roleId.Id);
                IList<Claim> databaseClaims = await roleManager.GetClaimsAsync(role);

                // Quyền cần xóa : những quyền trong database không nằm trong danh sách quyền từ client gửi lên
                IList<Claim> deleteClaims = databaseClaims.Where(x => !permissions.Contains(x.Value)).ToList();

                //Quyền cần thêm : những quyền ở client mà không có trong danh sách quyền của database
                string[] addClaims = permissions.Where(clientClaim => !databaseClaims.Any(dbClaim => dbClaim.Value == clientClaim)).ToArray();

                //Thực hiện xóa quyền
                foreach (Claim claim in deleteClaims)
                {
                    await roleManager.RemoveClaimAsync(role, claim);
                }

                //Thực hiện thêm quyền
                foreach (string claim in addClaims)
                {
                    await roleManager.AddClaimAsync(role, new Claim("Permission", claim));
                }
            }
            catch (Exception e)
            {
                throw;
            }

        }
    }
}
