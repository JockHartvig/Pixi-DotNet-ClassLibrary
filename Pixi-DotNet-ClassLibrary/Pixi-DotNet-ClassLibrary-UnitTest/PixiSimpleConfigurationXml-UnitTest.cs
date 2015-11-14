using System;
using System.IO;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Pixi.Configuration;

namespace Pixi_DotNet_ClassLibrary_UnitTest
{
    [TestClass]
    public class PixiSimpleConfigurationXml_UnitTest
    {
        public string cTestFolder = "C:\\PixiTestFolder";
        public string cCopyFolder = "C:\\PixiTestCopyFolder";
        public string cCfgFileName = "CONFIGTEST.XML";

        //<summary>
        //A test for PixiSimpleConfigurationXml Constructor
        //</summary>
        [TestMethod]
        public void PixiSimpleConfigurationXml_Constructor_Test()
        {
            PixiSimpleConfigurationXml target = new PixiSimpleConfigurationXml();
            Assert.AreEqual(0, target.PixiConfigItems.Count, "PixiSimplePcConfigurationXml_Constructor_Test: Test 1 - ConfigItems.Count <>= 0");
            Assert.AreEqual("", target.FileFolder, "PixiSimplePcConfigurationXml_Constructor_Test: Test 2 - FileFolder= ''");
            Assert.AreEqual("", target.FileName, "PixiSimplePcConfigurationXml_Constructor_Test: Test 3 - FileName= ''");
            Assert.AreEqual("", target.FilePath, "PixiSimplePcConfigurationXml_Constructor_Test: Test 4 - FilePath= ''");
        }

        //<summary>
        //A test for PixiSimpleConfigurationXml Properties
        //</summary>
        [TestMethod]
        public void PixiSimpleConfigurationXml_Properties_Test()
        {

            PixiSimpleConfigurationXml target = new PixiSimpleConfigurationXml();

            target.PixiConfigItems.Add(new PixiSimpleConfigItem("Item1", "123"));
            target.PixiConfigItems.Add(new PixiSimpleConfigItem("Item2", "456"));
            Assert.AreEqual(2, target.PixiConfigItems.Count, "PixiSimplePcConfigurationXml_Properties_Test: Test 1 - 2 ConfigItems added");

            target.FileFolder = "C:\\Folder";
            Assert.AreEqual("C:\\Folder", target.FileFolder, "PixiSimplePcConfigurationXml_Properties_Test: Test 2 - FileFolder");
            Assert.AreEqual("C:\\Folder\\", target.FilePath, "PixiSimplePcConfigurationXml_Properties_Test: Test 2 - FilePath");

            target.FileName = "FNAME";
            Assert.AreEqual("FNAME", target.FileName, "PixiSimplePcConfigurationXml_Properties_Test: Test 3 - FileName");
            Assert.AreEqual("C:\\Folder\\FNAME", target.FilePath, "PixiSimplePcConfigurationXml_Properties_Test: Test 23 - FilePath");

        }
        //<summary>
        //A test for CreateConfig_file
        //</summary>
        [TestMethod]
        public void PixiSimpleConfigurationXml_TC001_CreateConfigfile_UnitTest()
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
                Assert.Fail("PixiSimplePcConfigurationXml_CreateConfigfile: Test 1 - CreateConfigFile, Fail when No Directory/file exists");
            }
            target = null;

        }

        [TestMethod]
        public void PixiSimpleConfigurationXml_TC002_CreateConfigfile_UnitTest()
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
                Assert.Fail("PixiSimplePcConfigurationXml_CreateConfigfile: Test 2 - CreateConfigFile, Fail when Directory exists, File do not exist");
            }
            target = null;
        }


        [TestMethod]
        public void PixiSimpleConfigurationXml_TC003_CreateConfigfile_UnitTest()
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
                Assert.Fail("PixiSimplePcConfigurationXml_CreateConfigfile: Test 3 - CreateConfigFile(Folde,File), Fail when input parms");
            }
            Assert.AreEqual(cTestFolder, target.FileFolder, "PixiSimplePcConfigurationXml_CreateConfigfile: Test 3 - CreateConfigFile(Folder,File), FolderName");
            Assert.AreEqual(cCfgFileName, target.FileName, "PixiSimplePcConfigurationXml_CreateConfigfile: Test 3 - CreateConfigFile(Folder,File), FileName");
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
                Assert.Fail("PixiSimplePcConfigurationXml_CreateConfigfile: Test 4 - CreateConfigFile, Fail when Directory exists, File exist. No exception thrown");
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
        public void PixiSimpleConfigurationXml_TC004_ReadConfigfile__UnitTest()
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
                Assert.AreEqual(0, Rc, "PixiSimplePcConfigurationXml_ReadConfigfile: Test 0 - ReadConfigFile, Fail when No Directory/file exists");
            }
            target = null;
            
            //----------------------------------------------------
            // Test 1 - ReadConfigfile() with directory exists, File exists
            //-----------------------------------------------------
            target = new PixiSimpleConfigurationXml();
            target.PixiConfigItems.Add(new PixiSimpleConfigItem("Item1", "xxx"));
            target.PixiConfigItems.Add(new PixiSimpleConfigItem("Item2", "yyy"));
            target.FileFolder = cTestFolder;
            target.FileName = cCfgFileName;
            try
            {
                target.ReadConfigfile();
            }
            catch
            {
                Assert.Fail("PixiSimplePcConfigurationXml_ReadConfigfile: Test 1 - ReadConfigFile, Fail when Directory exists, File exist");
            }

            Count = 0;
            foreach (PixiSimpleConfigItem obj in target.PixiConfigItems)
            {
                Count = Count + 1;
                switch (Count)
                {
                    case 1:
                        Assert.AreEqual("Item1", obj.ItemName, "PixiSimplePcConfigurationXml_ReadConfigfile: Test 1a - ReadConfigFile, Item.ItemName <> 'Item1'");
                        Assert.AreEqual("123", obj.ItemValue, "PixiSimplePcConfigurationXml_ReadConfigfile: Test 1a - ReadConfigFile, Item.ItemValue <> 123");
                        break;
                    case 2:
                        Assert.AreEqual("Item2", obj.ItemName, "PixiSimplePcConfigurationXml_ReadConfigfile: Test 1a - ReadConfigFile, Item.ItemName <> 'Item2'");
                        Assert.AreEqual("456", obj.ItemValue, "PixiSimplePcConfigurationXml_ReadConfigfile: Test 1a - ReadConfigFile, Item.ItemValue <> '456'");
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
                Assert.Fail("PixiSimplePcConfigurationXml_ReadConfigfile: Test 2 - ReadConfigFile, Fail when Directory exists, File exist");
            }

            Count = 0;
            foreach (PixiSimpleConfigItem obj in target.PixiConfigItems)
            {
                Count = Count + 1;
                switch (Count)
                {
                    case 1:
                        Assert.AreEqual("Item1", obj.ItemName, "PixiSimplePcConfigurationXml_ReadConfigfile: Test 2a - ReadConfigFile, Item.ItemName <> 'Item1'");
                        Assert.AreEqual("123", obj.ItemValue, "PixiSimplePcConfigurationXml_ReadConfigfile: Test 2a - ReadConfigFile, Item.ItemValue <> 123");
                        break;
                    case 2:
                        Assert.AreEqual("Item2", obj.ItemName, "PixiSimplePcConfigurationXml_ReadConfigfile: Test 2a - ReadConfigFile, Item.ItemName <> 'Item2'");
                        Assert.AreEqual("456", obj.ItemValue, "PixiSimplePcConfigurationXml_ReadConfigfile: Test 2a - ReadConfigFile, Item.ItemValue <> '456'");
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
                    Assert.Fail("PixiSimplePcConfigurationXml_ReadConfigfile: Test 3 - ReadConfigFile, Fail when Directory exists, File exist. No exception thrown");
                }
                catch { }
            }
            catch (PixiConfigFileNotExistException) { }
            catch (Exception ex)
            {
                Assert.Fail("PixiSimplePcConfigurationXml_ReadConfigfile: Test 3 - ReadConfigFile, Fail when Directory exists, File exist. Exception=" + ex.Message);
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
                    Assert.Fail("PixiSimplePcConfigurationXml_ReadConfigfile: Test 4 - ReadConfigFile, Fail when Directory not exists, File exist. No exception thrown");
                }
                catch { }
            }
            catch (PixiConfigFileNotExistException) { }
            catch
            {
                Assert.Fail("PixiSimplePcConfigurationXml_ReadConfigfile: Test 4 - ReadConfigFile, Fail when Directory not exists, File exist. Rc <> -3");
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
        public void PixiSimpleConfigurationXml_SaveConfigfile_Test()
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
                Assert.Fail("PixiSimplePcConfigurationXml_CreateConfigfile: Test 0 - ReadConfigFile, Pretest");
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
                Assert.Fail("PixiSimplePcConfigurationXml_ReadConfigfile: Test 1 - SaveConfigFile, Fail when Directory exists, File exist");
            }

            Count = 0;
            foreach (PixiSimpleConfigItem obj in target.PixiConfigItems)
            {
                Count = Count + 1;
                switch (Count)
                {
                    case 1:
                        Assert.AreEqual("Item1", obj.ItemName, "PixiSimplePcConfigurationXml_ReadConfigfile: Test 1a - ReadConfigFile, Item.ItemName <> 'Item1'");
                        Assert.AreEqual(123, obj.ItemValue, "PixiSimplePcConfigurationXml_ReadConfigfile: Test 1a - ReadConfigFile, Item.ItemValue <> 123");
                        break;
                    case 2:
                        Assert.AreEqual("Item2", obj.ItemName, "PixiSimplePcConfigurationXml_ReadConfigfile: Test 1a - ReadConfigFile, Item.ItemName <> 'Item2'");
                        Assert.AreEqual("456", obj.ItemValue, "PixiSimplePcConfigurationXml_ReadConfigfile: Test 1a - ReadConfigFile, Item.ItemValue <> '456'");
                        break;
                    default:
                        break;
                }
            }

        }

        [TestMethod]
        public void PixiSimpleConfigurationXml_TC008_ConfigfileExists_UnitTest()
        {
            PixiSimpleConfigurationXml target;

            bool lExists = false;

            //-------------------------------------------------------------------------
            // Test Case 008 - ConfigFileExists(cTestFolder, cCfgFileName) - With input parms
            //-------------------------------------------------------------------------
            if (Directory.Exists(cTestFolder))
            {
                Directory.Delete(cTestFolder, true);
            }
            // Test Case 008a - config file do not exist.
            target = new PixiSimpleConfigurationXml();
            try
            {
                lExists = target.ConfigFileExists(cTestFolder, cCfgFileName);
                Assert.AreEqual(false, lExists, $"PixiSimpleConfigurationXml_TC008_ConfigfileExists_UnitTest: Test 008a - config file do not exist - ConfigFileExists({cTestFolder}, {cCfgFileName}), returnvalue <> false");
            }
            catch
            {
                Assert.Fail("PixiSimplePcConfigurationXml_CreateConfigfile: Test 008a - CreateConfigFile(Folde,File), Fail when input parms");
            }
            target = null;

            // Test Case 008b - config file do exist.
            target = new PixiSimpleConfigurationXml();

            try
            {
                target.CreateConfigfile(cTestFolder, cCfgFileName);
            }
            catch
            {
                Assert.Fail("PixiSimplePcConfigurationXml_CreateConfigfile: Test 008b - CreateConfigFile(Folde,File), Fail when input parms");
            }


            try
            {
                lExists = target.ConfigFileExists(cTestFolder, cCfgFileName);
                Assert.AreEqual(true, lExists, $"PixiSimpleConfigurationXml_TC008_ConfigfileExists_UnitTest: Test 008b - config file do exist - ConfigFileExists({cTestFolder}, {cCfgFileName}), returnvalue <> true");
            }
            catch
            {
                Assert.Fail("PixiSimplePcConfigurationXml_ConfigFileExists: Test 008b - ConfigFileExists(Folde,File), Fail when input parms");
            }
            target = null;



            if (Directory.Exists(cTestFolder))
            {
                Directory.Delete(cTestFolder, true);
            }
            // Test Case 008c - config file do not exist.
            target = new PixiSimpleConfigurationXml();
            target.FileFolder = cTestFolder;
            target.FileName = cCfgFileName;
            try
            {
                lExists = target.ConfigFileExists();
                Assert.AreEqual(false, lExists, "PixiSimpleConfigurationXml_TC008_ConfigfileExists_UnitTest: Test 008c - config file do not exist - ConfigFileExists({cTestFolder}, {cCfgFileName}), returnvalue <> false");
            }
            catch
            {
                Assert.Fail("PixiSimplePcConfigurationXml_CreateConfigfile: Test 008c - CreateConfigFile(Folde,File), Fail when input parms");
            }
            target = null;

            // Test Case 008d - config file do exist.
            target = new PixiSimpleConfigurationXml();
            target.FileFolder = cTestFolder;
            target.FileName = cCfgFileName;

            try
            {
                target.CreateConfigfile();
            }
            catch
            {
                Assert.Fail("PixiSimplePcConfigurationXml_CreateConfigfile: Test 008d - CreateConfigFile(Folde,File), Fail when input parms");
            }


            try
            {
                lExists = target.ConfigFileExists(cTestFolder, cCfgFileName);
                Assert.AreEqual(true, lExists, "PixiSimpleConfigurationXml_TC008_ConfigfileExists_UnitTest: Test 008d - config file do exist - ConfigFileExists({cTestFolder}, {cCfgFileName}), returnvalue <> true");
            }
            catch
            {
                Assert.Fail("PixiSimplePcConfigurationXml_CreateConfigfile: Test 3 - CreateConfigFile(Folde,File), Fail when input parms");
            }
            target = null;

            // Test cleanUp
            Directory.Delete(cTestFolder, true);
        }


        [TestMethod]
        public void PixiSimpleConfigurationXml_TC009_SetGetItemValue_UnitTest()
        {
            PixiSimpleConfigurationXml target;

            string tItemName1 = "ItemName1";
            string tItemValue1 = "123";
            string tItemName2 = "ItemName2";
            string tItemValue2 = "456";

            string ItemValue = "";

            //-------------------------------------------------------------------------
            // Test Case 009 - SetGetItemValue(ItemName)
            //-------------------------------------------------------------------------
            target = new PixiSimpleConfigurationXml();
            target.PixiConfigItems.Add(new PixiSimpleConfigItem(tItemName1, tItemValue1));
            target.PixiConfigItems.Add(new PixiSimpleConfigItem(tItemName2, tItemValue2));

            // Test Case 009a - GetItemValue ItemName1
            ItemValue = target.GetItemValue(tItemName1);
            Assert.AreEqual(tItemValue1, ItemValue, $"PixiSimpleConfigurationXml_TC009_SetGetItemValue_UnitTest: Test 009a - GetItemValue({tItemName1}, {tItemValue1}) = {tItemValue1}");

            // Test Case 009b - GetItemValue ItemName2
            ItemValue = target.GetItemValue(tItemName2);
            Assert.AreEqual(tItemValue2, ItemValue, $"PixiSimpleConfigurationXml_TC009_SetGetItemValue_UnitTest: Test 009b - GetItemValue({tItemName1}, {tItemValue1}) = {tItemValue1}");

            // Test Case 009c - SetItemValue ItemName1
            tItemValue1 = "Value1A";
            target.SetItemValue(tItemName1, tItemValue1);
            ItemValue = target.GetItemValue(tItemName1);
            Assert.AreEqual(tItemValue1, ItemValue, $"PixiSimpleConfigurationXml_TC009_SetGetItemValue_UnitTest: Test 009c - SetItemValue({tItemName1}, {tItemValue1}) = {tItemValue1})");

            // Test Case 009d - SetItemValue ItemName3 - Not in item collection.
            tItemValue1 = "Value1A";
            try
            {
                target.SetItemValue("ItemName3", tItemValue1);
                Assert.Fail($"PixiSimpleConfigurationXml_TC009_SetGetItemValue_UnitTest: Test 009d - SetItemValue for item not in collection. No exception thrown.");
            }
            catch( ArgumentException)
            {

            }

            ItemValue = target.GetItemValue(tItemName1);
            Assert.AreEqual(tItemValue1, ItemValue, $"PixiSimpleConfigurationXml_TC009_SetGetItemValue_UnitTest: Test 009c - SetItemValue({tItemName1}, {tItemValue1}) = {tItemValue1})");

            target = null;

        }

    }
}
