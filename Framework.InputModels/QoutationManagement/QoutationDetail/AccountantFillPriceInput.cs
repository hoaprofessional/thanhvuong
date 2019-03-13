using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Framework.InputModels.QoutationManagement.QoutationDetail
{
    public class AccountantFillPriceInput
    {
        [Required]
        public int QoutationId { get; set; }
        public ProductPrice[] Products { get; set; }
    }
}
