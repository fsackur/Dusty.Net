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
        public NetworkAddress(long newAddress) : base(newAddress) { }
        public NetworkAddress(byte[] address) : base(address) { }
        public NetworkAddress(byte[] address, long scopeid) : base(address, scopeid) { }

        public NetworkAddress(IPAddress ipaddress, SubnetMask subnetMask) : base(ipaddress, subnetMask)
        {
        }

        public NetworkAddress(long newAddress, long subnetMask) : base(newAddress, subnetMask)
        {
        }

        public NetworkAddress(byte[] address, byte[] subnetMask) : base(address, subnetMask)
        {
        }
        
        public NetworkAddress(byte[] address, byte[] subnetMask, long scopeid) : base(address, subnetMask, scopeid)
        {
        }

        public override NetworkAddress GetNetworkAddress()
        {
            return this;
        }
    }
}
