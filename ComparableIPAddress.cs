using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace Dusty.Net
{
    public class ComparableIPAddress : IPAddress, IComparable<IPAddress>
    {
        public ComparableIPAddress(byte[] address) : base(address) { }
        public ComparableIPAddress(long newAddress) : base(newAddress) { }
        public ComparableIPAddress(byte[] address, long scopeid) : base(address, scopeid) { }

        public int CompareTo(IPAddress other)
        {
            byte[] refBytes = this.GetAddressBytes();
            byte[] compBytes = other.GetAddressBytes();
            if(refBytes.Length != compBytes.Length) { throw new ArgumentException("Comparison IP address is not of same family"); }
            for (int i = 0; i < refBytes.Length; i++)
            {
                int c = refBytes[i].CompareTo(compBytes[i]);
                if (c != 0) { return c; }
            }
            return 0;
        }
        
    }
}
