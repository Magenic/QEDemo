using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace WiniumDemo
{
    [TestClass]
    class Setup
    {
        /// <summary>
        /// Winium process
        /// </summary>
       private static Process winium;

        /// <summary>
        /// Launch Winium
        /// </summary>
        /// <param name="context"></param>
        [AssemblyInitialize]
        public static void StartDriver(TestContext context)
        {
            winium = new Process();

            // Hard coded path for demo - Your path will likely be different
            winium.StartInfo.FileName =  @"C:\Demo\Winium.Desktop.Driver.exe";
            winium.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
            winium.Start();
        }

        /// <summary>
        /// End the Winium process
        /// </summary>
        [AssemblyCleanup]
        public static void Cleanup()
        {
            winium.Kill();
        }
    }
}
