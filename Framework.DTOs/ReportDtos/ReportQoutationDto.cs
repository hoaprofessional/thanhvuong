using Framework.Models.QoutationManagement;
using PagedList.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.DTOs.ReportDtos
{
    public class ReportQoutationDto
    {
        public IPagedList<Qoutation> Qoutations { get; set; }
        public int TotalQoutation { get; set; }
        public int TotalFinishQoutation { get; set; }
        public int TotalUnFinishQoutation
        {
            get
            {
                return TotalQoutation - TotalUnFinishQoutation;
            }
        }
    }
}
