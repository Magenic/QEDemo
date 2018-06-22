using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Utility;

namespace ParallelTesting
{
    [TestClass]
   [DeploymentItem("TestData.txt")]
   [DeploymentItem("TestDataPT.txt")]
    public class PTTests
    {
        [TestMethod]
        public void ConfigTestPT()
        {
            Assert.AreEqual("ValueForPT", Config.GetValue("KEY"));
        }

        [TestMethod]
        public void FileTestPT()
        {
            Assert.AreEqual("PT", FileReader.GetText("TestData.txt"));
        }

        [TestMethod]
        public void FileTest_OtherPT()
        {
            Assert.AreEqual("PT_OtherFile", FileReader.GetText("TestDataPT.txt"));
        }
    }
}
