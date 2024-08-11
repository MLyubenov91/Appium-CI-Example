using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Interactions;

namespace MobileGestursTesting
{
    internal class Swipe
    {
        private AppiumDriver driver;
        private AppiumLocalService service;

        [OneTimeSetUp]
        public void Setup()
        {
            var androidOptions = new AppiumOptions
            {
                PlatformName = "Android",
                AutomationName = "UiAutomator2",
                DeviceName = "Pixel 7 API 34",
                App = @"./apk/ApiDemos-debug.apk",
                PlatformVersion = "14"
            };

            service = new AppiumServiceBuilder()
                .WithIPAddress("127.0.0.1")
                .UsingPort(4723)
                .Build();

            driver = new AndroidDriver(service, androidOptions);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            driver?.Quit();
            driver?.Dispose();
            service?.Dispose();
        }

        [Test]
        public void SwipeTest()
        {
            var views = driver.FindElement(MobileBy.AccessibilityId("Views"));
            views.Click();

            var gallery = driver.FindElement(MobileBy.AccessibilityId("Gallery"));
            gallery.Click();

            var photos = driver.FindElement(MobileBy.AccessibilityId("1. Photos"));
            photos.Click();

            var firstPhoto = driver.FindElement(MobileBy.AndroidUIAutomator("new UiSelector().className(\"android.widget.ImageView\").instance(0)"));

            var action = new Actions(driver);
            var swipe = action.ClickAndHold(firstPhoto)
                              .MoveByOffset(-200, 0)
                              .Release()
                              .Build();
            swipe.Perform();

            var thirdPhoto = driver.FindElement(MobileBy.AndroidUIAutomator("new UiSelector().className(\"android.widget.ImageView\").instance(2)"));

            Assert.That(thirdPhoto, Is.Not.Null, "The third picture was not found after swiping");
        }
    }
}
