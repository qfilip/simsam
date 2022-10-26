using Messager.Shared;
using MQTTnet.Client;
using System.Text;

namespace Messager.WebApi;

public class MqttClientListenerService : BackgroundService
{
    private readonly IMqttClient _mqttClient;
    public MqttClientListenerService(IMqttClient mqttClient)
    {
        _mqttClient = mqttClient;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var clientOptions = new MqttClientOptionsBuilder()
            .WithClientId(Client.WebApi)
            .WithTcpServer(Broker.ServerAddress, Broker.Port)
            .Build();

        var subOptions = new MqttClientSubscribeOptionsBuilder()
            .WithSubscriptionIdentifier(2)
            .WithTopicFilter(Topic.IoTDevice) // listen to this topic
            .Build();

        await _mqttClient.ConnectAsync(clientOptions, stoppingToken);
        await _mqttClient.SubscribeAsync(subOptions, stoppingToken);

        _mqttClient.ApplicationMessageReceivedAsync += IoTDeviceListener;
    }

    private async Task IoTDeviceListener(MqttApplicationMessageReceivedEventArgs args)
    {
        var payload = Encoding.UTF8.GetString(args.ApplicationMessage.Payload);
        Console.WriteLine("Webapi - Got message from IoT device: " + payload);

        await Task.CompletedTask;
    }
}

