using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace empClientApp.Services
{
   public interface ITokenManager
    {
        public Task<string> GenerateToken(string Email);
    }
}
