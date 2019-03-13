using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Framework.InputModels.QoutationManagement.CreateOrder;
using Framework.Models.QoutationManagement;
using Framework.Repositories.Infrastructor;
using Framework.Services.ManageService.OrderManagement;
using Framework.Services.ManageService.QoutationManagement;
using Framework.Services.QoutationManagementService.CreateOrderService;
using Framework.Services.Shared;
using Framework.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using WebFramework.Controllers.Shared;
using WebFramework.Infrastructor;
using WebFramework.Models.QoutationManagementViewModels.CreateOrderViewModel;
using WebFramework.SignalR.Hubs;

namespace WebFramework.Controllers.QoutationManagement
{
    [ClaimRequirement(MyClaimType.Permission, PermissionValue.CanCreateOrder)]
    public class CreateOrderController : LayoutController
    {
        ICreateOrderIndexService createOrderIndexService;
        readonly IOrderManageService orderManageService;
        readonly IOrderDetailManageService orderDetailManageService;
        readonly IClientManageService clientManageService;
        readonly IProductManageService productManageService;
        readonly IOrderEventManageService orderEventManageService;
        readonly IIdConfigurationService idConfigurationService;
        readonly IUnitOfWork unitOfWork;
        public CreateOrderController(ILayoutService layoutService,
            ICreateOrderIndexService createOrderIndexService,
            IIdConfigurationService idConfigurationService,
            IOrderManageService orderManageService,
            IClientManageService clientManageService,
            IOrderEventManageService orderEventManageService,
            IProductManageService productManageService,
            IUnitOfWork unitOfWork,
            IHubContext<NotificationHub> hubcontext,
            IOrderDetailManageService orderDetailManageService) : base(layoutService, hubcontext)
        {
            this.createOrderIndexService = createOrderIndexService;
            this.orderManageService = orderManageService;
            this.orderDetailManageService = orderDetailManageService;
            this.idConfigurationService = idConfigurationService;
            this.clientManageService = clientManageService;
            this.productManageService = productManageService;
            this.orderEventManageService = orderEventManageService;
            this.unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<JsonResult> SubmitOrder(CreateOrderInput inputModel)
        {
            if (ModelState.IsValid)
            {
                if (inputModel.QoutationId == null)
                {
                    return Json(new { result = "fail", message = "Báo giá không hợp lệ" });
                }
                var qoutation = createOrderIndexService.GetQoutationById(inputModel.QoutationId.Value);
                if (qoutation == null)
                {
                    return Json(new { result = "fail", message = "Báo giá không hợp lệ" });
                }
                if (inputModel.Promotion < 0)
                {
                    return Json(new { result = "fail", message = "Đơn đặt hàng không hợp lệ" });
                }
                try
                {
                    unitOfWork.BeginTransaction();
                    Order order = new Order();
                    order.Id = this.idConfigurationService.GenerateNewId("DH");
                    order.OrderStatusId = OrderStatusIdHelper.AlreadyCreated;
                    order.CreationStaffId = GetCurrentStaffId();
                    order.ClientId = qoutation.ClientId;
                    order.CopyFrom(inputModel);
                    orderManageService.Add(order);
                    unitOfWork.Commit();

                    var qoutationDetails = createOrderIndexService.GetQoutationDetails(inputModel.QoutationId.Value);
                    decimal totalPrice = -order.Promotion;
                    foreach (var product in inputModel.Products)
                    {
                        var products = qoutationDetails.Where(x => x.ProductId == product.ProductId).ToList();
                        if (products.Count() == 0)
                        {
                            throw new Exception("Sản phẩm không hợp lệ");
                        }
                        var orderDetail = new OrderDetail();
                        orderDetail.CopyFrom(products[0]);
                        orderDetail.UnitPrice = products[0].UnitPriceSell;
                        orderDetail.VAT = products[0].VATSell;
                        orderDetail.CopyFrom(product);
                        orderDetail.OrderId = order.Id;
                        orderDetailManageService.Add(orderDetail);
                        totalPrice += orderDetail.ProductQuantity *
                            (orderDetail.UnitPrice + (decimal)((double)orderDetail.UnitPrice * orderDetail.VAT));
                    }
                    if (totalPrice < 0)
                    {
                        throw new Exception("Đơn hàng không hợp lệ");
                    }
                    order.TotalPrice = (double)totalPrice;
                    orderManageService.Update(order);

                    var orderEvent = orderEventManageService.AlreadyCreated(GetCurrentStaffId(), order.Id);
                    loggerService.AddInfomationLogger(orderEvent.Note);
                    createOrderIndexService.FixQoutationStatus(order.QoutationId.Value);

                    unitOfWork.Commit();
                    unitOfWork.CommitTransaction();

                    await AddNotification(new Framework.InputModels.HomePage.NotificationInput()
                    {
                        Content = orderEvent.Note,
                        IsSelf = false,
                        Link = "/OrderDetail/Index/" + order.Id,
                        Permission = PermissionValue.CanApproveAlreadyCreatedOrder.ToString()
                    });

                    return Json(new { result = "success" });
                }
                catch (Exception e)
                {
                    unitOfWork.RollbackTransaction();
                }
            }
            return Json(new { result = "fail", message = "Đơn đặt hàng không hợp lệ" });
        }


        [HttpPost]
        public async Task<JsonResult> SubmitBlankOrder(CreateBlankOrderInput inputModel)
        {
            if (ModelState.IsValid)
            {
                if (inputModel.Promotion < 0)
                {
                    return Json(new { result = "fail", message = "Đơn đặt hàng không hợp lệ" });
                }
                try
                {
                    unitOfWork.BeginTransaction();

                    Client client = createOrderIndexService
                        .GetClientByNameAddressPhoneNumber(inputModel.Name,
                        inputModel.Address,
                        inputModel.PhoneNumber);
                    if (client == null)
                    {
                        client = new Client();
                        client.CopyFrom(inputModel);
                        clientManageService.Add(client);
                        unitOfWork.Commit();
                    }

                    Order order = new Order();
                    order.Id = this.idConfigurationService.GenerateNewId("DH");
                    order.OrderStatusId = OrderStatusIdHelper.AlreadyCreated;
                    order.ClientId = client.Id;
                    order.CreationStaffId = GetCurrentStaffId();
                    order.CopyFrom(inputModel);
                    orderManageService.Add(order);
                    unitOfWork.Commit();


                    decimal totalPrice = -order.Promotion;
                    foreach (var product in inputModel.Products)
                    {
                        var orderDetail = new OrderDetail();
                        orderDetail.CopyFrom(product);
                        if (createOrderIndexService.GetProductById(product.ProductId) == null)
                        {
                            Product p = new Product();
                            p.Id = product.ProductId;
                            p.Name = product.ProductName;
                            p.Size = product.ProductSize;
                            p.Decription = product.ProductDetail;
                            p.Unit = product.Unit;
                            if (product.ProductImageCode != "data:,")
                            {
                                var unique = Guid.NewGuid().ToString();
                                var savePath = Path.Combine(
                                Directory.GetCurrentDirectory(), "wwwroot",
                                "upload/" + unique + ".png");
                                p.Images = "/upload/" + unique + ".png";
                                SaveBase64String(product.ProductImageCode, savePath);
                            }
                            productManageService.Add(p);
                            orderDetail.ProductId = p.Id;

                        }
                        orderDetail.OrderId = order.Id;
                        orderDetail.ProductUnit = product.Unit;
                        orderDetail.ProductDetail = product.ProductDetail;
                        orderDetail.UnitPrice = product.ProductPrice;
                        orderDetail.VAT = product.VAT;
                        orderDetailManageService.Add(orderDetail);
                        totalPrice += orderDetail.ProductQuantity *
                            (orderDetail.UnitPrice + (decimal)((double)orderDetail.UnitPrice * orderDetail.VAT));
                    }
                    if (totalPrice < 0)
                    {
                        throw new Exception("Đơn hàng không hợp lệ");
                    }
                    order.TotalPrice = (double)totalPrice;
                    orderManageService.Update(order);
                    orderEventManageService.AlreadyCreated(GetCurrentStaffId(), order.Id);
                    unitOfWork.Commit();
                    unitOfWork.CommitTransaction();

                    var orderEvent = orderEventManageService.AlreadyCreated(GetCurrentStaffId(), order.Id);
                    loggerService.AddInfomationLogger(orderEvent.Note);

                    await AddNotification(new Framework.InputModels.HomePage.NotificationInput()
                    {
                        Content = orderEvent.Note,
                        IsSelf = false,
                        Link = "/OrderDetail/Index/" + order.Id,
                        Permission = PermissionValue.AccountingManagerApproveOrder.ToString()
                    });

                    return Json(new { result = "success" });
                }
                catch (Exception e)
                {

                    unitOfWork.RollbackTransaction();
                }
            }
            return Json(new { result = "fail", message = "Đơn đặt hàng không hợp lệ" });
        }



        public IActionResult CreateBlankOrder()
        {
            var viewModel = new CreateBlankOrderViewModel();
            InitLayoutViewModel(viewModel);
            viewModel.Title = "Tạo báo giá";
            viewModel.Clients = createOrderIndexService.GetClients();
            viewModel.Products = createOrderIndexService.GetProducts();
            viewModel.Configuration = layoutService.GetCommonConfiguration();
            return View(viewModel);
        }

        public IActionResult Index(int qoutationId)
        {
            //createOrderIndexService.FixQoutationStatus(qoutationId);
            CreateOrderIndexViewModel viewModel = new CreateOrderIndexViewModel();
            viewModel.Qoutation = createOrderIndexService.GetQoutationById(qoutationId);
            if (viewModel.Qoutation == null)
            {
                return Redirect("/NotFound");
            }
            InitLayoutViewModel(viewModel);
            viewModel.Title = "Tạo đơn đặt hàng";
            viewModel.CommonConfiguration = layoutService.GetCommonConfiguration();
            return View(viewModel);
        }
    }
}