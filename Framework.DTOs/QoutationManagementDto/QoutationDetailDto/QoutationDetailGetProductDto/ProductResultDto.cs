using Framework.Models.QoutationManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.DTOs.QoutationManagementDto.QoutationDetailDto.QoutationDetailGetProductDto
{
    public class ProductResultDto : IRef<Product>
    {
		public String Decription { get; set; }
		public String Size { get; set; }
        public String Unit { get; set; }
		public decimal Price { get; set; }
        public String Images { get; set; }
    }
}
