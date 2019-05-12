using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OData;
using Microsoft.AspNet.OData;
using EmployeeService.infrastructure;
using EmployeeService.Models;

using Microsoft.EntityFrameworkCore;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmployeeService.Controllers
{

    [Produces("application/json")]
  // [Route("api/odata/CDetails")]
    public class CDetailsController : ODataController
    {
        public readonly InventoryContext _db;
        //initalizing  database context
        public CDetailsController(InventoryContext db)
        {
            _db = db;
        }

        //[HttpGet]
        //public ActionResult Get()
        //{

        //    //this code will return the details from the computerdetail table

        //    var data = (from d in _db.CLoans
        //                join
        //                f in _db.CDetails
        //                on d.CDetailId equals f.Id
        //                select new
        //                {
        //                    LogBook = f.Lb_no,
        //                    ItemMake = f.make,
        //                    SerialNo = f.sl_no,
        //                    CpuType = f.Cpu,
        //                    Player = f.DvdType,
        //                    RamConfig = f.Ram,
        //                    loanHolder = d.serNo
        //                }).ToList();
        //    return Ok(data);
        //}



        //  [ODataRoute("Detail")]
        [EnableQuery]
        [HttpGet]
        public IQueryable<CDetail> Get()
        {
            var res = _db.CDetails.AsQueryable();

            return res;
        }


    }
}
