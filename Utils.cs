using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Collections;

namespace Dusty.Net
{
    public static class Utils
    {
        public static bool IsInSameSubnet(SubnetIpAddress reference, IPAddress comparison)
        {
            if (reference.AddressFamily != comparison.AddressFamily)
            {
                throw new ArgumentException("reference and comparison IP addresses are not of same address family");
            }

            byte[] refBytes = reference.GetAddressBytes();
            byte[] compBytes = comparison.GetAddressBytes();

            //TODO
            return true;

        }

        public static bool IsInSameSubnet(IPAddress reference, IPAddress comparison, SubnetMask subnetMask)
        {
            return IsInSameSubnet(new SubnetIpAddress(reference, subnetMask), comparison);
        }

        public static byte[] GetBytes(BitArray bits)
        {
            if (bits.Length % 8 != 0)
            {
                throw new ArgumentException("Bit array length is not an integer number of bytes");
            }
            int length = bits.Length / 8;
            byte[] bytes = new byte[length];

            for (int i = 0; i < length; i++)
            {
                int b = 0;
                for (int j = 0; j < length; j++)
                {
                    b = b & (bits[i*8 + j] ? 1 : 0);
                    b = b << 1;
                }
                bytes[i] = (byte)b;
            }

            return bytes;
        }

        public static BitArray GetNetworkBits(BitArray bits, int networkPrefixLength)
        {
            BitArray outbits = new BitArray(bits.Length);
            for (int i = 0; i < networkPrefixLength; i++)
            {
                outbits[i] = bits[i];
            }
            for (int i = networkPrefixLength; i < bits.Length; i++)
            {
                outbits[i] = false;
            }
            return outbits;
        }

        public static BitArray GetHostBits(BitArray bits, int networkPrefixLength)
        {
            BitArray outbits = new BitArray(bits.Length);
            for (int i = 0; i < networkPrefixLength; i++)
            {
                outbits[i] = false;
            }
            for (int i = networkPrefixLength; i < bits.Length; i++)
            {
                outbits[i] = bits[i];
            }
            return outbits;
        }

        
        public static bool CompareBits(BitArray reference, BitArray comparison)
        {
            if (reference.Length != comparison.Length) { return false; }
            for (int i = 0; i < reference.Length; i++)
            {
                if (reference[i] != comparison[i]) { return false; }
            }
            return true;
        }

        public static bool CompareNetworkBits(BitArray reference, BitArray comparison, int networkPrefixLength)
        {
            if (reference.Length != comparison.Length) { return false; }
            for (int i = 0; i < networkPrefixLength; i++)
            {
                if (reference[i] != comparison[i]) { return false; }
            }
            return true;
        }

        public static bool CompareHostBits(BitArray reference, BitArray comparison, int networkPrefixLength)
        {
            if (reference.Length != comparison.Length) { return false; }
            for (int i = networkPrefixLength; i < reference.Length; i++)
            {
                if (reference[i] != comparison[i]) { return false; }
            }
            return true;
        }
        
        public static BitArray GetBits(byte[] bytes)
        {
            int length = bytes.Length * 8;
            BitArray bitArray = new BitArray(length);

            for (int i = 0; i < bytes.Length; i++)
            {
                int bitpos = 0x80;
                for (int j = 0; j < 8; j++)
                {
                    bitArray[i * 8 + j] = (bytes[i] & bitpos) != 0;
                    bitpos = bitpos >> 1;
                }
            }

            return bitArray;
        }
    }
}
