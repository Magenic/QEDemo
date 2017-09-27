using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Remote;
using System;
using System.Threading;

namespace WinAppDemo
{
    [TestClass]
    public class Excel
    {
        /// <summary>
        /// The Excel WinApp session
        /// </summary>
        protected static WindowsDriver<WindowsElement> ExcelSession;

        /// <summary>
        /// Connection to the Windows desktop
        /// </summary>
        protected static WindowsDriver<WindowsElement> DesktopSession;

        /// <summary>
        /// Setup multiple WinApp driver sessions
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            // Connect to the desktop
            DesiredCapabilities rootAppCapabilities = new DesiredCapabilities();
            rootAppCapabilities.SetCapability("app", "Root");
            DesktopSession = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723"), rootAppCapabilities, TimeSpan.FromSeconds(30));

            // Launch Excel
            DesiredCapabilities appCapabilities = new DesiredCapabilities();
            appCapabilities.SetCapability("app", @"C:\Program Files (x86)\Microsoft Office\Office16\EXCEL.EXE");
            ExcelSession = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723"), appCapabilities);
        }

        /// <summary>
        /// Close the app under test
        /// </summary>
        [TestCleanup]
        public void Teardown()
        {
            // Release Excel session
            ExcelSession.Quit();
            ExcelSession.Dispose();
            ExcelSession = null;

            // Release desktop session
            DesktopSession.Quit();
            DesktopSession.Dispose();
            DesktopSession = null;

            // Did not close app so it will be left open - This is obviously a bad thing
        }

        /// <summary>
        /// Write to notepad
        /// </summary>
        [TestMethod]
        [Priority(3)]
        public void VerifyCellValues()
        {
            // Demo hack
            Thread.Sleep(1000);

            // Find new Excel session, first one can go away
            WindowsElement eclipseWindow = DesktopSession.FindElementByName("Excel");
            string eclipseTopLevelWindowHandle = eclipseTopLevelWindowHandle = (int.Parse(eclipseWindow.GetAttribute("NativeWindowHandle"))).ToString("x");
            DesiredCapabilities appCapabilities = new DesiredCapabilities();
            appCapabilities.SetCapability("appTopLevelWindow", eclipseTopLevelWindowHandle);
            ExcelSession = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723"), appCapabilities);

            // Demo hack
            Thread.Sleep(1000);
            // Open new worksheet
            ExcelSession.FindElementByAccessibilityId("AIOStartDocument").Click();

            // Demo hack
            Thread.Sleep(1000);

            // Enter values - Note* The tab and enter keys work
            ExcelSession.FindElementByName("Grid").SendKeys("1" + Keys.Tab + "2" + Keys.Enter + "3" + Keys.Tab + "4" + Keys.Tab);

            // Verify the values we entered are present
            Assert.AreEqual("1", ExcelSession.FindElementByName("A1").Text);
            Assert.AreEqual("2", ExcelSession.FindElementByName("B1").Text);
            Assert.AreEqual("3", ExcelSession.FindElementByName("A2").Text);
            Assert.AreEqual("4", ExcelSession.FindElementByName("B2").Text);
        }
    }
}
