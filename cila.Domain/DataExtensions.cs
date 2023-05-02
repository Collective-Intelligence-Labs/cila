using System;
using Google.Protobuf;
using Nethereum.Util;
using System.Text;
using System.Text.RegularExpressions;

namespace cila.Domain
{
	public static class DataExtensions
	{
        public static string CalculateKeccak256(this string str)
        {
            var keccak = new Sha3Keccack();
            return keccak.CalculateHash(str);
        }

        public static byte[] ToByteArrayFromHex(this string str)
        {
            str = str.StartsWith("0x") ? str.Substring(2) : str;
            return Enumerable.Range(0, str.Length).Where(x => x % 2 == 0).Select(x => Convert.ToByte(str.Substring(x, 2), 16)).ToArray();
        }

        public static byte[] ToByteArray(this string str)
        {
            var bytes = Encoding.Default.GetBytes(str);
            return ToByteArrayFromHex(Convert.ToHexString(bytes));
        }

        public static ByteString ToByteStringFromByteArray(this byte[] bytes)
        {
            return ByteString.CopyFrom(bytes);
        }

        public static ByteString ToByteStringFromHex(this string str)
        {
            var bytes = str.ToByteArrayFromHex();
            return ByteString.CopyFrom(bytes);
        }

        public static ByteString ToByteString(this string str)
        {
            var bytes = str.ToByteArray();
            return ByteString.CopyFrom(bytes);
        }


        private static bool IsValidHexString(string str)
        {
            // For C-style hex notation (0xFF) you can use @"\A\b(0[xX])?[0-9a-fA-F]+\b\Z"
            return Regex.IsMatch(str, @"\A\b[0-9a-fA-F]+\b\Z");
        }
    }
}

