using Framework.DTOs.QoutationManagementDto.CreateQoutationDto;
using Framework.Models.Configuration;
using Framework.Models.QoutationManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebFramework.Models.Shared;

namespace WebFramework.Models.QoutationManagementViewModels.CreateQoutationViewModel
{
    
    public class CreateQoutationIndexViewModel : LayoutViewModel
    {
        public CommonConfiguration Configuration { get; set; }
        public List<ClientDto> Clients { get; set; }
        public List<ProductDto> Products { get; set; }
    }
}
