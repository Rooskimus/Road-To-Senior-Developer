using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class SpeakerController : MultiTenantMvcController
    {
        public async Task<ActionResult> Index()
        {
            var speakers = new List<Speaker>();
            using (var context = new MultiTenantContext())
            {
                var speakersAll = await context.Speakers.ToListAsync();
                speakers = new List<Speaker>();
                foreach (var speaker in speakersAll)
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
            }
            return View("Index", speakers);
        }
    }
}