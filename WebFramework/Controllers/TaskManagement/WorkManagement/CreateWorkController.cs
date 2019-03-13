using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Framework.InputModels.WorkManagement;
using Framework.Models.TaskManagement;
using Framework.Repositories.Infrastructor;
using Framework.Services.Shared;
using Framework.Services.Utils;
using Framework.Services.WorkManagementService.CreateWorkService;
using Framework.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using WebFramework.Controllers.Shared;
using WebFramework.Infrastructor;
using WebFramework.Models.TaskManagement.WorkManagement;
using WebFramework.SignalR.Hubs;

namespace WebFramework.Controllers.TaskManagement.WorkManagement
{
    [ClaimRequirement(MyClaimType.Permission, "AssignTask")]
    public class CreateWorkController : LayoutController
    {
        readonly ICreateWorkIndexService createWorkIndexService;
        readonly IUnitOfWork unitOfWork1;
        public CreateWorkController(
            ICreateWorkIndexService createWorkIndexService,
            IHubContext<NotificationHub> hubcontext,
            IUnitOfWork unitOfWork,
            ILayoutService layoutService) : base(layoutService, hubcontext)
        {
            this.createWorkIndexService = createWorkIndexService;
            this.unitOfWork1 = unitOfWork;
        }
        public IActionResult Index()
        {
            CreateWorkViewModel viewModel = new CreateWorkViewModel();
            InitLayoutViewModel(viewModel);
            viewModel.Title = "Tạo công việc";
            viewModel.Priorities = createWorkIndexService.GetPriorities();
            return View(viewModel);
        }

        public JsonResult SubmitAddWork(CreateWorkInput worksInput)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    this.unitOfWork1.BeginTransaction();
                    foreach(var workInput in worksInput.Works)
                    {
                        Work work = new Work();
                        work.CopyFrom(workInput);
                        work.WorkStatusId = WorkStatusIdHelper.InProgress;
                        this.loggerService.AddInfomationLogger("user tạo công việc " + work.Name);
                        this.createWorkIndexService.AddWork(work);
                    }
                    this.unitOfWork1.Commit();
                    this.unitOfWork1.CommitTransaction();
                    return Json(new { result = "success" });
                }
                catch
                {
                    this.unitOfWork1.RollbackTransaction();
                    return Json(new { result = "fail" });
                }
            }
            return Json(new { result = "fail" });
        }
    }
}