using Common.Events;

namespace AuthService.Publisher;

public class FakeEventPublisher : IEventPublisher<UserCreatedEvent>
{
    public readonly List<UserCreatedEvent> Messages = []; // bad solution!
    
    public Task Publish(UserCreatedEvent message, CancellationToken cancellationToken = default)
    {
        Messages.Add(message);
        
        return Task.CompletedTask;
    }
}