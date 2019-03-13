using Framework.DTOs.ReportDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebFramework.Models.Shared;

namespace WebFramework.Models.ReportQoutations
{
    public class ReportQoutationViewModel : LayoutViewModel
    {
        public ReportQoutationDto ReportDto { get; set; }
        public ReportFilterDto ReportFilter { get; set; }
    }
}
