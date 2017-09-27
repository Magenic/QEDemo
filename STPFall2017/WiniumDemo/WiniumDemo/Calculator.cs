using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Winium;
using System;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Reflection;

namespace WiniumDemo
{
    [TestClass]
    public class Calculator
    {
        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context 
        ///</summary>
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
            DesktopOptions options = new DesktopOptions { ApplicationPath = @"C:/windows/system32/calc.exe" };
            driver = new WiniumDriver(new Uri("http://localhost:9999"), options);

            // Find the application under test
            window = driver.FindElement(By.XPath("/*[@Name='Calculator' and @FrameworkId='Win32']"));
        }

        /// <summary>
        /// Cleanup after each test
        /// </summary>
        [TestCleanup]
        public void Teardown()
        {
            //Screeshot
            if(testContextInstance.CurrentTestOutcome == UnitTestOutcome.Failed)
            {
                Screenshot screenShot = ((ITakesScreenshot)driver).GetScreenshot();

                // Calculate the file name
                string path = Path.Combine(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Demo_" + DateTime.UtcNow.ToString("yyyy-MM-dd-hh-mm-ss", CultureInfo.InvariantCulture) + ".png"));

                // Save the screenshot
                screenShot.SaveAsFile(path, ImageFormat.Png);

                testContextInstance.AddResultFile(path);
            }

            // Cleanup the application and session
            window.FindElement(By.Id("Close")).Click();
            driver.Quit();
        }

        /// <summary>
        /// Simple test with element IDs
        /// </summary>
        [TestMethod]
        [Priority(1)]
        public void Add()
        {
            window.FindElement(By.Id("num7Button")).Click();
            window.FindElement(By.Id("plusButton")).Click();
            window.FindElement(By.Id("num3Button")).Click();
            window.FindElement(By.Id("equalButton")).Click();

            Assert.AreEqual("Display is 10", window.FindElement(By.Id("CalculatorResults")).GetAttribute("Name"));
        }

        /// <summary>
        /// This test should fail
        /// </summary>
        [TestMethod]
        [Priority(4)]
        public void Fail()
        {
            window.FindElement(By.Id("num7Button")).Click();

            Assert.AreEqual("Display is 10", window.FindElement(By.Id("CalculatorResults")).GetAttribute("Name"));
        }
    }
}
