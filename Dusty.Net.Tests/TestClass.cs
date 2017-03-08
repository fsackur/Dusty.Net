using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Dusty.Net.Tests
{
    [TestFixture]
    public class TestConstructors
    {

        [Test]
        public void TestSubnetMaskConstructors()
        {
            Assert.That(
                (new SubnetMask(new byte[] { 0xFF, 0xFF, 0xF0, 0x00 })).AddressFamily,
                Is.EqualTo(AddressFamily.InterNetwork)
            );
            Assert.That(
                (new SubnetMask(new byte[] {
                    0xFF, 0xFF, 0xFF, 0xFF,
                    0xFF, 0xFF, 0xF0, 0x00,
                    0x00, 0x00, 0x00, 0x00,
                    0x00, 0x00, 0x00, 0x00
                })).AddressFamily,
                Is.EqualTo(AddressFamily.InterNetworkV6)
            );
            Assert.Throws(
                Is.InstanceOf(typeof(ArgumentException)),
                delegate () { new SubnetMask(new byte[] { 0xFF, 0xFF, 0xF0, 0xF0 }); }
            );
        }

        [Test]
        public void TestSubnetIpAddressConstructors()
        {
            Assert.That(
                (new SubnetIpAddress(
                    new byte[] { 192, 168, 100, 15 },
                    new byte[] { 255, 255, 255, 0 }
                )).AddressFamily,
                Is.EqualTo(AddressFamily.InterNetwork)
            );
            Assert.Throws(
                Is.InstanceOf(typeof(ArgumentException)),
                delegate ()
                {
                    new SubnetIpAddress(
                        IPAddress.Parse("fe80::dead:beef").GetAddressBytes(),
                        new byte[] { 0xFF, 0xFF, 0xF0, 0xF0 }
                    );
                }

            );

            Assert.That(
                (new SubnetIpAddress(
                    new byte[] { 192, 168, 100, 15 },
                    new byte[] { 255, 255, 255, 0 }
                )).AddressFamily,
                Is.EqualTo(AddressFamily.InterNetwork)
            );
            Assert.Throws(
                Is.InstanceOf(typeof(ArgumentException)),
                delegate ()
                {
                    new SubnetIpAddress(
                        IPAddress.Parse("fe80::dead:beef").GetAddressBytes(),
                        new byte[] { 0xFF, 0xFF, 0xF0, 0xF0 }
                    );
                }

            );

        }

        [Test]
        public void TestNetworkAddressConstructors()
        {
            Assert.That(
                (new NetworkAddress(
                    new byte[] { 192, 168, 100, 0 },
                    new byte[] { 255, 255, 255, 0 }
                )).AddressFamily,
                Is.EqualTo(AddressFamily.InterNetwork)
            );
            Assert.Throws(
                Is.InstanceOf(typeof(ArgumentException)),
                delegate ()
                {
                    new NetworkAddress(
                        new byte[] { 192, 168, 100, 15 },
                        new byte[] { 255, 255, 255, 0 }
                    );
                }

            );
        }
    }
}
