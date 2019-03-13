using Framework.DTOs.TaskManagementDto.TaskManagement.TaskList.TaskListIndex;
using Framework.Models.TaskManagement;
using Framework.Repositories.TaskManagement;
using Framework.Repositories.UserManagement;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.Services.WorkManagementService.TaskListService
{
    public interface ITaskListIndexService
    {
        Work GetWork(string workId);
        List<PriorityDto> GetPriorities();
        List<AssignUserDto> GetAssignUsers();
        List<TaskStatusDto> GetTaskStatuses();
        Task GetTaskById(string taskId);
        int GetMaxTaskOrder(string workId);
    }
    public class TaskListIndexService : ITaskListIndexService
    {
        readonly IWorkRepository workRepository;
        readonly ITaskRepository taskRepository;
        readonly IPriorityRepository priorityRepository;
        readonly IApplicationUserRepository applicationUserRepository;
        readonly ITaskStatusRepository taskStatusRepository;
        readonly IIdentityUserRolesRepository identityUserRolesRepository;
        readonly IAssignWorkUserRepository assignWorkUserRepository;

        public TaskListIndexService(IWorkRepository workRepository,
            ITaskRepository taskRepository,
            IApplicationUserRepository applicationUserRepository,
            ITaskStatusRepository taskStatusRepository,
            IIdentityUserRolesRepository identityUserRolesRepository,
            IAssignWorkUserRepository assignWorkUserRepository,
            IPriorityRepository priorityRepository)
        {
            this.workRepository = workRepository;
            this.taskRepository = taskRepository;
            this.priorityRepository = priorityRepository;
            this.applicationUserRepository = applicationUserRepository;
            this.taskStatusRepository = taskStatusRepository;
            this.identityUserRolesRepository = identityUserRolesRepository;
            this.assignWorkUserRepository = assignWorkUserRepository;
        }

        public List<AssignUserDto> GetAssignUsers()
        {
            var currentUser =
                applicationUserRepository.GetSingleByCondition(x => x.UserName == applicationUserRepository.GetLoginedUserName());

            //lay danh sach quyen cua user hien tai
            var roles = identityUserRolesRepository.GetMulti(x => x.UserId == currentUser.Id).Select(x => x.RoleId).ToArray();

            var rolesFinder = "," + String.Join(",", roles) + ",";

            // tim danh sach quyen cua nhan vien duoc giao viec
            var assingToRoles = assignWorkUserRepository.GetMulti(x => rolesFinder.Contains("," + x.AssignerRole + ",")).
                Select(x => x.AssignToRole).ToArray();

            var assingToRolesFinder = "," + String.Join(",", assingToRoles) + ",";

            // tim danh sach id cua user duoc giao
            var assingToIds = identityUserRolesRepository.GetMulti(x =>
            assingToRolesFinder.Contains("," + x.RoleId + ",")).Select(x=>x.UserId).ToArray();

            var assingerIdsFinder = "," + String.Join(",", assingToIds) + ",";

            // tim danh sach user duoc giao
            return applicationUserRepository
                .GetMultiBySelectClassType<AssignUserDto>
                (x => x.Active == true &&
                assingerIdsFinder.Contains("," + x.Id + ",") &&
                x.IsBanned != true)
                .ToList();
        }

        public int GetMaxTaskOrder(string workId)
        {
            var tasks = taskRepository.GetMulti(x => x.WorkId == workId);
            if (tasks.Count() == 0)
                return 0;
            return tasks.Max(x => x.Order);
        }

        public List<PriorityDto> GetPriorities()
        {
            return priorityRepository
                .GetMultiBySelectClassType<PriorityDto>(x => x.Active == true)
                .ToList();
        }

        public Task GetTaskById(string taskId)
        {
            return this.taskRepository.GetMulti(x => x.Id == taskId)
                .Include(x => x.Work)
                .Include(x => x.Assigner)
                .Include(x => x.AssignTo)
                .Include(x => x.TaskStatus)
                .Include(x => x.Priority)
                .SingleOrDefault();
        }

        public List<TaskStatusDto> GetTaskStatuses()
        {
            return taskStatusRepository
                .GetMultiBySelectClassType<TaskStatusDto>(x => x.Active == true)
                .ToList();
        }

        public Work GetWork(string workId)
        {
            var work = workRepository.GetMulti(x => x.Id == workId && x.Active == true).SingleOrDefault();
            if (work == null)
                return null;
            work.Tasks = taskRepository.GetMulti(x => x.WorkId == workId && x.Active == true)
                .Include(x => x.Work)
                .Include(x => x.Assigner)
                .Include(x => x.AssignTo)
                .Include(x => x.TaskStatus)
                .Include(x => x.Priority)
                .ToList();
            return work;
        }
    }
}
