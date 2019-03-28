using Framework.InputModels.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Framework.InputModels.QoutationManagement.CreateQoutation
{
    public class QoutationProductInput
    {
        public string ProductId { get; set; }
        [Required]
        public String ProductName { get; set; }
        public String ProductNote { get; set; }
        public String ProductDetail { get; set; }
        public String ProductUnit { get; set; }
        public String ProductImage { get; set; }
        public String ProductImageCode { get; set; }
        public String ProductSize { get; set; }
        public String Discount { get; set; }
        [MinValue(0)]
        public int ProductQuantity { get; set; }

    }
}
