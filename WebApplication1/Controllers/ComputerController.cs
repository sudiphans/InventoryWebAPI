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
using Microsoft.EntityFrameworkCore;
using EmployeeService.Messagebroker;
using RabbitMQ.Client;
using EmployeeService.Global;



namespace EmployeeService.Controllers
{
    [Produces("application/json")]
    [Route("api/Computer")]
    public class ComputerController : Controller
    {
        public readonly InventoryContext _db;
        
        //global connection for rabbitmq client
        //IModel RabbitMqModel = RabbitMq.CreateChannel();
      
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

           
            var data = _db.CDetails
                      
                .ToList();

            List<int?> loanHolders = new List<int?>();

            foreach(CDetail d in data)
            {
                var LoanHolder = d.CLoan.serNo;
                loanHolders.Add(LoanHolder);
            }

            //var data = (from d in _db.CLoans
            //            join
            //            f in _db.CDetails
            //            on d.CDetailId equals f.Id
            //            select new
            //            {
            //                LogBook = f.Lb_no,
            //                ItemMake = f.make,
            //                SerialNo = f.sl_no,
            //                CpuType = f.Cpu,
            //                Player = f.DvdType,
            //                RamConfig = f.Ram,
            //                loanHolder = d.serNo
            //            }).ToList();


            //using message broker to publish a message in rabbitmq server

            // RabbitMq.PublishMesssage("sudip get method called", "demoExchange", "directexchange_key",Globals.username,Globals.password,Globals.virtualHost,Globals.hostname);

            RabbitMqEasy.Publish("hurray sudip easynetq!");

            return Ok(loanHolders);

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

        /// <summary>
        /// Deletes a specific LogBook Number.
        /// </summary>
        /// <param name="LbNo"></param> 
        /// /// <remarks>
        /// Sample request:
        ///
        ///     POST /delete
        ///     {
        ///        "LbNo":1
        ///     }
        ///
        /// </remarks>
        /// <returns>Deleted LogBook Number</returns>
        /// <response code="200">Returns the deleted logbook no code</response>
        /// <response code="501">Internal server error</response>   
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