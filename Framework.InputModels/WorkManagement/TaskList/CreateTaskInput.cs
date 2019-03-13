using Framework.DTOs;
using Framework.Models.TaskManagement;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Framework.InputModels.WorkManagement.TaskList
{
    public class CreateTaskInput : IRef<Task>
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string WorkId { get; set; }
        [Required]
        public string AssignToId { get; set; }
        [Required]
        public string PriorityId { get; set; }
        [Required]
        public DateTime Deadline { get; set; }
        public string Note { get; set; }
    }
}
