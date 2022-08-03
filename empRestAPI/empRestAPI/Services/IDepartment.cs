using empRestAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace empRestAPI.Services
{
   public interface IDepartment
    {
        public  Task<string> AddDepartment(Department department);
        public Task<string> UpadteDepartment(Department department);
        public Task<string> DeleteDepartment(int DepartmentID);
        public Task<Department> GetDepartment(int DepartmentID);
        public Task<List<Department>> GetDepartments();


    }
}
