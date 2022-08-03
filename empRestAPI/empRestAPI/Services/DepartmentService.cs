using empRestAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
namespace empRestAPI.Services
{
    public class DepartmentService : IDepartment
    {
        private readonly IJsonUtil jsonService;

        public DepartmentService(IJsonUtil _jsonService)
        {
            jsonService = _jsonService;
        }
        public async Task<string> AddDepartment(Department department)        {
            List<Department> departments = await GetDepartments();
            if (departments.Where(t=>t.DepartmentName == department.DepartmentName).Count() > 0)
               return $"Department with {department.DepartmentName} already existed";           
            else
            {
                Department dept = departments.LastOrDefault();                
                departments.Add(new Department() { DepartmentId = dept != null ? dept.DepartmentId + 1 : 100, DepartmentName = department.DepartmentName });
            }
            return await jsonService.WriteJson(FilePath.DEPARTMENT,JsonSerializer.Serialize(departments))== "success"?"Department Added sucessfully":"error";
        }

        public async Task<string> DeleteDepartment(int DepartmentID)
        {
            List<Department> departments = await GetDepartments();
            if (departments.Where(t => t.DepartmentId == DepartmentID).Count() > 0)
            {
                if (departments.RemoveAll(t => t.DepartmentId == DepartmentID) > 0)
                    return await jsonService.WriteJson(FilePath.DEPARTMENT, JsonSerializer.Serialize(departments)) == "success" ? $"Department with Department Id {DepartmentID} is deleted successfully": "error";                 
                else
                    return "Department not removed";
            }
            else
            return $"Department with Department Id {DepartmentID} is does not existed";


        }

        public async Task<Department> GetDepartment(int DepartmentID)
        {
            List<Department> departments = await GetDepartments();
            return departments.Where(t => t.DepartmentId == DepartmentID).FirstOrDefault();
        }

        public async Task<List<Department>> GetDepartments()
        {
            string json = await jsonService.ReadJson(FilePath.DEPARTMENT);
            return JsonSerializer.Deserialize<List<Department>>(json);
        }

        public async Task<string> UpadteDepartment(Department department)
        {
            List<Department> departments = await GetDepartments();
            if (departments.Where(t => t.DepartmentId == department.DepartmentId).Count() > 0)
            {
                departments= departments.Select(c => {
                    c.DepartmentName= c.DepartmentId== department.DepartmentId?department.DepartmentName:c.DepartmentName; 
                    return c; }).ToList();               
                return await jsonService.WriteJson(FilePath.DEPARTMENT, JsonSerializer.Serialize(departments))== "success" ? "Department updated sucessfully":"error";

            }
            else
                return $"Department with ${department.DepartmentId} not existed";
           
        }
    }
}
