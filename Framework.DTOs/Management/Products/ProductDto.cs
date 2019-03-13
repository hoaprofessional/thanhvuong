using Framework.Models.QoutationManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.DTOs.Management.Products
{
    public class ProductDto : IRef<Product>
    {
        public String Id { get; set; }
        public String Name { get; set; }
		public String Decription { get; set; }
		public String Size { get; set; }
        public String Unit { get; set; }
		public decimal Price { get; set; }
        public String Images { get; set; }
        public DateTime? ModifiedTime { get; set; }
        public bool? Active { get; set; }
    }
}
