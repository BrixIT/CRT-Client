using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using CRT.Helper;

namespace CRT
{
    class RemoteControl
    {
        private string baseUrl;

        public RemoteControl(string ip, int port)
        {
            var factory = new ConnectionFactory() { HostName = ip, Port = port, UserName = "CRT", Password = "CRT" };

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    // Define the exchange containing all the clients of it doesn't exist
                    channel.ExchangeDeclare(exchange: "clients",
                                    type: "direct");

                    // Create a queue for this client with a random name
                    var queueName = channel.QueueDeclare().QueueName;

                    // Get a unique id for this client
                    var clientId = Identification.GetWindowsInstallationId();

                    // Listen for messages on the clients exchange with my clientId as key
                    channel.QueueBind(queue: queueName,
                                  exchange: "clients",
                                  routingKey: clientId);

                    // Handle a single task at a time.
                    channel.BasicQos(0, 1, false);

                    var consumer = new QueueingBasicConsumer(channel);
                    channel.BasicConsume(queue: "clients",
                                         noAck: false,
                                         consumer: consumer);

                    // Start processing tasks in the queue
                    while (true)
                    {
                        string response = null;
                        var ea = (BasicDeliverEventArgs)consumer.Queue.Dequeue();

                        var body = ea.Body;
                        var props = ea.BasicProperties;
                        var replyProps = channel.CreateBasicProperties();
                        replyProps.CorrelationId = props.CorrelationId;

                        try
                        {
                            var message = Encoding.UTF8.GetString(body);
                            int n = int.Parse(message);
                            //TODO: Replace tutorial code with real code
                            Console.WriteLine(" [.] fib({0})", message);
                            //response = fib(n).ToString();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(" [.] " + e.Message);
                            response = "";
                        }
                        finally
                        {
                            var responseBytes = Encoding.UTF8.GetBytes(response);
                            channel.BasicPublish(exchange: "",
                                                 routingKey: props.ReplyTo,
                                                 basicProperties: replyProps,
                                                 body: responseBytes);
                            channel.BasicAck(deliveryTag: ea.DeliveryTag,
                                             multiple: false);
                        }
                    }

                }
            }
        }
    }
}
