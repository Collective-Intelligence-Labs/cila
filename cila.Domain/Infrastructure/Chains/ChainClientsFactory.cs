


using cila.Domain.Database.Services;

namespace cila.Domain.Infrastructure.Chains
{
    public class ChainClientsFactory
    {
        private readonly ChainsService chainsService;

        public ChainClientsFactory(ChainsService chainsService)
        {
            this.chainsService = chainsService;
        }

        public IChainClient GetChainClient(string chainId)
        {
            var chain = chainsService.Get(chainId);
            return new EthChainClient(chain.RPC, chain.DispatcherContract, chain.EventStoreContract, chain.PrivateKey);
        }
    }
}