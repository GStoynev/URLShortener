namespace URLShortener.Services
{
    public class SettingsProvider : ISettingsProvider
    {
        public bool AllowCustomShortURLInput { get; private set; }
                
        public SettingsProvider(bool allowCustomShortURLInput)
        {
            AllowCustomShortURLInput = allowCustomShortURLInput;
        }
    }
}
