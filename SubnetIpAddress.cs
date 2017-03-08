using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Text.RegularExpressions;

namespace Dusty.Net
{
    public class SubnetIpAddress : IPAddress
    {
        //Constructors from base class
        public SubnetIpAddress(byte[] address) : base(address) { }
        public SubnetIpAddress(long newAddress) : base(newAddress) { }
        public SubnetIpAddress(byte[] address, long scopeid) : base(address, scopeid) { }

        //Extended constructors
        public SubnetIpAddress(byte[] address, byte[] subnetMask) : base(address)
        {
            this.subnetMask = new SubnetMask(subnetMask);
        }

        public SubnetIpAddress(long newAddress, long subnetMask) : base(newAddress)
        {
            this.subnetMask = new SubnetMask(subnetMask);
        }

        public SubnetIpAddress(byte[] address, byte[] subnetMask, long scopeid) : base(address, scopeid)
        {
            this.subnetMask = new SubnetMask(subnetMask);
        }

        public SubnetIpAddress(IPAddress ipaddress, SubnetMask subnetMask) : base(ipaddress.GetAddressBytes())
        {
            this.subnetMask = subnetMask;
        }

        private SubnetMask mask;
        public SubnetMask subnetMask {
            get
            {
                return mask;
            }
            set
            {
                if (value.AddressFamily != this.AddressFamily)
                {
                    throw new ArgumentException("Subnet mask is not of same family as IP address");
                }
                this.mask = value;
            }
        }

        private NetworkAddress network;
        public NetworkAddress networkAddress
        {
            get
            {
                return network;
            }
        }

        public string ToCidrString()
        {
            return string.Format(
                "{0}/{1}",
                Regex.Replace(ToString(), "%.*$", ""),
                subnetMask.NetworkPrefixLength
            );
        }

        public bool IsInSameSubnet(IPAddress comparisonIp)
        {
            return Utils.IsInSameSubnet(this, comparisonIp);
        }

        public NetworkAddress GetNetworkAddress()
        {
            return new NetworkAddress(
                new IPAddress(Utils.GetBytes(this.GetNetworkBits())),
                this.subnetMask
                );
        }
    }


}
