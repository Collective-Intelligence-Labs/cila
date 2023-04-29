using Grpc.Core;
using cila.Domain.Infrastructure;
using System.Text;
using Google.Protobuf;
using System.Runtime.Serialization.Formatters.Binary;
using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.Util;
using cila.Domain;
using cila.Domain.Infrastructure.MessageQueue;
using cila.Router;
using cila.Domain.Infrastructure.Chains;
using static cila.Router.Omnichain;

namespace cila.Domain.Services;

public class OmnichainService : OmnichainBase
{ 
    private readonly ILogger<OmnichainService> _logger;
    private readonly KafkaProducer _producer;
    private readonly OperationDispatcher dispatcher;
    private readonly CilaSettings settings;

    public OmnichainService(ILogger<OmnichainService> logger, KafkaProducer producer, OperationDispatcher dispatcher, CilaSettings settings)
    {
        _logger = logger;
        _producer = producer;
        this.dispatcher = dispatcher;
        this.settings = settings;
    }

    public override async Task<OmnichainResponse> Mint(MintRequest request, ServerCallContext context)
    {
        try
        {
            var operation = new OperationDto
            {
                RouterId = FromHexString(settings.ExecutionEnvironmentId)
            };

            var payload = new MintNFTPayload
            {
                Hash = GetByteString(FromHexString(CalculateKeccak256(request.Hash))),
                Owner = GetByteString(FromHexString(request.Sender))
            };

            var cmd = new CommandDto
            {
                AggregateId = FromString(settings.AggregateID),
                CmdType = (uint)CommandType.MintNft,
                CmdPayload = payload.ToByteArray(),
                CmdSignature = FromHexString(request.Signature)
            };

            operation.Commands.Add(cmd);

            var result = await dispatcher.Dispatch(operation.ConvertToProtobuff(), settings.ExecutionEnvironmentId);
            
            var response = new OmnichainResponse
            {
                //replace here with something
                Success = true,
                Sender = request.Sender
            };
            response.Logs.AddRange(result.Select(x => string.Format("Executed on chain {0}, tx: {1}", x.ChainId, x.TransactionHash)));
            return response;
        }
        catch (Exception ex)
        {
            var response = new OmnichainResponse
            {
                Success = false,
                Sender = request.Sender
            };
            response.Logs.Add(ex.Message);
            return response;
        }
    }

    public override async Task<OmnichainResponse> Transfer(TransferRequest request, ServerCallContext context)
    {
        try
        {
            var operation = new OperationDto
            {
                RouterId = FromHexString(settings.ExecutionEnvironmentId)
            };

            var payload = new TransferNFTPayload
            {
                Hash = GetByteString(FromHexString(CalculateKeccak256(request.Hash))),
                To = GetByteString(FromHexString(request.Recipient))
            };

            var cmd = new CommandDto
            {
                AggregateId = FromString(settings.AggregateID),
                CmdType = (uint)CommandType.TransferNft,
                CmdPayload = payload.ToByteArray(),
                CmdSignature = FromHexString(request.Signature)
            };

            operation.Commands.Add(cmd);

            var result = await dispatcher.Dispatch(operation.ConvertToProtobuff(), settings.ExecutionEnvironmentId);

            var response = new OmnichainResponse
            {
                //replace here with something
                Success = true,
                Sender = request.Sender
            };
            response.Logs.AddRange(result.Select(x => string.Format("Executed on chain {0}, tx: {1}", x.ChainId, x.TransactionHash)));
            return response;
        }
        catch (Exception ex)
        {
            var response = new OmnichainResponse
            {
                Success = false,
                Sender = request.Sender
            };
            response.Logs.Add(ex.Message);
            return response;
        }
    }

    private string CalculateKeccak256(string str)
    {
        var keccak = new Sha3Keccack();
        return keccak.CalculateHash(str);
    }

    private byte[] FromHexString(string str)
    {
        str = str.StartsWith("0x") ? str.Substring(2) : str;
        return Enumerable.Range(0, str.Length).Where(x => x % 2 == 0).Select(x => Convert.ToByte(str.Substring(x, 2), 16)).ToArray();
    }

    private byte[] FromString(string str)
    {
        var bytes = Encoding.Default.GetBytes(str);
        return FromHexString(Convert.ToHexString(bytes));
    }

    private ByteString GetByteString(byte[] bytes)
    {
        return ByteString.CopyFrom(bytes);
    }
}

