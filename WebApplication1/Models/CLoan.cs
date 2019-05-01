using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace EmployeeService.Models
{
    
    public class CLoan
    {
       
        public int Id { get; set; }

        public int? serNo { get; set; }

        [ForeignKey("CDetailId")]
        public int CDetailId { get; set; }
        public virtual CDetail details { get; set; }
        
    }
}
