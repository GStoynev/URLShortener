using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace URLShortener.UI.Models
{
    public class UrlShortenerViewModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string OriginalURL { get; set; }

        public string ShortURL { get; set; }

        [NotMapped]
        public string CustomURL { get; set; }
    }
}