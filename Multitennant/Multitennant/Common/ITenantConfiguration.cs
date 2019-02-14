using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multitennant.Common
{
    public interface ITenantConfiguration
    {
        String Name { get; }
        String Theme { get; }
        String MasterPage { get; }
        IServiceProvider ServicProvider { get; }
        IDictionary<String, Object> Properties { get; }
        IEnumerable<String> Counters { get; }
        void Initialize();
    }
}

    // Example implementation:
    //public sealed class XyzNetTenantConfiguration : ITenantConfiguration
    //{
    //    public XyzNetTenantConfiguration()
    //    {
    //        //do something productive
    //        this.Properties = new Dictionary<String, Object>();
    //        this.Counters = new List<String> { "C", "D", "E" };
    //    }
    //    public void Initialize()
    //    {
    //        //do something productive
    //    }
    //    public String Name { get { return "xyz.net"; } }
    //    public String MasterPage { get { return this.Name; } }
    //    public String Theme { get { return this.Name; } }
    //    public IServiceProvider ServiceProvider { get; private set; }
    //    public IDictionary<String, Object> Properties { get; private set; }
    //    public IEnumerable<String> Counters { get; private set; }
    //}

