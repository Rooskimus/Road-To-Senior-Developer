using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multitennant.Common
{
    public interface ITenantLocationStrategy
    {
        /// <summary>
        /// The names in the dictionary are the unique tenant identifiers and
        /// the Types are the concrete class that implements ITenantConfiguration.
        /// </summary>
        /// <returns></returns>
        IDictionary<String, Type> GetTenants();
    }
}
