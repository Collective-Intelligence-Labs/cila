using System;
using cila.Domain.Database;
using MongoDB.Driver;

namespace cila.Domain.Database.Services
{
	public class ExecutionChainEventService
	{
        private readonly MongoDatabase database;

        public ExecutionChainEventService(MongoDatabase database)
        {
            this.database = database;
        }

        public ulong? GetLastVersion(string aggregateID)
        {
            var lastEvent = database.GetEventsCollection().Find(x => x.AggregateId == aggregateID).SortByDescending(x => x.Version).FirstOrDefault();
            return lastEvent != null ? lastEvent.Version : null;
        }
    }
}

