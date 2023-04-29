
using Nethereum.Web3;
using Nethereum.Contracts;
using System.Text;
using Nethereum.Contracts.ContractHandlers;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Web3.Accounts;
using Nethereum.Hex.HexTypes;
using System.ComponentModel;
using Nethereum.ABI;
using Nethereum.Contracts.QueryHandlers;
using Google.Protobuf;
using System.Numerics;
using cila.Domain;

namespace cila.Domain.Infrastructure.Chains
{
    public class EthChainClient : IChainClient
    {
        private Web3 _web3;
        private ContractHandler _dispatcherContract;
        private ContractHandler _eventStoreContract;
        private Account _account;

        private const UInt32 MAX_PULL_EVENTS_LIMIT = 1000000;

        public EthChainClient(string rpc, string dispatcherContract, string eventStoreContract, string pk)
        {
            _account = new Account(pk);
            _web3 = new Web3(_account, rpc, log: new EthLogger());
            _dispatcherContract = _web3.Eth.GetContractHandler(dispatcherContract);
            _eventStoreContract = _web3.Eth.GetContractHandler(eventStoreContract);

            Console.WriteLine("Account {0} is connected to chain {1}", _account.Address, rpc);
            Console.WriteLine("Dispatcher contract address {0}", dispatcherContract);
            Console.WriteLine("Event store contract address {0}", eventStoreContract);
        }

        public string GetChainId() => _web3.Eth.ChainId.SendRequestAsync().GetAwaiter().GetResult().ToString();

        public async Task<ChainResponse> DispatchOperationAsync(Operation op)
        {
            if (op == null)
                await Task.FromResult(true);

            var function = _dispatcherContract.GetFunction<DispatchFunction>();

            var opBytes = op.ToByteArray();
            
            var req = new DispatchFunction
            {
                OpBytes = opBytes
            };

            req.FromAddress = _account.Address;

            var _queryHandler = _web3.Eth.GetContractQueryHandler<DispatchFunction>();
            var txHandler = _web3.Eth.GetContractTransactionHandler<DispatchFunction>();

            var gasEstimate = await txHandler.EstimateGasAsync(_dispatcherContract.ContractAddress, req);
            req.Gas = new BigInteger(2) * gasEstimate;

            var gasPrice = _web3.Eth.GasPrice.SendRequestAsync().GetAwaiter().GetResult();
            req.GasPrice = new BigInteger(2) * gasPrice;

            var tx = await txHandler.SendRequestAsync(_dispatcherContract.ContractAddress, req);
            TransactionReceipt receipt = null; // await txHandler.SendRequestAndWaitForReceiptAsync(_contract.ContractAddress, req);
            return new ChainResponse {
                ChainId = _web3.Eth.ChainId.SendRequestAsync().GetAwaiter().GetResult().ToString(),
                ContractAddress = receipt?.ContractAddress ?? _dispatcherContract.ContractAddress,
                EffectiveGasPrice = receipt?.EffectiveGasPrice.ToUlong() ?? (ulong)req.GasPrice,
                GasUsed = receipt?.GasUsed.ToUlong() ?? (ulong)req.Gas,
                CumulativeGasUsed = receipt?.CumulativeGasUsed.ToUlong() ?? (ulong)req.Gas,
                BlockHash = receipt?.BlockHash ?? "Unknown",
                BlockNumber = receipt?.BlockNumber.ToUlong() ?? _web3.Eth.Blocks.GetBlockNumber.SendRequestAsync().GetAwaiter().GetResult().ToUlong(),
                Logs = receipt?.Logs.ToString() ?? string.Empty,
                TransactionHash = receipt?.TransactionHash ?? tx,
                TransactionIndex = receipt?.TransactionIndex.ToUlong() ?? 0
            };
        }

        public async Task<IEnumerable<byte[]>> PullAsync(string aggregateId, UInt32 position)
        {
            Console.WriteLine("Chain Service Pull execution started from position: {0}, aggregate: {1}", position, aggregateId);
            var handler = _eventStoreContract.GetFunction<PullFunction>();

            var request = new PullFunction
            {
                StartIndex = position,
                Limit = MAX_PULL_EVENTS_LIMIT,
                AggregateId = aggregateId
            };
            var result = await handler.CallAsync<PullFunctionOutputDTO>(request);

            Console.WriteLine("Chain Service Pull executed: {0}", result);
            return result.Events;
        }

        public async Task<string> PushAsync(string aggregateId, UInt32 position, IEnumerable<byte[]> events)
        {

            var _queryHandler = _web3.Eth.GetContractQueryHandler<PushFunction>();
            var txHandler = _web3.Eth.GetContractTransactionHandler<PushFunction>();

            var request = new PushFunction
            {
                Events = events.ToList(),
                Position = position,
                AggregateId = aggregateId
            };

            foreach (var ev in request.Events)
            {
                Console.WriteLine("Event: " + Convert.ToHexString(ev));
            }

            var gasEstimate = await txHandler.EstimateGasAsync(_eventStoreContract.ContractAddress, request);
            request.Gas = new BigInteger(2) * gasEstimate;

            var gasPrice = await _web3.Eth.GasPrice.SendRequestAsync();
            request.GasPrice = new BigInteger(2) * gasPrice;

            var result = await txHandler.SendRequestAsync(_eventStoreContract.ContractAddress, request);

            Console.WriteLine("Chain Service Push executed: {0}", result);
            return result;

        }


        public Task<string> GetRelayPermission()
        {
            return _eventStoreContract.GetFunction<ReadRelayFunction>().CallAsync<string>();
        }

        
    }

    public class ChainResponse
    {
        public string ChainId { get; set; }
        public string ContractAddress { get; set; }
        public ulong EffectiveGasPrice { get; set; }
        public ulong GasUsed { get; set; }
        public ulong CumulativeGasUsed { get; set; }
        public ulong BlockNumber { get; set; }
        public string BlockHash { get; set; }
        public ulong TransactionIndex { get; set; }
        public string TransactionHash { get; set; }
        public string Logs { get; set; }
    }
}