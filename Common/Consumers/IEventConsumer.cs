using Common.Events.Abstractions;

namespace Common.Consumers;

public interface IEventConsumer<in TEvent>
    where TEvent : EventBase
{
    Task Consume(CancellationToken cancellationToken = default);
}