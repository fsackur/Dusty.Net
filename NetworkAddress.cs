using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Dusty.Net
{
    public class NetworkAddress : SubnetIpAddress
    {
        //Constructors from base class
        public NetworkAddress(long newAddress) : base(newAddress) { init(); }
        public NetworkAddress(byte[] address) : base(address) { init(); }
        public NetworkAddress(byte[] address, long scopeid) : base(address, scopeid) { init(); }

        public NetworkAddress(IPAddress ipaddress, SubnetMask subnetMask) : base(ipaddress, subnetMask)
        {
            init();
        }

        public NetworkAddress(long newAddress, long subnetMask) : base(newAddress, subnetMask)
        {
            init();
        }

        public NetworkAddress(byte[] address, byte[] subnetMask) : base(address, subnetMask)
        {
            init();
        }
        
        public NetworkAddress(byte[] address, byte[] subnetMask, long scopeid) : base(address, subnetMask, scopeid)
        {
            init();
        }

        private void init()
        {
            IPAddress zero = this.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6 ?
                IPAddress.Parse("::0") :
                IPAddress.Parse("0.0.0.0");
            if (
                Utils.CompareHostBits(
                    this.GetAddressBits(),
                    zero.GetAddressBits(),
                    this.subnetMask.NetworkPrefixLength
                )
            ) { }
            else
            {
                throw new ArgumentException("Host bits supplied to NetworkAddress constructor");
            }
        }

        new public NetworkAddress GetNetworkAddress()
        {
            return this;
        }
    }
}
