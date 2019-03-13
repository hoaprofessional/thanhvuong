using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Framework.InputModels.QoutationManagement.QoutationDetail
{
    public class RejectQoutationInput
    {
        [Required]
        public int QoutationId { get; set; }
        [Required]
        public String Content { get; set; }
    }
}
