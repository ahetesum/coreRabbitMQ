// See https://aka.ms/new-console-template for more information
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

Console.WriteLine("Welcome to 4Mula Air ticketing Service!");

var factory = new ConnectionFactory()
{
    HostName = "localhost",
    UserName = "user",
    Password = "mypass",
    VirtualHost = "/",
};

var connection = factory.CreateConnection();
using var channel = connection.CreateModel();
channel.QueueDeclare("BOOKINGS_QUEUE", durable: true, exclusive: false);

var consumer = new EventingBasicConsumer(channel);
consumer.Received += (model, eventArgs) =>
{
    var body = eventArgs.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);

    Console.WriteLine("A new ticket processing initiated for "+message);
};

channel.BasicConsume("BOOKINGS_QUEUE", true,consumer);

Console.ReadKey();