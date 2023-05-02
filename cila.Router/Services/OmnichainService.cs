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

public class OmnichainService : CilaDispatcher.CilaDispatcherBase
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

    public override async Task<OmnichainResponse> Dispatch(Operation request, ServerCallContext context)
    {
        try
        {
            request.RouterId = settings.ExecutionEnvironmentId.ToByteString();

            var result = await dispatcher.Dispatch(request);

            var response = new OmnichainResponse
            {
                //replace here with something
                Success = true,
                Sender = request.Sender.ToStringUtf8()
            };
            response.Logs.AddRange(result.Select(x => string.Format("Executed on chain {0}, tx: {1}", x.ChainId, x.TransactionHash)));
            return response;
        }
        catch (Exception ex)
        {
            var response = new OmnichainResponse
            {
                Success = false,
                Sender = request.Sender.ToString()
            };
            response.Logs.Add(ex.Message);
            return response;
        }
    }
}

