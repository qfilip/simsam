using Messager.Shared;
using MQTTnet;
using MQTTnet.Client;
using System.Text;

var clientOptions = new MqttClientOptionsBuilder()
            .WithClientId(Client.IoTDevice)
            .WithTcpServer(Broker.ServerAddress, Broker.Port)
            .Build();

var subOptions = new MqttClientSubscribeOptionsBuilder()
    .WithSubscriptionIdentifier(1)
    .WithTopicFilter(Topic.WebApi) // listen to this topic
    .Build();

var mqttClient = new MqttFactory().CreateMqttClient();

await mqttClient.ConnectAsync(clientOptions);
await mqttClient.SubscribeAsync(subOptions);

mqttClient.ApplicationMessageReceivedAsync += MessageUtils.WebApiListener;

var sendMessageFunc = MessageUtils.GetSendMessageFn(mqttClient);
var timer = new Timer(sendMessageFunc, null, TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(5));

Console.ReadLine();

public static class MessageUtils
{
    private static int counter = 0;

    public static TimerCallback GetSendMessageFn(IMqttClient mqttClient)
    {
        return (_) =>
        {
            counter++;
            var message = new AppMessage($"Message no. {counter} sent")
                .ToMqttMessage(Topic.IoTDevice);
            
            mqttClient.PublishAsync(message).GetAwaiter().GetResult();
        };
    }

    public static async Task WebApiListener(MqttApplicationMessageReceivedEventArgs args)
    {
        var payload = Encoding.UTF8.GetString(args.ApplicationMessage.Payload);
        Console.WriteLine("IoT device - Got message from web-api: " + payload);

        await Task.CompletedTask;
    }
}