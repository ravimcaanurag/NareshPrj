using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace empRestAPI.Services
{
    public interface ILog
    {
        public void Log(string TagId, string Message, string Type);
    }
}
