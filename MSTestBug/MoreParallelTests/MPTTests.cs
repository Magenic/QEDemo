using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Utility;

namespace MoreParallelTests
{
    [TestClass]
    [DeploymentItem("TestData.txt")]
    [DeploymentItem("TestDataMPT.txt")]
    public class MPTTests
    {
        [TestMethod]
        public void ConfigTestMPT()
        {
            Assert.AreEqual("ValueForMPT", Config.GetValue("KEY"));
        }

        [TestMethod]
        public void FileTestMPT()
        {
            Assert.AreEqual("MPT", FileReader.GetText("TestData.txt"));
        }

        [TestMethod]
        public void FileTest_OtherMPT()
        {
            Assert.AreEqual("MPT_OtherFile", FileReader.GetText("TestDataMPT.txt"));
        }
    }
}
