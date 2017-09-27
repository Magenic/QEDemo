using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace WinAppDemo
{
    [TestClass]
    class Setup
    {
        /// <summary>
        /// The WinApp driver
        /// </summary>
       private static Process winAppDriver;

        /// <summary>
        /// Setup the WinApp process
        /// </summary>
        /// <param name="context">The test context</param>
        [AssemblyInitialize]
        public static void StartDriver(TestContext context)
        {
            winAppDriver = new Process();

            // Hard coded path for demo - Your path will likely be different
            winAppDriver.StartInfo.FileName = @"C:\Demo\WinAppDriver\WinAppDriver.exe";
            winAppDriver.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
            winAppDriver.Start();
        }

        /// <summary>
        /// End the WinApp process
        /// </summary>
        [AssemblyCleanup]
        public static void Cleanup()
        {
            winAppDriver.Kill();
        }
    }
}
