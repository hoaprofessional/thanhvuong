using Framework.Models.Configuration;
using Framework.Models.QoutationManagement;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebFramework.Models.Shared;

namespace WebFramework.Models.QoutationManagementViewModels.CreateOrderViewModel
{
    public class CreateOrderIndexViewModel : LayoutViewModel
    {
        public CommonConfiguration CommonConfiguration { get; set; }
        public Qoutation Qoutation { get; set; }
    }
}
