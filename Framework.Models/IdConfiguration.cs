using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Framework.Models
{
    [Table("IdConfiguration")]
    public class IdConfiguration
    {
        [MaxLength(100)]
        [Key]
        public String Prefix { get; set; }
        public int CurrentValue { get; set; }
    }
}
