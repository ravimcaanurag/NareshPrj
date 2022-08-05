using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace empRestAPI.Models
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }

        public List<Department> Departments { get; set; }

        public Employee()
        {
            Departments = new List<Department>();
        }

    }
}
