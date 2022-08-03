using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace empRestAPI.Services
{
    public class JsonService : IJsonUtil
    {

        public async Task<string> ReadJson(string filePath)
        {
            string json = "";
            try
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    json = await sr.ReadToEndAsync();
                    sr.Close();
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return json;
        }

        public async Task<string> WriteJson(string filePath,string json)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(filePath, false))
                {
                    await sw.WriteAsync(json);
                    sw.Close();
                }
            }
            catch(Exception ex)
            {
                return ex.Message;
            }

            return "success";
            
        }
    }
}
