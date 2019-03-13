using System;
using System.Collections.Generic;
using System.Text;
using WebCore.Utils.FilterHelper;
using WebCore.Utils.ModelHelper;

namespace WebCore.Services.Share.Admins.Orders.Dto
{
    /// <summary>
    /// Model Order
    /// </summary>
    public class OrderFilterInput : IPagingFilterDto
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        [Filter(FilterComparison.Equal)]
        public int? Id { get; set; }
    }
}
