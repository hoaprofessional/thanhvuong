using Framework.DTOs;
using Framework.Models.QoutationManagement;
using System;
using System.ComponentModel.DataAnnotations;

namespace Framework.InputModels.QoutationManagement.OrderDetail
{
    public class ChangeExpectedReturnGoodDateInput : IRef<Order>
    {
        [Required]
        public string OrderId { get; set; }
        [Required]
        public DateTime ExpectedReturnGoodDate { get; set; }
    }
}
