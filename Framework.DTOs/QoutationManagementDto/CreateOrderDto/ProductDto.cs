using Framework.Models.QoutationManagement;
using System;

namespace Framework.DTOs.QoutationManagementDto.CreateOrderDto
{
    public class ProductDto : IRef<Product>
    {
        public String Id { get; set; }
        public String Name { get; set; }
    }
}
