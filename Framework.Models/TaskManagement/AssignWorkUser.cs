using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Framework.Models.TaskManagement
{
    public class AssignWorkUser : Auditable
    {
        public string Id { get; set; }
        [MaxLength(100)]
        public string AssignerRole { get; set; }
        [MaxLength(100)]
        public string AssignToRole { get; set; }
    }
}
