using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EmployeeService.infrastructure;
using EmployeeService.Models;

namespace EmployeeService.Controllers
{
    [Produces("application/json")]
    [Route("api/Computer")]
    public class ComputerController : Controller
    {
        public readonly InventoryContext _db;
        //initalizing  database context
        public ComputerController (InventoryContext db)
        {
            _db = db;
        }
        
        //getting the computerdetails from this get method
        [HttpGet]
        public ActionResult Get()
        {

            //this code will return the details from the computerdetail table

            var data = (from d in _db.CLoans
                        join
                        f in _db.CDetails
                        on d.CDetailId equals f.Id
                        select new
                        {
                            LogBook = f.Lb_no,
                            ItemMake = f.make,
                            SerialNo = f.sl_no,
                            CpuType = f.Cpu,
                            Player = f.DvdType,
                            RamConfig = f.Ram,
                            loanHolder = d.serNo
                        }
                        


                        ).ToList();


            return Ok(data);


        }


        [Route("create")]
        [HttpPost]
        public ActionResult Post([FromBody]CDetail CDetailRec)
        {
            try
            {
                /*
                var cdetail = new CDetail
                {
                    Id = CDetailRec.Id,
                    Lb_no = CDetailRec.Lb_no,
                    sl_no = CDetailRec.sl_no,
                    make = CDetailRec.make,
                    Cpu = CDetailRec.Cpu,
                    DvdType = CDetailRec.DvdType,
                    Ram = CDetailRec.Ram,
                    CLoan = CDetailRec.CLoan



                };
                */


                _db.CDetails.Add(CDetailRec);
                _db.SaveChanges();

                /*
                var cdetail = new CDetail{
                    Id = 2,
                    Lb_no = 2,
                    sl_no = "1234",
                    make = "hcl"
                };

                var cloan = new CLoan { Id = 2, serNo = 777698 };

                cdetail.CLoan = cloan;

                _db.CDetails.Add(cdetail);
                _db.SaveChanges();
                  */


            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + "Inner" + ex.InnerException);
            }

            return Ok("Saved");
        }



    }
}