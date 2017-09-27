using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Remote;
using System;
using System.Globalization;
using System.IO;
using System.Reflection;

namespace WinAppDemo
{
    [TestClass]
    public class Calculator
    {
        /// <summary>
        /// The test context
        /// </summary>
        private TestContext testContextInstance;

        /// <summary>
        /// Gets and sets the test context
        /// </summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }
    
        /// <summary>
        /// WinApp driver session
        /// </summary>
        protected static WindowsDriver<WindowsElement> CalcSession;

        /// <summary>
        /// Setup the WinApp driver and launch the app
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            DesiredCapabilities appCapabilities = new DesiredCapabilities();
            appCapabilities.SetCapability("app", "Microsoft.WindowsCalculator_8wekyb3d8bbwe!App");
            CalcSession = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723"), appCapabilities);
        }

        /// <summary>
        /// Cleanup after the test
        /// </summary>
        [TestCleanup]
        public void Teardown()
        {
            //Screeshot
            if (testContextInstance.CurrentTestOutcome == UnitTestOutcome.Failed)
            {
                Screenshot screenShot = ((ITakesScreenshot)CalcSession).GetScreenshot();

                // Calculate the file name
                string path = Path.Combine(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Demo_" + DateTime.UtcNow.ToString("yyyy-MM-dd-hh-mm-ss", CultureInfo.InvariantCulture)));

                // Save the screenshot
                screenShot.SaveAsFile(path + ".png", ScreenshotImageFormat.Png);
                File.WriteAllText(path + ".txt", CalcSession.PageSource);

                // Add the screenshot and page source file to the test result
                testContextInstance.AddResultFile(path + ".png");
                testContextInstance.AddResultFile(path + ".txt");
            }

            CalcSession.Quit();
        }

        /// <summary>
        /// Add two numbers - Use multiple selector types
        /// </summary>
        [TestMethod]
        [Priority(1)]
        public void AddNumbers()
        {
            CalcSession.FindElement(By.XPath("//Button[@AutomationId=\"num7Button\"]")).Click();
            CalcSession.FindElementByName("Plus").Click();
            CalcSession.FindElement(By.Name("Three")).Click();
            CalcSession.FindElementByAccessibilityId("equalButton").Click();

            Assert.AreEqual("Display is 10", CalcSession.FindElementByAccessibilityId("CalculatorResults").GetAttribute("Name"));
        }

        /// <summary>
        /// Test that should fail so we can see logging 
        /// </summary>
        [TestMethod]
        [Priority(4)]
        public void Fail()
        {
            CalcSession.FindElementByName("Plus").Click();

            Assert.AreEqual("Display is 10", CalcSession.FindElementByAccessibilityId("CalculatorResults").GetAttribute("Name"));
        }
    }
}
