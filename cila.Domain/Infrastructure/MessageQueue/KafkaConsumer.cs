using System;
using Confluent.Kafka;

namespace cila.Domain.Infrastructure.MessageQueue
{
    public class KafkaConsumer
    {
        private readonly ConsumerConfig config;
        private readonly IConsumer<string, byte[]> consumer;


        public KafkaConsumer(ConsumerConfig config)
        {
            this.config = config;
            consumer = new ConsumerBuilder<string, byte[]>(config).Build();
        }

        public async Task ConsumeAsync(string topic, Action<ConsumeResult<string, byte[]>> callback, CancellationToken cancellationToken)
        {
            await Task.Factory.StartNew(() => {
                consumer.Subscribe(topic);
                while (!cancellationToken.IsCancellationRequested)
                {
                    try
                    {
                        var consumeResult = consumer.Consume(cancellationToken);
                        if (consumeResult != null && consumeResult.Message != null && consumeResult.Message.Value != null)
                        {
                            callback(consumeResult);
                            //dispatcher.Dispatch(OmniChainSerializer.DeserializeInfrastructureEvent(consumeResult.Message.Value));
                        }
                        //Console.WriteLine($"Consumed message '{consumeResult.Message.Value}' from topic {consumeResult.Topic}, partition {consumeResult.Partition}, offset {consumeResult.Offset}");
                    }
                    catch (ConsumeException ex)
                    {
                        Console.WriteLine($"Error occurred: {ex.Error.Reason}");
                    }
                }
            });
        }

        public void Dispose()
        {
            consumer?.Dispose();
        }
    }
}

