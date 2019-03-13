using Framework.DTOs.TaskManagementDto.WorkManagement.WorkList.WorkListIndex;
using Framework.InputModels.WorkManagement.WorkList;
using Framework.Models.TaskManagement;
using Framework.Repositories.Infrastructor;
using Framework.Services.ManageService;
using Framework.Services.Shared;
using Framework.Services.WorkManagementService.WorkListService;
using Framework.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using WebFramework.Controllers.Shared;
using WebFramework.Infrastructor;
using WebFramework.Models.TaskManagement.WorkManagement;
using WebFramework.Models.TaskManagement.WorkManagement.WorkList;
using WebFramework.SignalR.Hubs;

namespace WebFramework.Controllers.TaskManagement.WorkManagement
{
    [Authorize]
    public class WorkListController : LayoutController
    {
        readonly IWorkListIndexService workListService;
        readonly IWorkManageService workManageService;
        readonly IUnitOfWork unitOfWork1;
        public WorkListController(
            ILayoutService layoutService,
            IWorkManageService workManageService,
            IHubContext<NotificationHub> hubcontext,
            IUnitOfWork unitOfWork1,
            IWorkListIndexService workListService) : base(layoutService, hubcontext)
        {
            this.workListService = workListService;
            this.workManageService = workManageService;
            this.unitOfWork1 = unitOfWork1;
        }
        public IActionResult Index()
        {
            WorkListIndexViewModel viewModel = new WorkListIndexViewModel();
            InitLayoutViewModel(viewModel);
            HttpContext.Session.SetString("WorkListIndexUserName", viewModel.User.UserName);
            viewModel.Title = "Danh sách công việc";
            viewModel.CreateByFilters = workListService.GetCreateByFilters();
            viewModel.NumbersShowPage = workListService.PagingService.GetNumbersShowPage(new WorkFilterDto());
            viewModel.WorkStatusesFilters = workListService.GetWorkStatusesFilters();
            viewModel.Priorities = workListService.GetPriorities();
            return View(viewModel);
        }

        [ClaimRequirement(MyClaimType.Permission, "AssignTask")]
        public JsonResult DeleteWork(string workId)
        {
            Work work = this.workManageService.GetById(workId);
            if (work == null)
            {
                return Json(new
                {
                    result = "fail",
                    message = "Công việc không hợp lệ"
                });
            }
            if (work.CreationUserName != GetCurrentUserName())
            {
                return Json(new
                {
                    result = "fail",
                    message = "Bạn không có quyền sửa công việc này"
                });
            }
            workManageService.Delete(work);
            unitOfWork1.Commit();
            return Json(new
            {
                result = "success",
                data = work
            });
        }

        [ClaimRequirement(MyClaimType.Permission, "AssignTask")]
        [HttpPost]
        public JsonResult UpdateWork(UpdateWorkInput updateWorkInput)
        {
            if (!ModelState.IsValid)
            {
                return Json(new
                {
                    result = "fail",
                    message = "Thông tin nhập không đúng"
                });
            }
            if (updateWorkInput.WorkDateBegin > updateWorkInput.WorkTimeExpired)
            {
                return Json(new
                {
                    result = "fail",
                    message = "Deadline không được nhỏ hơn ngày bắt đầu"
                });
            }

            Work work = this.workManageService.GetById(updateWorkInput.WorkId);
            if(work==null)
            {
                return Json(new
                {
                    result = "fail",
                    message = "Công việc không hợp lệ"
                });
            }
            if (work.CreationUserName != GetCurrentUserName())
            {
                return Json(new
                {
                    result = "fail",
                    message = "Bạn không có quyền sửa công việc này"
                });
            }
            if (work == null)
            {
                return Json(new
                {
                    result = "fail",
                    message = "Mã công việc không đúng"
                });
            }
            work.CopyFrom(updateWorkInput);
            workManageService.Update(work);
            unitOfWork1.Commit();
            return Json(new
            {
                result = "success",
                data = work
            });
        }


        public IActionResult WorkListPartial(
            WorkListFilterInput workListFilterInput)
        {
            WorkListWorkListPartialViewModel viewModel = new WorkListWorkListPartialViewModel();
            var userName = HttpContext.Session.GetString("WorkListIndexUserName");

            var workFilterDto = new WorkFilterDto()
            {
                CreationUserName = userName,
                CurrentPage = workListFilterInput.Page,
                DateBeginFilter = workListFilterInput.FilterByDateBegin,
                ItemsPerPage = workListFilterInput.NumberItemPerPage,
                SortingAction = workListFilterInput.SortingAction,
                SortingColumnName = workListFilterInput.ColumnSortingName,
                StatusIdFilter = workListFilterInput.FilterByStatusId,
                UserNameFilter = workListFilterInput.FilterByUserName
            };
            viewModel.Priorities = workListService.GetPriorities();
            viewModel.WorkStatuses = workListService.GetWorkStatusesFilters();
            viewModel.Works = workListService.GetAllWork(workFilterDto);
            viewModel.NumberOfPages = workListService.PagingService.GetNumbersOfPage(workFilterDto);
            viewModel.CurrentPage = workListFilterInput.Page;
            //hien thi nut sort
            viewModel.ColumnSortingName = workListFilterInput.ColumnSortingName;
            viewModel.SortingAction = workListFilterInput.SortingAction;

            return PartialView(viewModel);
        }
    }
}