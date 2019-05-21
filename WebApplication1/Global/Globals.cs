using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RabbitMQ.Client;
using EmployeeService.Messagebroker;

namespace EmployeeService.Global
{
    public static  class Globals
    {
        //constants for rabbit mq server

        public const string username = "guest";
        public const string password = "guest";
        public const string virtualHost = "/";
        public const string hostname = "localhost";
    }
}
