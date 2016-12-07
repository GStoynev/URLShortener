using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using URLShortener.Services;
using URLShortener.UI.Models;

namespace URLShortener.UI.Controllers
{
    public class HomeController : Controller
    {
        // I demonstrate injection of a service provider; also best-practice for unit-testing
        private ISettingsProvider _settingsProvider;
        private IUrlMapService _urlMappingService;

        public HomeController(ISettingsProvider settings, IUrlMapService urlMappingService)
        {
            _settingsProvider = settings;
            _urlMappingService = urlMappingService;
        }

        // This controller is the default route handler.
        // It supports three possible URL types:
        // 1. (default) GET with no query string, or query string that doesn't fit method signature: in this case default view is displayed - a form prompting the user to shorten a URL
        // 2. GET with param slugOrId passed in query string
        //      a. when slugOrId is detected to be a numerical value, the assumption is that we are retrieving previously-shortened URL for the purpose of displaying it to user (e.g. user clicked Shorten URL button and are being presented with the result)
        //      b. in all other cases, the assumption is that someone is trying to navigate to a shorten URL; firs try to retrieve long URL by decoding the short URL into an Id
        //      c. lastly try to retrieve by short Url (custom short URLs)
        // 3. POST (of valid data): this is a request to shorten URL entered by user
        public ActionResult Index(string slugOrId)
        {
            // 1.
            if (string.IsNullOrEmpty(slugOrId)) return View();

            // 2.
            var vm = (UrlShortenerViewModel)null;
            var id = default(int);
            if (int.TryParse(slugOrId, out id))
            {
                // 2.a.
                vm = _urlMappingService.Find(id).Map();
                return vm != null ? View(vm) : View(new UrlShortenerViewModel { Id = 0 }); // Id = 0 is a convention for "not found" - the view displays a message in that case
            }

            // 2.b. or 2.c
            vm = _urlMappingService.Find(Encoder.Decode(slugOrId)).Map() ?? _urlMappingService.Find(slugOrId).Map();
            if (vm == null) return new HttpNotFoundResult("Unable to map your short URL to anything we previously shortened.");
            
           UriBuilder goToUrl = new UriBuilder(vm.OriginalURL); // if more robust URL validation is implemented, this is an area that can be improved
           return RedirectPermanent(goToUrl.ToString());     
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Index(UrlShortenerViewModel vm)
        {
            if (ModelState.IsValid)
            {
                // the steps to come up with short URL are:
                // 1. Use client's, if supplied
                // 2. Otherwise generate by:
                //     a. Insert a mapping entry to database, just to generate identity value
                //     b. encode the identity value and update inserted record's shortUrl
                var addedVm = _urlMappingService.Add(vm.Map()).Map();
                if (!string.IsNullOrEmpty(vm.CustomURL))
                {
                    addedVm.ShortURL = vm.CustomURL;
                }
                
                if (string.IsNullOrEmpty(addedVm.ShortURL))
                {
                    addedVm.ShortURL = Encoder.Encode(addedVm.Id);
                }

                addedVm = _urlMappingService.UpdateShortUrl(addedVm.Id, addedVm.ShortURL).Map();

                if (addedVm == null) addedVm = new UrlShortenerViewModel { Id = 0 };
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