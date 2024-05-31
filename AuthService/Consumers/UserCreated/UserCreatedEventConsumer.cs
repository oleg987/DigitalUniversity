using System.ComponentModel;
using System.Text.Json;
using Common.Consumers;
using Common.Events;
using Common.Settings;
using StackExchange.Redis;

namespace AuthService.Consumers.UserCreated;

public class UserCreatedEventConsumer : IHostedService, IEventConsumer<UserCreatedEvent>
{
    private readonly IServiceProvider _provider;

    public UserCreatedEventConsumer(IServiceProvider provider)
    {
        _provider = provider;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        return Consume(cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public async Task Consume(CancellationToken cancellationToken = default)
    {
        using var scope = _provider.CreateScope();
        
        var redisSettings = scope.ServiceProvider.GetRequiredService<RedisSettings>();
        
        var connectionString = $"{redisSettings.Host}:{redisSettings.Port}";

        var channel = nameof(UserCreatedEvent);

        await using var connection = await ConnectionMultiplexer.ConnectAsync(connectionString);

        var queue = connection.GetSubscriber();

        await queue.SubscribeAsync(channel, async (c, m) =>
        {
            var message = JsonSerializer.Deserialize<UserCreatedEvent>(m);
            await HandleMessage(message);
        });

        await Task.Delay(-1, cancellationToken);
    }

    private async Task HandleMessage(UserCreatedEvent? message)
    {
        throw new NotImplementedException();
    }
}