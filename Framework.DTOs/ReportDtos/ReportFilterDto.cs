using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Framework.DTOs.ReportDtos
{
    public class ReportFilterDto
    {
        [Required]
        public Nullable<DateTime> FromDate { get; set; }
        [Required]
        public Nullable<DateTime> ToDate { get; set; }
    }
}
