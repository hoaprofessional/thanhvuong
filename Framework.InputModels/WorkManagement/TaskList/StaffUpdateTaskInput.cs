using System.ComponentModel.DataAnnotations;

namespace Framework.InputModels.WorkManagement.TaskList
{
    public class StaffUpdateTaskInput
    {
        public string Id { get; set; }
        [Required]
        public string TaskStatusId { get; set; }
        public string Note { get; set; }
        public string Result { get; set; }
    }
}
