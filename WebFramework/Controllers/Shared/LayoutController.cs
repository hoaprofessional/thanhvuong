using Framework.InputModels.HomePage;
using Framework.Models.NotificationManagement;
using Framework.Models.UserManagement;
using Framework.Repositories.Infrastructor;
using Framework.Services.ManageService.TaskManagement;
using Framework.Services.Shared;
using Framework.Services.Utils;
using Framework.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebFramework.Infrastructor;
using WebFramework.Models.NotificationViewModels;
using WebFramework.Models.Shared;
using WebFramework.SignalR.Hubs;

namespace WebFramework.Controllers.Shared
{
    public class LayoutController : Controller
    {
        protected ILayoutService layoutService;
        protected readonly IHubContext<NotificationHub> hubcontext;
        protected readonly INotificationManageService notificationManageService;
        protected IUnitOfWork unitOfWork;
        protected readonly ILoggerService loggerService;
        private readonly UserManager<ApplicationUser> userManager;
        public LayoutController(ILayoutService layoutService, IHubContext<NotificationHub> hubcontext)
        {
            this.layoutService = layoutService;
            this.hubcontext = hubcontext;
            notificationManageService = ResolveDepedencyInjection.ServiceProvider.GetService<INotificationManageService>();
            unitOfWork = ResolveDepedencyInjection.ServiceProvider.GetService<IUnitOfWork>();
            loggerService = ResolveDepedencyInjection.ServiceProvider.GetService<ILoggerService>();
            userManager = ResolveDepedencyInjection.ServiceProvider.GetService<UserManager<ApplicationUser>>();
        }

        public void InitLayoutViewModel(LayoutViewModel viewModel)
        {
            viewModel.StaffId = layoutService.GetStaffByUserId(GetCurrentUserId())?.Id;
            viewModel.Permissions = GetPermissions().ToList();
            viewModel.User = layoutService.GetUserById(GetCurrentUserId());
            viewModel.NumberOfUnreadNotification = layoutService.GetUnReadNotificationCount(viewModel.StaffId);
            viewModel.TopNotifications = layoutService.GetTopNotifications(viewModel.StaffId);
        }

        public string CreateNotificationContent(string link, string notificationTitle, bool checkStaffId = false, DateTime? notificationTime = null, string staffId = null)
        {
            if (notificationTime == null)
            {
                notificationTime = DateTime.Now;
            }
            NotificationViewModel notification = new NotificationViewModel()
            {
                Link = link,
                NotificationTime = notificationTime.Value.ToString("dd/MM/yyyy lúc HH:mm"),
                NotificationTitle = notificationTitle,
                StaffId = staffId,
                CheckStaffId = checkStaffId
            };
            return Newtonsoft.Json.JsonConvert.SerializeObject(notification);
        }

        protected async Task SendNotification(string group, string link, string content, bool checkStaffId = false, string staffId = "")
        {
            await hubcontext.Clients.Group(group).SendAsync("ReceiveText", CreateNotificationContent(link, content, checkStaffId, DateTime.Now, staffId));
        }
        public void SaveBase64String(string base64String, string path)
        {
            System.IO.File.WriteAllBytes(path, Convert.FromBase64String(base64String));
        }
        protected string GetCurrentUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        protected string GetCurrentUserName()
        {
            return User.Identity.Name;
        }

        protected string GetCurrentStaffId()
        {
            return layoutService.GetStaffByUserId(GetCurrentUserId()).Id;
        }

        protected string[] GetPermissions()
        {
            if (HttpContext.User == null)
            {
                return new string[0];
            }
            return HttpContext.User.Claims.Where(x => x.Type == MyClaimType.Permission.ToString()).Select(x => x.Value).ToArray();
        }
        protected IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return Redirect("/");
            }
        }

        protected void AddErrors(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        protected object GetErrorDataModel()
        {
            return ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { key = x.Key, value = x.Value.Errors.ElementAt(0) });
        }

        public async Task<JsonResult> AddNotification(NotificationInput input)
        {
            try
            {
                if (input.IsSelf)
                {
                    Notification notification = new Notification();
                    notification.CopyFrom(input);
                    notification.IsRead = false;
                    notificationManageService.Add(notification);
                    unitOfWork.Commit();
                }
                else
                {
                    Claim claim = new Claim("Permission", input.Permission);

                    List<Claim> claims = User.Claims.ToList();

                    List<ApplicationUser> users = layoutService.GetUsersByClaim(input.Permission);
                    foreach (ApplicationUser user in users)
                    {
                        Framework.Models.QoutationManagement.Staff staff = layoutService.GetStaffByUserId(user.Id);
                        {
                            Notification notification = new Notification();
                            notification.CopyFrom(input);
                            notification.StaffId = staff.Id;
                            notification.IsRead = false;
                            notificationManageService.Add(notification);
                        }
                    }
                    unitOfWork.Commit();
                }
                await SendNotification(input.Permission, input.Link, input.Content, input.IsSelf, input.StaffId);
                return Json(new { result = "success" });
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}