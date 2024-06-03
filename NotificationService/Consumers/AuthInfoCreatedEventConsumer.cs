using System.Text.Json;
using Common.Events;
using Common.Settings;
using StackExchange.Redis;

namespace NotificationService.Consumers;

public class AuthInfoCreatedEventConsumer : BackgroundService
{
    private readonly ILogger<AuthInfoCreatedEventConsumer> _logger;
    private readonly RedisSettings _redisSettings;
    private readonly ConnectionMultiplexer _connection;
    private readonly string _channel = nameof(AuthInfoCreatedEvent);

    public AuthInfoCreatedEventConsumer(ILogger<AuthInfoCreatedEventConsumer> logger, RedisSettings redisSettings)
    {
        _logger = logger;
        _redisSettings = redisSettings;
        _connection = ConnectionMultiplexer.Connect($"{_redisSettings.Host}:{_redisSettings.Port}");
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var subscriber = _connection.GetSubscriber();

        await subscriber.SubscribeAsync(_channel, (c, m) =>
        {
            var message = JsonSerializer.Deserialize<AuthInfoCreatedEvent>(m)!;
            
            _logger.LogInformation($"SendDate: {DateTime.Now}; Email: {message.Email}; InviteCode: {message.InviteCode};");
        });
    }
}