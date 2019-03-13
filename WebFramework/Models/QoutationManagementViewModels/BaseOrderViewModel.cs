using Framework.DTOs.QoutationManagementDto.BaseOrderDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebFramework.Models.Shared;

namespace WebFramework.Models.OrderManagementViewModels
{
    public class BaseOrderViewModel : LayoutViewModel
    {
        public List<int> NumbersShowPage { get; set; }
        public List<OrderStatusFilterDto> OrderStatusFilters { get; set; }
        public List<ClientFilterDto> ClientFilters { get; set; }
        public List<ProductFilterDto> ProductFilters { get; set; }
        public List<CreateByFilterDto> CreateByFilters { get; set; }
    }
}
