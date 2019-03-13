using Framework.Models.QoutationManagement;
using Framework.Repositories.Infrastructor;
using Framework.Services.Admins.OrderDetails.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WebCore.Services.Share.Admins.OrderDetails;
using WebCore.Services.Share.Admins.OrderDetails.Dto;
using WebCore.Services.Share.Commons.Permissions;
using WebCore.Utils.Config;
using WebCore.Utils.ModelHelper;
using WebFramework.Areas.Management.Models;
using WebFramework.Infrastructor;

namespace WebFramework.Areas.Management.Controllers
{
    [Area("Management")]
    [ClaimRequirement(MyClaimType.Permission, PermissionValue.RegisterUser)]
    public class OrderDetailController : AdminBaseController
    {
        private readonly IOrderDetailService qoutationDetailService;
        private readonly IPermissionService permissionService;
        private readonly IUnitOfWork unitOfWork;

        public OrderDetailController(IServiceProvider serviceProvider, IOrderDetailService userService, IPermissionService permissionService, IUnitOfWork unitOfWork) : base(serviceProvider)
        {
            this.qoutationDetailService = userService;
            this.unitOfWork = unitOfWork;
            this.permissionService = permissionService;
        }

        public IActionResult Index(int page = 1,string orderId = "")
        {
            OrderDetailFilterInput filterInput = GetFilterInSession<OrderDetailFilterInput>("OrderDetailSession");
            filterInput.OrderId = orderId;
            filterInput.PageNumber = page;
            OrderDetailViewModel userViewModel = new OrderDetailViewModel
            {
                FilterInput = filterInput,
                PagingResult = qoutationDetailService.GetAllByPaging(filterInput)
            };
            InitAdminBaseViewModel(userViewModel);
            return View(userViewModel);
        }

        [HttpGet]
        public IActionResult InputPartial(EntityId<string> idModel = null)
        {
            OrderDetailInput input = null;
            if (idModel == null)
            {
                input = new OrderDetailInput();
            }
            else
            {
                input = qoutationDetailService.GetInputById(idModel?.Id);
            }

            return PartialView(input);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult InputPartial(OrderDetailInput inputModel)
        {
            if (inputModel.Id != null)
            {
                //update
                OrderDetail lastInfo = qoutationDetailService.GetById(inputModel.Id);
                qoutationDetailService.UpdateOrderDetail(inputModel);
                unitOfWork.Commit();
                return Ok(new { result = ConstantConfig.WebApiStatusCode.Success, message = "Cập nhật thành công" });
            }
            return Ok(new { result = ConstantConfig.WebApiStatusCode.Error, message = "Có lỗi xảy ra" });
        }
    }
}