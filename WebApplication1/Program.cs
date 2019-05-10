using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using EmployeeService.infrastructure;
using Serilog;

namespace WebApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }


        public static IWebHost BuildWebHost(string[] args)
        {
             var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false,reloadOnChange:true)
            
            .AddCommandLine(args)
            .Build();
            

            return WebHost.CreateDefaultBuilder(args)
                   .UseConfiguration(config)
                   .UseSerilog()
                   .UseStartup<Startup>()
                   .Build();
                   
          
        }
            




    }
}
