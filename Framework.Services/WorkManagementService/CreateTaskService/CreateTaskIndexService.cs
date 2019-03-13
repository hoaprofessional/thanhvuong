using Framework.DTOs.TaskManagementDto.TaskManagement.CreateTask.CreateTaskIndex;
using Framework.Repositories.TaskManagement;
using Framework.Repositories.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.Services.WorkManagementService.CreateTaskService
{
    public interface ICreateTaskIndexService
    {
        List<PriorityDto> GetPriorities();
        List<AssignUserDto> GetAssignUsers();
        WorkDto GetWork(string workId);
    }
    public class CreateTaskIndexService : ICreateTaskIndexService
    {
        IPriorityRepository priorityRepository;
        IApplicationUserRepository applicationUserRepository;
        IWorkRepository workRepository;

        public CreateTaskIndexService(IPriorityRepository priorityRepository,
            IWorkRepository workRepository,
            IApplicationUserRepository applicationUserRepository)
        {
            this.priorityRepository = priorityRepository;
            this.applicationUserRepository = applicationUserRepository;
            this.workRepository = workRepository;
        }

        public List<AssignUserDto> GetAssignUsers()
        {
            return applicationUserRepository
                .GetMultiBySelectClassType<AssignUserDto>
                (x => x.Active == true && 
                x.IsBanned!=true)
                .ToList();
        }

        public List<PriorityDto> GetPriorities()
        {
            return priorityRepository
                .GetMultiBySelectClassType<PriorityDto>(x => x.Active == true)
                .ToList();
        }

        public WorkDto GetWork(string workId)
        {
            return workRepository
                .GetMultiBySelectClassType<WorkDto>(x => x.Active == true && x.Id==workId)
                .SingleOrDefault();
        }
    }
}
