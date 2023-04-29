using cila.Domain;

namespace cila.Domain.Infrastructure.Chains
{
    public interface IChainClient
    {
        Task<ChainResponse> SendAsync(Operation op);
    }
}