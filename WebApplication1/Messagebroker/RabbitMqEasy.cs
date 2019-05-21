using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeService.Models;
using EasyNetQ;
using System.Text;
using EasyNetQ.Topology;

namespace EmployeeService.Messagebroker
{
    //This class is RabbitMq implementation using easynetq
    //please read EasyNetQ documentation for more details
    public static class RabbitMqEasy
    {
        public static void Publish(string input)
        {
            
            var bus = RabbitHutch.CreateBus("host=localhost;virtualhost=/;username=guest;password=guest");

            var advancedBus = bus.Advanced;
                var routingKey = "SimpleMessage";

                // declare some objects
                var queue = advancedBus.QueueDeclare("Q.demoqueue");
                var exchange = advancedBus.ExchangeDeclare("E.TestExchange.SimpleMessage", ExchangeType.Direct);
                var binding = advancedBus.Bind(exchange, queue, routingKey);

                var message = new RabbitMessage() { text = input };

                //var properties = new MessageProperties();
                //byte[] messageBuffer = Encoding.Default.GetBytes(input);
                advancedBus.Publish(exchange, routingKey, true, new Message<RabbitMessage>(message));

            advancedBus.Dispose();
        }

        



    }
}
