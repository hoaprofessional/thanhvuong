using Framework.Models.QoutationManagement;
using Framework.Repositories.Infrastructor;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WebCore.Services.Share.Admins.Qoutations;
using WebCore.Services.Share.Admins.Qoutations.Dto;
using WebCore.Services.Share.Commons.Permissions;
using WebCore.Utils.Config;
using WebCore.Utils.ModelHelper;
using WebFramework.Areas.Management.Models;
using WebFramework.Infrastructor;

namespace WebFramework.Areas.Management.Controllers
{
    [Area("Management")]
    [ClaimRequirement(MyClaimType.Permission, PermissionValue.RegisterUser)]
    public class QoutationController : AdminBaseController
    {
        private readonly IQoutationService qoutationService;
        private readonly IPermissionService permissionService;
        private readonly IUnitOfWork unitOfWork;

        public QoutationController(IServiceProvider serviceProvider, IQoutationService userService, IPermissionService permissionService, IUnitOfWork unitOfWork) : base(serviceProvider)
        {
            this.qoutationService = userService;
            this.unitOfWork = unitOfWork;
            this.permissionService = permissionService;
        }

        public IActionResult Index(int page = 1)
        {
            QoutationFilterInput filterInput = GetFilterInSession<QoutationFilterInput>("QoutationSession");
            filterInput.PageNumber = page;
            QoutationViewModel userViewModel = new QoutationViewModel
            {
                FilterInput = filterInput,
                PagingResult = qoutationService.GetAllByPaging(filterInput)
            };
            InitAdminBaseViewModel(userViewModel);
            return View(userViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult FilterPartial(QoutationFilterInput filterInput)
        {
            SetFilterToSession("QoutationSession", filterInput);
            return RedirectToAction("Index", new { page = 1 });
        }

        [HttpPost]
        public IActionResult DeleteQoutation(int qoutationId)
        {
            qoutationService.Delete(qoutationId);
            unitOfWork.Commit();
            return Ok();
        }
    }
}