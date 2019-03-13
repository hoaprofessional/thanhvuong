using Framework.Models.UserManagement;
using System;

namespace Framework.DTOs.TaskManagementDto.TaskManagement.TaskList.TaskListIndex
{
    public class AssignUserDto : IRef<ApplicationUser>
    {
        public String Id { get; set; }
        public String Name { get; set; }
        public String UserName { get; set; }
    }
}
