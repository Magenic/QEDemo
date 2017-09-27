using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Threading;
using Winium.Elements.Desktop;

namespace WiniumDemo
{
    [TestClass]
    public class NotePad
    {
        /// <summary>
        /// Base remote driver
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
            DesiredCapabilities dc = new DesiredCapabilities();
            dc.SetCapability("app", @"C:/windows/system32/notepad.exe");
            driver = new RemoteWebDriver(new Uri("http://localhost:9999"), dc);

            // Find the application under test
            window = driver.FindElement(By.XPath("/*[@ClassName='Notepad' and @FrameworkId='Win32']"));
        }

        /// <summary>
        /// Close the app under test
        /// </summary>
        [TestCleanup]
        public void Teardown()
        {
            driver.Quit();
        }

        /// <summary>
        /// Write to notepad
        /// </summary>
        [TestMethod]
        [Priority(1)]
        public void EnterText()
        {
            window.FindElement(By.ClassName("Edit")).SendKeys("Automation text");
            Assert.AreEqual("Automation text", window.FindElement(By.ClassName("Edit")).Text);
        }

        /// <summary>
        /// Write to notepad - With mix of selectors
        /// </summary>
        [TestMethod]
        [Priority(1)]
        public void SaveFile()
        {
            // Unique file name
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Demo_" + DateTime.UtcNow.ToString("yyyy-MM-dd-hh-mm-ss", CultureInfo.InvariantCulture) + ".txt");

            window.FindElement(By.ClassName("Edit")).SendKeys("Automation text");

            // Common control
            Menu menu = new Menu(window.FindElement(By.Id("MenuBar")));
            menu.FindElementByName("File").Click();
            menu.FindElementByName("Save As...").Click();

            window.FindElement(By.Id("1001")).SendKeys(path);

            // Save the file
            window.FindElement(By.Name("Save")).Click();

            // Hack for demo
            Thread.Sleep(1000);

            // Make sure the file exists
            Assert.IsTrue(File.Exists(path), "File was not saved.");
        }
    }
}
