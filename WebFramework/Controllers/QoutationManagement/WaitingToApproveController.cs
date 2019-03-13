using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Framework.Services.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using WebFramework.Controllers.Shared;
using WebFramework.Models.QoutationManagementViewModels.WaitingToApproveViewModel;
using WebFramework.SignalR.Hubs;

namespace WebFramework.Controllers.QoutationManagement
{
    public class WaitingToApproveController : LayoutController
    {
        public WaitingToApproveController(IHubContext<NotificationHub> hubcontext, ILayoutService layoutService) : base(layoutService,hubcontext)
        {
        }

        public IActionResult Index()
        {
            WaitingToApproveIndexViewModel viewModel = new WaitingToApproveIndexViewModel();
            InitLayoutViewModel(viewModel);
            viewModel.Title = "Báo giá đang đợi duyệt";
            return View(viewModel);
        }
    }
}