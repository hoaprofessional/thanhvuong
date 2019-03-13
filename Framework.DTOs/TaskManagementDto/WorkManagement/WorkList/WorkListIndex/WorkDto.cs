using Framework.Models.TaskManagement;
using Framework.Utils.Anotations.DtoAnotation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.DTOs.WorkManagementDto.WorkList.WorkListIndex
{
    public class WorkDto : IRef<Work>, IRef<Priority>, IRef<WorkStatus>
    {
        /// <summary>
        /// Mã công việc
        /// </summary>
        public string WorkId { get; set; }
        /// <summary>
        /// Tên công việc
        /// </summary>
        public string WorkName { get; set; }
        /// <summary>
        /// Mức độ ưu tiên
        /// </summary>
        public string PriorityName { get; set; }
        /// <summary>
        /// Mã mức độ ưu tiên
        /// </summary>
        public string PriorityId { get; set; }
        /// <summary>
        /// Màu mức độ ưu tiên
        /// </summary>
        public string PriorityColor { get; set; }
        public string WorkWorkStatusId { get; set; }
        /// <summary>
        /// Tổng số task
        /// </summary>
        [NotForMap]
        public int NumberOfTask { get; set; }
        /// <summary>
        /// Tổng số task chưa hoàn thành
        /// </summary>
        [NotForMap]
        public int NumberOfUnFinishTask { get; set; }
        /// <summary>
        /// UserName của người tạo
        /// </summary>
        public String WorkCreationUserName { get; set; }
        /// <summary>
        /// Deadline
        /// </summary>
        public DateTime WorkTimeExpired { get; set; }
        /// <summary>
        /// Chỉnh sửa lần cuối
        /// </summary>
        public DateTime? WorkModifiedTime { get; set; }
        /// <summary>
        /// Trạng thái
        /// </summary>
        public String WorkStatusDecription { get; set; }
        public String WorkStatusId { get; set; }
        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime? WorkCreationTime { get; set; }
        /// <summary>
        /// Người tạo
        /// </summary>
        public String ApplicationUserName { get; set; }
        public bool CanEdit { get; set; }
        public DateTime WorkDateBegin { get; set; }
    }
}
