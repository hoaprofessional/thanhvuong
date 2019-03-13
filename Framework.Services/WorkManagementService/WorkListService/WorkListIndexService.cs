using Framework.DTOs.TaskManagementDto.WorkManagement.WorkList.WorkListIndex;
using Framework.DTOs.WorkManagementDto.WorkList.WorkListIndex;
using Framework.Models.TaskManagement;
using Framework.Models.UserManagement;
using Framework.Repositories.TaskManagement;
using Framework.Repositories.UserManagement;
using Framework.Services.QoutationManagementService.CommonService;
using Framework.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.Services.WorkManagementService.WorkListService
{

    public interface IWorkPagingService :
        IPagingService<WorkDto, WorkFilterDto>
    {

    }
    public class WorkPagingService :
        PagingService<WorkDto, WorkFilterDto>, IWorkPagingService
    {

    }
    public interface IWorkListIndexService
    {
        IWorkPagingService PagingService { get; set; }
        /// <summary>
        /// Lấy danh sách người tạo cần filter
        /// </summary>
        /// <returns></returns>
        List<CreateByFilterDto> GetCreateByFilters();
        List<WorkStatusDto> GetWorkStatusesFilters();
        List<WorkDto> GetAllWork(WorkFilterDto workFilterDto);
        List<PriorityDto> GetPriorities();
    }
    public class WorkListIndexService : IWorkListIndexService,
        IPagingObject<WorkDto,WorkFilterDto>
    {
        readonly IWorkRepository workRepository;
        readonly IPriorityRepository priorityRepository;
        readonly IWorkStatusRepository workStatusRepository;
        readonly IApplicationUserRepository applicationUserRepository;
        readonly ITaskRepository taskRepository;
        readonly ITaskStatusRepository taskStatusRepository;

        public IWorkPagingService PagingService { get; set; }

        public WorkListIndexService(IWorkRepository workRepository,
            IPriorityRepository priorityRepository,
            IApplicationUserRepository applicationUserRepository,
            ITaskRepository taskRepository,
            ITaskStatusRepository taskStatusRepository,
            IWorkPagingService workPagingService,
            IWorkStatusRepository workStatusRepository)
        {
            this.workRepository = workRepository;
            this.priorityRepository = priorityRepository;
            this.workStatusRepository = workStatusRepository;
            this.applicationUserRepository = applicationUserRepository;
            this.taskRepository = taskRepository;
            this.taskStatusRepository = taskStatusRepository;
            this.PagingService = workPagingService;
            PagingService.PagingObject = this;
        }

        private void SetTaskCountWork(WorkDto workDto)
        {
            var tasks = this.taskRepository.GetMulti(x => x.Active == true && x.WorkId == workDto.WorkId);
            workDto.NumberOfTask = tasks.Count();
            workDto.NumberOfUnFinishTask = tasks.Where(x => x.TaskStatusId != TaskStatusIdHelper.Finish).Count();
        }

        public IQueryable<WorkDto> GetQuery(WorkFilterDto filter)
        {
            var works = workRepository.GetMulti(x => x.Active == true);
            var priorities = priorityRepository.GetMulti(x => x.Active == true);
            var workStatuss = workStatusRepository.GetMulti(x => x.Active == true);
            var users = applicationUserRepository.GetMulti(x => x.Active == true && x.IsBanned != true);

            var query = works
                    .LeftJoin(priorities,
                                (work => work.PriorityId),
                                (priority => priority.Id),
                                ExpressionHelper.JoinSelectResulExpression<Work, Priority, WorkDto>())
                     .LeftJoin(workStatuss,
                                (work => work.WorkWorkStatusId),
                                (workStatus => workStatus.Id),
                                ExpressionHelper.JoinSelectResulExpression<WorkDto, WorkStatus>())
                    .Join(users,
                                (work => work.WorkCreationUserName),
                                (user => user.UserName),
                                ExpressionHelper.JoinSelectResulExpression<WorkDto, ApplicationUser>());

            if (!String.IsNullOrEmpty(filter.UserNameFilter))
            {
                query = query.Where(x => x.WorkCreationUserName == filter.UserNameFilter);
            }
            if (filter.DateBeginFilter != null)
            {
                query = query.Where(x => (x.WorkDateBegin.Day == filter.DateBeginFilter.Value.Day)
                && (x.WorkDateBegin.Month == filter.DateBeginFilter.Value.Month)
                && (x.WorkDateBegin.Year == filter.DateBeginFilter.Value.Year));
            }
            if (!String.IsNullOrEmpty(filter.StatusIdFilter))
            {
                query = query.Where(x => x.WorkWorkStatusId == filter.StatusIdFilter);
            }

            return query;
        }


        public List<CreateByFilterDto> GetCreateByFilters()
        {
            var users = applicationUserRepository.GetMulti(x => x.Active == true);
            var works = workRepository.GetMulti(x => x.Active == true);
            var query = works
                .Join(users,
                            (work => work.CreationUserName),
                            (user => user.UserName),
                            ExpressionHelper.JoinSelectResulExpression<Work, ApplicationUser, CreateByFilterDto>()).Distinct();
            return query.ToList();
        }

        public List<WorkStatusDto> GetWorkStatusesFilters()
        {
            return workStatusRepository.GetMultiBySelectClassType<WorkStatusDto>(x => x.Active == true).ToList();
        }

        public List<WorkDto> GetAllWork(WorkFilterDto workFilterDto)
        {
            var works = PagingService.GetItems(workFilterDto).ToList();
            foreach(var w in works)
            {
                SetTaskCountWork(w);
                w.CanEdit = (w.WorkCreationUserName == workFilterDto.CreationUserName);
            }
            return works;
        }

        public List<PriorityDto> GetPriorities()
        {
            return priorityRepository.GetMultiBySelectClassType<PriorityDto>(x => x.Active == true).ToList();
        }
    }
}
