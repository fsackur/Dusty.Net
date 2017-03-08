using System;
using System.Collections;
using System.Linq;
using System.Net;
using System.Text;
using System.Net.Sockets;

namespace Dusty.Net
{
    public class SubnetMask : ComparableIPAddress
    {
        public static SubnetMask GetDefaultValue(AddressFamily family)
        {
            switch (family) {

                case AddressFamily.InterNetworkV6:
                    return new SubnetMask(
                        new byte[] {
                            0xFF, 0xFF, 0xFF, 0xFF,
                            0xFF, 0xFF, 0xFF, 0xFF,
                            0xFF, 0xFF, 0xFF, 0xFF,
                            0xFF, 0xFF, 0xFF, 0xFF
                        }
                    );

                case AddressFamily.InterNetwork:
                    return new SubnetMask(
                        new byte[] { 0xFF, 0xFF, 0xFF, 0xFF }
                    );

                default:
                    throw new ArgumentException(
                        "Invalid address family"
                    );
            }
        }

        //Constructors
        public SubnetMask(byte[] address) : base(address)
        {
            this.length = this.GetNetworkPrefixLength();
        }

        public SubnetMask(long newAddress) : base(newAddress)
        {
            this.length = this.GetNetworkPrefixLength();
        }

        public SubnetMask(byte[] address, long scopeid) : base(address, scopeid)
        {
            this.length = this.GetNetworkPrefixLength();
        }

        //Only interesting property over IPaddress is the concept of Network prefix length (e.g. '24' in '192.168.0.1/24')
        private int length;
        public int NetworkPrefixLength
        {
            get { return this.length; }
        }


        public int GetNetworkPrefixLength()
        {
            string errHostBits = "Subnet mask contains bits in host section";
            BitArray bits = this.GetAddressBits();
            bool reachedEndOfNetworkBits = false;
            int length = 0;

            foreach (bool bit in bits)
            {

                if (bit)
                {
                    if (reachedEndOfNetworkBits)
                    {
                        throw new ArgumentException(errHostBits);
                    }
                    else
                    {
                        length++;
                    }
                }
                else
                {
                    reachedEndOfNetworkBits = true;
                }
            }

            return length;
        }

    }
}
