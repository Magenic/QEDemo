using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Remote;
using System;

namespace WinAppDemo
{
    [TestClass]
    public class Cmd
    {
        /// <summary>
        /// WinApp driver session
        /// </summary>
        protected static WindowsDriver<WindowsElement> cmdSession;

        /// <summary>
        /// Startup app
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            DesiredCapabilities appCapabilities = new DesiredCapabilities();
            appCapabilities.SetCapability("app", @"C:\Windows\system32\cmd.exe");
            cmdSession = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723"), appCapabilities);
        }

        /// <summary>
        /// Close the app under test
        /// </summary>
        [TestCleanup]
        public void Teardown()
        {
            cmdSession.Quit();
        }

        /// <summary>
        /// Write to command line - Note Keys.Enter is being passed
        /// </summary>
        [TestMethod]
        [Priority(2)]
        public void OneToFive()
        {
            cmdSession.FindElement(By.ClassName("ConsoleWindowClass")).SendKeys("@echo 1-5" + Keys.Enter);

            for (int i = 1; i <= 5; i++)
            {
                cmdSession.FindElement(By.ClassName("ConsoleWindowClass")).SendKeys("@echo " + i + Keys.Enter);
            }
        }
    }
}
