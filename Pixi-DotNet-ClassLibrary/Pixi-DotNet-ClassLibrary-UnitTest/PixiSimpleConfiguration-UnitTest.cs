using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Pixi.Configuration;

namespace Pixi_DotNet_ClassLibrary_UnitTest
{
    [TestClass]
    public class PixiSimpleConfigurationXml_UnitTest
    {
        //<summary>
        //A test for PixiSimplePcConfiguration Constructor
        //</summary>
        [TestMethod]
        public void PixiSimpleConfiguration_Constructor_Test()
        {
            PixiSimpleConfigurationXml target = new PixiSimpleConfigurationXml();
            Assert.AreEqual(0, target.PixiConfigItems.Count, "PixiSimplePcConfiguration_Constructor_Test: Test 1 - ConfigItems.Count <>= 0");
            Assert.AreEqual("", target.FileFolder, "PixiSimplePcConfiguration_Constructor_Test: Test 2 - FileFolder= ''");
            Assert.AreEqual("", target.FileName, "PixiSimplePcConfiguration_Constructor_Test: Test 3 - FileName= ''");
        }

        //<summary>
        //A test for PixiSimplePcConfiguration Properties
        //</summary>
        [TestMethod]
        public void PixiSimpleConfiguration_Properties_Test()
        {

            PixiSimpleConfigurationXml target = new PixiSimpleConfigurationXml();

            target.PixiConfigItems.Add(new PixiSimpleConfigItem("Item1", "123"));
            target.PixiConfigItems.Add(new PixiSimpleConfigItem("Item2", "456"));
            Assert.AreEqual(2, target.PixiConfigItems.Count, "PixiSimplePcConfiguration_Properties_Test: Test 1 - 2 ConfigItems added");

            target.FileFolder = "C:\\Folder";
            Assert.AreEqual("C:\\Folder", target.FileFolder, "PixiSimplePcConfiguration_Properties_Test: Test 2 - FileFolder");

            target.FileName = "FNAME";
            Assert.AreEqual("FNAME", target.FileName, "PixiSimplePcConfiguration_Properties_Test: Test 3 - FileName");

        }

    }
}
