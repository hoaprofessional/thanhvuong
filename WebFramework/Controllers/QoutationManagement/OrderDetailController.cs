using Framework.InputModels.QoutationManagement.OrderDetail;
using Framework.Models.QoutationManagement;
using Framework.Repositories.Infrastructor;
using Framework.Services.ManageService.OrderManagement;
using Framework.Services.ManageService.QoutationManagement;
using Framework.Services.QoutationManagementService.OrderDetailService;
using Framework.Services.Shared;
using Framework.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebFramework.Controllers.Shared;
using WebFramework.Infrastructor;
using WebFramework.Models.QoutationManagementViewModels.OrderDetailViewModel;
using WebFramework.SignalR.Hubs;

namespace WebFramework.Controllers.QoutationManagement
{
    public class OrderDetailController : LayoutController
    {
        private readonly IOrderDetailIndexService orderDetailIndexService;
        private readonly IOrderManageService orderManageService;
        private readonly IOrderEventManageService orderEventManageService;
        private readonly IOrderDetailManageService orderDetailManageService;
        private readonly IUnitOfWork unitOfWork;
        public OrderDetailController(ILayoutService layoutService,
            IOrderManageService orderManageService,
            IOrderEventManageService orderEventManageService,
            IOrderDetailManageService orderDetailManageService,
            IHubContext<NotificationHub> hubcontext,
            IUnitOfWork unitOfWork,
            IOrderDetailIndexService orderDetailIndexService)
            : base(layoutService, hubcontext)
        {
            this.orderDetailIndexService = orderDetailIndexService;
            this.orderManageService = orderManageService;
            this.orderEventManageService = orderEventManageService;
            this.orderDetailManageService = orderDetailManageService;
            this.unitOfWork = unitOfWork;
        }

        public IActionResult Index(string id)
        {
            OrderDetailIndexViewModel viewModel = new OrderDetailIndexViewModel();
            InitLayoutViewModel(viewModel);
            viewModel.Order = orderDetailIndexService.GetOrderById(id);
            viewModel.CommonConfiguration = orderDetailIndexService.GetCommonConfiguration();
            Order order = viewModel.Order;

            viewModel.CanApproveAlreadyCreatedOrder =
                viewModel.HasPermission(PermissionValue.CanApproveAlreadyCreatedOrder) &&
               (order.OrderStatusId == OrderStatusIdHelper.AlreadyCreated);

            viewModel.CanAccountantHasOrdered = false;

            viewModel.CanChangeQuantityProduct =
                viewModel.HasPermission(PermissionValue.CanAccountantHasOrdered) &&
               (order.OrderStatusId == OrderStatusIdHelper.AccountingManagerReject || order.OrderStatusId == OrderStatusIdHelper.SalesManagerApprove);

            viewModel.CanChangeExpectedReturnGoodDate =
                viewModel.HasPermission(PermissionValue.CanAccountantHasOrdered) &&
                (order.StopUpdateExpectedReturnGoodDate != true) &&
               (order.OrderStatusId != OrderStatusIdHelper.SalesManagerApprove &&
               order.OrderStatusId != OrderStatusIdHelper.AccountantHasOrdered &&
               order.OrderStatusId != OrderStatusIdHelper.AccountingManagerApprove &&
               order.OrderStatusId != OrderStatusIdHelper.AccountingManagerReject);

            viewModel.CanAccountingManagerApprove =
                viewModel.HasPermission(PermissionValue.AccountingManagerApproveOrder) &&
               (order.OrderStatusId == OrderStatusIdHelper.AccountantHasOrdered || order.OrderStatusId == OrderStatusIdHelper.RejectOrder);

            viewModel.CanConfirmOrder = viewModel.HasPermission(PermissionValue.ConfirmOrder) &&
            (order.OrderStatusId == OrderStatusIdHelper.AccountingManagerApprove);

            viewModel.CanUpdateGoodOnWay =
                viewModel.HasPermission(PermissionValue.CanUpdateGoodOnWay) &&
               (order.OrderStatusId == OrderStatusIdHelper.ConfirmOrder);

            viewModel.CanUpdateReadyToDeliver =
                viewModel.HasPermission(PermissionValue.CanUpdateReadyToDeliver) &&
               (order.OrderStatusId == OrderStatusIdHelper.GoodOnWay);

            viewModel.CanRecommendedDelivery =
                viewModel.HasPermission(PermissionValue.CanRecommendedDelivery) &&
               (order.OrderStatusId == OrderStatusIdHelper.ReadyToDeliver);

            viewModel.CanTechnicalChiefApproveOrder =
                viewModel.HasPermission(PermissionValue.CanTechnicalChiefApproveOrder) &&
               (order.OrderStatusId == OrderStatusIdHelper.RecommendedDelivery ||
               order.OrderStatusId == OrderStatusIdHelper.ChiefTechnicalApprove);

            viewModel.CanFinishDelivery =
                viewModel.HasPermission(PermissionValue.CanFinishDelivery) &&
               (order.OrderStatusId == OrderStatusIdHelper.ChiefTechnicalApprove);


            viewModel.CanReceiveMoney =
                viewModel.HasPermission(PermissionValue.CanReceiveMoney) &&
               (order.OrderStatusId == OrderStatusIdHelper.ChiefTechnicalDeliver ||
               order.OrderStatusId == OrderStatusIdHelper.ClientDept);

            return View(viewModel);
        }


        [HttpPost]
        [ClaimRequirement(MyClaimType.Permission, PermissionValue.ConfirmOrder)]
        public async Task<JsonResult> ConfirmOrder(ConfirmOrderInput input)
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
            if (input.ApproveType != "approve" && input.ApproveType != "reject")
            {
                if (order.OrderStatusId != OrderStatusIdHelper.AlreadyCreated)
                {
                    return Json(new { result = "fail", content = "Đơn hàng không hợp lệ" });
                }
            }

            try
            {
                OrderEvent orderEvent = null;
                if (input.ApproveType == "approve")
                {
                    order.OrderStatusId = OrderStatusIdHelper.ConfirmOrder;
                    orderEvent = orderEventManageService
                     .ManagerConfirm(GetCurrentStaffId(), order.Id);

                    orderManageService.Update(order);

                    loggerService.AddInfomationLogger(orderEvent.Note);
                }
                else
                {
                    order.OrderStatusId = OrderStatusIdHelper.RejectOrder;
                    orderEvent = orderEventManageService
                     .ManagerReject(GetCurrentStaffId(), order.Id);

                    orderManageService.Update(order);

                    loggerService.AddInfomationLogger(orderEvent.Note);
                }
                unitOfWork.Commit();
                //TODO thêm thông báo cho nhân viên kế toán
                unitOfWork.CommitTransaction();
                await AddNotification(new Framework.InputModels.HomePage.NotificationInput()
                {
                    Content = orderEvent.Note,
                    IsSelf = false,
                    Link = "/OrderDetail/Index/" + order.Id,
                    Permission = PermissionValue.ConfirmOrder.ToString()
                });
                return Json(new
                {
                    result = "success",
                    content = order
                });
            }
            catch (Exception)
            {
                unitOfWork.RollbackTransaction();
                return Json(new { result = "fail", content = "Báo giá không hợp lệ" });
            }
        }


        /// <summary>
        /// Trưởng phòng sale duyệt đơn hàng
        /// </summary>
        /// <param name="approvePriceInput"></param>
        /// <returns></returns>
        [HttpPost]
        [ClaimRequirement(MyClaimType.Permission, PermissionValue.CanApproveAlreadyCreatedOrder)]
        public async Task<JsonResult> ApproveAlreadyCreatedOrder(ApproveAlreadyCreatedOrderInput approveAlreadyCreatedOrderInput)
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
            {
                if (order.OrderStatusId != OrderStatusIdHelper.AlreadyCreated)
                {
                    return Json(new { result = "fail", content = "Đơn hàng không hợp lệ" });
                }
            }

            try
            {
                unitOfWork.BeginTransaction();
                OrderEvent orderEvent = null;
                if (approveAlreadyCreatedOrderInput.ApproveType == "approve")
                {
                    order.OrderStatusId = OrderStatusIdHelper.SalesManagerApprove;
                    orderEvent = orderEventManageService
                     .SalesManagerApprove(GetCurrentStaffId(), order.Id);
                }
                else
                {
                    order.OrderStatusId = OrderStatusIdHelper.SalesManagerReject;
                    orderEvent = orderEventManageService
                     .SalesManagerReject(GetCurrentStaffId(), order.Id);
                }

                orderManageService.Update(order);

                loggerService.AddInfomationLogger(orderEvent.Note);
                unitOfWork.Commit();
                //TODO thêm thông báo cho nhân viên kế toán
                unitOfWork.CommitTransaction();
                if (approveAlreadyCreatedOrderInput.ApproveType == "approve")
                {
                    await AddNotification(new Framework.InputModels.HomePage.NotificationInput()
                    {
                        Content = orderEvent.Note,
                        IsSelf = false,
                        Link = "/OrderDetail/Index/" + order.Id,
                        Permission = PermissionValue.CanAccountantHasOrdered.ToString()
                    });
                }
                return Json(new
                {
                    result = "success",
                    content = order
                });
            }
            catch (Exception)
            {
                unitOfWork.RollbackTransaction();
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
        public async Task<JsonResult> AccountantChangeQuantityProduct(AccountantChangeQuantityProductInput input)
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

            if (order.StopUpdateExpectedReturnGoodDate == true)
            {
                return Json(new { result = "fail", content = "Đơn hàng không hợp lệ" });
            }

            if (!(order.OrderStatusId == OrderStatusIdHelper.AccountingManagerReject || order.OrderStatusId == OrderStatusIdHelper.SalesManagerApprove))
            {
                return Json(new { result = "fail", content = "Đơn hàng không hợp lệ" });
            }
            try
            {
                unitOfWork.BeginTransaction();
                order.OrderStatusId = OrderStatusIdHelper.AccountantHasOrdered;


                foreach (AccountantChangeQuantityProductItemInput product in input.Products)
                {
                    OrderDetail orderDetail = orderDetailIndexService.GetOrderDetailByProductId(order.Id, product.ProductId);
                    if (orderDetail == null)
                    {
                        return Json(new { result = "fail", content = "Đơn hàng không hợp lệ" });
                    }
                    orderDetail.ProductQuantity = product.Quantity;
                    orderDetail.UnitPrice = product.UnitPrice;
                    orderDetailManageService.Update(orderDetail);
                }
                unitOfWork.Commit();
                List<OrderDetail> orderDetails = orderDetailIndexService.GetOrderDetailsByOrderId(order.Id);

                order.TotalPrice = orderDetails.Sum(x => x.TotalPrice);

                orderManageService.Update(order);
                OrderEvent orderEvent = orderEventManageService
                    .AccountantHasOrder(GetCurrentStaffId(), order.Id);
                loggerService.AddInfomationLogger(orderEvent.Note);
                unitOfWork.Commit();
                //TODO thêm thông báo cho nhân viên kế toán
                unitOfWork.CommitTransaction();

                await AddNotification(new Framework.InputModels.HomePage.NotificationInput()
                {
                    Content = orderEvent.Note,
                    IsSelf = false,
                    Link = "/OrderDetail/Index/" + order.Id,
                    Permission = PermissionValue.AccountingManagerApproveOrder.ToString()
                });

                return Json(new
                {
                    result = "success",
                    content = ""
                });
            }
            catch (Exception)
            {
                unitOfWork.RollbackTransaction();
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
        public JsonResult AccountantHasOrdered(AccountantHasOrderedInput accountantHasOrderedInput)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { result = "fail", content = "Đơn hàng không hợp lệ" });
            }
            Order order = orderManageService.GetById(accountantHasOrderedInput.OrderId);
            if (order == null)
            {
                return Json(new { result = "fail", content = "Đơn hàng không hợp lệ" });
            }

            if (order.StopUpdateExpectedReturnGoodDate == true)
            {
                return Json(new { result = "fail", content = "Đơn hàng không hợp lệ" });
            }

            if (!(order.OrderStatusId == OrderStatusIdHelper.SalesManagerApprove))
            {
                return Json(new { result = "fail", content = "Đơn hàng không hợp lệ" });
            }
            try
            {
                unitOfWork.BeginTransaction();
                order.OrderStatusId = OrderStatusIdHelper.AccountantHasOrdered;
                orderManageService.Update(order);
                OrderEvent orderEvent = orderEventManageService
                    .AccountantHasOrder(GetCurrentStaffId(), order.Id);
                loggerService.AddInfomationLogger(orderEvent.Note);
                unitOfWork.Commit();
                //TODO thêm thông báo cho nhân viên kế toán
                unitOfWork.CommitTransaction();
                return Json(new
                {
                    result = "success",
                    content = order
                });
            }
            catch (Exception)
            {
                unitOfWork.RollbackTransaction();
                return Json(new { result = "fail", content = "Báo giá không hợp lệ" });
            }
        }

        /// <summary>
        /// Thay đổi ngày dự kiến hàng về
        /// </summary>
        /// <param name="approvePriceInput"></param>
        /// <returns></returns>
        [HttpPost]
        [ClaimRequirement(MyClaimType.Permission, PermissionValue.CanAccountantHasOrdered)]
        public JsonResult ChangeExpectedReturnGoodDate(ChangeExpectedReturnGoodDateInput approveAlreadyCreatedOrderInput)
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

            if (order.StopUpdateExpectedReturnGoodDate == true)
            {
                return Json(new { result = "fail", content = "Đơn hàng không hợp lệ" });
            }

            if (!(order.StopUpdateExpectedReturnGoodDate != true) &&
               (order.OrderStatusId != OrderStatusIdHelper.SalesManagerApprove &&
               order.OrderStatusId != OrderStatusIdHelper.AccountantHasOrdered &&
               order.OrderStatusId != OrderStatusIdHelper.AccountingManagerReject))
            {
                return Json(new { result = "fail", content = "Đơn hàng không hợp lệ" });
            }
            order.ExpectedReturnGoodDate = approveAlreadyCreatedOrderInput.ExpectedReturnGoodDate;
            try
            {
                unitOfWork.BeginTransaction();
                if (order.OrderStatusId == OrderStatusIdHelper.SalesManagerApprove)
                {
                    order.OrderStatusId = OrderStatusIdHelper.AccountantHasOrdered;
                }
                orderManageService.Update(order);
                OrderEvent orderEvent = orderEventManageService
                    .AccoutantChangeReturnGoodDate(GetCurrentStaffId(), order.Id);
                loggerService.AddInfomationLogger(orderEvent.Note);
                unitOfWork.Commit();
                //TODO thêm thông báo cho nhân viên kế toán
                unitOfWork.CommitTransaction();
                return Json(new
                {
                    result = "success",
                    content = order
                });
            }
            catch (Exception)
            {
                unitOfWork.RollbackTransaction();
                return Json(new { result = "fail", content = "Báo giá không hợp lệ" });
            }
        }

        [HttpPost]
        [ClaimRequirement(MyClaimType.Permission, PermissionValue.AccountingManagerApproveOrder)]
        public async Task<JsonResult> AccoutingManagerApprove(AccoutingManagerApproveInput accoutingManagerApproveInput)
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
            if (order.OrderStatusId != OrderStatusIdHelper.AccountantHasOrdered && order.OrderStatusId != OrderStatusIdHelper.RejectOrder)
            {
                return Json(new { result = "fail", content = "Đơn hàng không hợp lệ" });
            }
            try
            {
                unitOfWork.BeginTransaction();
                OrderEvent orderEvent = null;
                if (accoutingManagerApproveInput.ApproveType == "approve")
                {
                    order.OrderStatusId = OrderStatusIdHelper.AccountingManagerApprove;
                    orderEvent = orderEventManageService
                    .AccountingManagerApproveOrder(GetCurrentStaffId(), order.Id);
                }
                else
                {
                    order.ExpectedReturnGoodDate = null;
                    order.OrderStatusId = OrderStatusIdHelper.AccountingManagerReject;
                    orderEvent = orderEventManageService
                    .AccountingManagerRejectOrder(GetCurrentStaffId(), order.Id);
                }

                orderManageService.Update(order);
                loggerService.AddInfomationLogger(orderEvent.Note);
                unitOfWork.Commit();
                //TODO thêm thông báo cho nhân viên kế toán
                unitOfWork.CommitTransaction();


                if (accoutingManagerApproveInput.ApproveType == "approve")
                {
                    await AddNotification(new Framework.InputModels.HomePage.NotificationInput()
                    {
                        Content = orderEvent.Note,
                        IsSelf = false,
                        Link = "/OrderDetail/Index/" + order.Id,
                        Permission = PermissionValue.CanUpdateGoodOnWay.ToString()
                    });
                }
                else
                {
                    await AddNotification(new Framework.InputModels.HomePage.NotificationInput()
                    {
                        Content = orderEvent.Note,
                        IsSelf = true,
                        Link = "/OrderDetail/Index/" + order.Id,
                        Permission = PermissionValue.CanAccountantHasOrdered.ToString(),
                        StaffId = orderEventManageService.GetStaffIdCreateStatus(OrderStatusIdHelper.AccountantHasOrdered, order.Id)
                    });
                }

                return Json(new
                {
                    result = "success",
                    content = order
                });
            }
            catch (Exception)
            {
                unitOfWork.RollbackTransaction();
                return Json(new { result = "fail", content = "Báo giá không hợp lệ" });
            }
        }

        [HttpPost]
        [ClaimRequirement(MyClaimType.Permission, PermissionValue.CanUpdateGoodOnWay)]
        public async Task<JsonResult> UpdateGoodOnWay(UpdateGoodOnWayInput updateGoodOnWayInput)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { result = "fail", content = "Đơn hàng không hợp lệ" });
            }

            Order order = orderManageService.GetById(updateGoodOnWayInput.OrderId);
            if (order == null)
            {
                return Json(new { result = "fail", content = "Đơn hàng không hợp lệ" });
            }
            if (order.OrderStatusId != OrderStatusIdHelper.ConfirmOrder)
            {
                return Json(new { result = "fail", content = "Đơn hàng không hợp lệ" });
            }
            try
            {
                unitOfWork.BeginTransaction();
                order.OrderStatusId = OrderStatusIdHelper.GoodOnWay;
                orderManageService.Update(order);
                OrderEvent orderEvent = orderEventManageService
                    .GoodOnWay(GetCurrentStaffId(), order.Id);
                loggerService.AddInfomationLogger(orderEvent.Note);
                unitOfWork.Commit();
                //TODO thêm thông báo cho nhân viên kế toán
                unitOfWork.CommitTransaction();

                await AddNotification(new Framework.InputModels.HomePage.NotificationInput()
                {
                    Content = orderEvent.Note,
                    IsSelf = false,
                    Link = "/OrderDetail/Index/" + order.Id,
                    Permission = PermissionValue.AccountingManagerApproveOrder.ToString()
                });

                return Json(new
                {
                    result = "success",
                    content = order
                });
            }
            catch (Exception)
            {
                unitOfWork.RollbackTransaction();
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
                unitOfWork.BeginTransaction();
                order.OrderStatusId = OrderStatusIdHelper.ReadyToDeliver;
                order.StopUpdateExpectedReturnGoodDate = true;
                orderManageService.Update(order);
                OrderEvent orderEvent = orderEventManageService
                    .ReadyToDeliver(GetCurrentStaffId(), order.Id);
                loggerService.AddInfomationLogger(orderEvent.Note);
                unitOfWork.Commit();
                //TODO thêm thông báo cho nhân viên kế toán
                unitOfWork.CommitTransaction();

                return Json(new
                {
                    result = "success",
                    content = order
                });
            }
            catch (Exception)
            {
                unitOfWork.RollbackTransaction();
                return Json(new { result = "fail", content = "Báo giá không hợp lệ" });
            }
        }


        [HttpPost]
        [ClaimRequirement(MyClaimType.Permission, PermissionValue.CanRecommendedDelivery)]
        public async Task<JsonResult> UpdateRecommendedDelivery(UpdateRecommendedDeliveryInput input)
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
                unitOfWork.BeginTransaction();
                order.OrderStatusId = OrderStatusIdHelper.RecommendedDelivery;
                orderManageService.Update(order);
                OrderEvent orderEvent = orderEventManageService
                    .RecomendDeliver(GetCurrentStaffId(), order.Id);
                loggerService.AddInfomationLogger(orderEvent.Note);
                unitOfWork.Commit();
                //TODO thêm thông báo cho nhân viên kế toán
                unitOfWork.CommitTransaction();
                await AddNotification(new Framework.InputModels.HomePage.NotificationInput()
                {
                    Content = orderEvent.Note,
                    IsSelf = false,
                    Link = "/OrderDetail/Index/" + order.Id,
                    Permission = PermissionValue.CanTechnicalChiefApproveOrder.ToString()
                });
                return Json(new
                {
                    result = "success",
                    content = order
                });
            }
            catch (Exception)
            {
                unitOfWork.RollbackTransaction();
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

            if (input.ExpectedDeliveryDate < DateTime.Today)
            {
                return Json(new { result = "fail", content = "Đơn hàng không hợp lệ" });
            }

            Order order = orderManageService.GetById(input.OrderId);
            if (order == null)
            {
                return Json(new { result = "fail", content = "Đơn hàng không hợp lệ" });
            }
            if (!(order.OrderStatusId == OrderStatusIdHelper.RecommendedDelivery ||
               order.OrderStatusId == OrderStatusIdHelper.ChiefTechnicalApprove))
            {
                return Json(new { result = "fail", content = "Đơn hàng không hợp lệ" });
            }

            try
            {
                unitOfWork.BeginTransaction();
                order.OrderStatusId = OrderStatusIdHelper.ChiefTechnicalApprove;
                order.ExpectedDeliveryDate = input.ExpectedDeliveryDate;
                orderManageService.Update(order);
                OrderEvent orderEvent = orderEventManageService
                    .ChiefTechnicalApprove(GetCurrentStaffId(), order.Id);
                loggerService.AddInfomationLogger(orderEvent.Note);
                unitOfWork.Commit();
                //TODO thêm thông báo cho nhân viên kế toán
                unitOfWork.CommitTransaction();
                return Json(new
                {
                    result = "success",
                    content = order
                });
            }
            catch (Exception)
            {
                unitOfWork.RollbackTransaction();
                return Json(new { result = "fail", content = "Báo giá không hợp lệ" });
            }
        }

        [HttpPost]
        [ClaimRequirement(MyClaimType.Permission, PermissionValue.CanFinishDelivery)]
        public async Task<JsonResult> FinishDelivery(FinishDeliveryInput input)
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
                unitOfWork.BeginTransaction();
                order.OrderStatusId = OrderStatusIdHelper.ChiefTechnicalDeliver;
                orderManageService.Update(order);
                OrderEvent orderEvent = orderEventManageService
                    .ChiefTechnicalDeliver(GetCurrentStaffId(), order.Id);
                loggerService.AddInfomationLogger(orderEvent.Note);
                unitOfWork.Commit();
                //TODO thêm thông báo cho nhân viên kế toán
                unitOfWork.CommitTransaction();
                await AddNotification(new Framework.InputModels.HomePage.NotificationInput()
                {
                    Content = orderEvent.Note,
                    IsSelf = false,
                    Link = "/OrderDetail/Index/" + order.Id,
                    Permission = PermissionValue.CanReceiveMoney.ToString()
                });
                return Json(new
                {
                    result = "success",
                    content = order
                });
            }
            catch (Exception)
            {
                unitOfWork.RollbackTransaction();
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
                unitOfWork.BeginTransaction();
                order.OrderStatusId = OrderStatusIdHelper.AccountantReceiveMoney;
                OrderEvent orderEvent = null;
                order.PaidPrice += (double)input.PaidPrice;
                if (Math.Abs(order.PaidPrice - order.TotalPrice) < 100)
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
                unitOfWork.Commit();
                //TODO thêm thông báo cho nhân viên kế toán
                unitOfWork.CommitTransaction();
                return Json(new
                {
                    result = "success",
                    content = order
                });
            }
            catch (Exception)
            {
                unitOfWork.RollbackTransaction();
                return Json(new { result = "fail", content = "Báo giá không hợp lệ" });
            }
        }


        [HttpPost]
        [ClaimRequirement(MyClaimType.Permission, PermissionValue.CanReceiveMoney)]
        public JsonResult ChangeNote(NoteOrderInput input)
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
            if (order.OrderStatusId != OrderStatusIdHelper.ChiefTechnicalDeliver &&
                order.OrderStatusId != OrderStatusIdHelper.ClientDept)
            {
                return Json(new { result = "fail", content = "Đơn hàng không hợp lệ" });
            }
            try
            {
                unitOfWork.BeginTransaction();
                order.Note = input.Note;
                orderManageService.Update(order);
                loggerService.AddInfomationLogger("Thêm note vào đơn hàng:" + input.Note);
                unitOfWork.Commit();
                //TODO thêm thông báo cho nhân viên kế toán
                unitOfWork.CommitTransaction();
                return Json(new
                {
                    result = "success",
                    content = order
                });
            }
            catch (Exception)
            {
                unitOfWork.RollbackTransaction();
                return Json(new { result = "fail", content = "Báo giá không hợp lệ" });
            }
        }
    }
}