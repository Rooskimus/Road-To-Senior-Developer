using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class SpeakerController : MultiTenantMvcController
    {
        private MultiTenantContext context = new MultiTenantContext();

        [MultiTenantControllerAllow("svcc,cssc")]
        public async Task<ActionResult> Index()
        {
            Task<List<Speaker>> speakersAll =
                new TCache<Task<List<Speaker>>>().
                    Get("s-cache", 20,
                    () =>
                    {
                        var speakersAll1 = context.Speakers.ToListAsync();
                        return speakersAll1;
                    });

            var speakers = new List<Speaker>();
            speakers = new List<Speaker>();
            foreach (var speaker in await speakersAll) 
                // The "await" needs to be added where the function would be calling the thing containted within the Task<>.
                // In this case, our foreach is really wanting a List, not a Task.  Our speakersAll is really a Task<List> without the await.
            {
                bool speakerInTenant =
                    speaker.Sessions.
                    Any(a => a.Tenant.Name == Tenant.Name);
                if (speakerInTenant)
                {
                    speakers.Add(new Speaker
                    {
                        FirstName = speaker.FirstName,
                        LastName = speaker.LastName,
                        Id = speaker.Id,
                        PictureId = speaker.Id,
                        Bio = speaker.Bio,
                        AllowHtml = speaker.AllowHtml,
                        WebSite = speaker.WebSite,
                        Sessions =
                            speaker.Sessions.
                            Where(a => a.Tenant.Name == Tenant.Name).
                            OrderBy(a => a.Title).ToList()
                    });
                }
            }
            return View("Index", speakers);
        }
    }
}