using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Text.RegularExpressions;

namespace Dusty.Net
{
    public class SubnetIpAddress : ComparableIPAddress
    {
        //Constructors from base class
        public SubnetIpAddress(byte[] address) : base(address)
        {
            init(SubnetMask.GetDefaultValue(this.AddressFamily));
        }
        public SubnetIpAddress(long newAddress) : base(newAddress)
        {
            init(SubnetMask.GetDefaultValue(this.AddressFamily));
        }
        public SubnetIpAddress(byte[] address, long scopeid) : base(address, scopeid)
        {
            init(SubnetMask.GetDefaultValue(this.AddressFamily));
        }

        //Extended constructors
        public SubnetIpAddress(byte[] address, byte[] subnetMask) : base(address)
        {
            init(new SubnetMask(subnetMask));
        }

        public SubnetIpAddress(long newAddress, long subnetMask) : base(newAddress)
        {
            init(new SubnetMask(subnetMask));
        }

        public SubnetIpAddress(byte[] address, byte[] subnetMask, long scopeid) : base(address, scopeid)
        {
            init(new SubnetMask(subnetMask));
        }

        public SubnetIpAddress(IPAddress ipaddress, SubnetMask subnetMask) : base(ipaddress.GetAddressBytes())
        {
            init(subnetMask);
        }

        private void init(SubnetMask mask)
        {
            this.mask = mask;
            byte[] networkBytes = Utils.GetBytes(Utils.GetNetworkBits(this.GetAddressBits(), mask.NetworkPrefixLength));
            this.network = new NetworkAddress(
                new IPAddress(networkBytes),
                mask
            );
        }
        

        private SubnetMask mask
        {
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

        public SubnetMask subnetMask {
            get
            {
                return mask;
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
