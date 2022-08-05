using empRestAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace empRestAPI.Services
{
    public interface IEmployee
    {
        public Task<string> AddEmployee(DataInput dataInput);
        public Task<string> UpadteEmployee(DataInput dataInput);
        public Task<string> DeleteEmployee(int EmployeeID);
        public Task<Employee> GetEmployee(int EmployeeID);
        public Task<List<Employee>> GetEmployees();

    }
}
