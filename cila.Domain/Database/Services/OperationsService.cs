using System;
using System.Linq.Expressions;
using cila.Domain.Database.Documents;
using MongoDB.Driver;

namespace cila.Domain.Database.Services
{
    public class OperationsService
    {
        private readonly MongoDatabase database;

        public OperationsService(MongoDatabase database)
        {
            this.database = database;
        }

        public IEnumerable<OperationDocument> FindAllOperations()
        {
            var filter = Builders<OperationDocument>.Filter.Empty;
            return database.GetOperationsCollection().Find(filter).ToList();
        }

        public OperationDocument FindOne(string operationId)
        {
            var filter = Builders<OperationDocument>.Filter.Eq(x => x.Id, operationId);
            return database.GetOperationsCollection().Find(filter).FirstOrDefault();
        }

        public OperationDocument Create(string id, List<string> commands, string clientId, DateTime? timeStamp)
        {
            var doc = new OperationDocument
            {
                Id = id,
                Commands = commands,
                Created = timeStamp ?? DateTime.UtcNow,
                ClientID = clientId
            };
            database.GetOperationsCollection().InsertOne(doc);
            return doc;
        }

        public void InsertNewEvent(string operationId, InfrastructureEventItem e)
        {
            var builder = Builders<OperationDocument>.Update.AddToSet(x => x.InfrastructureEvents, e);
            database.GetOperationsCollection().UpdateOne(x => x.Id == operationId, builder);
        }

        public void InsertNewSyncItem(string operationId, Expression<Func<OperationDocument, IEnumerable<SyncItems>>> itemSelector, SyncItems item)
        {
            var builder = Builders<OperationDocument>.Update.AddToSet(itemSelector, item);
            database.GetOperationsCollection().UpdateOne(x => x.Id == operationId, builder);
        }

        public void InsertNewSyncItem(OperationDocument doc, Expression<Func<OperationDocument, List<SyncItems>>> itemSelector, SyncItems item)
        {
            var items = itemSelector.Compile()(doc);
            if (!items.Any(x => x.Id == item.Id && x.ErrorMessage == item.ErrorMessage))
            {
                items.Add(item);
                database.GetOperationsCollection().ReplaceOne(x => x.Id == doc.Id, doc);
            }
        }

        public void UpdateChainStatus(OperationDocument doc, string chainId, ChainStatus status)
        {
            var perChainStatus = doc.PerChainStatus;
            foreach (var s in perChainStatus)
            {
                if (s.ChainId == chainId)
                {
                    s.Status = status;
                }
            }

            var builder = Builders<OperationDocument>.Update.Set(x => x.PerChainStatus, perChainStatus);
            database.GetOperationsCollection().UpdateOne(x => x.Id == doc.Id, builder);
        }
    }
}

