using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace Multitennant.Common
{

    // This also needs mapping:
    //var s = new SourceDomainTenantIdentifierStrategy();
    //s.Add("some.domain", "abc.com");
    //s.Add("xyz.net");

    // This maps all client requests coming from some.domain to abc.com.
    // The tenant's name is dropped in the xyz.net mapping because
    // it will be identical to the domain name (it's handled by the overloads).
    public class DomainTenantIdentifierStrategy : ITenantIdentifierStrategy
    {

        private readonly Dictionary<String, String> domains = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        
        public DomainTenantIdentifierStrategy Add(String domain, String name)
        {
            this.domains[domain] = name;
            return this;
        }

        public DomainTenantIdentifierStrategy Add(String domain)
        {
            return this.Add(domain, domain);
        }

        public string GetCurrentTenant(RequestContext context)
        {
            var hostName = context.HttpContext.Request.UserHostName;
            var domainName = String.Join(".", hostName.Split('.').Skip(1)).ToLower();
            return this.domains.Where(domain => domain.Key == domainName)
                .Select(domain => domain.Value).FirstOrDefault();
        }
    }
}