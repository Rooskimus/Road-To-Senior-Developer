using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebApp.Models;

namespace WebApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //using (var context = new MultiTenantContext())
            //{
            //    var tenants = new List<Tenant>
            //    {
            //        new Tenant()
            //        {
            //            Name = "SVCC",
            //            DomainName = "www.siliconvalley-codecamp.com",
            //            Id = 1,
            //            Default = true
            //        },
            //        new Tenant()
            //        {
            //            Name = "ANGU",
            //            DomainName = "angularu.com",
            //            Id = 3,
            //            Default = false
            //        },
            //        new Tenant()
            //        {
            //            Name = "CSSC",
            //            DomainName = "codestarssummit.com",
            //            Id = 2,
            //            Default = false
            //        }
            //    };
            //    context.Tenants.AddRange(tenants);
            //};

            using (var context = new MultiTenantContext())
            {
                context.Speakers.Add(new Speaker()
                {
                    LastName = Guid.NewGuid().ToString()
                });
                context.Sessions.Add(new Models.Session()
                {
                    Title = Guid.NewGuid().ToString()
                });

                context.SaveChanges();
            }
            
            
        }
    }
}
