using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Framework.Models.UserManagement;
using Framework.Services.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebFramework.Controllers.Shared;
using WebFramework.Infrastructor;
using WebFramework.Models.UserLoginViewModel.RegisterAccountViewModel;
using WebFramework.Services;
using Framework.Utils;
using Framework.Services.UserLoginService.RegisterAccountService;
using Framework.Services.Shared;
using Framework.Services.ManageService.QoutationManagement;
using Framework.Models.QoutationManagement;
using Framework.Repositories.Infrastructor;
using Microsoft.AspNetCore.SignalR;
using WebFramework.SignalR.Hubs;

namespace WebFramework.Controllers.UserLogin
{
    [ClaimRequirement(MyClaimType.Permission, "RegisterUser")]
    public class RegisterAccountController : LayoutController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly IStaffManageService staffManageService;
        IRegisterAccountIndexService registerAccountIndexService;

        public RegisterAccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender,
            IStaffManageService staffManageService,
            IHubContext<NotificationHub> hubcontext,
            IRegisterAccountIndexService registerAccountIndexService,
            ILayoutService layoutService,
            IUnitOfWork unitOfWork) : base(layoutService, hubcontext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            this.registerAccountIndexService = registerAccountIndexService;
            this.staffManageService = staffManageService;
            this.unitOfWork = unitOfWork;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            var viewModel = new RegisterAccountIndexViewModel();
            viewModel.UserObjectDtos = registerAccountIndexService.GetUserObjectDtos();
            InitLayoutViewModel(viewModel);
            viewModel.Title = "Đăng ký tài khoản";
            return View(viewModel);
        }

        public async Task<string> RegisterAdminAccount()
        {
            RegisterAccountIndexViewModel model = new RegisterAccountIndexViewModel()
            {
                Address = "Admin",
                Avatar = "",
                Password = "P@ssword2018",
                ConfirmPassword = "P@ssword2018",
                Email = "mail@mail.com",
                IdentityCardNumber = "123535456",
                Name = "Admin",
                ObjectId = "admin",
                UserName = "admin"
            };
            var user = new ApplicationUser();
            user.CopyFrom(model);
            user.Active = true;
            var userObject = registerAccountIndexService.GetUserObjectById(model.ObjectId);
            if (registerAccountIndexService.GetUserByUserName(model.UserName) != null)
            {
                ModelState.AddModelError("UserName", "Đã tồn tại user này trong hệ thống");
            }
            
            var result = await _userManager.CreateAsync(user, model.Password);
            await _userManager.AddToRoleAsync(user, userObject.RoleId);
            user.Carrer = userObject.DefaultCarrer;
            await _userManager.AddToRoleAsync(user, userObject.Role.Name);
            if (result.Succeeded)
            {
                if (!String.IsNullOrEmpty(user.Avatar))
                    user.Avatar = user.Avatar.Substring(user.Avatar.IndexOf("wwwroot")).Replace("wwwroot", "");
                unitOfWork.BeginTransaction();
                Staff staff = new Staff()
                {
                    UserId = user.Id,
                    Name = user.Name,
                    Career = user.Carrer,
                    IdentityCard = user.IdentityCardNumber,
                    Address = user.Address
                };
                staffManageService.Add(staff);
                unitOfWork.Commit();
                unitOfWork.CommitTransaction();
                loggerService.AddInfomationLogger(String.Format("Tạo người dùng mới {0}", model.UserName));
                return "success";
                //return Redirect("/RegisterAccount");
            }
            AddErrors(result);
            return "fail";
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Register(RegisterAccountIndexViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser();
                user.CopyFrom(model);
                user.Active = true;
                var userObject = registerAccountIndexService.GetUserObjectById(model.ObjectId);
                if (registerAccountIndexService.GetUserByUserName(model.UserName) != null)
                {
                    ModelState.AddModelError("UserName", "Đã tồn tại user này trong hệ thống");
                }
                if (userObject == null)
                {
                    ModelState.AddModelError("ObjectId", "ObjectId không hợp lệ");
                    model.UserObjectDtos = registerAccountIndexService.GetUserObjectDtos();
                    if (System.IO.File.Exists(model.Avatar))
                    {
                        System.IO.File.Delete(model.Avatar);
                    }
                    return Json(new { result = "fail", data = GetErrorDataModel() });
                }
                var result = await _userManager.CreateAsync(user, model.Password);
                await _userManager.AddToRoleAsync(user, userObject.RoleId);
                user.Carrer = userObject.DefaultCarrer;
                await _userManager.AddToRoleAsync(user, userObject.Role.Name);
                if (result.Succeeded)
                {
                    if (!String.IsNullOrEmpty(user.Avatar))
                        user.Avatar = user.Avatar.Substring(user.Avatar.IndexOf("wwwroot")).Replace("wwwroot", "");
                    try
                    {
                        Staff staff = new Staff()
                        {
                            UserId = user.Id,
                            Name = user.Name,
                            Career = user.Carrer,
                            IdentityCard = user.IdentityCardNumber,
                            Address = user.Address
                        };
                        staffManageService.Add(staff);
                        unitOfWork.Commit();
                        loggerService.AddInfomationLogger(String.Format("Tạo người dùng mới {0}", model.UserName));
                        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme);
                        await _emailSender.SendEmailConfirmationAsync(model.Email, callbackUrl);
                        return Json(new { result = "success" });
                    }
                    catch(Exception e)
                    {
                        unitOfWork.RollbackTransaction();
                    }
                    //return Redirect("/RegisterAccount");
                }
                AddErrors(result);
            }
            // If we got this far, something failed, redisplay form

            model.UserObjectDtos = registerAccountIndexService.GetUserObjectDtos();
            if (System.IO.File.Exists(model.Avatar))
            {
                System.IO.File.Delete(model.Avatar);
            }
            return Json(new { result = "fail", data = GetErrorDataModel() });
        }
    }
}