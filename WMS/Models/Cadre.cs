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
    
    public partial class Cadre
    {
        public Cadre()
        {
            this.EmpPostings = new HashSet<EmpPosting>();
        }
    
        public short CadID { get; set; }
        public string CadreName { get; set; }
    
        public virtual ICollection<EmpPosting> EmpPostings { get; set; }
    }
}
