using Framework.DTOs.QoutationManagementDto.BaseOrderDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebFramework.Models.QoutationManagementViewModels.AllOrderViewModel
{
    public class AllOrderListPartialViewModel
    {
        public List<OrderDto> Orders { get; set; }
        public int NumberOfPages { get; set; }
        public int CurrentPage { get; set; }
        public string ColumnSortingName { get; set; }
        public string SortingAction { get; set; }
    }
}
