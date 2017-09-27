using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Winium;
using System;
using System.Threading;

namespace WiniumDemo
{
    [TestClass]
    public class Excel
    {
        /// <summary>
        /// The Winium driver
        /// </summary>
        WiniumDriver driver;

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
            DesktopOptions options = new DesktopOptions { ApplicationPath = @"C:\Program Files (x86)\Microsoft Office\Office16\EXCEL.EXE" };
            driver = new WiniumDriver(new Uri("http://localhost:9999"), options);

            // Hack for demo
            Thread.Sleep(1000);

            // Find the application under test
            window = driver.FindElement(By.XPath("/*[@Name='Excel' and @FrameworkId='Win32']"));
        }

        /// <summary>
        /// Cleanup after test
        /// </summary>
        [TestCleanup]
        public void Teardown()
        {
            window.FindElement(By.Id("Close")).Click();
            driver.Quit();
        }

        /// <summary>
        /// Write to notepad
        /// </summary>
        [TestMethod]
        [Priority(3)]
        public void VerifyCellValues()
        {
            // Hack for demo
            Thread.Sleep(1000);
            window.FindElement(By.Id("AIOStartDocument")).Click();

            // Hack for demo
            Thread.Sleep(1000);
            
            // Can only do one row
            window.FindElement(By.Name("Formula Bar")).SendKeys("1");
            window.FindElement(By.Name("Formula Bar")).Submit();
            window.FindElement(By.Name("Formula Bar")).SendKeys("2");
            window.FindElement(By.Name("Formula Bar")).Submit();

            Assert.AreEqual("1", window.FindElement(By.Name("A1")).Text);
            Assert.AreEqual("2", window.FindElement(By.Name("A2")).Text);
        }
    }
}
