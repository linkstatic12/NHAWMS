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
    
    public partial class Grade
    {
        public Grade()
        {
            this.Emps = new HashSet<Emp>();
        }
    
        public byte GradeID { get; set; }
        public string GradeName { get; set; }
        public Nullable<short> CompID { get; set; }
    
        public virtual Company Company { get; set; }
        public virtual ICollection<Emp> Emps { get; set; }
    }
}
