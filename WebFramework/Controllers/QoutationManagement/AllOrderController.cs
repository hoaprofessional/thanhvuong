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
    public class AllOrderController : LayoutController
    {
        readonly IAllOrderIndexService allOrderService;
        public AllOrderController(ILayoutService layoutService,
            IHubContext<NotificationHub> hubcontext,
            IAllOrderIndexService allOrderService) : base(layoutService, hubcontext)
        {
            this.allOrderService = allOrderService;
        }

        public IActionResult Index()
        {
            int numberOfActiveRows = allOrderService.GetNumberOfActiveRow(null, 
                null, 
                null,
                null, 
                null, 
                null,
                GetCurrentStaffId(),
                GetPermissions());
            AllOrderIndexViewModel viewModel = new AllOrderIndexViewModel()
            {
                ClientFilters = allOrderService.GetClientFilterDtos(),
                CreateByFilters = allOrderService.GetCreateByFilters(),
                OrderStatusFilters = allOrderService.GetOrderStatusFilterDtos(),
                ProductFilters = allOrderService.GetProductsFilters(),
                NumbersShowPage = allOrderService.GetNumbersShowPage(numberOfActiveRows)
            };

            InitLayoutViewModel(viewModel);
            viewModel.Title = "Tất cả đơn đặt hàng";
            return View(viewModel);
        }

        public IActionResult AllOrderListPartial(
           AllOrderInput allOrderInput)
        {
            AllOrderListPartialViewModel viewModel = new AllOrderListPartialViewModel();
            viewModel.Orders = allOrderService.GetOrders(
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

            int numberOfActiveRows = allOrderService.GetNumberOfActiveRow(allOrderInput.CreateByFilters,
                allOrderInput.FromDate,
                allOrderInput.ToDate,
                allOrderInput.ProductName,
                allOrderInput.ClientName,
                allOrderInput.OrderStatusId,
                GetCurrentStaffId(),
                GetPermissions());

            viewModel.NumberOfPages = allOrderService.GetNumberOfPages(numberOfActiveRows,
                allOrderInput.NumberItemPerPage);

            viewModel.CurrentPage = allOrderInput.Page;
            //hien thi nut sort
            viewModel.ColumnSortingName = allOrderInput.ColumnSortingName;
            viewModel.SortingAction = allOrderInput.SortingAction;
            return PartialView(viewModel);
        }
    }
}