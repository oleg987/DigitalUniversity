using AuthService.Events;

namespace AuthService.Publisher;

public class FakeEventPublisher : IEventPublisher<UserCreatedEvent>
{
    public readonly List<UserCreatedEvent> Messages = []; // bad solution!
    
    public void Publish(UserCreatedEvent message)
    {
        Messages.Add(message);
    }
}