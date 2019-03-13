using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Framework.InputModels.WorkManagement.TaskList;
using Framework.Repositories.Infrastructor;
using Framework.Services.ManageService;
using Framework.Services.ManageService.TaskManagement;
using Framework.Services.Shared;
using Framework.Services.Utils;
using Framework.Services.WorkManagementService.TaskListService;
using Framework.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using WebFramework.Controllers.Shared;
using WebFramework.Infrastructor;
using WebFramework.Models.TaskManagement.TaskManagement;
using WebFramework.SignalR.Hubs;

namespace WebFramework.Controllers.TaskManagement.TaskManagement
{
    public class TaskListController : LayoutController
    {
        ITaskListIndexService taskListIndexService;
        ITaskManageService taskManageService;
        IWorkManageService workManageService;
        IUnitOfWork unitOfWork1;
        public TaskListController(ILayoutService layoutService,
            ITaskListIndexService taskListIndexService,
            ITaskManageService taskManageService,
            IHubContext<NotificationHub> hubcontext,
            IUnitOfWork unitOfWork1,
            IWorkManageService workManageService) : base(layoutService,hubcontext)
        {
            this.taskListIndexService = taskListIndexService;
            this.taskManageService = taskManageService;
            this.workManageService = workManageService;
            this.unitOfWork1 = unitOfWork1;
        }

        public IActionResult Index(string workId)
        {
            var viewModel = new TaskListIndexViewModel();
            viewModel.Work = taskListIndexService.GetWork(workId);
            if (viewModel.Work == null)
            {
                return Redirect("/NotFound");
            }
            InitLayoutViewModel(viewModel);
            viewModel.Priorities = taskListIndexService.GetPriorities();
            viewModel.CanCreateTask = viewModel.Work.CreationUserName == GetCurrentUserName();
            viewModel.AssignToUsers = taskListIndexService.GetAssignUsers();
            viewModel.CurrentUserId = GetCurrentUserId();
            viewModel.TaskStatuss = taskListIndexService.GetTaskStatuses();
            return View(viewModel);
        }
        [Authorize()]
        public JsonResult StaffUpdateTask(StaffUpdateTaskInput input)
        {
            if(!ModelState.IsValid)
            {
                return Json(new
                {
                    result = "fail",
                    message = "Task không hợp lệ"
                });
            }
            var task = taskManageService.GetById(input.Id);

            //if(task.TaskStatusId == TaskStatusIdHelper.AlreadyCreated && 
            //    (!input.TaskStatusId.In( new[] {
            //                                TaskStatusIdHelper.InProgress,
            //                                TaskStatusIdHelper.Finish,
            //                                TaskStatusIdHelper.Postpone
            //                                } )
            //    ))
            //{
            //    return Json(new
            //    {
            //        result = "fail",
            //        message = "Task không hợp lệ"
            //    });
            //}

            if (task == null)
            {
                return Json(new
                {
                    result = "fail",
                    message = "Task không hợp lệ"
                });
            }

            try
            {
                unitOfWork1.BeginTransaction();
                task.CopyFrom(input);
                taskManageService.Update(task);
                loggerService.AddInfomationLogger(String.Format("Task {0} vừa được cập nhật bởi {1}", task.Name, GetCurrentUserName()));
                unitOfWork1.Commit();
                unitOfWork1.CommitTransaction();
                return Json(new
                {
                    result = "success",
                    data = task.Id
                });
            }
            catch
            {
                unitOfWork1.RollbackTransaction();
                return Json(new
                {
                    result = "fail",
                    message = "Có lỗi xảy ra"
                });
            }
        }

        [ClaimRequirement(MyClaimType.Permission, "AssignTask")]
        public JsonResult UpdateTask(UpdateTaskInput input)
        {
            if(!ModelState.IsValid)
            {
                return Json(new
                {
                    result = "fail",
                    message = "Task không hợp lệ"
                });
            }

            var task = taskManageService.GetById(input.Id);
            if (task == null)
            {
                return Json(new
                {
                    result = "fail",
                    message = "Task không hợp lệ"
                });
            }

            var work = workManageService.GetById(input.WorkId);
            if (work == null)
            {
                return Json(new
                {
                    result = "fail",
                    message = "Task không hợp lệ"
                });
            }


            if (input.Deadline < DateTime.Today)
            {
                return Json(new
                {
                    result = "fail",
                    message = "Deadline phải lớn hơn hôm nay"
                });
            }

            if (work.CreationUserName != GetCurrentUserName())
            {
                return Json(new
                {
                    result = "fail",
                    message = "Bạn không có quyền tạo task này"
                });
            }

            var user = layoutService.GetUserById(input.AssignToId);
            if (user == null)
            {
                return Json(new
                {
                    result = "fail",
                    message = "Người thực hiện không hợp lệ"
                });
            }
            try
            {
                unitOfWork1.BeginTransaction();
                task.CopyFrom(input);
                taskManageService.Update(task);
                loggerService.AddInfomationLogger(String.Format("Task {0} vừa được cập nhật bởi {1}", task.Name, GetCurrentUserName()));
                unitOfWork1.Commit();
                unitOfWork1.CommitTransaction();
                return Json(new
                {
                    result = "success",
                    data = task.Id
                });
            }
            catch
            {
                unitOfWork1.RollbackTransaction();
                return Json(new
                {
                    result = "fail",
                    message = "Có lỗi xảy ra"
                });
            }

        }

        [ClaimRequirement(MyClaimType.Permission, "AssignTask")]
        public JsonResult DeleteTask(string taskId)
        {
            var task = taskManageService.GetById(taskId);
            if(task==null)
            {
                return Json(new
                {
                    result = "fail",
                    message = "Task không hợp lệ"
                });
            }
            var work = workManageService.GetById(task.WorkId);
            if (work.CreationUserName != GetCurrentUserName())
            {
                return Json(new
                {
                    result = "fail",
                    message = "Bạn không có quyền xóa task này"
                });
            }
            taskManageService.Delete(task);
            unitOfWork1.Commit();
            return Json(new
            {
                result = "success",
            });
        }

        [ClaimRequirement(MyClaimType.Permission, "AssignTask")]
        [HttpPost]
        public JsonResult AddNewtask(CreateTaskInput input)
        {
            if (!ModelState.IsValid)
            {
                return Json(new
                {
                    result = "fail",
                    message = "Task không hợp lệ"
                });
            }
            var work = workManageService.GetById(input.WorkId);
            if (work == null)
            {
                return Json(new
                {
                    result = "fail",
                    message = "Task không hợp lệ"
                });
            }

            if (input.Deadline < DateTime.Today)
            {
                return Json(new
                {
                    result = "fail",
                    message = "Deadline phải lớn hơn hôm nay"
                });
            }

            if (work.CreationUserName != GetCurrentUserName())
            {
                return Json(new
                {
                    result = "fail",
                    message = "Bạn không có quyền tạo task này"
                });
            }

            var user = layoutService.GetUserById(input.AssignToId);
            if (user == null)
            {
                return Json(new
                {
                    result = "fail",
                    message = "Người thực hiện không hợp lệ"
                });
            }
            try
            {
                unitOfWork1.BeginTransaction();
                var task = new Framework.Models.TaskManagement.Task();
                task.CopyFrom(input);
                task.AssignerId = GetCurrentUserId();
                task.DateReception = DateTime.Now;
                task.TaskStatusId = TaskStatusIdHelper.AlreadyCreated;
                task.Order = taskListIndexService.GetMaxTaskOrder(input.WorkId) + 1;
                taskManageService.Add(task);
                loggerService.AddInfomationLogger(String.Format("Task {0} vừa được tạo bởi {1}", task.Name, GetCurrentUserName()));
                unitOfWork1.Commit();
                unitOfWork1.CommitTransaction();
                return Json(new
                {
                    result = "success",
                    data = task.Id
                });
            }
            catch
            {
                unitOfWork1.RollbackTransaction();
                return Json(new
                {
                    result = "fail",
                    message = "Có lỗi xảy ra"
                });
            }

        }

        public IActionResult TaskItemPartial(string taskId)
        {
            var task = taskListIndexService.GetTaskById(taskId);
            var viewModel = new TaskItemPartialViewModel();
            if (task != null)
            {
                viewModel.Task = task;
                viewModel.Priorities = taskListIndexService.GetPriorities();
                viewModel.CanCreateTask = viewModel.Task.Work.CreationUserName == GetCurrentUserName();
                viewModel.AssignToUsers = taskListIndexService.GetAssignUsers();
                viewModel.CurrentUserName = GetCurrentUserName();
                viewModel.CurrentUserId = GetCurrentUserId();
                viewModel.TaskStatuss = taskListIndexService.GetTaskStatuses();
                viewModel.IsNewTask = false;
                viewModel.IsAssigner = task.Work.CreationUserName == GetCurrentUserName();
                viewModel.IsPerformer = task.AssignTo.UserName == GetCurrentUserName();
            }
            else
            {
                viewModel.Task = new Framework.Models.TaskManagement.Task();
                viewModel.Priorities = taskListIndexService.GetPriorities();
                viewModel.CanCreateTask = true;
                viewModel.AssignToUsers = taskListIndexService.GetAssignUsers();
                viewModel.CurrentUserName = GetCurrentUserName();
                viewModel.CurrentUserId = GetCurrentUserId();
                viewModel.TaskStatuss = taskListIndexService.GetTaskStatuses();
                viewModel.IsNewTask = true;

            }
            return PartialView(viewModel);
        }
    }
}