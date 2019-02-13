using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace Multitennant.Common
{
    public class HostHeaderTenantIdentifierStrategy : ITenantIdentifierStrategy
    {
        public String GetCurrentTenant(RequestContext context)
        {
            return context.HttpContext.Request.Url.Host.ToLower();
            // This should contain validators in real implementation.
        }
    }
}