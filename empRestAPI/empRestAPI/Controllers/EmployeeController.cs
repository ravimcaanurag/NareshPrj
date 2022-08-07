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
        public async Task<IActionResult> AddEmployee([FromBody] DataInput dataInput)
        {
            if (dataInput == null) return BadRequest("Enter Employee Details");            
            if (string.IsNullOrWhiteSpace(dataInput.Department)) return BadRequest("Enter Department Name");
            if (string.IsNullOrWhiteSpace(dataInput.EmployeeName)) return BadRequest("Enter Employee Name");
            var result = await employeeService.checkEmployeeNameExsited(dataInput.EmployeeName);
            if (result) return BadRequest($"EmployeeName {dataInput.EmployeeName} already existed");
            var output = await employeeService.AddEmployee(dataInput);
            return Ok(output);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateEmployee([FromBody] DataInput dataInput)
        {
            if (dataInput == null) return BadRequest("Enter Employee Details");            
            if (string.IsNullOrWhiteSpace(dataInput.Department)) return BadRequest("Enter Department Name");
            if (string.IsNullOrWhiteSpace(dataInput.EmployeeName)) return BadRequest("Enter Employee Name");
            if (dataInput.EmployeeID == 0) return BadRequest("Enter Employee ID");
            var result = await employeeService.GetEmployee(dataInput.EmployeeID);
            if (result == null) return NotFound($"Employee with {dataInput.EmployeeID } not existed");
            var output = await employeeService.UpadteEmployee(dataInput);
            return Ok(output);
        }
        [HttpDelete("{employeeId}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute]int employeeId)
        {
            if (employeeId == 0) return BadRequest("Enter Employee ID");
            var result = await employeeService.GetEmployee(employeeId);
            if (result == null) return NotFound($"Employee with ID {employeeId } not existed");
            var output = await employeeService.DeleteEmployee(employeeId);
            return Ok(output);
        }
        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            var result = await employeeService.GetEmployees();
            return Ok(result);
        }
        [HttpGet("{employeeId}")]
        public async Task<IActionResult> GetEmployee([FromRoute]int employeeId)
        {
            if (employeeId == 0) return BadRequest("Enter Employee ID");
            var result = await employeeService.GetEmployee(employeeId);
            if (result == null) return NotFound($"Employee with ID {employeeId } not existed");
            return Ok(result);
        }


    }
}
