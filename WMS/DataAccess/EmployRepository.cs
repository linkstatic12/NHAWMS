using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WMS.DataAccess
{
    public class CustomFilter 
    {
        public List<int> DepartmentID { get; set; }

        public List<int> EmployeeID { get; set; }
    }

    public class EmployeeDataAccess
    {
        private ReportsDataContext db = new ReportsDataContext();

        public List<Emp> GetEmployees(string search)
        {
            if (search == string.Empty || string.IsNullOrEmpty(search))
            {
                return db.Emps.ToList();
            }
            var employees = from e in db.Emps
                            where e.EmpName.Contains(search) ||
                            e.FatherName.Contains(search) || 
                            e.NicNo.Contains(search)
                            select e;
            return employees.ToList();
        }
    }
}