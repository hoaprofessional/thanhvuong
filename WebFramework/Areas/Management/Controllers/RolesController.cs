using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Framework.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebFramework.Areas.Management.Models;

namespace WebFramework.Areas.Management.Controllers
{
    [Area("Management")]
    public class RolesController : Controller
    {

        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger _logger;

        public RolesController(RoleManager<IdentityRole> roleManager,
            ILogger<RolesController> logger)
        {
            _roleManager = roleManager;
            _logger = logger;
        }

        public IActionResult Index()
        {
            RolesViewModel viewModel = new RolesViewModel();
            viewModel.Roles = _roleManager.Roles.ToList();
            return View(viewModel);
        }

        [HttpDelete]
        public async Task<JsonResult> Delete(string roleId)
        {

            var role = await _roleManager.FindByIdAsync(roleId);
            //tim kiem xem role da co hay chua
            if (role == null)
            {
                //neu khong co thi thong bao loi
                return Json(new
                {
                    result = "failed",
                    data = roleId + " is not exists"
                });
            }
            var roleName = role.Name;
            var result = await _roleManager.DeleteAsync(role);

            // thanh cong
            if (result.Succeeded)
            {
                _logger.LogInformation("User delete role name " + roleName);
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

        [HttpPut]
        public async Task<JsonResult> Modifile(string roleId,string newRoleName)
        {
            var oldRole = await _roleManager.FindByIdAsync(roleId);
            //tim kiem xem old role da co hay chua
            if (oldRole == null)
            {
                //neu chua co thi thong bao that bai
                return Json(new
                {
                    result = "failed",
                    data = roleId + " is not exists"
                });
            }

            //tim kiem xem new role da co hay chua
            if ((await _roleManager.FindByNameAsync(newRoleName)) != null)
            {
                //neu co roi thi thong bao that bai
                return Json(new
                {
                    result = "failed",
                    data = newRoleName + " is exists"
                });
            }
            var oldRoleName = oldRole.Name;
            // cap nhat old role sang new role
            oldRole.Name = newRoleName;

            var result = await _roleManager.UpdateAsync(oldRole);

            // thanh cong
            if (result.Succeeded)
            {
                _logger.LogInformation("User modifiled role name " + oldRoleName + "to " + newRoleName);
                return Json(new
                {
                    result = "success",
                    data = newRoleName
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

        [HttpPost]
        public async Task<JsonResult> Create(string role)
        {
            //tim kiem xem role da co hay chua
            if((await _roleManager.FindByNameAsync(role))!=null)
            {
                //neu co roi thi thong bao khong the add
                return Json(new
                {
                    result = "failed",
                    data = role + " is exists"
                });
            }
            // neu chua co thi tien hanh add
            var result = await _roleManager.CreateAsync(new IdentityRole()
            {
                Id = Guid.NewGuid().ToString(),
                Name = role
            });
            // thanh cong
            if (result.Succeeded)
            {
                _logger.LogInformation("User created a new role name " + role);
                return Json(new
                {
                    result = "success",
                    data = role
                });
            }
            else
            {
                //that bai
                return Json(new
                {
                    result = "failed",
                    data = result.Errors.Select(x=>x.Description).ToArray()
                });
            }
        }

    }
}