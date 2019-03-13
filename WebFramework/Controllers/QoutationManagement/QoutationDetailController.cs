using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Framework.InputModels.QoutationManagement.QoutationDetail;
using Framework.Models.QoutationManagement;
using Framework.Repositories.Infrastructor;
using Framework.Services.ManageService.QoutationManagement;
using Framework.Services.QoutationManagementService.QoutationDetailService;
using Framework.Services.Shared;
using Framework.Services.Utils;
using Framework.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using WebFramework.Controllers.Shared;
using WebFramework.Infrastructor;
using WebFramework.Models.QoutationManagementViewModels.QoutationDetailViewModel;
using WebFramework.SignalR.Hubs;

namespace WebFramework.Controllers.QoutationManagement
{
    [Authorize]
    public class QoutationDetailController : LayoutController
    {
        readonly IQoutationDetailService qoutationDetailService;
        readonly IQoutationManageService qoutationManageService;
        readonly IQoutationEventManageService qoutationEventManageService;
        readonly IQoutationDetailManageService qoutationDetailManageService;
        protected readonly IUnitOfWork unitOfWork;

        public QoutationDetailController(ILayoutService layoutService,
            IQoutationDetailService qoutationDetailService,
            IQoutationManageService qoutationManageService,
            IQoutationDetailManageService qoutationDetailManageService,
            IHubContext<NotificationHub> hubcontext,
            IUnitOfWork unitOfWork,
            IQoutationEventManageService qoutationEventManageService) : base(layoutService, hubcontext)
        {
            this.qoutationDetailService = qoutationDetailService;
            this.qoutationManageService = qoutationManageService;
            this.qoutationEventManageService = qoutationEventManageService;
            this.qoutationDetailManageService = qoutationDetailManageService;
            this.unitOfWork = unitOfWork;
        }

        public IActionResult Index(int id)
        {
            var qoutation = qoutationDetailService.GetQoutationById(id);
            if (qoutation == null)
            {
                return Redirect("/NotFound");
            }
            QoutationDetailIndexViewModel viewModel = new QoutationDetailIndexViewModel();
            InitLayoutViewModel(viewModel);

            // có thể hủy đơn
            viewModel.CanRejectQoutation = viewModel.HasPermission(PermissionValue.RejectQoutation) &&
                qoutation.QoutationStatusId != QoutationStatusIdHelper.Terminated;
            // trưởng phòng sale có thể duyệt đơn mới tạo
            viewModel.CanApproveQoutation = viewModel.HasPermission(PermissionValue.ApproveQoutation) &&
                (qoutation.QoutationStatusId == QoutationStatusIdHelper.AlreadyCreated || qoutation.QoutationStatusId == QoutationStatusIdHelper.ClientRejected);

            // nhân viên kế toán có thể điền giá mua
            viewModel.CanAujustPriceBuy = viewModel.HasPermission(PermissionValue.AdjustPriceBuy) &&
                (qoutation.QoutationStatusId == QoutationStatusIdHelper.SalesManagerApproveSalesStaff ||
                qoutation.QoutationStatusId == QoutationStatusIdHelper.AccountantRejectSellPrice);

            // nhân viên kế toán có thể điền giá bán
            viewModel.CanAujustPriceSell = viewModel.HasPermission(PermissionValue.AdjustPriceSell) &&
                (qoutation.QoutationStatusId == QoutationStatusIdHelper.AccountantFilledPriceBuy ||
                qoutation.QoutationStatusId == QoutationStatusIdHelper.AccountingManagerRejectApprovedPrice ||
                qoutation.QoutationStatusId == QoutationStatusIdHelper.SalesManagerRejectPriceAccountingDepartment);

            // trưởng phòng kế toán có thể phê duyệt giá
            viewModel.CanApprovePriceByAccountingManager = viewModel.HasPermission(PermissionValue.AccountingManagerApprovePrice) &&
                (qoutation.QoutationStatusId == QoutationStatusIdHelper.AccountantFilledPriceSell);

            // trưởng phòng sale có thể duyệt giá
            viewModel.CanApprovePriceBySalesManager = viewModel.HasPermission(PermissionValue.SalesManagerApprovePrice) &&
                (qoutation.QoutationStatusId == QoutationStatusIdHelper.AccountingManagerApprovedPrice);

            // có thể in pdf
            viewModel.CanPrintPdf =
                qoutation.QoutationStatusId == QoutationStatusIdHelper.SalesManagerApproveAccountingDepartment ||
                qoutation.QoutationStatusId == QoutationStatusIdHelper.ClientAccepted;

            // nhân viên sale có thể báo giá
            viewModel.CanQuotesPrice = viewModel.HasPermission(PermissionValue.QuotesPrice) &&
                (qoutation.QoutationStatusId == QoutationStatusIdHelper.SalesManagerApproveAccountingDepartment);

            // có thể tạo đơn đặt hàng
            viewModel.CanCreateOrder = viewModel.HasPermission(PermissionValue.CanCreateOrder) &&
                (qoutation.QoutationStatusId == QoutationStatusIdHelper.SalesManagerApproveAccountingDepartment ||
                qoutation.QoutationStatusId == QoutationStatusIdHelper.ClientAccepted);

            viewModel.Qoutation = qoutation;
            viewModel.CommonConfiguration = qoutationDetailService.GetCommonConfiguration();
            return View(viewModel);
        }

        [HttpGet]
        public JsonResult GetProduct(string productId)
        {
            var product = qoutationDetailService.GetProductById(productId);
            if (product == null)
            {
                return Json(new { result = "fail" });
            }
            return Json(new { result = "success", data = product });
        }

        /// <summary>
        /// Nhân viên kế toán điền giá nhập báo giá
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [ClaimRequirement(MyClaimType.Permission, PermissionValue.AdjustPriceBuy)]
        public async Task<JsonResult> AccountantFillPriceBuy(AccountantFillPriceInput accountantFillPriceInput)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { result = "fail", content = "Báo giá không hợp lệ" });
            }
            Qoutation qoutation = qoutationManageService.GetById(accountantFillPriceInput.QoutationId);
            if (qoutation == null)
            {
                return Json(new { result = "fail", content = "Báo giá không hợp lệ" });
            }
            if (!(qoutation.QoutationStatusId == QoutationStatusIdHelper.SalesManagerApproveSalesStaff ||
                qoutation.QoutationStatusId == QoutationStatusIdHelper.AccountantRejectSellPrice))
            {
                return Json(new { result = "fail", content = "Báo giá không hợp lệ" });
            }
            try
            {
                List<QoutationDetail> qoutationDetails = qoutationDetailService.GetQoutationDetailsByQoutationId(qoutation.Id);

                // kiểm tra có đủ sản phẩm up lên hay không
                foreach (var product in accountantFillPriceInput.Products)
                {
                    if (qoutationDetails.Find(x => x.ProductId == product.ProductId) == null || product.UnitPrice <= 0
                        || (product.VAT != 0 && product.VAT != 0.05 && product.VAT != 0.1))
                    {
                        return Json(new { result = "fail", content = "Sản phẩm không hợp lệ" });
                    }
                }

                if (qoutationDetails.Count() != accountantFillPriceInput.Products.Count())
                {
                    return Json(new { result = "fail", content = "Sản phẩm không hợp lệ" });
                }
                // cập nhập Qoutation
                this.unitOfWork.BeginTransaction();
                qoutation.QoutationStatusId = QoutationStatusIdHelper.AccountantFilledPriceBuy;
                // cập nhật giá sản phẩm
                decimal sumPrice = 0;
                foreach (var qoutationDetail in qoutationDetails)
                {
                    var qoutationDetailView = accountantFillPriceInput
                                                .Products
                                                .Where(x => x.ProductId == qoutationDetail.ProductId)
                                                .SingleOrDefault();

                    qoutationDetail.UnitPriceBuy = qoutationDetailView.UnitPrice;
                    qoutationDetail.VATBuy = qoutationDetailView.VAT;
                    qoutationDetailManageService.Update(qoutationDetail);
                    sumPrice += qoutationDetail.ProductQuantity * (qoutationDetail.UnitPriceBuy
                        + (decimal)((double)qoutationDetail.UnitPriceBuy * qoutationDetail.VATBuy));
                }
                qoutation.TotalPriceBuy = (double)sumPrice;
                qoutationManageService.Update(qoutation);
                var qoutationEvent = qoutationEventManageService.AccountantFilledPriceBuy(GetCurrentStaffId(), qoutation.Id);
                loggerService.AddInfomationLogger(qoutationEvent.Note);
                this.unitOfWork.Commit();
                //TODO thêm thông báo cho nhân viên kế toán
                this.unitOfWork.CommitTransaction();

                await AddNotification(new Framework.InputModels.HomePage.NotificationInput()
                {
                    Content = qoutationEvent.Note,
                    IsSelf = false,
                    Link = "/QoutationDetail/Index/" + qoutation.Id,
                    Permission = PermissionValue.AdjustPriceSell.ToString()
                });

                return Json(new
                {
                    result = "success",
                    content = "Thành công"
                });
            }
            catch (Exception e)
            {
                this.unitOfWork.RollbackTransaction();
            }
            return Json(new { result = "fail", content = "Báo giá không hợp lệ" });
        }


        /// <summary>
        /// Nhân viên kế toán yêu cầu nhập lại giá nhập
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        [ClaimRequirement(MyClaimType.Permission, PermissionValue.AdjustPriceSell)]
        public async Task<JsonResult> AccountantRejectPriceBuy(int qoutationId)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { result = "fail", content = "Báo giá không hợp lệ" });
            }
            Qoutation qoutation = qoutationManageService.GetById(qoutationId);
            if (qoutation == null)
            {
                return Json(new { result = "fail", content = "Báo giá không hợp lệ" });
            }
            if (!(qoutation.QoutationStatusId == QoutationStatusIdHelper.AccountantFilledPriceBuy ||
                qoutation.QoutationStatusId == QoutationStatusIdHelper.AccountingManagerRejectApprovedPrice ||
                qoutation.QoutationStatusId == QoutationStatusIdHelper.SalesManagerRejectPriceAccountingDepartment))
            {
                return Json(new { result = "fail", content = "Báo giá không hợp lệ" });
            }
            try
            {
                this.unitOfWork.BeginTransaction();
                qoutation.QoutationStatusId = QoutationStatusIdHelper.AccountantRejectSellPrice;

                qoutationManageService.Update(qoutation);
                var qoutationEvent = qoutationEventManageService.AccountantRejectPriceBuy(GetCurrentStaffId(), qoutation.Id);
                loggerService.AddInfomationLogger(qoutationEvent.Note);
                this.unitOfWork.Commit();
                //TODO thêm thông báo cho nhân viên kế toán
                this.unitOfWork.CommitTransaction();


                await AddNotification(new Framework.InputModels.HomePage.NotificationInput()
                {
                    Content = qoutationEvent.Note,
                    IsSelf = true,
                    Link = "/QoutationDetail/Index/" + qoutation.Id,
                    Permission = PermissionValue.AdjustPriceBuy.ToString(),
                    StaffId = qoutationEventManageService.GetStaffIdCreateStatus(QoutationStatusIdHelper.AccountantFilledPriceBuy,
                    qoutation.Id)
                });

                return Json(new
                {
                    result = "success",
                    content = "Thành công"
                });
            }
            catch (Exception e)
            {
                this.unitOfWork.RollbackTransaction();
            }
            return Json(new { result = "fail", content = "Báo giá không hợp lệ" });
        }

        /// <summary>
        /// Nhân viên kế toán điền giá bán báo giá
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [ClaimRequirement(MyClaimType.Permission, PermissionValue.AdjustPriceSell)]
        public async Task<JsonResult> AccountantFillPriceSell(AccountantFillPriceInput accountantFillPriceInput)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { result = "fail", content = "Báo giá không hợp lệ" });
            }
            Qoutation qoutation = qoutationManageService.GetById(accountantFillPriceInput.QoutationId);
            if (qoutation == null)
            {
                return Json(new { result = "fail", content = "Báo giá không hợp lệ" });
            }
            if (!(qoutation.QoutationStatusId == QoutationStatusIdHelper.AccountantFilledPriceBuy ||
                qoutation.QoutationStatusId == QoutationStatusIdHelper.AccountingManagerRejectApprovedPrice ||
                qoutation.QoutationStatusId == QoutationStatusIdHelper.SalesManagerRejectPriceAccountingDepartment))
            {
                return Json(new { result = "fail", content = "Báo giá không hợp lệ" });
            }
            try
            {
                List<QoutationDetail> qoutationDetails = qoutationDetailService.GetQoutationDetailsByQoutationId(qoutation.Id);

                // kiểm tra có đủ sản phẩm up lên hay không
                foreach (var product in accountantFillPriceInput.Products)
                {
                    if (qoutationDetails.Find(x => x.ProductId == product.ProductId) == null || product.UnitPrice <= 0
                        || (product.VAT != 0 && product.VAT != 0.05 && product.VAT != 0.1))
                    {
                        return Json(new { result = "fail", content = "Sản phẩm không hợp lệ" });
                    }
                }

                if (qoutationDetails.Count() != accountantFillPriceInput.Products.Count())
                {
                    return Json(new { result = "fail", content = "Sản phẩm không hợp lệ" });
                }
                // cập nhập Qoutation
                this.unitOfWork.BeginTransaction();
                qoutation.QoutationStatusId = QoutationStatusIdHelper.AccountantFilledPriceSell;
                // cập nhật giá sản phẩm
                decimal sumPrice = 0;
                foreach (var qoutationDetail in qoutationDetails)
                {
                    var qoutationDetailView = accountantFillPriceInput
                                                .Products
                                                .Where(x => x.ProductId == qoutationDetail.ProductId)
                                                .SingleOrDefault();

                    if (qoutationDetailView.UnitPrice <= qoutationDetail.UnitPriceBuy)
                    {
                        return Json(new { result = "fail", content = "Giá bán phải lớn hơn giá nhập" });
                    }

                    qoutationDetail.UnitPriceSell = qoutationDetailView.UnitPrice;
                    qoutationDetail.VATSell = qoutationDetailView.VAT;
                    qoutationDetailManageService.Update(qoutationDetail);
                    sumPrice += qoutationDetail.ProductQuantity * (qoutationDetail.UnitPriceSell
                        + (decimal)((double)qoutationDetail.UnitPriceSell * qoutationDetail.VATSell));
                }
                qoutation.TotalPriceSell = (double)sumPrice;
                qoutationManageService.Update(qoutation);
                var qoutationEvent = qoutationEventManageService.AccountantFilledPriceSell(GetCurrentStaffId(), qoutation.Id);
                loggerService.AddInfomationLogger(qoutationEvent.Note);

                await AddNotification(new Framework.InputModels.HomePage.NotificationInput()
                {
                    Content = qoutationEvent.Note,
                    IsSelf = false,
                    Link = "/QoutationDetail/Index/" + qoutation.Id,
                    Permission = PermissionValue.AccountingManagerApprovePrice.ToString()
                });

                this.unitOfWork.Commit();
                //TODO thêm thông báo cho nhân viên kế toán
                this.unitOfWork.CommitTransaction();
                return Json(new
                {
                    result = "success",
                    content = "Thành công"
                });
            }
            catch (Exception e)
            {
                this.unitOfWork.RollbackTransaction();
            }
            return Json(new { result = "fail", content = "Báo giá không hợp lệ" });
        }

        /// <summary>
        /// Trưởng phòng kế toán phê duyệt giá
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [ClaimRequirement(MyClaimType.Permission, PermissionValue.AccountingManagerApprovePrice)]
        public async Task<JsonResult> AccountingManagerApprovePrice(AccountingManagerApprovePriceInput accountingManagerApprovePriceInput)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { result = "fail", content = "Báo giá không hợp lệ" });
            }
            if (accountingManagerApprovePriceInput.ApproveType != "approve" &&
                accountingManagerApprovePriceInput.ApproveType != "reject")
            {
                return Json(new { result = "fail", content = "Báo giá không hợp lệ" });
            }
            Qoutation qoutation = qoutationManageService.GetById(accountingManagerApprovePriceInput.QoutationId);
            if (qoutation == null)
            {
                return Json(new { result = "fail", content = "Báo giá không hợp lệ" });
            }
            if (!(qoutation.QoutationStatusId == QoutationStatusIdHelper.AccountantFilledPriceSell))
            {
                return Json(new { result = "fail", content = "Báo giá không hợp lệ" });
            }

            try
            {
                // cập nhập Qoutation
                this.unitOfWork.BeginTransaction();
                QoutationEvent qoutationEvent = null;
                if (accountingManagerApprovePriceInput.ApproveType == "approve")
                {
                    qoutation.QoutationStatusId = QoutationStatusIdHelper.AccountingManagerApprovedPrice;
                    qoutationEvent = qoutationEventManageService.AccountingManagerApprovedPrice(GetCurrentStaffId(), qoutation.Id);
                }
                else
                {
                    qoutation.QoutationStatusId = QoutationStatusIdHelper.AccountingManagerRejectApprovedPrice;
                    qoutationEvent = qoutationEventManageService.AccountingManagerDontApprovedPrice(GetCurrentStaffId(), qoutation.Id);
                }
                qoutationManageService.Update(qoutation);
                loggerService.AddInfomationLogger(qoutationEvent.Note);
                this.unitOfWork.Commit();
                //TODO thêm thông báo cho nhân viên kế toán
                this.unitOfWork.CommitTransaction();

                if (accountingManagerApprovePriceInput.ApproveType == "approve")
                {
                    await AddNotification(new Framework.InputModels.HomePage.NotificationInput()
                    {
                        Content = qoutationEvent.Note,
                        IsSelf = false,
                        Link = "/QoutationDetail/Index/" + qoutation.Id,
                        Permission = PermissionValue.SalesManagerApprovePrice.ToString()
                    });
                }
                else
                {
                    await AddNotification(new Framework.InputModels.HomePage.NotificationInput()
                    {
                        Content = qoutationEvent.Note,
                        IsSelf = true,
                        Link = "/QoutationDetail/Index/" + qoutation.Id,
                        StaffId = qoutationEventManageService.GetStaffIdCreateStatus(QoutationStatusIdHelper.AccountantFilledPriceSell, qoutation.Id),
                        Permission = PermissionValue.AdjustPriceSell.ToString()
                    });
                }

                return Json(new
                {
                    result = "success",
                    content = "Thành công"
                });
            }
            catch (Exception e)
            {
                this.unitOfWork.RollbackTransaction();
            }
            return Json(new { result = "fail", content = "Báo giá không hợp lệ" });
        }

        /// <summary>
        /// Trưởng phòng sale phê duyệt giá
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [ClaimRequirement(MyClaimType.Permission, PermissionValue.SalesManagerApprovePrice)]
        public async Task<JsonResult> SalesManagerApprovePrice(SalesManagerApprovePriceInput accountingManagerApprovePriceInput)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { result = "fail", content = "Báo giá không hợp lệ" });
            }
            if (accountingManagerApprovePriceInput.ApproveType != "approve" &&
                accountingManagerApprovePriceInput.ApproveType != "reject")
            {
                return Json(new { result = "fail", content = "Báo giá không hợp lệ" });
            }
            Qoutation qoutation = qoutationManageService.GetById(accountingManagerApprovePriceInput.QoutationId);
            if (qoutation == null)
            {
                return Json(new { result = "fail", content = "Báo giá không hợp lệ" });
            }
            if (!(qoutation.QoutationStatusId == QoutationStatusIdHelper.AccountingManagerApprovedPrice))
            {
                return Json(new { result = "fail", content = "Báo giá không hợp lệ" });
            }

            try
            {
                // cập nhập Qoutation
                this.unitOfWork.BeginTransaction();
                QoutationEvent qoutationEvent = null;
                if (accountingManagerApprovePriceInput.ApproveType == "approve")
                {
                    qoutation.QoutationStatusId = QoutationStatusIdHelper.SalesManagerApproveAccountingDepartment;
                    qoutationEvent = qoutationEventManageService.AccountingManagerApprovedPrice(GetCurrentStaffId(), qoutation.Id);
                }
                else
                {
                    qoutation.QoutationStatusId = QoutationStatusIdHelper.SalesManagerRejectPriceAccountingDepartment;
                    qoutationEvent = qoutationEventManageService.AccountingManagerDontApprovedPrice(GetCurrentStaffId(), qoutation.Id);
                }
                qoutationManageService.Update(qoutation);
                loggerService.AddInfomationLogger(qoutationEvent.Note);
                this.unitOfWork.Commit();
                this.unitOfWork.CommitTransaction();

                if (accountingManagerApprovePriceInput.ApproveType == "approve")
                {
                    await AddNotification(new Framework.InputModels.HomePage.NotificationInput()
                    {
                        Content = qoutationEvent.Note,
                        IsSelf = false,
                        Link = "/QoutationDetail/Index/" + qoutation.Id,
                        Permission = PermissionValue.QuotesPrice.ToString()
                    });
                }
                else
                {
                    await AddNotification(new Framework.InputModels.HomePage.NotificationInput()
                    {
                        Content = qoutationEvent.Note,
                        IsSelf = true,
                        Link = "/QoutationDetail/Index/" + qoutation.Id,
                        StaffId = qoutationEventManageService.GetStaffIdCreateStatus(QoutationStatusIdHelper.AccountantFilledPriceSell, qoutation.Id),
                        Permission = PermissionValue.AdjustPriceSell.ToString()
                    });
                }

                return Json(new
                {
                    result = "success",
                    content = "Thành công"
                });
            }
            catch (Exception e)
            {
                this.unitOfWork.RollbackTransaction();
            }
            return Json(new { result = "fail", content = "Báo giá không hợp lệ" });
        }

        /// <summary>
        /// Nhân viên sale báo giá
        /// </summary>
        /// <param name="quotesPriceInput"></param>
        /// <returns></returns>
        [HttpPost]
        [ClaimRequirement(MyClaimType.Permission, PermissionValue.QuotesPrice)]
        public async Task<JsonResult> QuotesPrice(QuotesPriceInput quotesPriceInput)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { result = "fail", content = "Báo giá không hợp lệ" });
            }
            if (quotesPriceInput.QuotesType != "accept" &&
                quotesPriceInput.QuotesType != "reject")
            {
                return Json(new { result = "fail", content = "Báo giá không hợp lệ" });
            }
            Qoutation qoutation = qoutationManageService.GetById(quotesPriceInput.QoutationId);
            if (qoutation == null)
            {
                return Json(new { result = "fail", content = "Báo giá không hợp lệ" });
            }
            if (!(qoutation.QoutationStatusId == QoutationStatusIdHelper.SalesManagerApproveAccountingDepartment))
            {
                return Json(new { result = "fail", content = "Báo giá không hợp lệ" });
            }

            try
            {
                // cập nhập Qoutation
                this.unitOfWork.BeginTransaction();
                QoutationEvent qoutationEvent = null;
                if (quotesPriceInput.QuotesType == "accept")
                {
                    qoutation.QoutationStatusId = QoutationStatusIdHelper.ClientAccepted;
                    qoutationEvent = qoutationEventManageService.ClientAccepted(GetCurrentStaffId(), qoutation.Id);
                }
                else
                {
                    qoutation.QoutationStatusId = QoutationStatusIdHelper.ClientRejected;
                    qoutationEvent = qoutationEventManageService.ClientRejected(GetCurrentStaffId(), qoutation.Id);
                }
                qoutationManageService.Update(qoutation);
                loggerService.AddInfomationLogger(qoutationEvent.Note);
                this.unitOfWork.Commit();
                //TODO thêm thông báo cho nhân viên kế toán
                this.unitOfWork.CommitTransaction();

                if (quotesPriceInput.QuotesType != "accept")
                {
                    await AddNotification(new Framework.InputModels.HomePage.NotificationInput()
                    {
                        Content = qoutationEvent.Note,
                        IsSelf = false,
                        Link = "/QoutationDetail/Index/" + qoutation.Id,
                        Permission = PermissionValue.ApproveQoutation.ToString()
                    });
                }

                return Json(new
                {
                    result = "success",
                    content = "Thành công"
                });
            }
            catch (Exception e)
            {
                this.unitOfWork.RollbackTransaction();
            }
            return Json(new { result = "fail", content = "Báo giá không hợp lệ" });
        }


        /// <summary>
        /// Trưởng phòng sale đồng ý báo giá
        /// </summary>
        /// <param name="approvePriceInput"></param>
        /// <returns></returns>
        [HttpPost]
        [ClaimRequirement(MyClaimType.Permission, PermissionValue.ApproveQoutation)]
        public async Task<JsonResult> ApproveQoutation(ApproveQoutationInput approvePriceInput)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { result = "fail", content = "Báo giá không hợp lệ" });
            }
            Qoutation qoutation = qoutationManageService.GetById(approvePriceInput.QoutationId);
            if (qoutation == null)
            {
                return Json(new { result = "fail", content = "Báo giá không hợp lệ" });
            }
            if (qoutation.QoutationStatusId != QoutationStatusIdHelper.AlreadyCreated && qoutation.QoutationStatusId != QoutationStatusIdHelper.ClientRejected)
            {
                return Json(new { result = "fail", content = "Báo giá không hợp lệ" });
            }
            try
            {
                this.unitOfWork.BeginTransaction();
                qoutation.QoutationStatusId = QoutationStatusIdHelper.SalesManagerApproveSalesStaff;
                qoutation.SalesAdminId = GetCurrentStaffId();
                qoutation.ManagerId = qoutation.SalesAdminId;
                qoutationManageService.Update(qoutation);
                var qoutationEvent = qoutationEventManageService.SalesManagerApproveSalesStaff(GetCurrentStaffId(), qoutation.Id);
                loggerService.AddInfomationLogger(qoutationEvent.Note);
                this.unitOfWork.Commit();
                //TODO thêm thông báo cho nhân viên kế toán
                this.unitOfWork.CommitTransaction();

                await AddNotification(new Framework.InputModels.HomePage.NotificationInput()
                {
                    Content = qoutationEvent.Note,
                    IsSelf = false,
                    Link = "/QoutationDetail/Index/" + qoutation.Id,
                    Permission = PermissionValue.AdjustPriceBuy.ToString()
                });

                return Json(new
                {
                    result = "success",
                    content = qoutation
                });
            }
            catch (Exception e)
            {
                this.unitOfWork.RollbackTransaction();
            }
            return Json(new { result = "fail", content = "Báo giá không hợp lệ" });
        }


        /// <summary>
        /// Trưởng phòng sale từ chối duyệt đơn
        /// </summary>
        /// <param name="rejectQoutationInput"></param>
        /// <returns></returns>
        [HttpPost]
        [ClaimRequirement(MyClaimType.Permission, PermissionValue.RejectQoutation)]
        public JsonResult RejectQoutation(RejectQoutationInput rejectQoutationInput)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { result = "fail", content = "Báo giá không hợp lệ" });
            }
            Qoutation Qoutation = qoutationManageService.GetById(rejectQoutationInput.QoutationId);
            if (Qoutation == null)
            {
                return Json(new { result = "fail", content = "Báo giá không hợp lệ" });
            }
            try
            {
                this.unitOfWork.BeginTransaction();
                Qoutation.QoutationStatusId = QoutationStatusIdHelper.Terminated;
                Qoutation.RejectReason = rejectQoutationInput.Content;
                qoutationManageService.Update(Qoutation);
                var QoutationEvent = qoutationEventManageService.Terminated(GetCurrentStaffId(), Qoutation.Id);
                loggerService.AddWariningLogger(QoutationEvent.Note);
                //TODO thêm thông báo cho nhân viên sales
                this.unitOfWork.Commit();
                this.unitOfWork.CommitTransaction();
                return Json(new
                {
                    result = "success",
                    content = Qoutation
                });
            }
            catch (Exception e)
            {
                this.unitOfWork.RollbackTransaction();
            }
            return Json(new { result = "fail", content = "Báo giá không hợp lệ" });
        }


    }
}