using MQTTnet;
using System.Text;

namespace Messager.Shared;

public record AppMessage(string Text);

public struct Broker
{
    public const string ServerAddress = "localhost";
    public const int Port = 707;
}

public struct Client
{
    public const string WebApi = "WebApiId";
    public const string IoTDevice = "IoTDeviceId";
}

public struct Topic
{
    public const string WebApi = "WebApi";
    public const string IoTDevice = "IoTDevice";
}

public static class SharedExtensions
{
    public static MqttApplicationMessage ToMqttMessage(this AppMessage message, string topic)
    {
        return new MqttApplicationMessage
        {
            Payload = Encoding.UTF8.GetBytes(message.Text),
            Topic = topic
        };
    }
}

