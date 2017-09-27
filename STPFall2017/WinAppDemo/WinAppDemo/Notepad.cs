using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Remote;
using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Threading;

namespace WinAppDemo
{
    [TestClass]
    public class Notepad
    {
        /// <summary>
        /// Appium Windows Driver
        /// </summary>
        protected static WindowsDriver<WindowsElement> NotepadSession;

        /// <summary>
        /// Setup the driver and launch the app
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            DesiredCapabilities appCapabilities = new DesiredCapabilities();
            appCapabilities.SetCapability("app", @"C:\Windows\System32\notepad.exe");
            NotepadSession = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723"), appCapabilities, TimeSpan.FromSeconds(20));
        }

        /// <summary>
        /// Close the app under test
        /// </summary>
        [TestCleanup]
        public void Teardown()
        {
            // If there is no session than there is no cleanup
            if (NotepadSession == null)
            {
                return;
            }

            // Find the window handle
            string value = NotepadSession.WindowHandles[0];
            if (value.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
            {
                value = value.Substring(2);
                value = int.Parse(value, NumberStyles.HexNumber).ToString();
            }

            if (NotepadSession != null)
            {
                NotepadSession.Quit();
                NotepadSession.Dispose();
                NotepadSession = null;
            }

            // Kill the assocated notepad process - DEMO ONLY
            foreach (var proc in Process.GetProcessesByName("Notepad"))
            {
                if (proc.MainWindowHandle.ToString() == value)
                {
                    proc.Kill();
                }
            }
        }

        /// <summary>
        /// Write to notepad
        /// </summary>
        [TestMethod]
        [Priority(1)]
        public void WriteText()
        {
            NotepadSession.FindElement(By.ClassName("Edit")).SendKeys("Automation text");
            Assert.AreEqual("Automation text", NotepadSession.FindElement(By.ClassName("Edit")).Text);
        }

        /// <summary>
        /// Write to notepad and save - use multiple selector types
        /// </summary>
        [TestMethod]
        [Priority(1)]
        public void SaveTextFile()
        {
            // Unique file name
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Demo_" + DateTime.UtcNow.ToString("yyyy-MM-dd-hh-mm-ss", CultureInfo.InvariantCulture) + ".txt");

            // Enter text and save as
            NotepadSession.FindElement(By.ClassName("Edit")).SendKeys("Automation text");
            NotepadSession.FindElementByAccessibilityId("MenuBar").Click();
            NotepadSession.FindElement(By.Name("File")).Click();
            NotepadSession.FindElementByAccessibilityId("4").Click();

            NotepadSession.FindElements(By.XPath("//ToolBar[@AutomationId='1001']"));
            NotepadSession.FindElementByAccessibilityId("1001").SendKeys(path);

            // Save the file
            NotepadSession.FindElementByName("Save").Click();

            // Demo hack
            Thread.Sleep(1000);

            // Make sure the file exists
            Assert.IsTrue(File.Exists(path), "File was not saved.");
        }
    }
}
