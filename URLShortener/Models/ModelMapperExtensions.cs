using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace URLShortener.UI.Models
{
    public static class ModelMapperExtensions
    {
        public static UrlShortenerViewModel Map(this URLShortener.Models.UrlMap domainModel)
        {
            if (domainModel == null) return null;
            return new UrlShortenerViewModel
            {
                Id = domainModel.Id,
                CustomURL = null, // we don't store that
                OriginalURL = domainModel.OriginalURL,
                ShortURL = domainModel.ShortURL
            };
        }

        public static URLShortener.Models.UrlMap Map(this UrlShortenerViewModel vm)
        {
            if (vm == null) return null;
            return new URLShortener.Models.UrlMap
            {
                Id = vm.Id,
                OriginalURL = vm.OriginalURL,
                ShortURL = vm.ShortURL
            };
        }
    }
}