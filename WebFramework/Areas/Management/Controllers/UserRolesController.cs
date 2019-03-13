using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Framework.Models.UserManagement;
using Framework.Services.ManageService.UserManagement;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebFramework.Areas.Management.Models;

namespace WebFramework.Areas.Management.Controllers
{
    [Area("Management")]
    public class UserRolesController : Controller
    {
        private readonly IUserRolesManagementService userRolesManagementService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger _logger;

        public UserRolesController(IUserRolesManagementService userRolesManagementService,
            UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager,
            ILogger<UserRolesController> logger)
        {
            this.userRolesManagementService = userRolesManagementService;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        public IActionResult Index()
        {
            UserRoleViewModel viewModel = new UserRoleViewModel();
            viewModel.Users = userRolesManagementService.GetUsers();
            viewModel.Roles = userRolesManagementService.GetRoles();
            return View(viewModel);
        }

        [HttpPost]
        public async Task<JsonResult> AddRole(string userId, string roleName)
        {
            var user = userRolesManagementService.GetUserById(userId);
            //neu khong ton tai user
            if (user == null)
            {
                //xuat loi
                string errorMessage = "Can't add because user is null";
                _logger.LogInformation(errorMessage);
                return Json(new
                {
                    result = "failed",
                    data = errorMessage
                });
            }

            var roles = await _userManager.GetRolesAsync(user);

            //neu user co role
            if (roles.Contains(roleName))
            {
                //xuat loi
                string errorMessage = "Can't add role because user already have role name " + roleName;
                _logger.LogInformation(errorMessage);
                return Json(new
                {
                    result = "failed",
                    data = errorMessage
                });
            }

            var result = await _userManager.AddToRoleAsync(user, roleName);

            // thanh cong
            if (result.Succeeded)
            {
                _logger.LogInformation(String.Format("Successful add role '{0}' to user '{1}'", roleName, user.Name));
                return Json(new
                {
                    result = "success",
                    data = roleName
                });
            }
            else
            {
                //that bai
                return Json(new
                {
                    result = "failed",
                    data = result.Errors.Select(x => x.Description).SingleOrDefault()
                });
            }
        }

        [HttpDelete]
        public async Task<JsonResult> RemoveRole(string userId,string roleName)
        {
            var user = userRolesManagementService.GetUserById(userId);
            //neu khong ton tai user
            if (user == null)
            {
                //xuat loi
                string errorMessage = "Can't remove role because user is null";
                _logger.LogInformation(errorMessage);
                return Json(new
                {
                    result = "failed",
                    data = errorMessage
                });
            }

            var roles = await _userManager.GetRolesAsync(user);

            //neu user k co role
            if(!roles.Contains(roleName))
            {
                //xuat loi
                string errorMessage = "Can't remove role because user have not had role name "+roleName;
                _logger.LogInformation(errorMessage);
                return Json(new
                {
                    result = "failed",
                    data = errorMessage
                });
            }

            var result = await _userManager.RemoveFromRoleAsync(user, roleName);

            // thanh cong
            if (result.Succeeded)
            {
                _logger.LogInformation(String.Format("Successful remove role '{0}' from user '{1}'", roleName, user.Name));
                return Json(new
                {
                    result = "success",
                    data = roleName
                });
            }
            else
            {
                //that bai
                return Json(new
                {
                    result = "failed",
                    data = result.Errors.Select(x => x.Description).SingleOrDefault()
                });
            }

        }
    }
}