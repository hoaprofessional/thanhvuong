using Framework.DTOs;
using Framework.Models.QoutationManagement;
using System;
using System.ComponentModel.DataAnnotations;

namespace Framework.InputModels.QoutationManagement.CreateOrder
{
    public class CreateBlankOrderInput : IRef<Order>
    {
        [Required]
        public String Name { get; set; }
        [Required]
        public String Address { get; set; }
        [Required]
        public String PhoneNumber { get; set; }
        public String DeliveryPlace { get; set; }
        [Required]
        public OrderProductInput[] Products { get; set; }
        public decimal Promotion { get; set; }
    }
}
