using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Framework.InputModels.QoutationManagement.OrderDetail
{
    public class ApproveAlreadyCreatedOrderInput
    {
        [Required]
        public string OrderId { get; set; }
        public String ApproveType { get; set; }
    }
}
