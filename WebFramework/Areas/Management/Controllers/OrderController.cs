using Framework.Models.QoutationManagement;
using Framework.Repositories.Infrastructor;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WebCore.Services.Share.Admins.Orders;
using WebCore.Services.Share.Admins.Orders.Dto;
using WebCore.Services.Share.Commons.Permissions;
using WebCore.Utils.Config;
using WebCore.Utils.ModelHelper;
using WebFramework.Areas.Management.Models;
using WebFramework.Infrastructor;

namespace WebFramework.Areas.Management.Controllers
{
    [Area("Management")]
    [ClaimRequirement(MyClaimType.Permission, PermissionValue.RegisterUser)]
    public class OrderController : AdminBaseController
    {
        private readonly IOrderService orderService;
        private readonly IPermissionService permissionService;
        private readonly IUnitOfWork unitOfWork;

        public OrderController(IServiceProvider serviceProvider, IOrderService userService, IPermissionService permissionService, IUnitOfWork unitOfWork) : base(serviceProvider)
        {
            this.orderService = userService;
            this.unitOfWork = unitOfWork;
            this.permissionService = permissionService;
        }

        public IActionResult Index(int page = 1)
        {
            OrderFilterInput filterInput = GetFilterInSession<OrderFilterInput>("OrderSession");
            filterInput.PageNumber = page;
            OrderViewModel userViewModel = new OrderViewModel
            {
                FilterInput = filterInput,
                PagingResult = orderService.GetAllByPaging(filterInput)
            };
            InitAdminBaseViewModel(userViewModel);
            return View(userViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult FilterPartial(OrderFilterInput filterInput)
        {
            SetFilterToSession("OrderSession", filterInput);
            return RedirectToAction("Index", new { page = 1 });
        }
        [HttpPost]
        public IActionResult DeleteOrder(string orderId)
        {
            orderService.DeleteOrder(orderId);
            unitOfWork.Commit();
            return Ok();
        }
    }
}