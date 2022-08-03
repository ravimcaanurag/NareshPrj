using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace empRestAPI.Services
{
   public interface IJsonUtil
    {
        public Task<string> ReadJson(string filePath);
        public Task<string> WriteJson(string filePath,string json);
    }
}
