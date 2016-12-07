using URLShortener.Models;

namespace URLShortener.Services
{
    public interface IUrlMapService
    {
        UrlMap Find(int id);
        
        UrlMap Find(string shortURL);

        UrlMap Add(UrlMap urlMap);

        UrlMap UpdateShortUrl(int id, string newShortUrl);
    }
}
