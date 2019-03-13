using Framework.DTOs;
using Framework.Models.TaskManagement;
using Framework.Utils.Anotations.DtoAnotation;
using System;
using System.ComponentModel.DataAnnotations;

namespace Framework.InputModels.WorkManagement.WorkList
{
    public class UpdateWorkInput : IRef<Work>
    {
        [Required]
        public string WorkId { get; set; }
        [Required]
        [MapColumn("Name")]
        public string WorkName { get; set; }
        [Required]
        public string PriorityId { get; set; }
        [Required]
        [MapColumn("DateBegin")]
        public DateTime WorkDateBegin { get; set; }
        [Required]
        [MapColumn("TimeExpired")]
        public DateTime WorkTimeExpired { get; set; }
        [Required]
        public string WorkStatusId { get; set; }
    }
}
