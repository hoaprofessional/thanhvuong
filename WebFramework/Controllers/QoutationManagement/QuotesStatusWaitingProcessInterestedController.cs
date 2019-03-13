using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Framework.InputModels.QoutationManagement.QoutationList;
using Framework.Services.QoutationManagementService.AllOrderService;
using Framework.Services.QoutationManagementService.AllQoutationService;
using Framework.Services.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using WebFramework.Controllers.Shared;
using WebFramework.Models.QoutationManagementViewModels.AllQoutationViewModels;
using WebFramework.SignalR.Hubs;

namespace WebFramework.Controllers.QoutationManagement
{
    public class QuotesStatusWaitingProcessInterestedController : LayoutController
    {
        IQuotesStatusWaitingProcessInterestedService quotesStatusWaitingProcessInterestedService;
        public QuotesStatusWaitingProcessInterestedController(ILayoutService layoutService,
            IHubContext<NotificationHub> hubcontext,
            IQuotesStatusWaitingProcessInterestedService quotesStatusWaitingProcessInterestedService) : base(layoutService, hubcontext)
        {
            this.quotesStatusWaitingProcessInterestedService = quotesStatusWaitingProcessInterestedService;
        }

        public IActionResult Index()
        {
            int numberOfActiveRows = quotesStatusWaitingProcessInterestedService
                .GetNumberOfActiveRow(null, null, null, null, null, null, GetCurrentStaffId(), GetPermissions());
            AllQoutationIndexViewModel viewModel = new AllQoutationIndexViewModel()
            {
                ClientFilters = quotesStatusWaitingProcessInterestedService.GetClientFilterDtos(),
                CreateByFilters = quotesStatusWaitingProcessInterestedService.GetCreateByFilters(),
                QoutationStatusFilters = quotesStatusWaitingProcessInterestedService.GetQoutationStatusFilterDtos(),
                ProductFilters = quotesStatusWaitingProcessInterestedService.GetProductsFilters(),
                NumbersShowPage = quotesStatusWaitingProcessInterestedService.GetNumbersShowPage(numberOfActiveRows)
            };
            InitLayoutViewModel(viewModel);
            viewModel.Title = "Tất cả báo giá";
            return View("~/Views/AllQoutation/Index.cshtml", viewModel);
        }

        public IActionResult AllQoutationListPartial(
           AllQoutationInput allQoutationInput)
        {
            var viewModel = new AllQoutationListPartialViewModel();
            viewModel.Qoutations = quotesStatusWaitingProcessInterestedService.GetQoutations(
                allQoutationInput.CreateByFilters,
                allQoutationInput.FromDate,
                allQoutationInput.ToDate,
                allQoutationInput.ProductName,
                allQoutationInput.ClientName,
                allQoutationInput.QoutationStatusId,
                allQoutationInput.ColumnSortingName,
                allQoutationInput.SortingAction,
                allQoutationInput.Page,
                allQoutationInput.NumberItemPerPage,
                GetCurrentStaffId(),
                GetPermissions()).ToList();

            int numberOfActiveRows = quotesStatusWaitingProcessInterestedService.GetNumberOfActiveRow(allQoutationInput.CreateByFilters,
                allQoutationInput.FromDate,
                allQoutationInput.ToDate,
                allQoutationInput.ProductName,
                allQoutationInput.ClientName,
                allQoutationInput.QoutationStatusId,
                GetCurrentStaffId(),
                GetPermissions());

            viewModel.NumberOfPages = quotesStatusWaitingProcessInterestedService.GetNumberOfPages(numberOfActiveRows,
                allQoutationInput.NumberItemPerPage);

            viewModel.CurrentPage = allQoutationInput.Page;
            //hien thi nut sort
            viewModel.ColumnSortingName = allQoutationInput.ColumnSortingName;
            viewModel.SortingAction = allQoutationInput.SortingAction;
            return PartialView("~/Views/AllQoutation/AllQoutationListPartial.cshtml", viewModel);
        }
    }
}