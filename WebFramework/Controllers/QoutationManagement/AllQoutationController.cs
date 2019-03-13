using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Framework.InputModels.QoutationManagement.QoutationList;
using Framework.Services.QoutationManagementService.AllQoutationService;
using Framework.Services.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using WebFramework.Controllers.Shared;
using WebFramework.Infrastructor;
using WebFramework.Models.QoutationManagementViewModels.AllQoutationViewModels;
using WebFramework.SignalR.Hubs;

namespace WebFramework.Controllers.QoutationManagement
{
    public class AllQoutationController : LayoutController
    {
        protected IAllQoutationIndexService allQoutationService;
        readonly IQoutationCanCreateOrderService iIQoutationCanCreateOrderService;
        public AllQoutationController(ILayoutService layoutService,
            IQoutationCanCreateOrderService iIQoutationCanCreateOrderService,
            IHubContext<NotificationHub> hubcontext,
            IAllQoutationIndexService allQoutationService) : base(layoutService, hubcontext)
        {
            this.allQoutationService = allQoutationService;
            this.iIQoutationCanCreateOrderService = iIQoutationCanCreateOrderService;
        }

        public IActionResult Index()
        {
            int numberOfActiveRows = allQoutationService.GetNumberOfActiveRow(null, null, null, null, null, null, null, null);
            AllQoutationIndexViewModel viewModel = new AllQoutationIndexViewModel()
            {
                ClientFilters = allQoutationService.GetClientFilterDtos(),
                CreateByFilters = allQoutationService.GetCreateByFilters(),
                QoutationStatusFilters = allQoutationService.GetQoutationStatusFilterDtos(),
                ProductFilters = allQoutationService.GetProductsFilters(),
                NumbersShowPage = allQoutationService.GetNumbersShowPage(numberOfActiveRows)
            };
            InitLayoutViewModel(viewModel);
            viewModel.Title = "Tất cả báo giá";
            return View(viewModel);
        }
        [ClaimRequirement(MyClaimType.Permission, PermissionValue.CanCreateOrder)]
        public IActionResult QoutationCanCreateOrder()
        {
            int numberOfActiveRows = allQoutationService.GetNumberOfActiveRow(null, null, null, null, null, null, null, null);
            var viewModel = new QoutationCanCreateOrderViewModel()
            {
                ClientFilters = allQoutationService.GetClientFilterDtos(),
                CreateByFilters = allQoutationService.GetCreateByFilters(),
                QoutationStatusFilters = allQoutationService.GetQoutationStatusFilterDtos(),
                ProductFilters = allQoutationService.GetProductsFilters(),
                NumbersShowPage = allQoutationService.GetNumbersShowPage(numberOfActiveRows)
            };
            InitLayoutViewModel(viewModel);
            viewModel.Title = "Tất cả báo giá";
            return View(viewModel);
        }

        public IActionResult QoutationCanCreateOrderPartial(QoutationCanCreatePartialInput input)
        {
            var viewModel = new QoutationCanCreatePartialViewModel();
            viewModel.Qoutations = iIQoutationCanCreateOrderService.GetQoutations(
                input.CreateByFilters,
                input.FromDate,
                input.ToDate,
                input.ProductName,
                input.ClientName,
                null,// không lọc theo status
                input.ColumnSortingName,
                input.SortingAction,
                input.Page,
                input.NumberItemPerPage,
                null,
                null).ToList();

            int numberOfActiveRows = iIQoutationCanCreateOrderService.GetNumberOfActiveRow(input.CreateByFilters,
                input.FromDate,
                input.ToDate,
                input.ProductName,
                input.ClientName,
                null,// không lọc theo status
                null,
                null
                );

            viewModel.NumberOfPages = iIQoutationCanCreateOrderService.GetNumberOfPages(numberOfActiveRows,
                input.NumberItemPerPage);

            viewModel.CurrentPage = input.Page;
            //hien thi nut sort
            viewModel.ColumnSortingName = input.ColumnSortingName;
            viewModel.SortingAction = input.SortingAction;
            return PartialView(viewModel);
        }

        public IActionResult AllQoutationListPartial(
           AllQoutationInput allQoutationInput)
        {
            var viewModel = new AllQoutationListPartialViewModel();
            viewModel.Qoutations = allQoutationService.GetQoutations(
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

            int numberOfActiveRows = allQoutationService.GetNumberOfActiveRow(allQoutationInput.CreateByFilters,
                allQoutationInput.FromDate,
                allQoutationInput.ToDate,
                allQoutationInput.ProductName,
                allQoutationInput.ClientName,
                allQoutationInput.QoutationStatusId,
                GetCurrentStaffId(),
                GetPermissions());

            viewModel.NumberOfPages = allQoutationService.GetNumberOfPages(numberOfActiveRows,
                allQoutationInput.NumberItemPerPage);

            viewModel.CurrentPage = allQoutationInput.Page;
            //hien thi nut sort
            viewModel.ColumnSortingName = allQoutationInput.ColumnSortingName;
            viewModel.SortingAction = allQoutationInput.SortingAction;
            return PartialView(viewModel);
        }

    }
}