using Framework.DTOs.ReportDtos;
using Framework.Services.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using WebFramework.Controllers.Shared;
using WebFramework.Models.ReportQoutations;
using WebFramework.SignalR.Hubs;

namespace WebFramework.Controllers.QoutationManagement
{
    public class ReportQoutationController : LayoutController
    {
        public ReportQoutationController(ILayoutService layoutService, IHubContext<NotificationHub> hubcontext) : base(layoutService, hubcontext)
        {
        }

        public IActionResult Index(ReportFilterDto filter = null)
        {
            ReportQoutationViewModel viewModel = new ReportQoutationViewModel();
            InitLayoutViewModel(viewModel);
            if(filter==null)
            {
                viewModel.ReportFilter = new ReportFilterDto();
            }
            else
            {
                viewModel.ReportFilter = filter;
            }
            return View(viewModel);
        }
    }
}