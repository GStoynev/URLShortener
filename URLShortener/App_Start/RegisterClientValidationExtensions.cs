using DataAnnotationsExtensions.ClientValidation;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(URLShortener.UI.App_Start.RegisterClientValidationExtensions), "Start")]
 
namespace URLShortener.UI.App_Start {
    public static class RegisterClientValidationExtensions {
        public static void Start() {
            DataAnnotationsModelValidatorProviderExtensions.RegisterValidationExtensions();            
        }
    }
}