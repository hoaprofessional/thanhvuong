using Framework.Models.QoutationManagement;
using Framework.Repositories.Infrastructor;
using Framework.Services.Admins.QoutationDetails.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WebCore.Services.Share.Admins.QoutationDetails;
using WebCore.Services.Share.Admins.QoutationDetails.Dto;
using WebCore.Services.Share.Commons.Permissions;
using WebCore.Utils.Config;
using WebCore.Utils.ModelHelper;
using WebFramework.Areas.Management.Models;
using WebFramework.Infrastructor;

namespace WebFramework.Areas.Management.Controllers
{
    [Area("Management")]
    [ClaimRequirement(MyClaimType.Permission, PermissionValue.RegisterUser)]
    public class QoutationDetailController : AdminBaseController
    {
        private readonly IQoutationDetailService qoutationDetailService;
        private readonly IPermissionService permissionService;
        private readonly IUnitOfWork unitOfWork;

        public QoutationDetailController(IServiceProvider serviceProvider, IQoutationDetailService userService, IPermissionService permissionService, IUnitOfWork unitOfWork) : base(serviceProvider)
        {
            this.qoutationDetailService = userService;
            this.unitOfWork = unitOfWork;
            this.permissionService = permissionService;
        }

        public IActionResult Index(int page = 1,int qoutationId = 0)
        {
            QoutationDetailFilterInput filterInput = GetFilterInSession<QoutationDetailFilterInput>("QoutationDetailSession");
            filterInput.QoutationId = qoutationId;
            filterInput.PageNumber = page;
            QoutationDetailViewModel userViewModel = new QoutationDetailViewModel
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
            QoutationDetailInput input = null;
            if (idModel == null)
            {
                input = new QoutationDetailInput();
            }
            else
            {
                input = qoutationDetailService.GetInputById(idModel?.Id);
            }

            return PartialView(input);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult InputPartial(QoutationDetailInput inputModel)
        {
            if (inputModel.Id != null)
            {
                //update
                QoutationDetail lastInfo = qoutationDetailService.GetById(inputModel.Id);
                qoutationDetailService.UpdateQoutationDetail(inputModel);
                unitOfWork.Commit();
                return Ok(new { result = ConstantConfig.WebApiStatusCode.Success, message = "Cập nhật thành công" });
            }
            return Ok(new { result = ConstantConfig.WebApiStatusCode.Error, message = "Có lỗi xảy ra" });
        }
    }
}