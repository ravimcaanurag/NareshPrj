using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace empRestAPI.Services
{
   public  interface ITokenManager
    {
        public Task<string> GenerateToken(string user);
    }
}
