using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace Dusty.Net
{
    public static class ExtensionMethods
    {
        public static BitArray GetAddressBits(this IPAddress ipaddress)
        {
            int length = GetBitLength(ipaddress);
            BitArray bitArray = new BitArray(length);

            byte[] bytes = ipaddress.GetAddressBytes();

            for (int i = 0; i < bytes.Length; i++)
            {
                int bitpos = 0x80;
                for (int j = 0; j < 8; j++)
                {
                    bitArray[i*8 + j] = (bytes[i] & bitpos) != 0;
                    bitpos = bitpos >> 1;
                }
            }

            return bitArray;
        }

        public static int GetBitLength(this IPAddress ipaddress)
        {
            return ipaddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6 ? 128 : 32;
        }


        //Returns a string representation of the bits in the address
        public static string ToBinaryString(this IPAddress ipaddress)
        {
            int length = GetBitLength(ipaddress);
            BitArray bits = ipaddress.GetAddressBits();
            StringBuilder sb = new StringBuilder(length);
            foreach (var bit in bits)
            {
                sb.Append((bool)bit ? "1" : "0");
            }
            return sb.ToString();
        }



        
    }
}
