using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSEmbroidery.Core;

namespace NSEmbroidery.UnitTests
{
    [TestClass]
    public class UnitResolution
    {
        [TestMethod]
        public void Test_Resolution_SetResolution()
        {
            Resolution resolution = new Resolution(10, 5);
            resolution.SetResolution(4, 2);

            Assert.AreEqual(resolution.Width, 4);
            Assert.AreEqual(resolution.Height, 2);
        }
    }
}
