using empClientApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace empClientApp.Services
{
    public interface IEmployee
    {
        public Task<string> AddEmployee(DataInput dataInput,string Jwttoken);
        public Task<string> UpadteEmployee(DataInput dataInput, string Jwttoken);
        public Task<string> DeleteEmployee(int EmployeeID, string Jwttoken);
        public Task<Employee> GetEmployee(int EmployeeID, string Jwttoken);
        public Task<List<Employee>> GetEmployees(string Jwttoken);

    }
}
