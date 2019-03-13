using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Framework.DTOs.AllNotification;
using Framework.Models.NotificationManagement;
using Framework.Services.AllNotificationServices;
using Framework.Services.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using WebFramework.Controllers.Shared;
using WebFramework.Models.AllNotificationViewModels;
using WebFramework.SignalR.Hubs;

namespace WebFramework.Controllers
{
    public class AllNotificationController : LayoutController
    {
        readonly IAllNotificationIndexService allNotificationIndexService;
        public AllNotificationController(ILayoutService layoutService,
            IHubContext<NotificationHub> hubcontext,
            IAllNotificationIndexService allNotificationIndexService) : base(layoutService, hubcontext)
        {
            this.allNotificationIndexService = allNotificationIndexService;
        }

        public IActionResult Index(string staffId)
        {
            var viewModel = new AllNotificationIndexViewModel();
            InitLayoutViewModel(viewModel);
            return View(viewModel);
        }

        public PartialViewResult NotificationPartial(string staffId, int currentPage)
        {
            AllNotificationPartialViewModel viewModel = new AllNotificationPartialViewModel();
            viewModel.CurrentPage = currentPage;
            var filter = new AllNotificationIndexFilter()
            {
                CurrentPage = currentPage,
                StaffId = staffId,
                ItemsPerPage = 10,
                SortingAction = "desc",
                SortingColumnName = nameof(Notification.ModifiedTime)
            };
            viewModel.NumberOfPages = allNotificationIndexService.GetNumbersOfPage(filter);
            viewModel.Notifications = allNotificationIndexService.GetItems(filter).ToList();
            return PartialView(viewModel);
        }

    }
}