using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;

namespace TestProject1rog
{
    [TestFixture]
    public class RogovskiTests
    {
        private IWebDriver? driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver(@"C:\Users\opilane\source\repos\TestProject1rog\TestProject1rog\bin\Debug\net8.0");
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [Test]
        public void NavigateToPage()
        {
            driver!.Navigate().GoToUrl("https://aleksanderrogovski22.thkit.ee/");
        }

        [Test]
        public void VerifyPageTitle()
        {
            NavigateToPage();
            Assert.AreEqual("Rogovski veebirakenduste leht", driver.Title, "Page title does not match expected value.");
        }

        [Test]
        public void VerifyMinuInfoHeader()
        {
            NavigateToPage();
            var minuInfoHeader = driver!.FindElement(By.CssSelector("#section1 h2"));
            Assert.AreEqual("Minu info", minuInfoHeader.Text, "The 'Minu info' header text does not match.");
        }

        [Test]
        public void VerifyPanelIsHiddenByDefault()
        {
            NavigateToPage();
            var panelSection = driver!.FindElement(By.Id("panel"));
            Assert.IsFalse(panelSection.Displayed, "'panel' section should be hidden initially.");
        }

        [Test]
        public void TogglePanelVisibility()
        {
            NavigateToPage();
            driver!.FindElement(By.Id("section1")).Click();
            System.Threading.Thread.Sleep(500);  // Wait for the animation
            var panelSection = driver.FindElement(By.Id("panel"));
            Assert.IsTrue(panelSection.Displayed, "'panel' section should be visible after clicking.");
        }

        [Test]
        public void VerifyHomepageLink()
        {
            NavigateToPage();
            TogglePanelVisibility();  // Ensure the panel is visible
            var homepageLink = driver!.FindElement(By.LinkText("link"));
            Assert.AreEqual("http://www.tthk.ee/", homepageLink.GetAttribute("href"), "Homepage link does not match the expected URL.");
        }

        [Test]
        public void VerifyFooterContent()
        {
            driver!.Navigate().GoToUrl("https://aleksanderrogovski22.thkit.ee/");
            IWebElement footer = driver.FindElement(By.TagName("footer"));
            Assert.IsTrue(footer.Text.Contains("© A.Rogovski"), "Footer does not contain the expected content.");
        }

        [TearDown]
        public void Teardown()
        {
            if (driver != null)
            {
                driver.Quit();
                driver.Dispose();
                driver = null;
            }
        }
    }
}
