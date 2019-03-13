using Framework.Models.QoutationManagement;
using Framework.Utils.Anotations.DtoAnotation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.DTOs.Areas.Test
{
    public class QoutationEventDto: IRef<QoutationEvent>
    {
        [MapColumn("QoutationEventNote")]
        public String Content { get; set; }
        public String StaffName { get; set; }
        [MapColumn("QoutationEventCreationTime")]
        public DateTime? Time { get; set; }
        public string QoutationEventStaffId { get; set; }
        public double ProcessTime { get; set; }
    }
}
