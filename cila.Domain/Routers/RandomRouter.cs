using System;
using Nethereum.Web3;
using cila.Domain;
using cila.Domain.Database.Services;

namespace cila.Domain.Routers
{
	public class RandomRouter: ICilaRouter
	{
        private readonly ChainsService chainsService;
        private object syncLock = new object();

        public RandomRouter(ChainsService chainsService)
		{
            this.chainsService = chainsService;
        }

        public OmnichainRoute CalculateRoute(Command operation)
        {
            var chains = chainsService.GetAll();
            lock (syncLock)
            {
                Random random = new Random();
                int randomNumber = random.Next(chains.Count);
                var randomElement = chains.ElementAt(randomNumber);
                return new OmnichainRoute
                {
                    ChainId = randomElement.Id
                };
            }
        }
    }
}