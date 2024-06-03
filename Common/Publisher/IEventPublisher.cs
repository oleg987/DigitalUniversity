using Common.Events.Abstractions;

namespace Common.Publisher;

public interface IEventPublisher<in TEvent>
    where TEvent : EventBase
{
    Task Publish(TEvent message, CancellationToken cancellationToken = default);
}