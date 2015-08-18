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
    
    public partial class Company
    {
        public Company()
        {
            this.Crews = new HashSet<Crew>();
            this.Designations = new HashSet<Designation>();
            this.Emps = new HashSet<Emp>();
            this.EmpTypes = new HashSet<EmpType>();
            this.Grades = new HashSet<Grade>();
            this.Readers = new HashSet<Reader>();
            this.Users = new HashSet<User>();
        }
    
        public short CompID { get; set; }
        public string CompName { get; set; }
        public string Address { get; set; }
        public string TelNo { get; set; }
        public string Email { get; set; }
    
        public virtual ICollection<Crew> Crews { get; set; }
        public virtual ICollection<Designation> Designations { get; set; }
        public virtual ICollection<Emp> Emps { get; set; }
        public virtual ICollection<EmpType> EmpTypes { get; set; }
        public virtual ICollection<Grade> Grades { get; set; }
        public virtual ICollection<Reader> Readers { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}