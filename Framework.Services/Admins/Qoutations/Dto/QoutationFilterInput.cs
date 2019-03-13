using System;
using System.Collections.Generic;
using System.Text;
using WebCore.Utils.FilterHelper;
using WebCore.Utils.ModelHelper;

namespace WebCore.Services.Share.Admins.Qoutations.Dto
{
    /// <summary>
    /// Model Qoutation
    /// </summary>
    public class QoutationFilterInput : IPagingFilterDto
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        [Filter(FilterComparison.Equal)]
        public int? Id { get; set; }
    }
}
