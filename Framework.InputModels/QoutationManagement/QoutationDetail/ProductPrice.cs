using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.InputModels.QoutationManagement.QoutationDetail
{
    public class ProductPrice
    {
        public string ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public double VAT { get; set; }
        public string Discount{ get; set; }
    }
}
