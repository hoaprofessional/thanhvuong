using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Framework.InputModels.HomePage;
using Framework.InputModels.QoutationManagement.CreateQoutation;
using Framework.Models.QoutationManagement;
using Framework.Repositories.Infrastructor;
using Framework.Services.ManageService.QoutationManagement;
using Framework.Services.QoutationManagementService.CreateQoutation;
using Framework.Services.Shared;
using Framework.Services.Utils;
using Framework.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using WebFramework.Controllers.Shared;
using WebFramework.Infrastructor;
using WebFramework.Models.QoutationManagementViewModels.CreateQoutationViewModel;
using WebFramework.SignalR.Hubs;

namespace WebFramework.Controllers.QoutationManagement
{
    [Authorize()]
    [ClaimRequirement(MyClaimType.Permission, PermissionValue.CreateQoutation)]
    public class CreateQoutationController : LayoutController
    {
        ICreateQoutationIndexService createQoutationIndexService;

        IProductManageService productManageService;
        IQoutationManageService qoutationManageService;
        IQoutationDetailManageService qoutationDetailManageService;
        IClientManageService clientManageService;
        IQoutationEventManageService qoutationEventManageService;
        IIdConfigurationService idConfigurationService;
        ILoggerService loggerService;
        IUnitOfWork unitOfWork1;
        public CreateQoutationController(ILayoutService layoutService,
            ICreateQoutationIndexService createQoutationIndexService,
            IQoutationManageService qoutationManageService,
            IQoutationDetailManageService qoutationDetailManageService,
            IClientManageService clientManageService,
            IQoutationEventManageService qoutationEventManageService,
            IProductManageService productManageService,
            IIdConfigurationService idConfigurationService,
            ILoggerService loggerService,
            IHubContext<NotificationHub> hubcontext,
            IUnitOfWork unitOfWork1) : base(layoutService, hubcontext)
        {
            this.createQoutationIndexService = createQoutationIndexService;
            this.qoutationManageService = qoutationManageService;
            this.qoutationDetailManageService = qoutationDetailManageService;
            this.unitOfWork1 = unitOfWork1;
            this.clientManageService = clientManageService;
            this.qoutationEventManageService = qoutationEventManageService;
            this.productManageService = productManageService;
            this.idConfigurationService = idConfigurationService;
            this.loggerService = loggerService;
        }

        public async Task<JsonResult> SubmitQoutation(CreateQoutationInput createQoutationInput)
        {
            if (!ModelState.IsValid)
                return Json(new { result = "fail", message = "Dữ liệu nhập không đúng" });
            if (createQoutationInput == null || createQoutationInput.Products.Count() == 0)
            {
                return Json(new { result = "fail", message = "Dữ liệu nhập không đúng" });
            }
            try
            {
                unitOfWork1.BeginTransaction();
                Client client = createQoutationIndexService.GetClientByNameAddressPhoneNumber(createQoutationInput.Name, createQoutationInput.Address, createQoutationInput.PhoneNumber);
                if (client == null)
                {
                    client = new Client();
                    client.CopyFrom(createQoutationInput);
                    clientManageService.Add(client);
                    unitOfWork1.Commit();
                }
                var qoutation = new Qoutation();
                qoutation.EstimatedInstallationTime = createQoutationInput.EstimatedInstallationTime;
                qoutation.DeliveryPlace = createQoutationInput.DeliveryPlace;
                qoutation.QoutationStatusId = QoutationStatusIdHelper.AlreadyCreated;
                qoutation.ClientId = client.Id;
                string currentStaffId = GetCurrentStaffId();
                qoutation.QouteStaffId = currentStaffId;
                qoutation.ConfirmStaffId = currentStaffId;
                qoutation.PaymentMethod = @"<ul>
                        <li>- Tiền mặt hoặc chuyển khoản</li>
                        <li>- Thanh toán 100 % báo giá trong vòng 7 ngày sau khi bàn giao nghiệm thu</li>
                        </ul>";
                qoutation = qoutationManageService.Add(qoutation);
                unitOfWork1.Commit();

                var userId = layoutService.GetUserByUserName(qoutation.CreationUserName).Id;
                var staff = layoutService.GetStaffByUserId(userId);

                if (staff == null)
                {
                    return Json(new { result = "fail", message = "Dữ liệu nhập không đúng" });
                }

                foreach (var product in createQoutationInput.Products)
                {
                    var qoutationDetail = new QoutationDetail();
                    qoutationDetail.CopyFrom(product);
                    if (createQoutationIndexService.GetProductById(product.ProductId) == null)
                    {
                        Product p = new Product();
                        p.Id = product.ProductId;
                        p.Name = qoutationDetail.ProductName;
                        p.Decription = qoutationDetail.ProductDetail;
                        p.Size = qoutationDetail.ProductSize;
                        p.Unit = qoutationDetail.ProductUnit;
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
                    }
                    qoutationDetail.ProductId = product.ProductId;
                    qoutationDetail.QoutationId = qoutation.Id;
                    qoutationDetailManageService.Add(qoutationDetail);
                }
                var qoutationEvent = qoutationEventManageService.AlreadyCreated(staff.Id, qoutation.Id);
                loggerService.AddInfomationLogger(qoutationEvent.Note);
                unitOfWork1.Commit();
                unitOfWork1.CommitTransaction();
                var permission = layoutService.GetPermissionNotification(qoutation.QoutationStatusId);
                string contentNotification = String.Format("Đơn hàng {0} vừa được nhân viên {1} tạo", qoutation.Id, staff.Name);
                await AddNotification(new NotificationInput()
                {
                    Content = contentNotification,
                    Link = "/QoutationDetail/Index/" + qoutation.Id,
                    Permission = PermissionValue.ApproveQoutation.ToString(),
                    IsSelf = false
                });
            }

            catch (Exception e)
            {
                //unitOfWork1.RollbackTransaction();
                //unitOfWork1.BeginTransaction();
                //loggerService.AddInfomationLogger(e.Message);
                //unitOfWork1.Commit();
                //unitOfWork1.CommitTransaction();
                string detail = e.ToLogString(Environment.StackTrace);
                return Json(new
                {
                    result = "fail",
                    content = detail
                });
            }
            return Json(new { result = "success" });

        }
        public IActionResult Index()
        {
            CreateQoutationIndexViewModel viewModel = new CreateQoutationIndexViewModel();
            InitLayoutViewModel(viewModel);
            viewModel.Title = "Tạo báo giá";
            viewModel.Clients = this.createQoutationIndexService.GetClients();
            viewModel.Products = this.createQoutationIndexService.GetProducts();
            viewModel.Configuration = this.layoutService.GetCommonConfiguration();
            return View(viewModel);
        }

    }
}