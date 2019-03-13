using Framework.Models.QoutationManagement;
using Framework.Utils.Anotations.DtoAnotation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.DTOs.Areas.Test
{
    public class QoutationEventListDto : IRef<QoutationEvent>
    {
        [MapColumn("Id")]
        public int QoutationId { get; set; }
        public double TotalMinutes { get; set; }
        public double TotalDays { get; set; }
    }
}
