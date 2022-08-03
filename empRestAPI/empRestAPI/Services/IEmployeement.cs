using empRestAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace empRestAPI.Services
{
    public interface IEmployee
    {
        public Task<string> AddEmployee(Employee employee);
        public Task<string> UpadteEmployee(Employee employee);
        public Task<string> DeleteEmployee(int EmployeeID);
        public Task<Employee> GetEmployee(int EmployeeID);
        public Task<List<Employee>> GetEmployees();

    }
}
