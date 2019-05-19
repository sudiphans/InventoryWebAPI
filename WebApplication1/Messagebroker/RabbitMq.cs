using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace EmployeeService.Messagebroker
{

    public class RabbitMq
    {
        public void PublishMesssage()
        {
            try
            {
                ConnectionFactory factory = new ConnectionFactory();
                factory.UserName = "guest";
                factory.Password = "guest";
                factory.VirtualHost = "/";
                factory.HostName = "localhost";

                IConnection conn = factory.CreateConnection();
                var model = conn.CreateModel();
                var properties = model.CreateBasicProperties();
                properties.Persistent = false;

                byte[] messageBuffer = Encoding.Default.GetBytes("Direct Message");

                model.BasicPublish("demoExchange", "directexchange_key", properties, messageBuffer);

            }catch(Exception ex)
            {
                
            }

        }




    }
}
