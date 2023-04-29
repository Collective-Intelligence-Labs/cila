using System;
using Google.Protobuf;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;

namespace cila.Domain.Infrastructure.Chains
{
    // Router

    [Function("dispatch")]
    public class DispatchFunction : FunctionMessage
    {
        [Parameter("bytes", "opBytes")]
        public byte[] OpBytes { get; set; }
    }


    // Relay

    [Function("relay", "address")]
    public class ReadRelayFunction
    {
    }

    [Function("pullBytes")]
    public class PullFunction : FunctionMessage
    {
        [Parameter("string", "aggregateId", 1)]
        public string AggregateId { get; set; }

        [Parameter("uint", "startIndex", 2)]
        public UInt32 StartIndex { get; set; }

        [Parameter("uint", "limit", 3)]
        public UInt32 Limit { get; set; }
    }

    [FunctionOutput]
    public class PullFunctionOutputDTO : IFunctionOutputDTO
    {
        [Parameter("bytes[]", order: 1)]
        public List<byte[]> Events { get; set; }
    }

    [Function("pushBytes")]
    public class PushFunction : FunctionMessage
    {
        [Parameter("string", "aggregateId", 1)]
        public string AggregateId { get; set; }

        [Parameter("uint", "startIndex", 2)]
        public UInt32 Position { get; set; }

        [Parameter("bytes[]", "evnts", 3)]
        public List<byte[]> Events { get; set; }
    }


    // DTO

    public class CommandDto
    {
        public byte[] AggregateId { get; set; }
        public uint CmdType { get; set; }
        public byte[] CmdPayload { get; set; }
        public byte[] CmdSignature { get; set; }
    }

    public class OperationDto
    {
        public byte[] RouterId { get; set; }
        public List<CommandDto> Commands { get; set; }

        public OperationDto()
        {
            Commands = new List<CommandDto>();
        }

        public Operation ConvertToProtobuff()
        {
            var pbOperation = new Operation()
            {
                RouterId = ByteString.CopyFrom(RouterId)
            };

            foreach (var c in Commands)
            {
                pbOperation.Commands.Add(new Command()
                {
                    AggregateId = ByteString.CopyFrom(c.AggregateId),
                    CmdPayload = ByteString.CopyFrom(c.CmdPayload),
                    CmdSignature = ByteString.CopyFrom(c.CmdSignature),
                    CmdType = (CommandType)c.CmdType
                });
            }
            return pbOperation;
        }
    }

}

