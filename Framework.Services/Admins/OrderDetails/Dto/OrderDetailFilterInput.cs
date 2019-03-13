using System;
using System.Collections.Generic;
using System.Text;
using WebCore.Utils.FilterHelper;
using WebCore.Utils.ModelHelper;

namespace WebCore.Services.Share.Admins.OrderDetails.Dto
{
    /// <summary>
    /// Model OrderDetail
    /// </summary>
    public class OrderDetailFilterInput : IPagingFilterDto
    {
        public string OrderId { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
