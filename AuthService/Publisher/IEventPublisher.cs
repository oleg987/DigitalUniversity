namespace AuthService.Publisher;

public interface IEventPublisher<TEvent>
{
    void Publish(TEvent message);
}