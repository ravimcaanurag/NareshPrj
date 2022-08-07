using System;
using Xunit;
using empRestAPI.Controllers;
using empRestAPI.Services;
using empRestAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmpRestApI.Test
{
    public class RestAPITestCases
    {
        private readonly IJsonUtil jsonUtil;
        private readonly IEmployee employeeService;
        private readonly ILog logService;
        private readonly EmployeeController employeeController;
        public RestAPITestCases()
        {
            jsonUtil = new JsonService();
            logService = new LogService();
            employeeService = new EmployeeService(jsonUtil, logService);
            employeeController=new EmployeeController(employeeService);
        }
        [Fact]
        public void AddEmployee_Without_EmployeeName()
        {
            //Arrange
            DataInput dataInput = new DataInput() { Department = "Testing,IT" };
            //Act            
            var result=  employeeController.AddEmployee(dataInput).Result;
            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
           
        }
        [Fact]
        public void AddEmployee_Without_Department()
        {
            //Arrange
            DataInput dataInput = new DataInput() { EmployeeName="ABCD" };
            //Act            
            var result = employeeController.AddEmployee(dataInput).Result;
            //Assert
            Assert.IsType<BadRequestObjectResult>(result);

        }
        [Fact]
        public void AddDepartment()
        {
            //Arrange
            DataInput dataInput = new DataInput() { EmployeeName = "KKKK", Department="Testing,IT"};
            //Act            
            var response = employeeController.AddEmployee(dataInput).Result as OkObjectResult;
            //Assert
            Assert.IsType<OkObjectResult>(response);
            Assert.Equal("Employee Added sucessfully", response.Value.ToString());

        }
        [Fact]
        public void AddDepartment_WithDupliacte_EmployeeName()
        {
            //Arrange
            DataInput dataInput = new DataInput() { EmployeeName = "RRK", Department = "Testing,IT" };
            //Act            
            var response = employeeController.AddEmployee(dataInput).Result as BadRequestObjectResult;
            //Assert
            Assert.IsType<BadRequestObjectResult>(response);
            Assert.Equal($"EmployeeName {dataInput.EmployeeName} already existed", response.Value.ToString());

        }
    }
}
