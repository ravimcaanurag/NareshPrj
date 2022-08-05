using empRestAPI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace empRestAPI.Services
{
    public class LogService : ILog
    {
        public static string AddEmployee = "1001";
        public static string UpdateEmployee = "1002";
        public static string GetEmployee = "1003";
        public static string DeleteEmployee = "1004";
        public static string GetEmployees = "1005";
        public static string FetchDepartments = "1006";

        public async void Log(string TagId, string Message, string Type)
        {
          using(StreamWriter sw=new StreamWriter(FilePath.LogPath,true))
            {
                await sw.WriteLineAsync(TagId+"=>"+Type+"=>"+Message);
                sw.Close();
            }  
        }
    }
}
