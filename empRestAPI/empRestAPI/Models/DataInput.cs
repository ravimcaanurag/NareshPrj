using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace empRestAPI.Models
{
    public class DataInput
    {
        public int EmployeeID { get; set; }
        
        [Required]
        public string EmployeeName { get; set;  }
        [Required]
        public string Department { get; set; }

    }
}
