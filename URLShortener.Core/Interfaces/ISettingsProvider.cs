
namespace URLShortener.Services
{
    public interface ISettingsProvider
    {
        bool AllowCustomShortURLInput{ get; }
    }
}
