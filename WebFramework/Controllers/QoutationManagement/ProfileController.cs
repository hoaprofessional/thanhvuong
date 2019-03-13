using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Framework.Services.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using WebFramework.Controllers.Shared;
using WebFramework.Models.QoutationManagementViewModels.ProfileViewModels;
using WebFramework.SignalR.Hubs;

namespace WebFramework.Controllers.QoutationManagement
{
    [Authorize]
    public class ProfileController : LayoutController
    {
        public ProfileController(ILayoutService layoutService,IHubContext<NotificationHub> hubcontext) : base(layoutService, hubcontext)
        {
        }

        public IActionResult Index()
        {
            ProfileIndexViewModel viewModel = new ProfileIndexViewModel();
            InitLayoutViewModel(viewModel);
            return View(viewModel);
        }
    }
}