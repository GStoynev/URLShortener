using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAnnotationsExtensions;

namespace URLShortener.UI.Models
{
    public class UrlShortenerViewModel
    {
        public int Id { get; set; }

        [AllowHtml]
        [Required(ErrorMessage = "Required Field")]
        [DataAnnotationsExtensions.Url(UrlOptions.OptionalProtocol, ErrorMessage = "Please enter a valid url")]
        public string OriginalURL { get; set; }

        public string ShortURL { get; set; }

        [AllowHtml]
        [DataAnnotationsExtensions.Url(UrlOptions.OptionalProtocol, ErrorMessage = "Please enter a valid url")]
        public string CustomURL { get; set; }
    }
}