using System.Text.Json;
using Common.Events.Abstractions;
using Common.Settings;
using StackExchange.Redis;

namespace Common.Publisher;

public class RedisEventPublisher<TEvent> : IEventPublisher<TEvent> 
    where TEvent : EventBase
{
    private readonly RedisSettings _settings;

    public RedisEventPublisher(RedisSettings settings)
    {
        _settings = settings;
    }

    public async Task Publish(TEvent message, CancellationToken cancellationToken = default)
    {
        var connectionString = $"{_settings.Host}:{_settings.Port}";

        var channel = nameof(TEvent);

        await using var connection = await ConnectionMultiplexer.ConnectAsync(connectionString);

        var serializedMessage = JsonSerializer.Serialize(message);

        var queue = connection.GetSubscriber();

        await queue.PublishAsync(channel, serializedMessage, CommandFlags.FireAndForget);
    }
}