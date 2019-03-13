using Framework.DTOs;
using Framework.Models.TaskManagement;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Framework.InputModels.WorkManagement
{
    public class CreateWorkItemInput : IRef<Work>
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime TimeExpired { get; set; }
        [Required]
        [MaxLength(450)]
        public string PriorityId { get; set; }
        public DateTime DateBegin { get; set; }
    }
}
