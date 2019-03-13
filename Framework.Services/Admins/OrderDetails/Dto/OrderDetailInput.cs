using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Framework.Services.Admins.OrderDetails.Dto
{
    public class OrderDetailInput
    {
        public String Id { get; set; }
        [Required(ErrorMessage = "Số lượng sản phẩm bắt buộc nhập")]
        public int ProductQuantity { get; set; }
        [Required(ErrorMessage = "Đơn giá bắt buộc nhập")]
        public decimal UnitPrice { get; set; }
        [Required(ErrorMessage = "VAT bắt buộc nhập")]
        public double VAT { get; set; }

    }
}
