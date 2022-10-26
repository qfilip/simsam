using MQTTnet;
using MQTTnet.Server;
using System.Text;

var options = new MqttServerOptionsBuilder()
    .WithDefaultEndpoint()
    .WithDefaultEndpointPort(707)
    .Build();

var server = new MqttFactory().CreateMqttServer(options);

server.InterceptingSubscriptionAsync += async (ev) =>
{
    Console.WriteLine("Subscription intercepted");
    Console.WriteLine(ev.ClientId);
    Console.WriteLine(ev.ReasonString);
    await Task.CompletedTask;
};

server.InterceptingPublishAsync += async (ev) =>
{
    Console.WriteLine("Publish intercepted");
    Console.WriteLine($"Client: {ev.ClientId}, Topic: {ev.ApplicationMessage.Topic}");
    Console.WriteLine(Encoding.UTF8.GetString(ev.ApplicationMessage.Payload));
    await Task.CompletedTask;
};

server.StartAsync().GetAwaiter().GetResult();

Console.ReadLine();