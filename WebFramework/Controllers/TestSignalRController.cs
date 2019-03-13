using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Framework.Services.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using WebFramework.Controllers.Shared;
using WebFramework.Models.Shared;
using WebFramework.SignalR.Hubs;

namespace WebFramework.Controllers
{
    public class TestSignalRController : LayoutController
    {
        public TestSignalRController(ILayoutService layoutService, IHubContext<NotificationHub> hubcontext) : base(layoutService,hubcontext)
        {
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Room1()
        {
            LayoutViewModel model = new LayoutViewModel();
            InitLayoutViewModel(model);
            return View(model);
        }

        public IActionResult Room2()
        {
            LayoutViewModel model = new LayoutViewModel();
            InitLayoutViewModel(model);
            return View(model);
        }

    }
}