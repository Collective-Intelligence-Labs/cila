// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: operation.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021, 8981
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
/// <summary>Holder for reflection information generated from operation.proto</summary>
public static partial class OperationReflection {

  #region Descriptor
  /// <summary>File descriptor for operation.proto</summary>
  public static pbr::FileDescriptor Descriptor {
    get { return descriptor; }
  }
  private static pbr::FileDescriptor descriptor;

  static OperationReflection() {
    byte[] descriptorData = global::System.Convert.FromBase64String(
        string.Concat(
          "Cg9vcGVyYXRpb24ucHJvdG8iOgoJT3BlcmF0aW9uEhEKCXJvdXRlcl9pZBgB",
          "IAEoDBIaCghjb21tYW5kcxgCIAMoCzIILkNvbW1hbmQiawoHQ29tbWFuZBIU",
          "CgxhZ2dyZWdhdGVfaWQYASABKAwSFQoNY21kX3NpZ25hdHVyZRgCIAEoDBIe",
          "CghjbWRfdHlwZRgDIAEoDjIMLkNvbW1hbmRUeXBlEhMKC2NtZF9wYXlsb2Fk",
          "GAQgASgMIi0KDk1pbnRORlRQYXlsb2FkEgwKBGhhc2gYASABKAwSDQoFb3du",
          "ZXIYAiABKAwiLgoSVHJhbnNmZXJORlRQYXlsb2FkEgwKBGhhc2gYASABKAwS",
          "CgoCdG8YAiABKAwqPgoLQ29tbWFuZFR5cGUSDwoLVU5TUEVDSUZJRUQQABIM",
          "CghNSU5UX05GVBABEhAKDFRSQU5TRkVSX05GVBACYgZwcm90bzM="));
    descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
        new pbr::FileDescriptor[] { },
        new pbr::GeneratedClrTypeInfo(new[] {typeof(global::CommandType), }, null, new pbr::GeneratedClrTypeInfo[] {
          new pbr::GeneratedClrTypeInfo(typeof(global::Operation), global::Operation.Parser, new[]{ "RouterId", "Commands" }, null, null, null, null),
          new pbr::GeneratedClrTypeInfo(typeof(global::Command), global::Command.Parser, new[]{ "AggregateId", "CmdSignature", "CmdType", "CmdPayload" }, null, null, null, null),
          new pbr::GeneratedClrTypeInfo(typeof(global::MintNFTPayload), global::MintNFTPayload.Parser, new[]{ "Hash", "Owner" }, null, null, null, null),
          new pbr::GeneratedClrTypeInfo(typeof(global::TransferNFTPayload), global::TransferNFTPayload.Parser, new[]{ "Hash", "To" }, null, null, null, null)
        }));
  }
  #endregion

}
#region Enums
public enum CommandType {
  [pbr::OriginalName("UNSPECIFIED")] Unspecified = 0,
  [pbr::OriginalName("MINT_NFT")] MintNft = 1,
  [pbr::OriginalName("TRANSFER_NFT")] TransferNft = 2,
}

#endregion

#region Messages
public sealed partial class Operation : pb::IMessage<Operation>
#if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    , pb::IBufferMessage
#endif
{
  private static readonly pb::MessageParser<Operation> _parser = new pb::MessageParser<Operation>(() => new Operation());
  private pb::UnknownFieldSet _unknownFields;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public static pb::MessageParser<Operation> Parser { get { return _parser; } }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public static pbr::MessageDescriptor Descriptor {
    get { return global::OperationReflection.Descriptor.MessageTypes[0]; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  pbr::MessageDescriptor pb::IMessage.Descriptor {
    get { return Descriptor; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public Operation() {
    OnConstruction();
  }

  partial void OnConstruction();

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public Operation(Operation other) : this() {
    routerId_ = other.routerId_;
    commands_ = other.commands_.Clone();
    _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public Operation Clone() {
    return new Operation(this);
  }

  /// <summary>Field number for the "router_id" field.</summary>
  public const int RouterIdFieldNumber = 1;
  private pb::ByteString routerId_ = pb::ByteString.Empty;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public pb::ByteString RouterId {
    get { return routerId_; }
    set {
      routerId_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
    }
  }

  /// <summary>Field number for the "commands" field.</summary>
  public const int CommandsFieldNumber = 2;
  private static readonly pb::FieldCodec<global::Command> _repeated_commands_codec
      = pb::FieldCodec.ForMessage(18, global::Command.Parser);
  private readonly pbc::RepeatedField<global::Command> commands_ = new pbc::RepeatedField<global::Command>();
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public pbc::RepeatedField<global::Command> Commands {
    get { return commands_; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public override bool Equals(object other) {
    return Equals(other as Operation);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public bool Equals(Operation other) {
    if (ReferenceEquals(other, null)) {
      return false;
    }
    if (ReferenceEquals(other, this)) {
      return true;
    }
    if (RouterId != other.RouterId) return false;
    if(!commands_.Equals(other.commands_)) return false;
    return Equals(_unknownFields, other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public override int GetHashCode() {
    int hash = 1;
    if (RouterId.Length != 0) hash ^= RouterId.GetHashCode();
    hash ^= commands_.GetHashCode();
    if (_unknownFields != null) {
      hash ^= _unknownFields.GetHashCode();
    }
    return hash;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public override string ToString() {
    return pb::JsonFormatter.ToDiagnosticString(this);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public void WriteTo(pb::CodedOutputStream output) {
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    output.WriteRawMessage(this);
  #else
    if (RouterId.Length != 0) {
      output.WriteRawTag(10);
      output.WriteBytes(RouterId);
    }
    commands_.WriteTo(output, _repeated_commands_codec);
    if (_unknownFields != null) {
      _unknownFields.WriteTo(output);
    }
  #endif
  }

  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
    if (RouterId.Length != 0) {
      output.WriteRawTag(10);
      output.WriteBytes(RouterId);
    }
    commands_.WriteTo(ref output, _repeated_commands_codec);
    if (_unknownFields != null) {
      _unknownFields.WriteTo(ref output);
    }
  }
  #endif

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public int CalculateSize() {
    int size = 0;
    if (RouterId.Length != 0) {
      size += 1 + pb::CodedOutputStream.ComputeBytesSize(RouterId);
    }
    size += commands_.CalculateSize(_repeated_commands_codec);
    if (_unknownFields != null) {
      size += _unknownFields.CalculateSize();
    }
    return size;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public void MergeFrom(Operation other) {
    if (other == null) {
      return;
    }
    if (other.RouterId.Length != 0) {
      RouterId = other.RouterId;
    }
    commands_.Add(other.commands_);
    _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public void MergeFrom(pb::CodedInputStream input) {
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    input.ReadRawMessage(this);
  #else
    uint tag;
    while ((tag = input.ReadTag()) != 0) {
      switch(tag) {
        default:
          _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
          break;
        case 10: {
          RouterId = input.ReadBytes();
          break;
        }
        case 18: {
          commands_.AddEntriesFrom(input, _repeated_commands_codec);
          break;
        }
      }
    }
  #endif
  }

  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input) {
    uint tag;
    while ((tag = input.ReadTag()) != 0) {
      switch(tag) {
        default:
          _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
          break;
        case 10: {
          RouterId = input.ReadBytes();
          break;
        }
        case 18: {
          commands_.AddEntriesFrom(ref input, _repeated_commands_codec);
          break;
        }
      }
    }
  }
  #endif

}

public sealed partial class Command : pb::IMessage<Command>
#if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    , pb::IBufferMessage
#endif
{
  private static readonly pb::MessageParser<Command> _parser = new pb::MessageParser<Command>(() => new Command());
  private pb::UnknownFieldSet _unknownFields;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public static pb::MessageParser<Command> Parser { get { return _parser; } }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public static pbr::MessageDescriptor Descriptor {
    get { return global::OperationReflection.Descriptor.MessageTypes[1]; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  pbr::MessageDescriptor pb::IMessage.Descriptor {
    get { return Descriptor; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public Command() {
    OnConstruction();
  }

  partial void OnConstruction();

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public Command(Command other) : this() {
    aggregateId_ = other.aggregateId_;
    cmdSignature_ = other.cmdSignature_;
    cmdType_ = other.cmdType_;
    cmdPayload_ = other.cmdPayload_;
    _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public Command Clone() {
    return new Command(this);
  }

  /// <summary>Field number for the "aggregate_id" field.</summary>
  public const int AggregateIdFieldNumber = 1;
  private pb::ByteString aggregateId_ = pb::ByteString.Empty;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public pb::ByteString AggregateId {
    get { return aggregateId_; }
    set {
      aggregateId_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
    }
  }

  /// <summary>Field number for the "cmd_signature" field.</summary>
  public const int CmdSignatureFieldNumber = 2;
  private pb::ByteString cmdSignature_ = pb::ByteString.Empty;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public pb::ByteString CmdSignature {
    get { return cmdSignature_; }
    set {
      cmdSignature_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
    }
  }

  /// <summary>Field number for the "cmd_type" field.</summary>
  public const int CmdTypeFieldNumber = 3;
  private global::CommandType cmdType_ = global::CommandType.Unspecified;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public global::CommandType CmdType {
    get { return cmdType_; }
    set {
      cmdType_ = value;
    }
  }

  /// <summary>Field number for the "cmd_payload" field.</summary>
  public const int CmdPayloadFieldNumber = 4;
  private pb::ByteString cmdPayload_ = pb::ByteString.Empty;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public pb::ByteString CmdPayload {
    get { return cmdPayload_; }
    set {
      cmdPayload_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
    }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public override bool Equals(object other) {
    return Equals(other as Command);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public bool Equals(Command other) {
    if (ReferenceEquals(other, null)) {
      return false;
    }
    if (ReferenceEquals(other, this)) {
      return true;
    }
    if (AggregateId != other.AggregateId) return false;
    if (CmdSignature != other.CmdSignature) return false;
    if (CmdType != other.CmdType) return false;
    if (CmdPayload != other.CmdPayload) return false;
    return Equals(_unknownFields, other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public override int GetHashCode() {
    int hash = 1;
    if (AggregateId.Length != 0) hash ^= AggregateId.GetHashCode();
    if (CmdSignature.Length != 0) hash ^= CmdSignature.GetHashCode();
    if (CmdType != global::CommandType.Unspecified) hash ^= CmdType.GetHashCode();
    if (CmdPayload.Length != 0) hash ^= CmdPayload.GetHashCode();
    if (_unknownFields != null) {
      hash ^= _unknownFields.GetHashCode();
    }
    return hash;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public override string ToString() {
    return pb::JsonFormatter.ToDiagnosticString(this);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public void WriteTo(pb::CodedOutputStream output) {
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    output.WriteRawMessage(this);
  #else
    if (AggregateId.Length != 0) {
      output.WriteRawTag(10);
      output.WriteBytes(AggregateId);
    }
    if (CmdSignature.Length != 0) {
      output.WriteRawTag(18);
      output.WriteBytes(CmdSignature);
    }
    if (CmdType != global::CommandType.Unspecified) {
      output.WriteRawTag(24);
      output.WriteEnum((int) CmdType);
    }
    if (CmdPayload.Length != 0) {
      output.WriteRawTag(34);
      output.WriteBytes(CmdPayload);
    }
    if (_unknownFields != null) {
      _unknownFields.WriteTo(output);
    }
  #endif
  }

  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
    if (AggregateId.Length != 0) {
      output.WriteRawTag(10);
      output.WriteBytes(AggregateId);
    }
    if (CmdSignature.Length != 0) {
      output.WriteRawTag(18);
      output.WriteBytes(CmdSignature);
    }
    if (CmdType != global::CommandType.Unspecified) {
      output.WriteRawTag(24);
      output.WriteEnum((int) CmdType);
    }
    if (CmdPayload.Length != 0) {
      output.WriteRawTag(34);
      output.WriteBytes(CmdPayload);
    }
    if (_unknownFields != null) {
      _unknownFields.WriteTo(ref output);
    }
  }
  #endif

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public int CalculateSize() {
    int size = 0;
    if (AggregateId.Length != 0) {
      size += 1 + pb::CodedOutputStream.ComputeBytesSize(AggregateId);
    }
    if (CmdSignature.Length != 0) {
      size += 1 + pb::CodedOutputStream.ComputeBytesSize(CmdSignature);
    }
    if (CmdType != global::CommandType.Unspecified) {
      size += 1 + pb::CodedOutputStream.ComputeEnumSize((int) CmdType);
    }
    if (CmdPayload.Length != 0) {
      size += 1 + pb::CodedOutputStream.ComputeBytesSize(CmdPayload);
    }
    if (_unknownFields != null) {
      size += _unknownFields.CalculateSize();
    }
    return size;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public void MergeFrom(Command other) {
    if (other == null) {
      return;
    }
    if (other.AggregateId.Length != 0) {
      AggregateId = other.AggregateId;
    }
    if (other.CmdSignature.Length != 0) {
      CmdSignature = other.CmdSignature;
    }
    if (other.CmdType != global::CommandType.Unspecified) {
      CmdType = other.CmdType;
    }
    if (other.CmdPayload.Length != 0) {
      CmdPayload = other.CmdPayload;
    }
    _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public void MergeFrom(pb::CodedInputStream input) {
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    input.ReadRawMessage(this);
  #else
    uint tag;
    while ((tag = input.ReadTag()) != 0) {
      switch(tag) {
        default:
          _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
          break;
        case 10: {
          AggregateId = input.ReadBytes();
          break;
        }
        case 18: {
          CmdSignature = input.ReadBytes();
          break;
        }
        case 24: {
          CmdType = (global::CommandType) input.ReadEnum();
          break;
        }
        case 34: {
          CmdPayload = input.ReadBytes();
          break;
        }
      }
    }
  #endif
  }

  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input) {
    uint tag;
    while ((tag = input.ReadTag()) != 0) {
      switch(tag) {
        default:
          _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
          break;
        case 10: {
          AggregateId = input.ReadBytes();
          break;
        }
        case 18: {
          CmdSignature = input.ReadBytes();
          break;
        }
        case 24: {
          CmdType = (global::CommandType) input.ReadEnum();
          break;
        }
        case 34: {
          CmdPayload = input.ReadBytes();
          break;
        }
      }
    }
  }
  #endif

}

public sealed partial class MintNFTPayload : pb::IMessage<MintNFTPayload>
#if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    , pb::IBufferMessage
#endif
{
  private static readonly pb::MessageParser<MintNFTPayload> _parser = new pb::MessageParser<MintNFTPayload>(() => new MintNFTPayload());
  private pb::UnknownFieldSet _unknownFields;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public static pb::MessageParser<MintNFTPayload> Parser { get { return _parser; } }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public static pbr::MessageDescriptor Descriptor {
    get { return global::OperationReflection.Descriptor.MessageTypes[2]; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  pbr::MessageDescriptor pb::IMessage.Descriptor {
    get { return Descriptor; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public MintNFTPayload() {
    OnConstruction();
  }

  partial void OnConstruction();

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public MintNFTPayload(MintNFTPayload other) : this() {
    hash_ = other.hash_;
    owner_ = other.owner_;
    _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public MintNFTPayload Clone() {
    return new MintNFTPayload(this);
  }

  /// <summary>Field number for the "hash" field.</summary>
  public const int HashFieldNumber = 1;
  private pb::ByteString hash_ = pb::ByteString.Empty;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public pb::ByteString Hash {
    get { return hash_; }
    set {
      hash_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
    }
  }

  /// <summary>Field number for the "owner" field.</summary>
  public const int OwnerFieldNumber = 2;
  private pb::ByteString owner_ = pb::ByteString.Empty;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public pb::ByteString Owner {
    get { return owner_; }
    set {
      owner_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
    }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public override bool Equals(object other) {
    return Equals(other as MintNFTPayload);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public bool Equals(MintNFTPayload other) {
    if (ReferenceEquals(other, null)) {
      return false;
    }
    if (ReferenceEquals(other, this)) {
      return true;
    }
    if (Hash != other.Hash) return false;
    if (Owner != other.Owner) return false;
    return Equals(_unknownFields, other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public override int GetHashCode() {
    int hash = 1;
    if (Hash.Length != 0) hash ^= Hash.GetHashCode();
    if (Owner.Length != 0) hash ^= Owner.GetHashCode();
    if (_unknownFields != null) {
      hash ^= _unknownFields.GetHashCode();
    }
    return hash;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public override string ToString() {
    return pb::JsonFormatter.ToDiagnosticString(this);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public void WriteTo(pb::CodedOutputStream output) {
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    output.WriteRawMessage(this);
  #else
    if (Hash.Length != 0) {
      output.WriteRawTag(10);
      output.WriteBytes(Hash);
    }
    if (Owner.Length != 0) {
      output.WriteRawTag(18);
      output.WriteBytes(Owner);
    }
    if (_unknownFields != null) {
      _unknownFields.WriteTo(output);
    }
  #endif
  }

  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
    if (Hash.Length != 0) {
      output.WriteRawTag(10);
      output.WriteBytes(Hash);
    }
    if (Owner.Length != 0) {
      output.WriteRawTag(18);
      output.WriteBytes(Owner);
    }
    if (_unknownFields != null) {
      _unknownFields.WriteTo(ref output);
    }
  }
  #endif

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public int CalculateSize() {
    int size = 0;
    if (Hash.Length != 0) {
      size += 1 + pb::CodedOutputStream.ComputeBytesSize(Hash);
    }
    if (Owner.Length != 0) {
      size += 1 + pb::CodedOutputStream.ComputeBytesSize(Owner);
    }
    if (_unknownFields != null) {
      size += _unknownFields.CalculateSize();
    }
    return size;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public void MergeFrom(MintNFTPayload other) {
    if (other == null) {
      return;
    }
    if (other.Hash.Length != 0) {
      Hash = other.Hash;
    }
    if (other.Owner.Length != 0) {
      Owner = other.Owner;
    }
    _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public void MergeFrom(pb::CodedInputStream input) {
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    input.ReadRawMessage(this);
  #else
    uint tag;
    while ((tag = input.ReadTag()) != 0) {
      switch(tag) {
        default:
          _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
          break;
        case 10: {
          Hash = input.ReadBytes();
          break;
        }
        case 18: {
          Owner = input.ReadBytes();
          break;
        }
      }
    }
  #endif
  }

  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input) {
    uint tag;
    while ((tag = input.ReadTag()) != 0) {
      switch(tag) {
        default:
          _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
          break;
        case 10: {
          Hash = input.ReadBytes();
          break;
        }
        case 18: {
          Owner = input.ReadBytes();
          break;
        }
      }
    }
  }
  #endif

}

public sealed partial class TransferNFTPayload : pb::IMessage<TransferNFTPayload>
#if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    , pb::IBufferMessage
#endif
{
  private static readonly pb::MessageParser<TransferNFTPayload> _parser = new pb::MessageParser<TransferNFTPayload>(() => new TransferNFTPayload());
  private pb::UnknownFieldSet _unknownFields;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public static pb::MessageParser<TransferNFTPayload> Parser { get { return _parser; } }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public static pbr::MessageDescriptor Descriptor {
    get { return global::OperationReflection.Descriptor.MessageTypes[3]; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  pbr::MessageDescriptor pb::IMessage.Descriptor {
    get { return Descriptor; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public TransferNFTPayload() {
    OnConstruction();
  }

  partial void OnConstruction();

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public TransferNFTPayload(TransferNFTPayload other) : this() {
    hash_ = other.hash_;
    to_ = other.to_;
    _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public TransferNFTPayload Clone() {
    return new TransferNFTPayload(this);
  }

  /// <summary>Field number for the "hash" field.</summary>
  public const int HashFieldNumber = 1;
  private pb::ByteString hash_ = pb::ByteString.Empty;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public pb::ByteString Hash {
    get { return hash_; }
    set {
      hash_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
    }
  }

  /// <summary>Field number for the "to" field.</summary>
  public const int ToFieldNumber = 2;
  private pb::ByteString to_ = pb::ByteString.Empty;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public pb::ByteString To {
    get { return to_; }
    set {
      to_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
    }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public override bool Equals(object other) {
    return Equals(other as TransferNFTPayload);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public bool Equals(TransferNFTPayload other) {
    if (ReferenceEquals(other, null)) {
      return false;
    }
    if (ReferenceEquals(other, this)) {
      return true;
    }
    if (Hash != other.Hash) return false;
    if (To != other.To) return false;
    return Equals(_unknownFields, other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public override int GetHashCode() {
    int hash = 1;
    if (Hash.Length != 0) hash ^= Hash.GetHashCode();
    if (To.Length != 0) hash ^= To.GetHashCode();
    if (_unknownFields != null) {
      hash ^= _unknownFields.GetHashCode();
    }
    return hash;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public override string ToString() {
    return pb::JsonFormatter.ToDiagnosticString(this);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public void WriteTo(pb::CodedOutputStream output) {
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    output.WriteRawMessage(this);
  #else
    if (Hash.Length != 0) {
      output.WriteRawTag(10);
      output.WriteBytes(Hash);
    }
    if (To.Length != 0) {
      output.WriteRawTag(18);
      output.WriteBytes(To);
    }
    if (_unknownFields != null) {
      _unknownFields.WriteTo(output);
    }
  #endif
  }

  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
    if (Hash.Length != 0) {
      output.WriteRawTag(10);
      output.WriteBytes(Hash);
    }
    if (To.Length != 0) {
      output.WriteRawTag(18);
      output.WriteBytes(To);
    }
    if (_unknownFields != null) {
      _unknownFields.WriteTo(ref output);
    }
  }
  #endif

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public int CalculateSize() {
    int size = 0;
    if (Hash.Length != 0) {
      size += 1 + pb::CodedOutputStream.ComputeBytesSize(Hash);
    }
    if (To.Length != 0) {
      size += 1 + pb::CodedOutputStream.ComputeBytesSize(To);
    }
    if (_unknownFields != null) {
      size += _unknownFields.CalculateSize();
    }
    return size;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public void MergeFrom(TransferNFTPayload other) {
    if (other == null) {
      return;
    }
    if (other.Hash.Length != 0) {
      Hash = other.Hash;
    }
    if (other.To.Length != 0) {
      To = other.To;
    }
    _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public void MergeFrom(pb::CodedInputStream input) {
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    input.ReadRawMessage(this);
  #else
    uint tag;
    while ((tag = input.ReadTag()) != 0) {
      switch(tag) {
        default:
          _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
          break;
        case 10: {
          Hash = input.ReadBytes();
          break;
        }
        case 18: {
          To = input.ReadBytes();
          break;
        }
      }
    }
  #endif
  }

  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input) {
    uint tag;
    while ((tag = input.ReadTag()) != 0) {
      switch(tag) {
        default:
          _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
          break;
        case 10: {
          Hash = input.ReadBytes();
          break;
        }
        case 18: {
          To = input.ReadBytes();
          break;
        }
      }
    }
  }
  #endif

}

#endregion


#endregion Designer generated code