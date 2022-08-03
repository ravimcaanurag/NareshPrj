using empRestAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace empRestAPI.Services
{
    public class EmployeeService : IEmployee
    {
        private readonly IJsonUtil jsonService;
        private readonly IDepartment departmentService;
        public EmployeeService(IJsonUtil _jsonService, IDepartment _departmentService)
        {
            jsonService = _jsonService;
            departmentService = _departmentService;
        }
        public async Task<string> AddEmployee(Employee employee)
        {
            List<Employee> employees = await GetEmployees();
            List<Department> departments = await departmentService.GetDepartments();
            if (departments.Where(t => t.DepartmentId == employee.DepartmentID).Count() <= 0)
                return $"Department with {employee.DepartmentID} does not existed";
            else
            {
                Employee lastEmployee = employees.LastOrDefault();
                employees.Add(new Employee() { EmployeeID = lastEmployee != null ? lastEmployee.EmployeeID + 1 : 100, EmployeeName = employee.EmployeeName, DepartmentID=employee.DepartmentID });  ;
            }
            return await jsonService.WriteJson(FilePath.EMPLOYEE, JsonSerializer.Serialize(employees)) == "success" ? "Employee Added sucessfully" : "error";
        }

        public async Task<string> DeleteEmployee(int EmployeeID)
        {
            List<Employee> employees = await GetEmployees();
            if (employees.Where(t => t.EmployeeID == EmployeeID).Count() > 0)
            {
                if (employees.RemoveAll(t => t.EmployeeID == EmployeeID) > 0)
                    return await jsonService.WriteJson(FilePath.EMPLOYEE, JsonSerializer.Serialize(employees)) == "success" ? $"Employee with Employee Id {EmployeeID} is deleted successfully" : "error";
                else
                    return "Employee not removed";
            }
            else
                return $"Employee with Employee Id {EmployeeID} is does not existed";

        }

        public async Task<Employee> GetEmployee(int EmployeeID)
        {
            List<Employee> employees = await GetEmployees();
            return employees.Where(t => t.EmployeeID == EmployeeID).FirstOrDefault();
        }

        public async Task<List<Employee>> GetEmployees()
        {
            string json = await jsonService.ReadJson(FilePath.EMPLOYEE);
            return JsonSerializer.Deserialize<List<Employee>>(json);

        }

        public async Task<string> UpadteEmployee(Employee employee)
        {
            List<Department> departments = await departmentService.GetDepartments();
            if (departments.Where(t => t.DepartmentId == employee.DepartmentID).Count() <= 0)
                return $"Department with {employee.DepartmentID} does not existed";            
                List<Employee> employees = await GetEmployees();
            if (employees.Where(t => t.EmployeeID == employee.EmployeeID).Count() > 0)
            {
                employees = employees.Select(c => {
                    c.EmployeeName = c.EmployeeID == employee.EmployeeID ? employee.EmployeeName : c.EmployeeName;
                    c.DepartmentID  = c.EmployeeID == employee.EmployeeID ? employee.DepartmentID : c.DepartmentID;
                    return c;
                }).ToList();
                return await jsonService.WriteJson(FilePath.EMPLOYEE, JsonSerializer.Serialize(employees)) == "success" ? "Employee updated sucessfully" : "error";

            }
            else
                return $"Employee with ${employee.EmployeeID} not existed";


        }
    }
}
