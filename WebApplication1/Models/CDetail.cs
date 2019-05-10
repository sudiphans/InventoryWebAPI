using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace EmployeeService.Models
{
  
    public class CDetail
    {
        
        
        public int Id{get; set;}

        public int Lb_no { get; set; }

        public string  sl_no{ get; set; }

        public string make { get; set; }
        
        public string Cpu { get; set; }

        public string DvdType { get; set; }

        public string Ram { get; set; }

        //navigational property
        public CLoan CLoan { get; set; }

    }
}
