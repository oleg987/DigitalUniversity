using System.Text.Json;
using AuthService.Commands;
using AuthService.Data;
using Common.Events;
using Common.Publisher;
using Common.Settings;
using StackExchange.Redis;

namespace AuthService.Consumers.UserCreated;

public class UserCreatedEventConsumer : BackgroundService
{
    private readonly IServiceProvider _provider;
    private readonly RedisSettings _redisSettings;
    private readonly ConnectionMultiplexer _connection;
    private readonly string _channel = nameof(UserCreatedEvent);
    private readonly ILogger<UserCreatedEventConsumer> _logger;

    public UserCreatedEventConsumer(IServiceProvider provider, RedisSettings redisSettings, ILogger<UserCreatedEventConsumer> logger)
    {
        _provider = provider;
        _redisSettings = redisSettings;
        _logger = logger;
        _connection = ConnectionMultiplexer.Connect($"{_redisSettings.Host}:{_redisSettings.Port}");
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var subscriber = _connection.GetSubscriber();

        await subscriber.SubscribeAsync(_channel, (c, m) =>
        {
            var message = JsonSerializer.Deserialize<UserCreatedEvent>(m);

            Handle(message);
        });
    }

    private async Task Handle(UserCreatedEvent message)
    {
        using var scope = _provider.CreateScope();

        await using var ctx = scope.ServiceProvider.GetRequiredService<AuthDbContext>();

        var publisher = scope.ServiceProvider.GetRequiredService<IEventPublisher<AuthInfoCreatedEvent>>();

        var command = new CreateAuthInfoCommand(message, ctx, publisher);

        await command.Execute();
    }
}