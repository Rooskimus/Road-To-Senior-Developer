using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace Multitennant.Common
{
    public class SubnetMask
    {
        public static IPAddress CreateByHostBitLength(Int32 hostPartLength)
        {
            var binaryMask = new Byte[4];
            var netPartLength = 32 - hostPartLength;

            if (netPartLength < 2)
            {
                throw new ArgumentException("Number of hosts is too large for IPv4.");
            }

            for (var i=0; i < 4; i++)
            {
                if (i * 8 + 8 <= netPartLength)
                {
                    binaryMask[i] = (Byte)255;
                }
                else
                {
                    var oneLength = netPartLength - i * 8;
                    var binaryDigit = String.Empty.PadLeft(oneLength, '1').PadRight(8, '0');
                    binaryMask[i] = Convert.ToByte(binaryDigit, 2);
                }
            }

            return new IPAddress(binaryMask);
        }

        public static IPAddress CreateByNetBitLength(Int32 netPartLength)
        {
            var hostPartLength = 32 - netPartLength;
            return CreateByHostBitLength(hostPartLength);
        }

        public static IPAddress CreateByHostNumber(Int32 numberOfHosts)
        {
            var maxNumber = numberOfHosts + 1;
            var b = Convert.ToString(maxNumber, 2);
            return CreateByHostBitLength(b.Length);
        }
    }

    public static class IPAddressExtentions
    {
        public static IPAddress[] ParseIPAddressAndSubnetMask(String ipAddress)
        {
            var ipParts = ipAddress.Split('/');
            var parts = new IPAddress[] { ParseIPAddress(ipParts[0]), ParseSubnetMask(ipParts[1]) };
            return parts;
        }

        public static IPAddress ParseIPAddress(String ipAddress)
        {
            return IPAddress.Parse(ipAddress.Split('/').First());
        }

        public static IPAddress ParseSubnetMask(String ipAddress)
        {
            var subnetMask = ipAddress.Split('/').Last();
            var subnetMaskNumber = 0;
            if (!Int32.TryParse(subnetMask, out subnetMaskNumber))
            {
                return IPAddress.Parse(subnetMask);
            }
            else
            {
                return SubnetMask.CreateByNetBitLength(subnetMaskNumber);
            }
        }

        public static IPAddress GetBroadcastAddress(this IPAddress address, IPAddress subnetMask)
        {
            var ipAddressBytes = address.GetAddressBytes();
            var subnetMaskBytes = subnetMask.GetAddressBytes();
            if (ipAddressBytes.Length != subnetMaskBytes.Length)
            {
                throw new ArgumentException("Lengths of IP address and subnet mask do not match.");
            }
            var broadcastAddress = new Byte[ipAddressBytes.Length];
            for (var i = 0; i < broadcastAddress.Length; i++)
            {
                broadcastAddress[i] = (Byte)(ipAddressBytes[i] | (subnetMaskBytes[i] ^ 255));
            }
            return new IPAddress(broadcastAddress);
        }

        public static IPAddress GetNetworkAddress(this IPAddress address, IPAddress subnetMask)
        {
            var ipAddressBytes = address.GetAddressBytes();
            var subnetMaskBytes = subnetMask.GetAddressBytes();
            if (ipAddressBytes.Length != subnetMaskBytes.Length)
            {
                throw new ArgumentException("Lengths of IP address and subnet mask do not match");
            }

            var broadcastAddress = new Byte[ipAddressBytes.Length];
            for (var i= 0; i < broadcastAddress.Length; i++)
            {
                broadcastAddress[i] = (Byte)(ipAddressBytes[i] & (subnetMaskBytes[i]));
            }
            return new IPAddress(broadcastAddress);
        }

        public static Boolean IsInSameSubnet(this IPAddress address2, IPAddress address, Int32 hostPartLength)
        {
            return IsInSameSubnet(address2, address, SubnetMask.CreateByHostBitLength(hostPartLength));
        }

        public static Boolean IsInSameSubnet(this IPAddress address2, IPAddress address, IPAddress subnetMask)
        {
            var network1 = address.GetNetworkAddress(subnetMask);
            var network2 = address2.GetNetworkAddress(subnetMask);
            return network1.Equals(network2);
        }
    }
}