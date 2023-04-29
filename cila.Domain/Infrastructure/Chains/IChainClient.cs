using cila.Domain;

namespace cila.Domain.Infrastructure.Chains
{
    public interface IChainClient
    {
        string GetChainId();

        // Dispatcher
        Task<ChainResponse> DispatchOperationAsync(Operation op);

        // Relay
        Task<IEnumerable<byte[]>> PullAsync(string aggregateId, UInt32 position);
        Task<string> PushAsync(string aggregateId, UInt32 position, IEnumerable<byte[]> events);
        Task<string> GetRelayPermission();   
    }
}