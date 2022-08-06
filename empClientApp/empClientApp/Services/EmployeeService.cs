using empClientApp.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace empClientApp.Services
{
    public class EmployeeService : IEmployee
    {
       
        public HttpClient getHttpClient(string Jwttoken)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(Settings.RestApiUrl);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Jwttoken.Replace('"', ' ').Trim());
            return httpClient;
        }
        public async Task<string> AddEmployee(DataInput dataInput, string Jwttoken)
        {
            string result = "";
            try
            {
                HttpClient httpClient = getHttpClient(Jwttoken);                
                HttpResponseMessage response = await httpClient.PostAsync("api/Employee/AddEmployee", new StringContent(JsonSerializer.Serialize(dataInput), Encoding.UTF8, "application/json"));
                response.EnsureSuccessStatusCode();
                result = await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }

            return !string.IsNullOrEmpty(result)?result.Replace('"', ' ').Trim():"Error";

        }

        public async Task<string> DeleteEmployee(int EmployeeID, string Jwttoken)
        {
            string result = "";
            try
            {
                HttpClient httpClient = getHttpClient(Jwttoken);             
                HttpResponseMessage response = await httpClient.DeleteAsync($"api/Employee/DeleteEmployee/{EmployeeID}");
                response.EnsureSuccessStatusCode();
                result = await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }

            return !string.IsNullOrEmpty(result) ? result.Replace('"', ' ').Trim() : "Error";

        }

        public async Task<Employee> GetEmployee(int EmployeeID, string Jwttoken)
        {
            Employee employee = new Employee();
            try
            {
                HttpClient httpClient = getHttpClient(Jwttoken);
                HttpResponseMessage response = await httpClient.GetAsync($"api/Employee/GetEmployee/{EmployeeID}");
                response.EnsureSuccessStatusCode();
                string result = await response.Content.ReadAsStringAsync();
                if (!string.IsNullOrWhiteSpace(result))
                {
                    employee = JsonSerializer.Deserialize<Employee>(result);
                }
            }
            catch (Exception ex)
            {
                
            }

            return employee;
        }

        public async Task<List<Employee>> GetEmployees(string Jwttoken)
        {
            List<Employee> employees = new List<Employee>();
            try
            {
                HttpClient httpClient = getHttpClient(Jwttoken);
                HttpResponseMessage response = await httpClient.GetAsync($"api/Employee/GetEmployees");
                response.EnsureSuccessStatusCode();
                string result = await response.Content.ReadAsStringAsync();
                if (!string.IsNullOrWhiteSpace(result))
                {
                    employees = JsonSerializer.Deserialize<List<Employee>>(result);
                }
            }
            catch (Exception ex)
            {

            }

            return employees;
        }

        public async Task<string> UpadteEmployee(DataInput dataInput, string Jwttoken)
        {
            string result = "";
            try
            {
                HttpClient httpClient = getHttpClient(Jwttoken);                
                HttpResponseMessage response = await httpClient.PutAsync("api/Employee/UpdateEmployee", new StringContent(JsonSerializer.Serialize(dataInput), Encoding.UTF8, "application/json"));
                response.EnsureSuccessStatusCode();
                result = await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }

            return !string.IsNullOrEmpty(result) ? result.Replace('"', ' ').Trim() : "Error";
        }
    }
}
