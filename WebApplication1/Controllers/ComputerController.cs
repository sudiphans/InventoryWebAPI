using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EmployeeService.infrastructure;
using EmployeeService.Models;
using System.Net;
using Microsoft.AspNet.OData;

namespace EmployeeService.Controllers
{
    [Produces("application/json")]
    [Route("api/Computer")]
    public class ComputerController : Controller
    {
        public readonly InventoryContext _db;
        //initalizing  database context
        public ComputerController(InventoryContext db)
        {
            _db = db;
        }

        //getting the computerdetails from this get method
        [HttpGet]
        //[EnableQuery]
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
                        }).ToList();
            return Ok(data);
        }


        [Route("create")]
        [HttpPost]
        public ActionResult Post([FromBody]CDetail CDetailRec)
        {

            //adding multiple data in database
            //int LbNo = 4;

            //for (int i = 0; i <= 500; i++)
            //{
            //    var SeedData = new CDetail
            //    {
            //        Lb_no = LbNo,
            //        sl_no = "CA589785",
            //        make = "DELL",
            //        Cpu = "I3",
            //        DvdType = "DVDROM",
            //        Ram = "2*2GB",
            //        CLoan = new CLoan { serNo = 777698 }



            //    };
            //  
            //    LbNo++;

            //}
            _db.CDetails.Add(CDetailRec);
            _db.SaveChanges();


            return Ok("Saved");
        }

        
        [HttpDelete("{LbNo}")]
        public async Task<ActionResult> Delete(int LbNo)
        {
            List<int> DeletedId = new List<int>();

            var CdetailRecieved = (from d in _db.CDetails where d.Lb_no == LbNo select d);

            if(CdetailRecieved == null)
            {
                return NoContent();
            }
            else
            {
                foreach (CDetail c in CdetailRecieved)
                {
                    DeletedId.Add(c.Id);
                    _db.CDetails.Remove(c);
              


                }

                await _db.SaveChangesAsync();
                return Ok("Deleted Items:"+DeletedId.Count());
            }



        }



    }
}