using Framework.Models.UserManagement;
using Framework.Services.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using WebFramework.Controllers.Shared;
using WebFramework.Models.ResetPassword;
using WebFramework.SignalR.Hubs;

namespace WebFramework.Controllers.QoutationManagement
{
    public class ResetPasswordController : LayoutController
    {
        private UserManager<ApplicationUser> userManager;
        public ResetPasswordController(ILayoutService layoutService, IHubContext<NotificationHub> hubcontext, UserManager<ApplicationUser> userManager) : base(layoutService, hubcontext)
        {
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            ResetPasswordViewModel layoutViewModel = new ResetPasswordViewModel();
            InitLayoutViewModel(layoutViewModel);
            return View(layoutViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Index(ResetPasswordViewModel layoutViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            ApplicationUser user = await userManager.FindByIdAsync(GetCurrentUserId());
            string code = await userManager.GeneratePasswordResetTokenAsync(user);
            await userManager.ResetPasswordAsync(user, code, layoutViewModel.Password);

            return Redirect("/Profile");
        }
    }
}