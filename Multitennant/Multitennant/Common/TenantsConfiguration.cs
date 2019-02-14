using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Multitennant.Common
{
    public class TenantsConfiguration
    {
        public static String DefaultTenant { get; set; }

        public static ITenantIdentifierStrategy TenantIdentifier { get; set; }
        public static ITenantLocationStrategy TenantLocation { get; set; }

        public static class Identifiers
        {
            public static readonly HostHeaderTenantIdentifierStrategy HostHeader =
                new HostHeaderTenantIdentifierStrategy();

            public static readonly QueryStringTenantIdentifierStrategy QueryString =
                new QueryStringTenantIdentifierStrategy();


            // The two below are factories for lower coupling; we can change
            // the base code if needed and not change our references.
            public static DomainTenantIdentifierStrategy SourceDomain()
            {
                return new DomainTenantIdentifierStrategy();
            }

            public static IPTenantIdentifierStrategy SourceIP()
            {
                return new IPTenantIdentifierStrategy();
            }
        }
    }
}