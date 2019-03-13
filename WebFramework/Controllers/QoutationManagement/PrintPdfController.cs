using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Framework.Services.PrintPdfPage;
using Framework.Services.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using WebFramework.Controllers.Shared;
using WebFramework.Models.PrintPdfViewModels;
using WebFramework.SignalR.Hubs;

namespace WebFramework.Controllers
{
    public class PrintPdfController : LayoutController
    {
        readonly IPrintPdfQoutationPrintService printPdfQoutationPrintService;

        public PrintPdfController(ILayoutService layoutService,
            IHubContext<NotificationHub> hubcontext,
            IPrintPdfQoutationPrintService printPdfQoutationPrintService
            ) : base(layoutService, hubcontext)
        {
            this.printPdfQoutationPrintService = printPdfQoutationPrintService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult QoutationPrint(int QoutationId)
        {
            PrintPdfQoutationPrintViewModel viewModel = new PrintPdfQoutationPrintViewModel();
            InitLayoutViewModel(viewModel);
            viewModel.QoutationDto = printPdfQoutationPrintService.GetQoutationFromId(QoutationId);
            return View(viewModel);
        }
    }
}