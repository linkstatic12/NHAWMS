//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WMS.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Option
    {
        public short ID { get; set; }
        public short LateMin { get; set; }
        public short EarlyIn { get; set; }
        public short EarlyOut { get; set; }
        public short ExtraOut { get; set; }
        public string PicFolder { get; set; }
        public string CompanyName { get; set; }
        public Nullable<bool> DownTime { get; set; }
        public Nullable<short> OverTime { get; set; }
        public Nullable<System.DateTime> PermanentMonthlyClose { get; set; }
        public Nullable<System.DateTime> ContractualMonthlyClose { get; set; }
        public Nullable<short> CompanyID { get; set; }
        public Nullable<bool> EmergencyStart { get; set; }
    }
}
