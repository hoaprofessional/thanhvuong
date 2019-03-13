using Framework.DTOs.WorkManagementDto.CreateWork.CreateWorkIndexDto;
using Framework.Models.TaskManagement;
using Framework.Repositories.TaskManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.Services.WorkManagementService.CreateWorkService
{
    public interface ICreateWorkIndexService
    {
        List<PriorityDto> GetPriorities();
        void AddWork(Work work);
    }
    public class CreateWorkIndexService : ICreateWorkIndexService
    {
        IPriorityRepository priorityRepository;
        IWorkRepository workRepository;

        public CreateWorkIndexService(IPriorityRepository priorityRepository, IWorkRepository workRepository)
        {
            this.priorityRepository = priorityRepository;
            this.workRepository = workRepository;
        }

        public void AddWork(Work work)
        {
            workRepository.Add(work);
        }

        public List<PriorityDto> GetPriorities()
        {
            return priorityRepository.GetMultiBySelectClassType<PriorityDto>(x => x.Active == true).ToList();
        }
    }
}
