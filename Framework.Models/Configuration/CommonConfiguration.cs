using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Models.Configuration
{
    public class CommonConfiguration : Auditable
    {
        public String Id { get; set; }
        public String CompanyName { get; set; }
        public String Logo { get; set; }
        public String HeadQuarter { get; set; }
        public String Contact { get; set; }
        public String Website { get; set; }
        public String Hotline { get; set; }
    }
}
