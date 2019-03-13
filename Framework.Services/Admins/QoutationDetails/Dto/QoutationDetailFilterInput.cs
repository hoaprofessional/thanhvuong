using System;
using System.Collections.Generic;
using System.Text;
using WebCore.Utils.FilterHelper;
using WebCore.Utils.ModelHelper;

namespace WebCore.Services.Share.Admins.QoutationDetails.Dto
{
    /// <summary>
    /// Model QoutationDetail
    /// </summary>
    public class QoutationDetailFilterInput : IPagingFilterDto
    {
        public int QoutationId { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
