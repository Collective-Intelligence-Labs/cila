using System.Security.Cryptography;
using cila.Domain;
using cila.Domain.Database.Documents;
using MongoDB.Bson;
using Nethereum.Util.HashProviders;

namespace cila.Aggregator
{
    public class AggregatedEvent
    {
        public DomainEvent DomainEvent { get; set; }
        public string AggregateId { get; set; }
        public string ChainId { get; set; }
        public ulong BlockNumber { get; set; }
        public string BlockHash { get; set; }

        // Has to be replaced with real operation and Command ID
        public string OperaionId { get; set; }
        public string CommandId { get; set; }
        public byte[] Payload { get; set; }
        public ulong Version { get { return DomainEvent.EvntIdx; } }

        public string Hash
        {
            get
            {
                byte[] data = Payload;
                var hashProvider = new Sha3KeccackHashProvider();

                // Compute the hash value of the input data
                byte[] hash = hashProvider.ComputeHash(data);

                // Convert the hash to a string representation
                string hashString = BitConverter.ToString(hash).Replace("-", string.Empty);

                return hashString;
            }
        }

        public AggregatedEventDocument ToMongoDocument()
        {
            var doc = new AggregatedEventDocument
            {
                Id = ObjectId.GenerateNewId().ToString(),
                AggregateId = AggregateId,
                ChainId = ChainId,
                Payload = Payload,
                BlockHash = BlockHash,
                BlockNumber = BlockNumber,
                Version = Version,
                Hash = Hash,
                Timestamp = DateTime.UtcNow
            };

            return doc;
        }
    }
}