using System;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http;
using EmployeeService.Models;
using EmployeeService.infrastructure;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication1.Controllers
{
   [Authorize]
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        //initializing the dbcontext for controllers user
        private readonly InventoryContext _db;

        public ValuesController(InventoryContext db)
        {
            _db = db;
        }


        List<string> myVal = new List<string>(new string[] {"sudip","romi" });
        

        // GET api/values
        [HttpGet]
        public ActionResult Get()
        {
            //return new string[] { "value1", "value2" };
            //this code will return the details from the computerdetail table

            var data = (from d in _db.CDetails
                        join
                        f in _db.CLoans
                        on d.Id equals f.CDetailId
                        select new
                        {
                            LogBook = d.Lb_no,
                            ItemMake = d.make,
                            SerialNo = d.sl_no,
                            loanHolder = f.serNo
                        } 
                        
                        
                        ).ToList();


            return Ok(data);

            
        }


        //changing  the code to statisfy
        //various ways of http responses
        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            //testing various type of response messages
            return Ok(myVal[id]);

        }

        // POST api/values
        [Route("create")]
        [HttpPost]
        public ActionResult Post([FromBody]CDetail CDetailRec )
        {
            try
            {
                var cdetail = new CDetail
                {
                    Id = CDetailRec.Id,
                    Lb_no = CDetailRec.Lb_no,
                    sl_no = CDetailRec.sl_no,
                    make = CDetailRec.make,
                    CLoan = CDetailRec.CLoan



                };


                _db.CDetails.Add(cdetail);
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


            }catch(Exception ex)
            {
                return BadRequest(ex.Message+"Inner"+ex.InnerException);
            }

            return Ok("Saved");
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {

            

        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {

        }
    }
}
