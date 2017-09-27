using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Remote;
using System;

namespace WinAppDemo
{
    [TestClass]
    public class JavaApp
    {
        /// <summary>
        /// Appium Windows Driver
        /// </summary>
        protected static WindowsDriver<WindowsElement> swtApp;

        /// <summary>
        /// Setup the WinApp driver for sample Java app
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            // Get the SWT java application
            DesiredCapabilities appCapabilities = new DesiredCapabilities();
            appCapabilities.SetCapability("app", @"C:\Users\troyw\Desktop\WiniumDemo\DemoSWT.jar");
            swtApp = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723"), appCapabilities);
        }

        /// <summary>
        /// Close the app under test
        /// </summary>
        [TestCleanup]
        public void Teardown()
        {
            swtApp.Quit();
        }

        /// <summary>
        /// Interact with the Java App
        /// </summary>
        [TestMethod]
        [Priority(4)]
        public void JavaAppTest()
        {
            swtApp.FindElements(By.ClassName("Edit"))[0].SendKeys("Test");
            swtApp.FindElement(By.Name("Click Me")).Click();
            Assert.AreEqual("Test", swtApp.FindElements(By.ClassName("Edit"))[1].Text);
        }
    }
}
