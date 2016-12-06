using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using URLShortener.UI.Models;

namespace URLShortener.UI.Controllers
{
    public class HomeController : Controller
    {
        private UrlMapContext db = new UrlMapContext();

        // This is the default route handler.
        // It supports three possible URL types:
        // 1. (default) GET with no query string, or query string that doesn't fit method signature: in this case default view is displayed - the form promptin user to shorten URL
        // 2. GET with param slugOrId passed in query string
        //      a. when slugOrId is detected to be a numerical value, the assumption is that we are retrieving previously-shortened URL for the purpose of displaying it to user (e.g. user clicked Shorten URL button and are being presented with the result)
        //      b. in all other cases, the assumption is that someone is trying to navigate to a shorten URL - attempt to redirect
        // 3. POST (of valid data): this is a request to shorten URL entered by user
        public ActionResult Index(string slugOrId)
        {
            // 1.
            if (string.IsNullOrEmpty(slugOrId))
            {
                return View();
            }

            UrlShortenerViewModel vm = null;
            int id = default(int);
            if (int.TryParse(slugOrId, out id))
            {
                // 2.a.
                vm = db.UrlMappings.Find(id);
                return vm != null ? View(vm) : View(new UrlShortenerViewModel { Id = 0 });
            }

            // 2.b.
            vm = db.UrlMappings.Find(Encoder.Decode(slugOrId));
            if (vm == null)
            {
                vm = db.UrlMappings.FirstOrDefault(m => m.ShortURL == slugOrId);
            }
            if (null != vm)
            {
                UriBuilder goToUrl = new UriBuilder(vm.OriginalURL);
                return RedirectPermanent(goToUrl.ToString());
            }
            else
                return new HttpNotFoundResult("Unable to map your short URL to anything we previously shortened.");
        }

        [HttpPost]
        public ActionResult Index(UrlShortenerViewModel vm)
        {
            if (ModelState.IsValid)
            {
                // the steps to come up with short URL are:
                // 1. Use client's, if supplied
                // 2. Otherwise generate by:
                //     a. Insert a mapping entry to database, just to generate identity value
                //     b. encode the identity value and update inserted record's shortUrl
                var addedVm = db.UrlMappings.Add(vm);
                if (!string.IsNullOrEmpty(vm.CustomURL))
                {
                    vm.ShortURL = vm.CustomURL;
                }
                db.SaveChanges();
                if (string.IsNullOrEmpty(addedVm.ShortURL))
                {
                    addedVm.ShortURL = Encoder.Encode(vm.Id);
                    db.SaveChanges();
                }

                return RedirectToAction("Index", new { slugOrId = addedVm.Id });
            }

            return View(vm);
        }


        public ActionResult About()
        {
            return View();
        }
    }
}