
using cila.Domain.Infrastructure.MessageQueue;
using cila.Domain.Serializers;

namespace cila.Aggregator;

public class MessageQueueWorkerService : BackgroundService
{
    private KafkaConsumer _consumer;
    private EventsDispatcher _dispatcher;

    public MessageQueueWorkerService(KafkaConsumer consumer, EventsDispatcher dispatcher)
    {
        this._consumer = consumer;
        this._dispatcher = dispatcher;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _consumer.ConsumeAsync("infr",
            (consumeResult) => _dispatcher.Dispatch(CilaDomainSerializer.DeserializeInfrastructureEvent(consumeResult.Message.Value)),
            stoppingToken);
    }
}