using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSEmbroidery.Core;
using System.Drawing;
using System.Collections.Generic;

namespace NSEmbroidery.UnitTests
{
    [TestClass]
    public class Test_SymbolsDecorator
    {
        public void TestMethod_Decorate()
        {
            IDecorator decorator = new SymbolsDecorator();

            Settings.Palette = new Palette(new Color[] { Color.Red, Color.Green });
            Settings.Symbols = new char[] { '@', '$' };

            
        }

    }
}
