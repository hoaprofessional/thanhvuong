using Framework.Models.UserManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.DTOs.TaskManagementDto.TaskManagement.CreateTask.CreateTaskIndex
{
    public class AssignUserDto : IRef<ApplicationUser>
    {
        public String Id { get; set; }
        public String Name { get; set; }
        public String UserName { get; set; }
    }
}
