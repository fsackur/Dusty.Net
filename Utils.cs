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
                throw new ArgumentException("Bit array length is not an even number of bytes");
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
    }
}
