using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Framework.Repositories.Infrastructor;
using Framework.Services.Shared;
using Framework.Services.WorkManagementService.CreateTaskService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using WebFramework.Controllers.Shared;
using WebFramework.Models.TaskManagement.TaskManagement;
using WebFramework.SignalR.Hubs;

namespace WebFramework.Controllers.TaskManagement.TaskManagement
{
    public class CreateTaskController : LayoutController
    {
        ICreateTaskIndexService createTaskIndexService;
        readonly IUnitOfWork unitOfWork1;
        public CreateTaskController(ILayoutService layoutService,
            IHubContext<NotificationHub> hubcontext,
            ICreateTaskIndexService createTaskIndexService,
            IUnitOfWork unitOfWork1) : base(layoutService, hubcontext)
        {
            this.createTaskIndexService = createTaskIndexService;
            this.unitOfWork1 = unitOfWork1;
        }

        public IActionResult Index(string workId)
        {
            CreateTaskViewModel viewModel = new CreateTaskViewModel();
            viewModel.Work = createTaskIndexService.GetWork(workId);
            if (viewModel.Work == null)
            {
                return Redirect("/NotFound");
            }
            viewModel.Title = "Tạo task";
            InitLayoutViewModel(viewModel);
            viewModel.Priorities = createTaskIndexService.GetPriorities();
            viewModel.AssignUsers = createTaskIndexService.GetAssignUsers();
            return View(viewModel);
        }
    }
}