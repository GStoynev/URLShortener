using System.ComponentModel.DataAnnotations.Schema;

namespace URLShortener.Models
{
    public class UrlMap
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string OriginalURL { get; set; }

        public string ShortURL { get; set; }
    }
}