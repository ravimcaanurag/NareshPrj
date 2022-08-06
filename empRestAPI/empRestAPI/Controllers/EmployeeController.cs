using empRestAPI.Models;
using empRestAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace empRestAPI.Controllers
{
    [Authorize]
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
        public async Task<IActionResult> AddEmployee(DataInput dataInput)
        {
            if (string.IsNullOrWhiteSpace(dataInput.Department)) return BadRequest("Enter Department Name");
            if (string.IsNullOrWhiteSpace(dataInput.EmployeeName)) return BadRequest("Enter Employee Name");
            var result = await employeeService.AddEmployee(dataInput);
            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateEmployee(DataInput dataInput)
        {
            if (string.IsNullOrWhiteSpace(dataInput.Department)) return BadRequest("Enter Department Name");
            if (string.IsNullOrWhiteSpace(dataInput.EmployeeName)) return BadRequest("Enter Employee Name");
            if (dataInput.EmployeeID==0) return BadRequest("Enter Employee ID");

            var result = await employeeService.UpadteEmployee(dataInput);
            return Ok(result);
        }
        [HttpDelete("{employeeId}")]
        public async Task<IActionResult> DeleteEmployee(int employeeId)
        {
            if (employeeId == 0) return BadRequest("Enter Employee ID");
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
            if (employeeId == 0) return BadRequest("Enter Employee ID");
            var result = await employeeService.GetEmployee(employeeId);
            return Ok(result);
        }


    }
}
