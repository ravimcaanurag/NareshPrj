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
        private readonly ILog logService;
        public EmployeeService(IJsonUtil _jsonService, ILog _logService)
        {
            jsonService = _jsonService;            
            logService = _logService;
        }
        public async Task<string> AddEmployee(DataInput dataInput)
        {
            string result = "";
            try
            {
                if (dataInput == null) return "Enter Employee Details";
                if (string.IsNullOrWhiteSpace(dataInput.EmployeeName)) return "Enter Employee Name";
                if (string.IsNullOrWhiteSpace(dataInput.Department)) return "ENter Department";
                List<Employee> employees = await GetEmployees();
                if (employees.Where(t => t.EmployeeName == dataInput.EmployeeName).Count() > 0) return $"EmployeeName {dataInput.EmployeeName} already existed";
                int EmployeeID = employees.Count > 0 ? employees.Max(t => t.EmployeeID) + 1 : 100;
                employees.Add(new Employee()
                {
                    EmployeeID = EmployeeID,
                    Departments = await FindDepartments(dataInput.Department),
                    EmployeeName = dataInput.EmployeeName
                });
                result = await jsonService.WriteJson(FilePath.EMPLOYEE, JsonSerializer.Serialize(employees)) == "success" ? "Employee Added sucessfully" : "error";
                logService.Log(LogService.AddEmployee, result, "Info");

            }
            catch (Exception ex)
            {
                logService.Log(LogService.AddEmployee, ex.Message, "Error");
                result = ex.Message;
            }
            return result;
        }

        public async Task<string> DeleteEmployee(int EmployeeID)
        {
            string result = "";
            try
            {
                if (EmployeeID == 0) return "Enter Valid Employee ID";
                List<Employee> employees = await GetEmployees();
                if (employees.Where(t => t.EmployeeID == EmployeeID).Count() > 0)
                {
                    if (employees.RemoveAll(t => t.EmployeeID == EmployeeID) > 0)
                        result = await jsonService.WriteJson(FilePath.EMPLOYEE, JsonSerializer.Serialize(employees)) == "success" ? $"Employee with Employee Id {EmployeeID} is deleted successfully" : "error";
                    else
                        result = "Employee not removed";
                }
                else
                    result = $"Employee with Employee Id {EmployeeID} is does not existed";
                logService.Log(LogService.DeleteEmployee, result, "Info");
            }
            catch (Exception ex)
            {
                result = ex.Message;
                logService.Log(LogService.DeleteEmployee, result, "Error");
            }
            return result;
        }

        public async Task<Employee> GetEmployee(int EmployeeID)
        {
            List<Employee> employees = await GetEmployees();
            logService.Log(LogService.GetEmployee, "Employee List Fetched", "Info");
            return employees.Where(t => t.EmployeeID == EmployeeID).FirstOrDefault();

        }
        public async Task<List<Employee>> GetEmployees()
        {
            string json = await jsonService.ReadJson(FilePath.EMPLOYEE);
            logService.Log(LogService.GetEmployees, "Employee List Fetched", "Info");
            return JsonSerializer.Deserialize<List<Employee>>(json);

        }

        public async Task<string> UpadteEmployee(DataInput dataInput)
        {
            string result = "";
            try
            {            
            if (dataInput == null) return "Enter Employee Details";
            if (dataInput.EmployeeID == 0) return "Enter EmployeeID";
            if (string.IsNullOrWhiteSpace(dataInput.EmployeeName)) return "Enter Employee Name";
            if (string.IsNullOrWhiteSpace(dataInput.Department)) return "ENter Department";
            List<Employee> employees = await GetEmployees();
            if (employees.Where(t => t.EmployeeID == dataInput.EmployeeID).Count() > 0)
            {
                employees = employees.Select(c =>
                   {
                       c.EmployeeName = c.EmployeeID == dataInput.EmployeeID ? dataInput.EmployeeName : c.EmployeeName;
                       c.Departments = c.EmployeeID == dataInput.EmployeeID ? Task.Run(() => FindDepartments(dataInput.Department)).Result : c.Departments;
                       return c;
                   }).ToList();

                result= await jsonService.WriteJson(FilePath.EMPLOYEE, JsonSerializer.Serialize(employees)) == "success" ? "Employee updated sucessfully" : "error";
            }
            else
                result= $"Employee with ${dataInput.EmployeeID} not existed";

            logService.Log(LogService.UpdateEmployee, result, "Info");
            }
            catch (Exception ex)
            {
                result = ex.Message;
                logService.Log(LogService.UpdateEmployee, result, "Error");
            }
            return result;
        }

        public async Task<List<Department>> FindDepartments(string Departments)
        {
            List<Department> departments = new List<Department>();
            try
            {
                List<Department> allDepartments = new List<Department>();
                List<Employee> employees = await GetEmployees();
                allDepartments = employees.SelectMany(t => t.Departments).Distinct().ToList();
                int DepartmentID = allDepartments.Count > 0 ? allDepartments.Max(t => t.DepartmentId) : 100;
                List<string> dNames = Departments.Split(',').ToList();
                int increment = 1;

                foreach (string dName in dNames.Distinct())
                {
                    if (!string.IsNullOrWhiteSpace(dName))
                    {
                        Department department = allDepartments.Where(t => t.DepartmentName.ToLower().Trim() == dName.ToLower().Trim()).FirstOrDefault();
                        departments.Add(new Department() { DepartmentId = department != null ? department.DepartmentId : DepartmentID + increment, DepartmentName = dName });
                        if (department == null) increment++;
                    }
                }
                logService.Log(LogService.FetchDepartments, Departments, "Info");
            }
            catch (Exception ex)
            {
                logService.Log(LogService.FetchDepartments, ex.Message, "Error");
            }
            return departments;
        }
        public async Task<bool> checkEmployeeNameExsited(string EmployeeName)
        {
            bool result = false;
            try
            {
                List<Employee> employees = await GetEmployees();
                result = employees.Count > 0 ? (employees.Where(t => t.EmployeeName == EmployeeName).Count() > 0 ? true : false) : false;
                logService.Log(LogService.AddEmployee, "checkEmployeeNameExsited method executed", "Info");
            }
            catch(Exception ex)
            {
                logService.Log(LogService.AddEmployee, ex.Message,"Error");
            }
           
            return result;
        }
        

    }
}
