using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Service;

namespace MobileGestursTesting
{
    public class Scroll
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
                App = @"./ApiDemos-debug.apk",
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
        public void ScrollTest()
        {
            var views = driver.FindElement(MobileBy.AccessibilityId("Views"));
            views.Click();

            ScrollToText("Lists");

            var lists = driver.FindElement(MobileBy.AccessibilityId("Lists"));
            Assert.That(lists, Is.Not.Null, "The 'Lists' element was not found after scrolling");
            lists.Click();

            var elementInLists = driver.FindElement(MobileBy.AccessibilityId("10. Single choice list"));
            Assert.That(elementInLists, Is.Not.Null, "The expected element in the list was not found");
        }

        private void ScrollToText(string text)
        {
            driver.FindElement(MobileBy.AndroidUIAutomator("new UiScrollable(new UiSelector().scrollable(true)).scrollIntoView(new UiSelector().text(\"" + text + "\"))"));
        }
    }
}
