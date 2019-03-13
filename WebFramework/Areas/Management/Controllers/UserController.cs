using Framework.Models.UserManagement;
using Framework.Repositories.Infrastructor;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WebCore.Services.Share.Admins.Users;
using WebCore.Services.Share.Admins.Users.Dto;
using WebCore.Services.Share.Commons.Permissions;
using WebCore.Utils.Config;
using WebCore.Utils.ModelHelper;
using WebFramework.Areas.Management.Models;
using WebFramework.Infrastructor;

namespace WebFramework.Areas.Management.Controllers
{
    [Area("Management")]
    [ClaimRequirement(MyClaimType.Permission, PermissionValue.RegisterUser)]
    public class UserController : AdminBaseController
    {
        private readonly IUserService userService;
        private readonly IPermissionService permissionService;
        private readonly IUnitOfWork unitOfWork;
        private UserManager<ApplicationUser> userManager;

        public UserController(IServiceProvider serviceProvider, IUserService userService, IPermissionService permissionService, IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager) : base(serviceProvider)
        {
            this.userService = userService;
            this.unitOfWork = unitOfWork;
            this.permissionService = permissionService;
            this.userManager = userManager;
        }

        public IActionResult Index(int page = 1)
        {
            UserFilterInput filterInput = GetFilterInSession<UserFilterInput>(ConstantConfig.SessionName.UserSession);
            filterInput.PageNumber = page;
            UserViewModel userViewModel = new UserViewModel
            {
                FilterInput = filterInput,
                PagingResult = userService.GetAllByPaging(filterInput)
            };
            InitAdminBaseViewModel(userViewModel);
            return View(userViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult FilterPartial(UserFilterInput filterInput)
        {
            SetFilterToSession(ConstantConfig.SessionName.UserSession, filterInput);
            return RedirectToAction("Index", new { page = 1 });
        }

        [HttpGet]
        public IActionResult ResetPasswordPartial(EntityId<string> idModel = null)
        {
            UserResetPasswordInput input = null;
            if (idModel == null)
            {
                input = new UserResetPasswordInput();
            }
            else
            {
                input = userService.GetResetPasswordInputById(idModel);
            }
            return PartialView(input);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPasswordPartial(UserResetPasswordInput input = null)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(input);
            }
            ApplicationUser user = await userManager.FindByIdAsync(input.Id);
            var code = await userManager.GeneratePasswordResetTokenAsync(user);
            await userManager.ResetPasswordAsync(user, code, input.Password);
            return PartialView(input);
        }

        [HttpGet]
        [ClaimRequirement(MyClaimType.Permission, PermissionValue.UserManagement_AssignPermission)]
        public async Task<IActionResult> AssignPermissionPartial(EntityId<string> idModel = null)
        {
            ApplicationUser user = userService.GetById(idModel);
            UserAssignPermissionViewModel viewModel = new UserAssignPermissionViewModel();
            if (user == null)
            {
                return Forbid();
            }
            string[] allClaims = await userService.GetAllClaimsAsync(idModel);

            viewModel.TreeViewPermission = await permissionService.GetPermissionTreeViewAsync(allClaims);
            viewModel.AllRoles = await userService.GetAllRolesAsync(idModel);

            ViewBag.UserId = idModel.Id;

            return PartialView(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AssignPermissionPartial(AssignPermissionInput assignPermissionInput)
        {
            ApplicationUser user = userService.GetById(new EntityId<string>()
            {
                Id = assignPermissionInput.UserId
            });
            if (user == null)
            {
                return Ok(new { result = ConstantConfig.WebApiStatusCode.Error, message = "Có lỗi xảy ra" });
            }
            bool success = await userService.UpdatePermissionsAsync(assignPermissionInput);
            if (success)
            {
                return Ok(new { result = ConstantConfig.WebApiStatusCode.Success, message = "Thành công" });
            }
            else
            {
                return Ok(new { result = ConstantConfig.WebApiStatusCode.Error, message = "Có lỗi xảy ra" });
            }
        }

        [HttpGet]
        public IActionResult InputInfoPartial(EntityId<string> idModel = null)
        {
            UserInfoInput input = null;
            if (idModel == null)
            {
                input = new UserInfoInput();
            }
            else
            {
                input = userService.GetInputById(idModel);
            }

            return PartialView(input);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> InputInfoPartial(UserInfoInput inputModel)
        {
            if (inputModel.Id != null)
            {
                //update
                ApplicationUser lastInfo = userService.GetById(inputModel);
                await userService.UpdateInfo(inputModel);
                unitOfWork.Commit();
                return Ok(new { result = ConstantConfig.WebApiStatusCode.Success, message = "Cập nhật thành công" });
            }
            return Ok(new { result = ConstantConfig.WebApiStatusCode.Error, message = "Có lỗi xảy ra" });
        }

        [HttpPost]
        [ClaimRequirement(MyClaimType.Permission, PermissionValue.UserManagement_BlockUser)]
        public async Task<IActionResult> ActiveUser(EntityId<string> userId)
        {
            await userService.SetActiveAsync(userId, true);
            unitOfWork.Commit();
            return Ok();
        }

        [HttpPost]
        [ClaimRequirement(MyClaimType.Permission, PermissionValue.UserManagement_BlockUser)]
        public async Task<IActionResult> InActiveUser(EntityId<string> userId)
        {
            await userService.SetActiveAsync(userId, false);
            unitOfWork.Commit();
            return Ok();
        }

        public IActionResult MainListPartial()
        {
            UserFilterInput filterInput = GetFilterInSession<UserFilterInput>(ConstantConfig.SessionName.UserSession);
            PagingResultDto<UserDto> pagingResult = userService.GetAllByPaging(filterInput);
            return PartialView(pagingResult);
        }


    }
}