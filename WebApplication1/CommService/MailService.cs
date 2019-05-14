using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Serilog;

namespace EmployeeService.CommService
{
    public class MailService
    {
        

        public void OnGetCompleted(object source, EventArgs e)
        {

            Log.Information("sudip event fired");

        }


    }
}
