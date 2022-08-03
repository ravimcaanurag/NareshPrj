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
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployee employeeService;
        public EmployeeController(IEmployee _employeeService)
        {
            employeeService = _employeeService;
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee(Employee employee)
        {
            var result = await employeeService.AddEmployee(employee);
            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateEmployee(Employee employee)
        {
            var result = await employeeService.UpadteEmployee(employee);
            return Ok(result);
        }
        [HttpDelete("{employeeId}")]
        public async Task<IActionResult> DeleteEmployee(int employeeId)
        {
            var result = await employeeService.DeleteEmployee(employeeId);
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            var result = await employeeService.GetEmployees();
            return Ok(result);
        }
        [HttpGet("{employeeId}")]
        public async Task<IActionResult> GetEmployee(int employeeId)
        {
            var result = await employeeService.GetEmployee(employeeId);
            return Ok(result);
        }


    }
}
