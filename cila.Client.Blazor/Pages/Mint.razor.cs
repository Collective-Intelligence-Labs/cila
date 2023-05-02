using System;
using System.Net.NetworkInformation;
using cila.Domain;
using MetaMask.Blazor;
using Microsoft.AspNetCore.Components;
using Nethereum.Signer;
using Google.Protobuf;
using Grpc.Net.Client;
using Confluent.Kafka;
using MetaMask.Blazor.Exceptions;
using Nethereum.ABI.FunctionEncoding;
using Nethereum.ABI.Model;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text;
using Microsoft.AspNetCore.Components.Forms;

namespace cila.Client.Blazor.Pages
{
    public partial class Mint : IDisposable
    {
        public struct ImageFile
        {
            public string Base64Data { get; set; }
            public string ContentType { get; set; }
            public string FileName { get; set; }
        }

        private const string AggregateId = "8863F36E552Fd66296C0b3a3D2e4028105226DB7";
        private const string ClientId = "47B0430A426fe33931aBa1e7b85d6fa34C344847";

        [Inject]
        public GrpcChannel Channel { get; set; }

        private ImageFile NftImage;
        private string NftData = string.Empty;
        private string Signature = string.Empty;
        private string Signer = string.Empty;
        private string Response = string.Empty;


        [Inject]
        public IMetaMaskService MetaMaskService { get; set; }
        public string? MetaMaskInitializedMessage { get; set; }
        public MetaMask.Blazor.Enums.Chain? Chain { get; set; }
        private bool HasMetaMask { get; set; } = false;
        private string? SelectedAddress { get; set; } = null;



        private void OnNftDataInputEvent(ChangeEventArgs args)
        {
            NftData = args.Value.ToString();
        }

        private async Task OnChange(InputFileChangeEventArgs args)
        {
            var file = args.File; // get the files selected by the users

            var buf = new byte[file.Size]; // allocate a buffer to fill with the file's data
            using (var stream = file.OpenReadStream())
            {
                await stream.ReadAsync(buf); // copy the stream to the buffer
            }

            NftImage = new ImageFile
            {
                Base64Data = Convert.ToBase64String(buf),
                ContentType = file.ContentType,
                FileName = file.Name
            };

            NftData = NftImage.Base64Data.CalculateKeccak256();
        }

        private async Task SignAndMint()
        {
            try
            {
                if (!HasMetaMask || string.IsNullOrEmpty(SelectedAddress))
                {
                    throw new Exception("MetaMask is not connected");
                }

                if (string.IsNullOrEmpty(NftData))
                {
                    throw new ArgumentException("NFT data is empty");
                }

                Response = "Minting...";

                var client = new CilaDispatcher.CilaDispatcherClient(Channel);

                var payload = new MintNFTPayload
                {
                    Hash = NftData.ToByteString(),
                    Owner = Signer.ToByteStringFromHex()
                };

                var payloadBytes = payload.ToByteArray();

                Signature = await PersonalSign(payloadBytes.ByteArrayToHex());

                var cmd = new Command
                {
                    AggregateId = AggregateId.ToByteString(),
                    CmdType = CommandType.MintNft,
                    CmdPayload = payload.ToByteArray().ToByteStringFromByteArray(),
                    CmdSignature = Signature.ToByteStringFromHex()
                };

                var operation = new Operation
                {
                    Sender = string.Format("{0}-{1}", ClientId, Signer).ToByteString(),
                };

                operation.Commands.Add(cmd);

                cila.Domain.OmnichainResponse omnichainResponse = await client.DispatchAsync(operation);
                Response = omnichainResponse.ToString();
            }
            catch (Exception ex)
            {
                Response = ex.Message;
            }
        }

        private async Task<string> PersonalSign(string data)
        {
            try
            {
                var result = await MetaMaskService.PersonalSign(data);

                var signer = new Nethereum.Signer.EthereumMessageSigner();
                var address = signer.EncodeUTF8AndEcRecover(data, result);

                Signer = address.ToLower();

                return result;
            }
            catch (UserDeniedException)
            {
                return "User Denied";
            }
            catch (Exception ex)
            {
                return $"Exception: {ex}";
            }
        }

        public async Task GetSelectedAddress()
        {
            SelectedAddress = await MetaMaskService.GetSelectedAddress();
            Console.WriteLine($"Address: {SelectedAddress}");
        }

        public async Task GetSelectedNetwork()
        {
            var chainInfo = await MetaMaskService.GetSelectedChain();
            Chain = chainInfo.chain;
        }

        protected override async Task OnInitializedAsync()
        {
            //Subscribe to events
            IMetaMaskService.AccountChangedEvent += MetaMaskService_AccountChangedEvent;
            IMetaMaskService.ChainChangedEvent += MetaMaskService_ChainChangedEvent;
            IMetaMaskService.OnConnectEvent += IMetaMaskService_OnConnectEvent;
            IMetaMaskService.OnDisconnectEvent += IMetaMaskService_OnDisconnectEvent;

            HasMetaMask = await MetaMaskService.HasMetaMask();
            if (HasMetaMask)
                await MetaMaskService.ListenToEvents();

            bool isSiteConnected = await MetaMaskService.IsSiteConnected();
            if (isSiteConnected)
            {
                await GetSelectedAddress();
                await GetSelectedNetwork();
            }

        }

        private void IMetaMaskService_OnDisconnectEvent()
        {
            Console.WriteLine("Disconnect");
        }

        private void IMetaMaskService_OnConnectEvent()
        {
            Console.WriteLine("Connect");
        }

        private async Task MetaMaskService_ChainChangedEvent((long, MetaMask.Blazor.Enums.Chain) arg)
        {
            Console.WriteLine("Chain Changed");
            await GetSelectedNetwork();
            StateHasChanged();
        }

        private async Task MetaMaskService_AccountChangedEvent(string arg)
        {
            Console.WriteLine("Account Changed");
            await GetSelectedAddress();
            StateHasChanged();
        }

        public async Task ConnectMetaMask()
        {
            try
            {
                await MetaMaskService.ConnectMetaMask();

                await GetSelectedAddress();
            }
            catch (Exception ex)
            {
                MetaMaskInitializedMessage = ex.ToString();
            }
        }

        public void Dispose()
        {
            IMetaMaskService.AccountChangedEvent -= MetaMaskService_AccountChangedEvent;
            IMetaMaskService.ChainChangedEvent -= MetaMaskService_ChainChangedEvent;
            IMetaMaskService.OnConnectEvent -= IMetaMaskService_OnConnectEvent;
            IMetaMaskService.OnDisconnectEvent -= IMetaMaskService_OnDisconnectEvent;
        }
    }
}

