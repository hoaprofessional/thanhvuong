using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.InputModels.QoutationManagement.CreateOrder
{
    public class OrderProductInput
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDetail { get; set; }
        public string ProductSize { get; set; }
        public string Unit { get; set; }
        public decimal ProductPrice { get; set; }
        public float VAT { get; set; }
        public int ProductQuantity { get; set; }
        public string ProductNote { get; set; }
        public string ProductImage { get; set; }
        public string ProductImageCode { get; set; }
    }
}
