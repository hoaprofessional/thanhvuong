using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Framework.Context;
using Framework.InputModels;
using Framework.InputModels.HomePage;
using Framework.Models.NotificationManagement;
using Framework.Models.UserManagement;
using Framework.Repositories;
using Framework.Repositories.Infrastructor;
using Framework.Services;
using Framework.Services.ManageService;
using Framework.Services.ManageService.TaskManagement;
using Framework.Services.Shared;
using Framework.Services.UserLoginService.HomePageService;
using Framework.Services.Utils;
using Framework.Utils;
using Framework.Utils.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WebFramework.Controllers.Shared;
using WebFramework.Models;
using WebFramework.Models.NotificationViewModels;
using WebFramework.Models.Shared;
using WebFramework.Models.UserLoginViewModel.AccountViewModels;
using WebFramework.Models.UserLoginViewModel.HomeViewModel;
using WebFramework.Services;
using WebFramework.SignalR.Hubs;

namespace WebFramework.Controllers.UserLogin
{
    public class HomeController : LayoutController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly IHomeIndexService homeIndexService;
        private readonly INotificationManageService notificationManageService;
        private readonly IUnitOfWork unitOfWork;

        public HomeController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender,
            ILoggerService loggerService,
            IUnitOfWork unitOfWork,
            IHubContext<NotificationHub> hubcontext,
            INotificationManageService notificationManageService,
            IHomeIndexService homeIndexService)
            : base(ResolveDepedencyInjection.Resolve<ILayoutService>(), hubcontext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            this.homeIndexService = homeIndexService;
            this.notificationManageService = notificationManageService;
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Index(string returnUrl = null)
        {
            HomeIndexViewModel viewModel = new HomeIndexViewModel();
            InitLayoutViewModel(viewModel);
            if (viewModel.User != null)
            {
                return Redirect("/Profile");
            }
            viewModel.Title = "Trang chủ";
            viewModel.ReturnUrl = returnUrl;
            return View(viewModel);
        }

        public JsonResult MakeOldNotification(string notificationId)
        {
            try
            {
                var notification = notificationManageService.GetById(notificationId);
                if (notification == null)
                {
                    return Json(new { result = "fail" });
                }
                notification.IsRead = true;
                notificationManageService.Update(notification);
                unitOfWork.Commit();
                return Json(new { result = "success" });
            }
            catch
            {
                return Json(new { result = "fail" });
            }
        }

        public async Task<JsonResult> DemoSendSignalR()
        {
            await SendNotification("ApproveQoutation", "https://facebook.com", "Demo test");
            

            //await hubcontext.Clients.Group("ApproveQoutation").SendAsync("ReceiveText", CreateNotificationContent("https://facebook.com", "Demo test"));
            return Json("success");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(HomeIndexViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, lockoutOnFailure: true);
                model.Title = "Trang chủ";
                if (result.Succeeded)
                {
                    loggerService.AddInfomationLogger(String.Format("Người dùng {0} đã đăng nhập vào hệ thống", model.UserName));
                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    ModelState.AddModelError(nameof(model.UserName), "Thông tin đăng nhập không chính xác.");
                    return View(model);
                }
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        public async Task<String> RegisterAdminUser()
        {
            var user = new ApplicationUser { UserName = "admin", Email = "admin@gmail.com" };
            var result = await _userManager.CreateAsync(user, "P@ssword2018");
            await _userManager.AddToRoleAsync(user, "admin");
            if (result.Succeeded)
            {
                return "success";
            }
            return "fail";
        }

        public async Task<ActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            loggerService.AddInfomationLogger(String.Format("Người dùng {0} đã đăng xuất khỏi hệ thống", User.Identity.Name));
            return RedirectToAction("/");
        }


        public IActionResult ForgotPassword()
        {
            LayoutViewModel viewModel = new LayoutViewModel();
            InitLayoutViewModel(viewModel);
            viewModel.Title = "Quên mật khẩu";
            return View(viewModel);
        }

        public String Logined()
        {
            return "Đã đăng nhập";
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
