using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Pixi.Configuration;

namespace Pixi_DotNet_ClassLibrary_UnitTest
{
    [TestClass]
    public class PixiSimpleConfigurationItem_UnitTest
    {
        [TestMethod]
        public void PixiSimpleConfigItem_Constructor_Test()
        {
            string tItemName = String.Empty;
            string tItemValue = String.Empty;
            int tInt = 2;

            PixiSimpleConfigItem target = new PixiSimpleConfigItem("Item1", tInt.ToString());
            Assert.AreEqual("Item1", target.ItemName, "PixiSimpleConfigItem_Constructor_Test: Test 1 - ItemName not correct");
            Assert.AreEqual("2", target.ItemValue, "PixiSimpleConfigItem_Constructor_Test: Test 1 - ItemValue not correct");
            target = null;

            target = new PixiSimpleConfigItem("Item2", "Val1");
            Assert.AreEqual("Item2", target.ItemName, "PixiSimpleConfigItem_Constructor_Test: Test 2 - ItemName not correct");
            Assert.AreEqual("Val1", target.ItemValue, "PixiSimpleConfigItem_Constructor_Test: Test 2 - ItemValue not correct");
            target = null;

        }

    }

}
