using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Framework.Models.QoutationManagement
{
    [Table("QuotesStatusWaitingApprovalInterested")]
    public class QuotesStatusWaitingApprovalInterested : Auditable
    {
        public string Id { get; set; }
        public string Permission { get; set; }
        public string QoutationStatusId { get; set; }
        public string QoutationStatusStaffCreated { get; set; }
        public bool IsSelf { get; set; }
    }
}
