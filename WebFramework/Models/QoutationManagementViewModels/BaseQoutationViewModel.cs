using Framework.DTOs.QoutationManagementDto.BaseQoutationDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebFramework.Models.Shared;

namespace WebFramework.Models.QoutationManagementViewModels
{
    public class BaseQoutationViewModel : LayoutViewModel
    {
        public List<int> NumbersShowPage { get; set; }
        public List<QoutationStatusFilterDto> QoutationStatusFilters { get; set; }
        public List<ClientFilterDto> ClientFilters { get; set; }
        public List<ProductFilterDto> ProductFilters { get; set; }
        public List<CreateByFilterDto> CreateByFilters { get; set; }
    }
}
