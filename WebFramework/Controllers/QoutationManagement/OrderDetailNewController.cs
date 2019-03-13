using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Framework.InputModels.QoutationManagement.OrderDetail;
using Framework.Models.QoutationManagement;
using Framework.Repositories.Infrastructor;
using Framework.Services.ManageService.OrderManagement;
using Framework.Services.ManageService.QoutationManagement;
using Framework.Services.QoutationManagementService.OrderDetailService;
using Framework.Services.Shared;
using Framework.Services.Utils;
using Framework.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using WebFramework.Controllers.Shared;
using WebFramework.Infrastructor;
using WebFramework.Models.QoutationManagementViewModels.OrderDetailViewModel;
using WebFramework.SignalR.Hubs;

namespace WebFramework.Controllers.QoutationManagement
{
    public class OrderDetailNewController : LayoutController
    {
        readonly IOrderDetailIndexService orderDetailIndexService;
        readonly IOrderManageService orderManageService;
        readonly IOrderEventManageService orderEventManageService;
        public OrderDetailNewController(ILayoutService layoutService,
            IOrderManageService orderManageService,
            IOrderEventManageService orderEventManageService,
            IHubContext<NotificationHub> hubcontext,
            IOrderDetailIndexService orderDetailIndexService)
            : base(layoutService, hubcontext)
        {
            this.orderDetailIndexService = orderDetailIndexService;
            this.orderManageService = orderManageService;
            this.orderEventManageService = orderEventManageService;
        }

        public IActionResult Index(string id)
        {
            var viewModel = new OrderDetailIndexViewModel();
            InitLayoutViewModel(viewModel);
            viewModel.Order = this.orderDetailIndexService.GetOrderById(id);
            viewModel.CommonConfiguration = this.orderDetailIndexService.GetCommonConfiguration();
            var order = viewModel.Order;

            viewModel.CanApproveAlreadyCreatedOrder =
                viewModel.HasPermission(PermissionValue.CanApproveAlreadyCreatedOrder) &&
               (order.OrderStatusId == OrderStatusIdHelper.AlreadyCreated);

            viewModel.CanAccountantHasOrdered =
                viewModel.HasPermission(PermissionValue.CanAccountantHasOrdered) &&
               (order.OrderStatusId == OrderStatusIdHelper.SalesManagerApprove ||
               order.OrderStatusId == OrderStatusIdHelper.AccountingManagerReject);

            viewModel.CanAccountingManagerApprove =
                viewModel.HasPermission(PermissionValue.AccountingManagerApproveOrder) &&
               (order.OrderStatusId == OrderStatusIdHelper.AccountantHasOrdered);

            viewModel.CanUpdateGoodOnWay =
                viewModel.HasPermission(PermissionValue.CanUpdateGoodOnWay) &&
               (order.OrderStatusId == OrderStatusIdHelper.AccountingManagerApprove);


            viewModel.CanUpdateReadyToDeliver =
                viewModel.HasPermission(PermissionValue.CanUpdateReadyToDeliver) &&
               (order.OrderStatusId == OrderStatusIdHelper.GoodOnWay);

            viewModel.CanRecommendedDelivery =
                viewModel.HasPermission(PermissionValue.CanRecommendedDelivery) &&
               (order.OrderStatusId == OrderStatusIdHelper.ReadyToDeliver);

            viewModel.CanTechnicalChiefApproveOrder =
                viewModel.HasPermission(PermissionValue.CanTechnicalChiefApproveOrder) &&
               (order.OrderStatusId == OrderStatusIdHelper.RecommendedDelivery);

            viewModel.CanFinishDelivery =
                viewModel.HasPermission(PermissionValue.CanFinishDelivery) &&
               (order.OrderStatusId == OrderStatusIdHelper.ChiefTechnicalApprove);

            viewModel.CanReceiveMoney =
                viewModel.HasPermission(PermissionValue.CanReceiveMoney) &&
               (order.OrderStatusId == OrderStatusIdHelper.ChiefTechnicalDeliver ||
               order.OrderStatusId == OrderStatusIdHelper.ClientDept);

            return View(viewModel);
        }

        /// <summary>
        /// Trưởng phòng sale duyệt đơn hàng
        /// </summary>
        /// <param name="approvePriceInput"></param>
        /// <returns></returns>
        [HttpPost]
        [ClaimRequirement(MyClaimType.Permission, PermissionValue.CanApproveAlreadyCreatedOrder)]
        public JsonResult ApproveAlreadyCreatedOrder(ApproveAlreadyCreatedOrderInput approveAlreadyCreatedOrderInput)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { result = "fail", content = "Đơn hàng không hợp lệ" });
            }
            Order order = orderManageService.GetById(approveAlreadyCreatedOrderInput.OrderId);
            if (order == null)
            {
                return Json(new { result = "fail", content = "Đơn hàng không hợp lệ" });
            }
            if (approveAlreadyCreatedOrderInput.ApproveType != "approve" && approveAlreadyCreatedOrderInput.ApproveType != "reject")
                if (order.OrderStatusId != OrderStatusIdHelper.AlreadyCreated)
                {
                    return Json(new { result = "fail", content = "Đơn hàng không hợp lệ" });
                }
            try
            {
                this.unitOfWork.BeginTransaction();
                if (approveAlreadyCreatedOrderInput.ApproveType == "approve")
                {
                    order.OrderStatusId = OrderStatusIdHelper.SalesManagerApprove;
                }
                else
                {
                    order.OrderStatusId = OrderStatusIdHelper.SalesManagerReject;
                }

                orderManageService.Update(order);

                var orderEvent = orderEventManageService
                    .SalesManagerApprove(GetCurrentStaffId(), order.Id);
                loggerService.AddInfomationLogger(orderEvent.Note);
                this.unitOfWork.Commit();
                //TODO thêm thông báo cho nhân viên kế toán
                this.unitOfWork.CommitTransaction();
                return Json(new
                {
                    result = "success",
                    content = order
                });
            }
            catch (Exception e)
            {
                this.unitOfWork.RollbackTransaction();
                return Json(new { result = "fail", content = "Báo giá không hợp lệ" });
            }
        }


        /// <summary>
        /// Nhân viên kế toán đặt hàng
        /// </summary>
        /// <param name="approvePriceInput"></param>
        /// <returns></returns>
        [HttpPost]
        [ClaimRequirement(MyClaimType.Permission, PermissionValue.CanAccountantHasOrdered)]
        public JsonResult AccountantHasOrdered(ChangeExpectedReturnGoodDateInput approveAlreadyCreatedOrderInput)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { result = "fail", content = "Đơn hàng không hợp lệ" });
            }
            if (approveAlreadyCreatedOrderInput.ExpectedReturnGoodDate < DateTime.Today)
            {
                return Json(new { result = "fail", content = "Đơn hàng không hợp lệ" });
            }
            Order order = orderManageService.GetById(approveAlreadyCreatedOrderInput.OrderId);
            if (order == null)
            {
                return Json(new { result = "fail", content = "Đơn hàng không hợp lệ" });
            }
            if (order.OrderStatusId != OrderStatusIdHelper.SalesManagerApprove &&
                order.OrderStatusId != OrderStatusIdHelper.AccountingManagerReject)
            {
                return Json(new { result = "fail", content = "Đơn hàng không hợp lệ" });
            }
            order.ExpectedDeliveryDate = approveAlreadyCreatedOrderInput.ExpectedReturnGoodDate;
            try
            {
                this.unitOfWork.BeginTransaction();
                order.OrderStatusId = OrderStatusIdHelper.AccountantHasOrdered;
                orderManageService.Update(order);
                var orderEvent = orderEventManageService
                    .AccountantHasOrder(GetCurrentStaffId(), order.Id);
                loggerService.AddInfomationLogger(orderEvent.Note);
                this.unitOfWork.Commit();
                //TODO thêm thông báo cho nhân viên kế toán
                this.unitOfWork.CommitTransaction();
                return Json(new
                {
                    result = "success",
                    content = order
                });
            }
            catch (Exception e)
            {
                this.unitOfWork.RollbackTransaction();
                return Json(new { result = "fail", content = "Báo giá không hợp lệ" });
            }
        }

        [HttpPost]
        [ClaimRequirement(MyClaimType.Permission, PermissionValue.AccountingManagerApproveOrder)]
        public JsonResult AccoutingManagerApprove(AccoutingManagerApproveInput accoutingManagerApproveInput)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { result = "fail", content = "Đơn hàng không hợp lệ" });
            }
            if (accoutingManagerApproveInput.ApproveType != "approve"
                && accoutingManagerApproveInput.ApproveType != "reject")
            {
                return Json(new { result = "fail", content = "Đơn hàng không hợp lệ" });
            }
            Order order = orderManageService.GetById(accoutingManagerApproveInput.OrderId);
            if (order == null)
            {
                return Json(new { result = "fail", content = "Đơn hàng không hợp lệ" });
            }
            if (order.OrderStatusId != OrderStatusIdHelper.AccountantHasOrdered)
            {
                return Json(new { result = "fail", content = "Đơn hàng không hợp lệ" });
            }
            try
            {
                this.unitOfWork.BeginTransaction();
                if (accoutingManagerApproveInput.ApproveType == "approve")
                {
                    order.OrderStatusId = OrderStatusIdHelper.AccountingManagerApprove;
                }
                else
                {
                    order.ExpectedDeliveryDate = null;
                    order.OrderStatusId = OrderStatusIdHelper.AccountingManagerReject;
                }

                orderManageService.Update(order);
                var orderEvent = orderEventManageService
                    .AccountingManagerApproveOrder(GetCurrentStaffId(), order.Id);
                loggerService.AddInfomationLogger(orderEvent.Note);
                this.unitOfWork.Commit();
                //TODO thêm thông báo cho nhân viên kế toán
                this.unitOfWork.CommitTransaction();
                return Json(new
                {
                    result = "success",
                    content = order
                });
            }
            catch (Exception e)
            {
                this.unitOfWork.RollbackTransaction();
                return Json(new { result = "fail", content = "Báo giá không hợp lệ" });
            }
        }

        [HttpPost]
        [ClaimRequirement(MyClaimType.Permission, PermissionValue.CanUpdateGoodOnWay)]
        public JsonResult UpdateGoodOnWay(UpdateGoodOnWayInput accoutingManagerApproveInput)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { result = "fail", content = "Đơn hàng không hợp lệ" });
            }

            Order order = orderManageService.GetById(accoutingManagerApproveInput.OrderId);
            if (order == null)
            {
                return Json(new { result = "fail", content = "Đơn hàng không hợp lệ" });
            }
            if (order.OrderStatusId != OrderStatusIdHelper.AccountingManagerApprove)
            {
                return Json(new { result = "fail", content = "Đơn hàng không hợp lệ" });
            }
            try
            {
                this.unitOfWork.BeginTransaction();
                order.OrderStatusId = OrderStatusIdHelper.GoodOnWay;
                orderManageService.Update(order);
                var orderEvent = orderEventManageService
                    .GoodOnWay(GetCurrentStaffId(), order.Id);
                loggerService.AddInfomationLogger(orderEvent.Note);
                this.unitOfWork.Commit();
                //TODO thêm thông báo cho nhân viên kế toán
                this.unitOfWork.CommitTransaction();
                return Json(new
                {
                    result = "success",
                    content = order
                });
            }
            catch (Exception e)
            {
                this.unitOfWork.RollbackTransaction();
                return Json(new { result = "fail", content = "Báo giá không hợp lệ" });
            }
        }

        [HttpPost]
        [ClaimRequirement(MyClaimType.Permission, PermissionValue.CanUpdateGoodOnWay)]
        public JsonResult UpdateReadyToDeliver(UpdateGoodOnWayInput accoutingManagerApproveInput)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { result = "fail", content = "Đơn hàng không hợp lệ" });
            }

            Order order = orderManageService.GetById(accoutingManagerApproveInput.OrderId);
            if (order == null)
            {
                return Json(new { result = "fail", content = "Đơn hàng không hợp lệ" });
            }
            if (order.OrderStatusId != OrderStatusIdHelper.GoodOnWay)
            {
                return Json(new { result = "fail", content = "Đơn hàng không hợp lệ" });
            }
            try
            {
                this.unitOfWork.BeginTransaction();
                order.OrderStatusId = OrderStatusIdHelper.ReadyToDeliver;
                orderManageService.Update(order);
                var orderEvent = orderEventManageService
                    .ReadyToDeliver(GetCurrentStaffId(), order.Id);
                loggerService.AddInfomationLogger(orderEvent.Note);
                this.unitOfWork.Commit();
                //TODO thêm thông báo cho nhân viên kế toán
                this.unitOfWork.CommitTransaction();
                return Json(new
                {
                    result = "success",
                    content = order
                });
            }
            catch (Exception e)
            {
                this.unitOfWork.RollbackTransaction();
                return Json(new { result = "fail", content = "Báo giá không hợp lệ" });
            }
        }


        [HttpPost]
        [ClaimRequirement(MyClaimType.Permission, PermissionValue.CanRecommendedDelivery)]
        public JsonResult UpdateRecommendedDelivery(UpdateRecommendedDeliveryInput input)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { result = "fail", content = "Đơn hàng không hợp lệ" });
            }

            Order order = orderManageService.GetById(input.OrderId);
            if (order == null)
            {
                return Json(new { result = "fail", content = "Đơn hàng không hợp lệ" });
            }
            if (order.OrderStatusId != OrderStatusIdHelper.ReadyToDeliver)
            {
                return Json(new { result = "fail", content = "Đơn hàng không hợp lệ" });
            }
            try
            {
                this.unitOfWork.BeginTransaction();
                order.OrderStatusId = OrderStatusIdHelper.RecommendedDelivery;
                orderManageService.Update(order);
                var orderEvent = orderEventManageService
                    .ReadyToDeliver(GetCurrentStaffId(), order.Id);
                loggerService.AddInfomationLogger(orderEvent.Note);
                this.unitOfWork.Commit();
                //TODO thêm thông báo cho nhân viên kế toán
                this.unitOfWork.CommitTransaction();
                return Json(new
                {
                    result = "success",
                    content = order
                });
            }
            catch (Exception e)
            {
                this.unitOfWork.RollbackTransaction();
                return Json(new { result = "fail", content = "Báo giá không hợp lệ" });
            }
        }


        [HttpPost]
        [ClaimRequirement(MyClaimType.Permission, PermissionValue.CanTechnicalChiefApproveOrder)]
        public JsonResult TechnicalChiefApproveOrder(TechnicalChiefApproveOrderInput input)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { result = "fail", content = "Đơn hàng không hợp lệ" });
            }

            Order order = orderManageService.GetById(input.OrderId);
            if (order == null)
            {
                return Json(new { result = "fail", content = "Đơn hàng không hợp lệ" });
            }
            if (order.OrderStatusId != OrderStatusIdHelper.RecommendedDelivery)
            {
                return Json(new { result = "fail", content = "Đơn hàng không hợp lệ" });
            }
            try
            {
                this.unitOfWork.BeginTransaction();
                order.OrderStatusId = OrderStatusIdHelper.ChiefTechnicalApprove;
                orderManageService.Update(order);
                var orderEvent = orderEventManageService
                    .ChiefTechnicalApprove(GetCurrentStaffId(), order.Id);
                loggerService.AddInfomationLogger(orderEvent.Note);
                this.unitOfWork.Commit();
                //TODO thêm thông báo cho nhân viên kế toán
                this.unitOfWork.CommitTransaction();
                return Json(new
                {
                    result = "success",
                    content = order
                });
            }
            catch (Exception e)
            {
                this.unitOfWork.RollbackTransaction();
                return Json(new { result = "fail", content = "Báo giá không hợp lệ" });
            }
        }

        [HttpPost]
        [ClaimRequirement(MyClaimType.Permission, PermissionValue.CanFinishDelivery)]
        public JsonResult FinishDelivery(FinishDeliveryInput input)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { result = "fail", content = "Đơn hàng không hợp lệ" });
            }

            Order order = orderManageService.GetById(input.OrderId);
            if (order == null)
            {
                return Json(new { result = "fail", content = "Đơn hàng không hợp lệ" });
            }
            if (order.OrderStatusId != OrderStatusIdHelper.ChiefTechnicalApprove)
            {
                return Json(new { result = "fail", content = "Đơn hàng không hợp lệ" });
            }
            try
            {
                this.unitOfWork.BeginTransaction();
                order.OrderStatusId = OrderStatusIdHelper.ChiefTechnicalDeliver;
                orderManageService.Update(order);
                var orderEvent = orderEventManageService
                    .ChiefTechnicalDeliver(GetCurrentStaffId(), order.Id);
                loggerService.AddInfomationLogger(orderEvent.Note);
                this.unitOfWork.Commit();
                //TODO thêm thông báo cho nhân viên kế toán
                this.unitOfWork.CommitTransaction();
                return Json(new
                {
                    result = "success",
                    content = order
                });
            }
            catch (Exception e)
            {
                this.unitOfWork.RollbackTransaction();
                return Json(new { result = "fail", content = "Báo giá không hợp lệ" });
            }
        }


        [HttpPost]
        [ClaimRequirement(MyClaimType.Permission, PermissionValue.CanReceiveMoney)]
        public JsonResult ReceiveMoney(FinishDeliveryInput input)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { result = "fail", content = "Đơn hàng không hợp lệ" });
            }
            if (input.PaidPrice < 0)
            {
                return Json(new { result = "fail", content = "Đơn hàng không hợp lệ" });
            }
            Order order = orderManageService.GetById(input.OrderId);
            if (order == null)
            {
                return Json(new { result = "fail", content = "Đơn hàng không hợp lệ" });
            }
            if (order.OrderStatusId != OrderStatusIdHelper.ChiefTechnicalDeliver &&
                order.OrderStatusId != OrderStatusIdHelper.ClientDept)
            {
                return Json(new { result = "fail", content = "Đơn hàng không hợp lệ" });
            }
            if (input.PaidPrice > (decimal)order.RemainingPrice)
            {
                return Json(new { result = "fail", content = "Số tiền trả quá số tiền còn lại" });
            }
            try
            {
                this.unitOfWork.BeginTransaction();
                order.OrderStatusId = OrderStatusIdHelper.AccountantReceiveMoney;
                OrderEvent orderEvent = null;
                order.PaidPrice += (double)input.PaidPrice;
                if (order.PaidPrice == order.TotalPrice)
                {
                    order.OrderStatusId = OrderStatusIdHelper.AccountantReceiveMoney;
                    orderEvent = orderEventManageService
                    .AccountantReceiveMoney(GetCurrentStaffId(), order.Id);
                }
                else
                {
                    order.OrderStatusId = OrderStatusIdHelper.ClientDept;
                    orderEvent = orderEventManageService
                    .ClientDept(GetCurrentStaffId(), order.Id);
                }

                orderManageService.Update(order);
                loggerService.AddInfomationLogger(orderEvent.Note);
                this.unitOfWork.Commit();
                //TODO thêm thông báo cho nhân viên kế toán
                this.unitOfWork.CommitTransaction();
                return Json(new
                {
                    result = "success",
                    content = order
                });
            }
            catch (Exception e)
            {
                this.unitOfWork.RollbackTransaction();
                return Json(new { result = "fail", content = "Báo giá không hợp lệ" });
            }
        }
    }
}