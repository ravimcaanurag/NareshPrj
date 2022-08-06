using empClientApp.Models;
using empClientApp.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Threading;
using ConsoleTables;
using System.Linq;

namespace empClientApp
{
    public class Executer
    {

        private readonly ITokenManager tokenManager;
        private readonly IEmployee employeeService;
        public Executer(ITokenManager _tokenManager, IEmployee _employeeService)
        {
            tokenManager = _tokenManager;
            employeeService = _employeeService;
        }
        public  void Run()
        {
            Console.WriteLine("------------------ Employee Operations---------------");
            Console.WriteLine("Please Wait,Generating Jwt Token......");
            Thread.Sleep(1000);
            string token = Task.Run(async () => await tokenManager.GenerateToken(Settings.Email)).Result;
            Console.WriteLine("JWT Token generated Successfully");
            Console.WriteLine();

            while (true)
            {
                Console.WriteLine("Please Wait,fetching Options");
                Thread.Sleep(2000);
                Console.WriteLine("------Options------");
                Console.WriteLine("1.Add Employee");
                Console.WriteLine("2.Update Employee");
                Console.WriteLine("3.Delete Employee");
                Console.WriteLine("4.Get Employee by Employee ID");
                Console.WriteLine("5.Get All Employees");
                Console.WriteLine("6.Regenerate Jwt Token");
                Console.WriteLine("7.Exit");
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Enter your Option");
                string option = Console.ReadLine();
                string output = "", EmployeeName = "", Departments = "", EmployeeID = "";
                
                switch (option)
                {
                    case "1": {

                            EmployeeName: Console.WriteLine("Enter EmployeeName");
                                          EmployeeName = Console.ReadLine();
                            if (CheckEmployeeName(EmployeeName)) goto EmployeeName;

                            Departments:
                                        Console.WriteLine("Enter Departments");
                                        Departments = Console.ReadLine();
                                        if (CheckDepartments(Departments)) goto Departments;
                            output = Task.Run(async () => await employeeService.AddEmployee(new DataInput() { EmployeeName = EmployeeName, Department = Departments }, token)).Result;
                            Console.WriteLine(output);

                        };break;


                    case "2": {

                            EmployeeID: Console.WriteLine("Enter EmployeeID");
                                        EmployeeID = Console.ReadLine();
                            if (CheckEmployeeId(EmployeeID)) { Console.WriteLine("Enter Valid Employee ID"); goto EmployeeID; }
                            EmpName:Console.WriteLine("Enter EmployeeName");
                                          EmployeeName = Console.ReadLine();
                                        if (CheckEmployeeName(EmployeeName)) goto EmpName;
                            Depts: Console.WriteLine("Enter Departments");
                                         Departments = Console.ReadLine();
                                         if (CheckDepartments(Departments)) goto Depts;
                            output = Task.Run(async () => await employeeService.UpadteEmployee(new DataInput() { EmployeeID=int.Parse(EmployeeID), EmployeeName = EmployeeName, Department = Departments }, token)).Result;
                            Console.WriteLine(output);

                        }break;

                    case "3":
                        {
                            EmployeeID: Console.WriteLine("Enter EmployeeID");
                                        EmployeeID = Console.ReadLine();
                                        if (CheckEmployeeId(EmployeeID)) goto EmployeeID;

                            output = Task.Run(async () => await employeeService.DeleteEmployee(int.Parse(EmployeeID), token)).Result;
                            Console.WriteLine(output);
                            
                        }
                        break;

                    case "4":
                        {
                            EmployeeID:
                            {
                                Console.WriteLine("Enter EmployeeID");
                                EmployeeID = Console.ReadLine();
                            }
                            if (CheckEmployeeId(EmployeeID)) { goto EmployeeID; }
                            Employee employee = Task.Run(async () => await employeeService.GetEmployee(int.Parse(EmployeeID), token)).Result;
                            if (employee.EmployeeID  != 0)
                            {
                                var table = new ConsoleTable("Employee ID", "Name", "Departments");
                                table.AddRow(employee.EmployeeID, employee.EmployeeName, string.Join(",", employee.Departments.Select(x => x.DepartmentName.ToString()).ToArray()));
                                table.Write();
                            }
                            else
                            {
                                Console.WriteLine("Employee Not Found");
                            }
                            Console.WriteLine();
                        };break;

                    case "5":
                        {
                            List<Employee> employees = Task.Run(async () => await employeeService.GetEmployees(token)).Result;
                            var table = new ConsoleTable("Employee ID", "Name", "Departments");
                            foreach(Employee employee in employees)
                            table.AddRow(employee.EmployeeID, employee.EmployeeName, string.Join(",", employee.Departments.Select(x => x.DepartmentName.ToString()).ToArray()));
                            table.Write();
                            Console.WriteLine();

                        }; break;
                    case "6":
                        {
                            Console.WriteLine("Please Wait,Generating Jwt Token......");
                            Thread.Sleep(1000);
                            token = Task.Run(async () => await tokenManager.GenerateToken(Settings.Email)).Result;
                            Console.WriteLine("JWT Token generated Successfully");
                            Console.WriteLine();

                        }; break;
                    case "7":
                        {
                            Console.WriteLine("Thank you");
                            return;
                        }; break;
                }
            }


            Console.ReadKey();
        }

        public bool CheckEmployeeName(string name)
        {
            return string.IsNullOrWhiteSpace(name) ? true : false;
        }
        public bool CheckEmployeeId(string employeeId)
        {
            if (!int.TryParse(employeeId, out int n)) return true; 
            return string.IsNullOrWhiteSpace(employeeId) ? true : false;
        }
        public bool CheckDepartments(string departments){
            if (departments.Trim() == ",") return true;
            return string.IsNullOrWhiteSpace(departments) ? true : false;
        }
    }
}
