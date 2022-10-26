using Messager.Shared;
using Messager.WebApi;
using Microsoft.AspNetCore.Mvc;
using MQTTnet;
using MQTTnet.Client;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IMqttClient>(_ => new MqttFactory().CreateMqttClient());
builder.Services.AddHostedService<MqttClientListenerService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/", () =>
{
    Results.Ok(42);
});

app.MapPost("/publish",
    async ([FromBody] AppMessage message, [FromServices] IMqttClient client) =>
{
    await client.PublishAsync(message.ToMqttMessage(Topic.WebApi));
    Results.Ok("Message sent");
});

app.Run();