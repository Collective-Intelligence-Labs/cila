
using cila.Domain.Database;
using cila.Domain.Database.Documents;
using MongoDB.Bson;
using MongoDB.Driver;

namespace cila.Relay;

public class EventStore
{
    private readonly IMongoCollection<ExecutionChainEventDocument> _events;

    public EventStore(MongoDatabase database)
    {
        _events = database.GetEventsCollection();;

        // Create an index on the AggregateId field
        var indexKeysDefinition = Builders<ExecutionChainEventDocument>.IndexKeys.Ascending(e => e.AggregateId);
        var indexModel = new CreateIndexModel<ExecutionChainEventDocument>(indexKeysDefinition);
        _events.Indexes.CreateOne(indexModel);
    }

    public async Task<IEnumerable<ExecutionChainEventDocument>> GetEvents(string aggregateId)
    {
        // Find all events for the specified aggregateId, sorted by version
        var filter = Builders<ExecutionChainEventDocument>.Filter.Eq(e => e.AggregateId, aggregateId);
        var sort = Builders<ExecutionChainEventDocument>.Sort.Ascending(e => e.Version);
        var events = await _events.Find(filter).Sort(sort).ToListAsync();
        return events;
    }

    public ulong? GetLatestVersion(string aggregateId)
    {
        // Find the latest version for the specified aggregateId
        var filter = Builders<ExecutionChainEventDocument>.Filter.Eq(e => e.AggregateId, aggregateId);
        var sort = Builders<ExecutionChainEventDocument>.Sort.Descending(e => e.Version);
        var latestVersion = _events.Find(filter).Sort(sort).FirstOrDefault();

        return latestVersion?.Version ?? null;
    }

    public async Task<IEnumerable<string>> GetAggregateIds()
    {
        var filter = new BsonDocument();
        return _events.Distinct<string>("AggregateId", filter).ToList();
    }

    public async Task AppendEvents(string aggregateId, IEnumerable<ExecutionChainEventDocument> events)
    {
        await _events.InsertManyAsync(events);
    }
}