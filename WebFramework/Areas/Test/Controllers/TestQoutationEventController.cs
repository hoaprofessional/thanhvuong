using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Framework.DTOs.Areas.Test;
using Framework.Services.Areas.Test;
using Microsoft.AspNetCore.Mvc;

namespace WebFramework.Areas.Test.Controllers
{
    [Area("Test")]
    public class TestQoutationEventController : Controller
    {
        ITestQoutationEventService testQoutationEventService;

        public TestQoutationEventController(ITestQoutationEventService testQoutationEventService)
        {
            this.testQoutationEventService = testQoutationEventService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult QoutationEventList()
        {
            List<QoutationEventListDto> viewModel = testQoutationEventService.GetQoutationEventLists();
            return View(viewModel);
        }
        public IActionResult QoutationEventDetail(int QoutationId)
        {
            List<QoutationEventDto> viewModel = testQoutationEventService.GetQoutationEventDetail(QoutationId);
            return View(viewModel);
        }
    }
}