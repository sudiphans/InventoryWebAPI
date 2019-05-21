using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyNetQ;

namespace EmployeeService.Models
{
    [Queue("TestMessagesQueue", ExchangeName = "MyTestExchange")]
    public class RabbitMessage
    {
        public string text { get; set; }


    }
}
