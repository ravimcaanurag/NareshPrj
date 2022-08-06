using System;
using System.Collections.Generic;
using System.Text;

namespace empClientApp.Models
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
