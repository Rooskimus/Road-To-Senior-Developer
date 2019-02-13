using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;

namespace Multitennant.Common
{
    public interface ITenantIdentifierStrategy
    {
        String GetCurrentTenant(RequestContext context);
    }
}
