using Framework.DTOs;
using Framework.Models.TaskManagement;
using System;
using System.ComponentModel.DataAnnotations;

namespace Framework.InputModels.WorkManagement.TaskList
{
    public class UpdateTaskInput : IRef<Task>
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
        [Required]
        public string TaskStatusId { get; set; }
        public string Result { get; set; }
        public string Id { get; set; }
    }
}
