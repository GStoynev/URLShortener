using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using URLShortener.UI.Controllers;

namespace URLShortener.UI.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            var settingProvider = new URLShortener.Services.SettingsProvider(false);
            // Arrange
            HomeController controller = new HomeController(settingProvider, null);

            // Act
            ViewResult result = controller.Index((string)null) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void About()
        {
            var settingProvider = new URLShortener.Services.SettingsProvider(false);
            // Arrange
            HomeController controller = new HomeController(settingProvider, null);

            // Act
            ViewResult result = controller.About() as ViewResult;

            // Assert
            Assert.AreEqual("Your application description page.", result.ViewBag.Message);
        }
    }
}
