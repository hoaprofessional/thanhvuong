using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Framework.DTOs.Management.Products
{
    public class ProductInput
    {
        [Required(ErrorMessage ="Mã SP không được để trống")]
        public String Id { get; set; }
        [Required(ErrorMessage = "Tên SP không được để trống")]
        public String Name { get; set; }
		public String Decription { get; set; }
        [Required(ErrorMessage = "Kích thước không được để trống")]
        public String Size { get; set; }
        [Required(ErrorMessage = "Đơn vị không được để trống")]
        public String Unit { get; set; }
        public IList<IFormFile> Files { get; set; }
        public String Images { get; set; }
    }
}
