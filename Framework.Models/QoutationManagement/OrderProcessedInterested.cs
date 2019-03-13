using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Framework.Models.QoutationManagement
{
    [Table("OrderProcessedInterested")]
    public class OrderProcessedInterested : Auditable
    {
        public string Id { get; set; }
        public string Permission { get; set; }
        public string OrderStatusId { get; set; }
        public string OrderStatusStaffCreated { get; set; }
        public bool IsSelf { get; set; }
    }
}
