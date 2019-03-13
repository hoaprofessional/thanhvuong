using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Framework.InputModels.QoutationManagement.QoutationList;
using Framework.Services.QoutationManagementService.AllOrderService;
using Framework.Services.QoutationManagementService.AllQoutationService;
using Framework.Services.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using WebFramework.Controllers.Shared;
using WebFramework.Models.QoutationManagementViewModels.AllQoutationViewModels;
using WebFramework.SignalR.Hubs;

namespace WebFramework.Controllers.QoutationManagement
{
    [Authorize()]
    public class QuotesStatusWaitingApprovalInterestedController : LayoutController
    {
        IQuotesStatusWaitingApprovalInterestedService quotesStatusWaitingApprovalInterestedService;
        public QuotesStatusWaitingApprovalInterestedController(ILayoutService layoutService,
            IHubContext<NotificationHub> hubcontext,
            IQuotesStatusWaitingApprovalInterestedService quotesStatusWaitingApprovalInterestedService) : base(layoutService, hubcontext)
        {
            this.quotesStatusWaitingApprovalInterestedService = quotesStatusWaitingApprovalInterestedService;
        }

        public IActionResult Index()
        {
            int numberOfActiveRows = quotesStatusWaitingApprovalInterestedService
                .GetNumberOfActiveRow(null, null, null, null, null, null, GetCurrentStaffId(), GetPermissions());
            AllQoutationIndexViewModel viewModel = new AllQoutationIndexViewModel()
            {
                ClientFilters = quotesStatusWaitingApprovalInterestedService.GetClientFilterDtos(),
                CreateByFilters = quotesStatusWaitingApprovalInterestedService.GetCreateByFilters(),
                QoutationStatusFilters = quotesStatusWaitingApprovalInterestedService.GetQoutationStatusFilterDtos(),
                ProductFilters = quotesStatusWaitingApprovalInterestedService.GetProductsFilters(),
                NumbersShowPage = quotesStatusWaitingApprovalInterestedService.GetNumbersShowPage(numberOfActiveRows)
            };
            InitLayoutViewModel(viewModel);
            viewModel.Title = "Tất cả báo giá";
            return View("~/Views/AllQoutation/Index.cshtml", viewModel);
        }

        public IActionResult AllQoutationListPartial(
           AllQoutationInput allQoutationInput)
        {
            var viewModel = new AllQoutationListPartialViewModel();
            viewModel.Qoutations = quotesStatusWaitingApprovalInterestedService.GetQoutations(
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

            int numberOfActiveRows = quotesStatusWaitingApprovalInterestedService.GetNumberOfActiveRow(allQoutationInput.CreateByFilters,
                allQoutationInput.FromDate,
                allQoutationInput.ToDate,
                allQoutationInput.ProductName,
                allQoutationInput.ClientName,
                allQoutationInput.QoutationStatusId,
                GetCurrentStaffId(),
                GetPermissions());

            viewModel.NumberOfPages = quotesStatusWaitingApprovalInterestedService.GetNumberOfPages(numberOfActiveRows,
                allQoutationInput.NumberItemPerPage);

            viewModel.CurrentPage = allQoutationInput.Page;
            //hien thi nut sort
            viewModel.ColumnSortingName = allQoutationInput.ColumnSortingName;
            viewModel.SortingAction = allQoutationInput.SortingAction;
            return PartialView("~/Views/AllQoutation/AllQoutationListPartial.cshtml", viewModel);
        }
    }
}