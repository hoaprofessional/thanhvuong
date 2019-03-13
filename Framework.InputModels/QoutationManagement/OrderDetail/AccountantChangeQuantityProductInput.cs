using Framework.DTOs;
using Framework.Models.QoutationManagement;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Framework.InputModels.QoutationManagement.OrderDetail
{
    public class AccountantChangeQuantityProductInput : IRef<Order>
    {
        [Required]
        public string OrderId { get; set; }
        public List<AccountantChangeQuantityProductItemInput> Products { get; set; }
    }
}
