using empRestAPI.Models;
using empRestAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace empRestAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartment departmentService; 
        public DepartmentController(IDepartment _departmentService)
        {
            departmentService = _departmentService;
        }

        [HttpPost]
        public async Task<IActionResult> AddDepartment(Department department)
        {
            var result = await departmentService.AddDepartment(department);
            return Ok(result) ;
        }
        [HttpPut]
        public async Task<IActionResult> UpdateDepartment(Department department)
        {
            var result = await departmentService.UpadteDepartment(department);
            return Ok(result);
        }
        [HttpDelete("{departmentId}")]
        public async Task<IActionResult> DeleteDepartment(int departmentId)
        {
            var result = await departmentService.DeleteDepartment(departmentId);
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetDepartments()
        {
            var result = await departmentService.GetDepartments();
            return Ok(result);
        }
        [HttpGet("{departmentId}")]
        public async Task<IActionResult> GetDepartment(int departmentId)
        {
            var result = await departmentService.GetDepartment(departmentId);
            return Ok(result);
        }
    }
}
