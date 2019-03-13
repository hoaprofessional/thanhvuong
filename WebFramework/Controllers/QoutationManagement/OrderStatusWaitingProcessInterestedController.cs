using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Framework.InputModels.QoutationManagement.OrderList;
using Framework.Services.QoutationManagementService.AllOrderService;
using Framework.Services.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using WebFramework.Controllers.Shared;
using WebFramework.Models.QoutationManagementViewModels.AllOrderViewModel;
using WebFramework.SignalR.Hubs;

namespace WebFramework.Controllers.QoutationManagement
{
    public class OrderStatusWaitingApprovalInterestedController : LayoutController
    {
        readonly IOrderStatusWaitingApprovalInterestedService orderStatusWaitingApprovalInterestedService;
        public OrderStatusWaitingApprovalInterestedController(ILayoutService layoutService,
            IHubContext<NotificationHub> hubcontext,
            IOrderStatusWaitingApprovalInterestedService orderProcessedInterestedService) : base(layoutService, hubcontext)
        {
            this.orderStatusWaitingApprovalInterestedService = orderProcessedInterestedService;
        }

        public IActionResult Index()
        {
            int numberOfActiveRows = orderStatusWaitingApprovalInterestedService.GetNumberOfActiveRow(null, 
                null, 
                null,
                null, 
                null, 
                null,
                GetCurrentStaffId(),
                GetPermissions());
            AllOrderIndexViewModel viewModel = new AllOrderIndexViewModel()
            {
                ClientFilters = orderStatusWaitingApprovalInterestedService.GetClientFilterDtos(),
                CreateByFilters = orderStatusWaitingApprovalInterestedService.GetCreateByFilters(),
                OrderStatusFilters = orderStatusWaitingApprovalInterestedService.GetOrderStatusFilterDtos(),
                ProductFilters = orderStatusWaitingApprovalInterestedService.GetProductsFilters(),
                NumbersShowPage = orderStatusWaitingApprovalInterestedService.GetNumbersShowPage(numberOfActiveRows)
            };

            InitLayoutViewModel(viewModel);
            viewModel.Title = "Tất cả đơn đặt hàng";
            return View("~/Views/AllOrder/Index.cshtml", viewModel);
        }

        public IActionResult AllOrderListPartial(
           AllOrderInput allOrderInput)
        {
            AllOrderListPartialViewModel viewModel = new AllOrderListPartialViewModel();
            viewModel.Orders = orderStatusWaitingApprovalInterestedService.GetOrders(
                allOrderInput.CreateByFilters,
                allOrderInput.FromDate,
                allOrderInput.ToDate,
                allOrderInput.ProductName,
                allOrderInput.ClientName,
                allOrderInput.OrderStatusId,
                allOrderInput.ColumnSortingName,
                allOrderInput.SortingAction,
                allOrderInput.Page,
                allOrderInput.NumberItemPerPage,
                GetCurrentStaffId(),
                GetPermissions()).ToList();

            int numberOfActiveRows = orderStatusWaitingApprovalInterestedService.GetNumberOfActiveRow(allOrderInput.CreateByFilters,
                allOrderInput.FromDate,
                allOrderInput.ToDate,
                allOrderInput.ProductName,
                allOrderInput.ClientName,
                allOrderInput.OrderStatusId,
                GetCurrentStaffId(),
                GetPermissions());

            viewModel.NumberOfPages = orderStatusWaitingApprovalInterestedService.GetNumberOfPages(numberOfActiveRows,
                allOrderInput.NumberItemPerPage);

            viewModel.CurrentPage = allOrderInput.Page;
            //hien thi nut sort
            viewModel.ColumnSortingName = allOrderInput.ColumnSortingName;
            viewModel.SortingAction = allOrderInput.SortingAction;
            return PartialView("~/Views/AllOrder/AllOrderListPartial.cshtml", viewModel);
        }
    }
}