using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;

namespace WebApp.Models
{
    [DbConfigurationType(typeof(DataConfiguration))]
    public class MultiTenantContext : DbContext
    {
        public MultiTenantContext()
        {

        }
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<Speaker> Speakers { get; set; }
        public DbSet<Session> Sessions { get; set; }
    }

    public class DataConfiguration : DbConfiguration
    {
        public DataConfiguration()
        {
            SetDatabaseInitializer(new MultiTenantContextInitializer());
        }
    }

    public class MultiTenantContextInitializer : 
        CreateDatabaseIfNotExists<MultiTenantContext>
    {
        protected override void Seed(MultiTenantContext context)
        {
            var tenants = new List<Tenant>
            {
                new Tenant()
                {
                    Name = "SVCC",
                    DomainName = "www.siliconvalley-codecamp.com",
                    Id = 1,
                    Default = true
                },
                new Tenant()
                {
                    Name = "ANGU",
                    DomainName = "angularu.com",
                    Id = 3,
                    Default = false
                },
                new Tenant()
                {
                    Name = "CSSC",
                    DomainName = "codestarssummit.com",
                    Id = 2,
                    Default = false
                }
            };
            tenants.ForEach(a => context.Tenants.Add(a));
            context.SaveChanges();
            // var sessionJsonAll = GetEmbeddedResourceAsString("WebApp.speaker.json");
            CreateSpeakers(context);
            CreateSessions(context);

        }

        private void CreateSpeakers(MultiTenantContext context)
        {
            var speakerJsonAll = GetEmbeddedResourceAsString("WebApp.speaker.json");

            JArray jsonValSpeakers = JArray.Parse(speakerJsonAll) as JArray;
            dynamic speakersData = jsonValSpeakers;
            foreach (dynamic speaker in speakersData)
            {
                context.Speakers.Add(new Speaker
                {
                    PictureId = speaker.id,
                    FirstName = speaker.firstName,
                    LastName = speaker.lastName,
                    AllowHtml = speaker.allowHtml,
                    Bio = speaker.bio,
                    WebSite = speaker.webSite,
                });
            }
            context.SaveChanges();
        }

        private void CreateSessions(MultiTenantContext context)
        {
            // Resume here!
        }

        private string GetEmbeddedResourceAsString(string resourceName)
        {
            // A good trick to find the name of any resources you need:
            // var list = Assembly.GetExecutingAssembly().GetManifestResourceNames();

            var assembly = Assembly.GetExecutingAssembly();

            string result;
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                result = reader.ReadToEnd();
            }
            return result;
        }
    }
}