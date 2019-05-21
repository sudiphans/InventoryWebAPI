using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace EmployeeService.Messagebroker
{
    // this is the rabitmq implementation without EasyNetQ
    public static class RabbitMq
    { 
        private static IConnection _connection { get; set; }

        private static object LockObject = new object();

      


        private static IConnection GetConnection(string UserName,string Password,string VirtualHost,string HostName)
        {
            
                if (_connection == null)
                {
                    lock (LockObject)
                    {
                    if (_connection == null)
                    {
                        ConnectionFactory factory = new ConnectionFactory
                        {
                            UserName = UserName,
                            Password = Password,
                            VirtualHost = VirtualHost,
                            HostName = HostName
                        };

                            _connection = factory.CreateConnection();

                           
                        }
                    }
                }

                return _connection;
           
           
        }

        public static void PublishMesssage(string message, string exchangeName , string routingKey, string username,string password,string virtualHost,string hostname)
        {
           
            using (IModel model = GetConnection(username, password, virtualHost, hostname).CreateModel())
            {

                var properties = model.CreateBasicProperties();
                properties.Persistent = false;

                byte[] messageBuffer = Encoding.Default.GetBytes(message);

                model.BasicPublish(exchangeName, routingKey, properties, messageBuffer);


            }


        }

     }
}
