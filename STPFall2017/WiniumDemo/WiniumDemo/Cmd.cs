using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Winium;
using System;

namespace WiniumDemo
{
    [TestClass]
    public class Cmd
    {
        /// <summary>
        /// Base driver
        /// </summary>
        RemoteWebDriver driver;

        /// <summary>
        /// The application under test
        /// </summary>
        IWebElement window;

        /// <summary>
        /// Connect to Winium and launch the app under test
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            // Connect to Winium and provide the application under test
            DesktopOptions options = new DesktopOptions { ApplicationPath = @"C:/windows/system32/cmd.exe" };
            driver = new WiniumDriver(new Uri("http://localhost:9999"), options);

            // Find the application under test
            window = driver.FindElement(By.XPath("/*[@ClassName='ConsoleWindowClass' and @FrameworkId='Win32']"));
        }

        /// <summary>
        /// Cleanup after the test
        /// </summary>
        [TestCleanup]
        public void Teardown()
        {
            window.FindElement(By.Id("Close")).Click();
            driver.Quit();
        }

        /// <summary>
        /// Interacting with the command line
        /// </summary>
        [TestMethod]
        [Priority(2)]
        public void OneToFive()
        {
            // Write 1-5
            for (int i = 1; i <= 5; i++)
            {
                window.SendKeys("@echo " + i);

                // You need to submit, because you cannot send Keys.Enter
                window.Submit();
            }
        }
    }
}
