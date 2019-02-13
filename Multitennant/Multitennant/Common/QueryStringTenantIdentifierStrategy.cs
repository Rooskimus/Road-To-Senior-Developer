using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace Multitennant.Common
{
    public class QueryStringTenantIdentifierStrategy : ITenantIdentifierStrategy
    {
        public string GetCurrentTenant(RequestContext context)
        {
            return (context.HttpContext.Request.QueryString["Tenant"] ?? String.Empty).ToLower();
        }
    }
}