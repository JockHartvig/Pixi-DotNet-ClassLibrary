using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Pixi.Configuration;

namespace Pixi_DotNet_ClassLibrary_UnitTest
{
    [TestClass]
    public class PixiSimpleConfigurationItem_UnitTest
    {
        public string cTestFolder = "C:\\PixiTestFolder";
        public string cCopyFolder = "C:\\PixiTestCopyFolder";
        public string cCfgFileName = "CONFIGTEST.XML";

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



        //<summary>
        //A test for CreateConfig_file
        //</summary>
        [TestMethod]
        public void PixiSimpleConfiguration_CreateConfigfile_TC001_Test()
        {
            PixiSimpleConfigurationXml target;

            //-------------------------------------------------------------------------
            // Test 1 - CreateCongigFile() - No input parms with directory do not exist
            //-------------------------------------------------------------------------
            if (Directory.Exists(cTestFolder))
            {
                Directory.Delete(cTestFolder, true);
            }

            target = new PixiSimpleConfigurationXml();
            target.PixiConfigItems.Add(new PixiSimpleConfigItem("Item1", "123"));
            target.PixiConfigItems.Add(new PixiSimpleConfigItem("Item2", "456"));
            target.FileFolder = cTestFolder;
            target.FileName = cCfgFileName;
            try
            {
                target.CreateConfigfile();
            }
            catch
            {
                Assert.Fail("PixiSimplePcConfiguration_CreateConfigfile: Test 1 - CreateConfigFile, Fail when No Directory/file exists");
            }
            target = null;

        }

        [TestMethod]
        public void PixiSimpleConfiguration_CreateConfigfile_TC002_Test()
        {
            PixiSimpleConfigurationXml target;

            //--------------------------------------------------------------------------------------
            // Test 2 - CreateConfigFile() - No input parms with directory exists, File do not exist
            //--------------------------------------------------------------------------------------
            if (Directory.Exists(cTestFolder))
            {
                if (File.Exists(cTestFolder + "\\" + cCfgFileName))
                {
                    File.Delete(cTestFolder + "\\" + cCfgFileName);
                }
            }
            target = new PixiSimpleConfigurationXml();
            target.PixiConfigItems.Add(new PixiSimpleConfigItem("Item1", "123"));
            target.PixiConfigItems.Add(new PixiSimpleConfigItem("Item2", "456"));
            target.FileFolder = cTestFolder;
            target.FileName = cCfgFileName;
            try
            {
                target.CreateConfigfile();
            }
            catch
            {
                Assert.Fail("PixiSimplePcConfiguration_CreateConfigfile: Test 2 - CreateConfigFile, Fail when Directory exists, File do not exist");
            }
            target = null;
        }


        [TestMethod]
        public void PixiSimpleConfiguration_CreateConfigfile_TC003_Test()
        {
            PixiSimpleConfigurationXml target;

            //-------------------------------------------------------------------------
            // Test 3 - CreateConfigfile(cTestFolder, cCfgFileName) - With input parms
            //-------------------------------------------------------------------------
            if (Directory.Exists(cTestFolder))
            {
                Directory.Delete(cTestFolder, true);
            }
            target = new PixiSimpleConfigurationXml();
            target.PixiConfigItems.Add(new PixiSimpleConfigItem("Item1", "123"));
            target.PixiConfigItems.Add(new PixiSimpleConfigItem("Item2", "456"));
            try
            {
                target.CreateConfigfile(cTestFolder, cCfgFileName);
            }
            catch
            {
                 Assert.Fail("PixiSimplePcConfiguration_CreateConfigfile: Test 3 - CreateConfigFile(Folde,File), Fail when input parms");
            }
            Assert.AreEqual(cTestFolder, target.FileFolder, "PixiSimplePcConfiguration_CreateConfigfile: Test 3 - CreateConfigFile(Folder,File), FolderName");
            Assert.AreEqual(cCfgFileName, target.FileName, "PixiSimplePcConfiguration_CreateConfigfile: Test 3 - CreateConfigFile(Folder,File), FileName");
            target = null;

            //-----------------------------------------------------
            // Test 4 - CreateConfigfile() with directory exists, File  exists
            //-----------------------------------------------------
            target = new PixiSimpleConfigurationXml();
            target.PixiConfigItems.Add(new PixiSimpleConfigItem("Item1", "123"));
            target.PixiConfigItems.Add(new PixiSimpleConfigItem("Item2", "456"));
            target.FileFolder = cTestFolder;
            target.FileName = cCfgFileName;
            try
            {
                target.CreateConfigfile();
                Assert.Fail("PixiSimplePcConfiguration_CreateConfigfile: Test 4 - CreateConfigFile, Fail when Directory exists, File exist. No exception thrown");
            }
            catch (PixiConfigFileAllreadyExistException) { }
            target = null;

            // Test cleanUp
            Directory.Delete(cTestFolder, true);
        }

        //<summary>
        //A test for ReadConfigfile
        //</summary>
        [TestMethod]
        public void PixiSimpleConfiguration_ReadConfigfile_Test()
        {
            int Rc = 0;
            int Count = 0;
            PixiSimpleConfigurationXml target;

            // Pretest - Create Config file
            if (Directory.Exists(cTestFolder))
            {
                Directory.Delete(cTestFolder, true);
            }


            target = new PixiSimpleConfigurationXml();
            target.PixiConfigItems.Add(new PixiSimpleConfigItem("Item1", "123"));
            target.PixiConfigItems.Add(new PixiSimpleConfigItem("Item2", "456"));
            try
            {
                target.CreateConfigfile(cTestFolder, cCfgFileName);
            }
            catch
            {
                Assert.AreEqual(0, Rc, "PixiSimplePcConfiguration_ReadConfigfile: Test 0 - ReadConfigFile, Fail when No Directory/file exists");
            }
            target = null;

            //----------------------------------------------------
            // Test 1 - ReadConfigfile() with directory exists, File exists
            //-----------------------------------------------------
            target = new PixiSimpleConfigurationXml();
            target.FileFolder = cTestFolder;
            target.FileName = cCfgFileName;
            try
            {
                target.ReadConfigfile();
            }
            catch
            {
                Assert.Fail("PixiSimplePcConfiguration_ReadConfigfile: Test 1 - ReadConfigFile, Fail when Directory exists, File exist");
            }

            Count = 0;
            foreach (PixiSimpleConfigItem obj in target.PixiConfigItems)
            {
                Count = Count + 1;
                switch (Count)
                {
                    case 1:
                        Assert.AreEqual("Item1", obj.ItemName, "PixiSimplePcConfiguration_ReadConfigfile: Test 1a - ReadConfigFile, Item.ItemName <> 'Item1'");
                        Assert.AreEqual(123, obj.ItemValue, "PixiSimplePcConfiguration_ReadConfigfile: Test 1a - ReadConfigFile, Item.ItemValue <> 123");
                        break;
                    case 2:
                        Assert.AreEqual("Item2", obj.ItemName, "PixiSimplePcConfiguration_ReadConfigfile: Test 1a - ReadConfigFile, Item.ItemName <> 'Item2'");
                        Assert.AreEqual("456", obj.ItemValue, "PixiSimplePcConfiguration_ReadConfigfile: Test 1a - ReadConfigFile, Item.ItemValue <> '456'");
                        break;
                    default:
                        break;
                }
            }

            //-----------------------------------------------------
            // Test 2 - read with directory exists, Input parms
            //-----------------------------------------------------
            target = new PixiSimpleConfigurationXml();
            try
            {
                target.ReadConfigfile(cTestFolder, cCfgFileName);
            }
            catch
            {
                Assert.Fail("PixiSimplePcConfiguration_ReadConfigfile: Test 2 - ReadConfigFile, Fail when Directory exists, File exist");
            }

            Count = 0;
            foreach (PixiSimpleConfigItem obj in target.PixiConfigItems)
            {
                Count = Count + 1;
                switch (Count)
                {
                    case 1:
                        Assert.AreEqual("Item1", obj.ItemName, "PixiSimplePcConfiguration_ReadConfigfile: Test 2a - ReadConfigFile, Item.ItemName <> 'Item1'");
                        Assert.AreEqual(123, obj.ItemValue, "PixiSimplePcConfiguration_ReadConfigfile: Test 2a - ReadConfigFile, Item.ItemValue <> 123");
                        break;
                    case 2:
                        Assert.AreEqual("Item2", obj.ItemName, "PixiSimplePcConfiguration_ReadConfigfile: Test 2a - ReadConfigFile, Item.ItemName <> 'Item2'");
                        Assert.AreEqual("456", obj.ItemValue, "PixiSimplePcConfiguration_ReadConfigfile: Test 2a - ReadConfigFile, Item.ItemValue <> '456'");
                        break;
                    default:
                        break;
                }
            }

            //-----------------------------------------------------
            // Test 3 - Read with directory exists, File not exists
            //-----------------------------------------------------
            File.Delete(cTestFolder + "\\" + cCfgFileName);

            target = new PixiSimpleConfigurationXml();
            target.FileFolder = cTestFolder;
            target.FileName = cCfgFileName;
            try
            {
                target.ReadConfigfile();
                try
                {
                    Assert.Fail("PixiSimplePcConfiguration_ReadConfigfile: Test 3 - ReadConfigFile, Fail when Directory exists, File exist. No exception thrown");
                }
                catch { }
            }
            catch (PixiConfigFileNotExistException) { }
            catch (Exception ex)
            {
                Assert.Fail("PixiSimplePcConfiguration_ReadConfigfile: Test 3 - ReadConfigFile, Fail when Directory exists, File exist. Exception=" + ex.Message);
            }

            //-----------------------------------------------------
            // Test 4 - Read with directory not exists, File not exists
            //-----------------------------------------------------

            target = new PixiSimpleConfigurationXml();
            target.FileFolder = cTestFolder;
            target.FileName = cCfgFileName;
            try
            {
                target.ReadConfigfile();
                try
                {
                    Assert.Fail("PixiSimplePcConfiguration_ReadConfigfile: Test 4 - ReadConfigFile, Fail when Directory not exists, File exist. No exception thrown");
                }
                catch { }
            }
            catch (PixiConfigFileNotExistException) { }
            catch
            {
                Assert.Fail("PixiSimplePcConfiguration_ReadConfigfile: Test 4 - ReadConfigFile, Fail when Directory not exists, File exist. Rc <> -3");
            }

            //-----------------------------------------------------
            // Test cleanUp
            //-----------------------------------------------------
            Directory.Delete(cTestFolder, true);
        }

        //<summary>
        //A test for SaveConfigfile
        //</summary>
        [TestMethod]
        public void PixiSimpleConfiguration_SaveConfigfile_Test()
        {
            string strTestFolder = "C:\\PixiTestFolder";
            string strCfgFileName = "CONFIGTEST.XML";

            int Count = 0;
            PixiSimpleConfigurationXml target;

            // Pretest - Create Config file
            if (Directory.Exists(strTestFolder))
            {
                Directory.Delete(strTestFolder, true);
            }
            target = new PixiSimpleConfigurationXml();
            target.PixiConfigItems.Add(new PixiSimpleConfigItem("Item1", "123"));
            target.PixiConfigItems.Add(new PixiSimpleConfigItem("Item2", "456"));
            target.FileFolder = strTestFolder;
            target.FileName = strCfgFileName;
            try
            {
                target.CreateConfigfile();
            }
            catch
            {
                Assert.Fail("PixiSimplePcConfiguration_CreateConfigfile: Test 0 - ReadConfigFile, Pretest");
            }
            target = null;

            //-----------------------------------------------------
            // Test 1 - Save with directory exists, File exists
            //-----------------------------------------------------
            target = new PixiSimpleConfigurationXml();
            target.FileFolder = strTestFolder;
            target.FileName = strCfgFileName;
            try
            {
                target.SaveConfigfile();
            }
            catch
            {
                Assert.Fail("PixiSimplePcConfiguration_ReadConfigfile: Test 1 - SaveConfigFile, Fail when Directory exists, File exist");
            }

            Count = 0;
            foreach (PixiSimpleConfigItem obj in target.PixiConfigItems)
            {
                Count = Count + 1;
                switch(Count)
                {
                    case 1:
                        Assert.AreEqual("Item1", obj.ItemName, "PixiSimplePcConfiguration_ReadConfigfile: Test 1a - ReadConfigFile, Item.ItemName <> 'Item1'");
                        Assert.AreEqual(123, obj.ItemValue, "PixiSimplePcConfiguration_ReadConfigfile: Test 1a - ReadConfigFile, Item.ItemValue <> 123");
                        break;
                    case 2:
                        Assert.AreEqual("Item2", obj.ItemName, "PixiSimplePcConfiguration_ReadConfigfile: Test 1a - ReadConfigFile, Item.ItemName <> 'Item2'");
                        Assert.AreEqual("456", obj.ItemValue, "PixiSimplePcConfiguration_ReadConfigfile: Test 1a - ReadConfigFile, Item.ItemValue <> '456'");
                        break;
                    default:
                        break;
                }
            }

        }

    }

}
