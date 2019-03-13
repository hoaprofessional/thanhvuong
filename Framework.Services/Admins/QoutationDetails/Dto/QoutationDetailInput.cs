using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Framework.Services.Admins.QoutationDetails.Dto
{
    public class QoutationDetailInput
    {
        public String Id { get; set; }
        [Required(ErrorMessage = "Số lượng sản phẩm bắt buộc nhập")]
        public int ProductQuantity { get; set; }
        [Required(ErrorMessage = "Đơn giá nhập bắt buộc nhập")]
        public decimal UnitPriceSell { get; set; }
        [Required(ErrorMessage = "Đơn giá bán bắt buộc nhập")]
        public decimal UnitPriceBuy { get; set; }
        [Required(ErrorMessage = "VAT nhập bắt buộc nhập")]
        public double VATBuy { get; set; }
        [Required(ErrorMessage = "VAT bán bắt buộc nhập")]
        public double VATSell { get; set; }

    }
}
