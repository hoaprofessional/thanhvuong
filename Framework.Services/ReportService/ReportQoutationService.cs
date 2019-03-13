using Framework.DTOs.ReportDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Services.ReportService
{
    public interface IReportQoutationService
    {
        ReportQoutationDto GetReportQoutationDto(ReportFilterDto filterDto);
    }

    public class ReportQoutationService : IReportQoutationService
    {
        public ReportQoutationDto GetReportQoutationDto(ReportFilterDto filterDto)
        {
            throw new NotImplementedException();
        }
    }
}
