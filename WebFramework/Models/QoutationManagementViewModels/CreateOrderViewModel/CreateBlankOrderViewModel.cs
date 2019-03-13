using Framework.DTOs.QoutationManagementDto.CreateOrderDto;
using Framework.Models.Configuration;
using System.Collections.Generic;
using WebFramework.Models.Shared;

namespace WebFramework.Models.QoutationManagementViewModels.CreateOrderViewModel
{
    public class CreateBlankOrderViewModel : LayoutViewModel
    {
        public CommonConfiguration Configuration { get; set; }
        public List<ClientDto> Clients { get; set; }
        public List<ProductDto> Products { get; set; }
    }
}
