using cila.Aggregator;
using cila.Domain;
using cila.Domain.Database;
using cila.Domain.Database.Services;
using cila.Domain.Infrastructure.Chains;
using cila.Domain.Infrastructure.MessageQueue;
using Confluent.Kafka;
using Microsoft.Extensions.DependencyInjection;

internal class Program
{
    public static CilaSettings AppSettings {get;set;}

    public static ServiceProvider _serviceProvider;

    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        var cnfg = new ConfigurationBuilder();
        var configuration = cnfg.AddJsonFile("aggregatorsettings.json")
            .Build();
        AppSettings = configuration.GetSection("Settings").Get<CilaSettings>();

        var configProducer = new ProducerConfig
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
        var configConsumer = new ConsumerConfig
        {
            BootstrapServers = "localhost:9092",
            ClientId = "dotnet-kafka-consumer",
            GroupId = "test-group",
            AutoOffsetReset = AutoOffsetReset.Earliest,
            EnableAutoCommit = false,
            MaxPollIntervalMs = 10000,
            EnablePartitionEof = true,
            SessionTimeoutMs = 6000,
            FetchWaitMaxMs = 1000,
            IsolationLevel = IsolationLevel.ReadCommitted,
            Acks = Acks.All
        };

        var services = builder.Services;

        // Register your dependencies here
        services.AddScoped<ServiceLocator, ServiceLocator>();
        services.AddSingleton(AppSettings);
        services.AddScoped<AggregatorService>()
            .AddScoped<ChainsService>()
            .AddScoped<AggregagtedEventsService>()
            .AddScoped<ChainClientsFactory>()
            .AddScoped<NftService>()
            .AddScoped<OperationsService>()
            .AddScoped<MarketsService>()
            .AddScoped<BalancesService>();
            
        services.AddSingleton<EventsDispatcher>();
        services.AddSingleton(configProducer);
        services.AddSingleton(configConsumer);

        services.AddSingleton<KafkaConsumer>();
        services.AddSingleton<KafkaProducer>();

        services.AddTransient<InfrastructureEventsHandler>();
        services.AddTransient<EventsHandler>();
        services.AddTransient<MongoDatabase>();
        services.AddSingleton<NftService>();
    
        // Build the service provider
        
        services.AddScoped<ServiceProvider>(x=> _serviceProvider);
        _serviceProvider = services.BuildServiceProvider();

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        
        builder.Services.AddCors();
        
        builder.Services.AddHostedService<AggregatorWorkerService>();
        builder.Services.AddHostedService<MessageQueueWorkerService>();

        //Initialize chains in database
        _serviceProvider.GetService<ChainsService>().InitializeFromSettings(AppSettings);

        var app = builder.Build();

        

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();
        
        app.Run();
    }
}