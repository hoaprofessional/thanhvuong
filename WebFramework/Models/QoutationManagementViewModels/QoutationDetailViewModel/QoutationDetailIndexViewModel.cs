using Framework.Models.Configuration;
using Framework.Models.QoutationManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebFramework.Models.Shared;

namespace WebFramework.Models.QoutationManagementViewModels.QoutationDetailViewModel
{
    public class QoutationDetailIndexViewModel : LayoutViewModel
    {
        public Qoutation Qoutation { get; set; }
        public CommonConfiguration CommonConfiguration { get; set; }
        public bool CanApproveQoutation { get; set; }
        public bool CanAujustPriceSell { get; set; }
        public bool CanAujustPriceBuy { get; set; }
        public bool CanRejectQoutation { get; set; }
        public bool CanPrintPdf { get; set; }
        public bool CanApprovePriceByAccountingManager { get; set; }
        public bool CanApprovePriceBySalesManager { get; set; }
        public bool CanQuotesPrice { get; set; }
        public bool CanCreateOrder { get; set; }
    }
}
