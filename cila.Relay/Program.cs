
using cila.Domain;
using cila.Relay;
using Confluent.Kafka;

internal class Program
{
    
    private static void Main(string[] args)
    {
        var cnfg = new ConfigurationBuilder();
        var configuration = cnfg.AddJsonFile("relaysettings.json")
            .Build();
        var settings = configuration.GetSection("Settings").Get<CilaSettings>();

        Console.WriteLine("Number of chains in settiings: {0}", settings.Chains.Count);
        var relayService = new RelayService(settings);
        var timer = new Timer(ExecuteTask, relayService, TimeSpan.Zero, TimeSpan.FromSeconds(30));
        // Wait indefinitely
        Console.WriteLine("Press Ctrl+C to exit.");
        var exitEvent = new ManualResetEvent(false);
        Console.CancelKeyPress += (sender, e) => exitEvent.Set();
        exitEvent.WaitOne();
    }

    private static ProducerConfig _producerConfig;
    public static ProducerConfig ProducerConfig()
    {
        return _producerConfig = _producerConfig ?? new ProducerConfig
        {
            BootstrapServers = "localhost:9092",
            ClientId = "dotnet-kafka-producer",
            Acks = Acks.All,
            MessageSendMaxRetries = 10,
            MessageTimeoutMs = 10000,
            EnableIdempotence = true,
            CompressionType = CompressionType.Snappy,
            BatchSize = 16384,
            LingerMs = 10,
            MaxInFlight = 5,
            EnableDeliveryReports = true,
            DeliveryReportFields = "all"
        };
    }

    private static void ExecuteTask(object state)
    {
        var relayService = (RelayService)state;
        relayService.SyncAllChains();
        Console.WriteLine($"Task executed at {DateTime.UtcNow}");
    }
}