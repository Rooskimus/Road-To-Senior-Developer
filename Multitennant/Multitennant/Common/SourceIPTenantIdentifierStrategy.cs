﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Routing;

namespace Multitennant.Common
{
    public class IPTenantIdentifierStrategy : ITenantIdentifierStrategy
    {

        private readonly Dictionary<Tuple<IPAddress, IPAddress>, String> networks = new Dictionary<Tuple<IPAddress, IPAddress>, string>();

        public IPTenantIdentifierStrategy Add (IPAddress ipAddress, Int32 netmaskBits, String name)
        {
            return this.Add(ipAddress, SubnetMask.CreateByNetBitLength(netmaskBits), name);
        }

        public IPTenantIdentifierStrategy Add(IPAddress ipAddress, IPAddress netmaskAddress, String name)
        {
            this.networks[new Tuple<IPAddress, IPAddress>(ipAddress, netmaskAddress)] = name.ToLower();
            return this;
        }

        public IPTenantIdentifierStrategy Add(IPAddress ipAddress, String name)
        {
            return this.Add(ipAddress, null, name);
        }

        public string GetCurrentTenant(RequestContext context)
        {
            var ip = IPAddress.Parse(context.HttpContext.Request.UserHostAddress);
            foreach (var entry in this.networks)
            {
                if (entry.Key.Item2 == null)
                {
                    if (ip.Equals(entry.Key.Item1))
                    {
                        return entry.Value.ToLower();
                    }
                }
                else
                {
                    if (ip.IsInSameSubnet(entry.Key.Item1, entry.Key.Item2))
                    {
                        return entry.Value;
                    }
                }
            }

            return null;
        }
    }
}