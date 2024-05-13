namespace AuthService.Publisher;

public interface IEventPublisher<in TEvent>
{
    Task Publish(TEvent message, CancellationToken cancellationToken = default);
}