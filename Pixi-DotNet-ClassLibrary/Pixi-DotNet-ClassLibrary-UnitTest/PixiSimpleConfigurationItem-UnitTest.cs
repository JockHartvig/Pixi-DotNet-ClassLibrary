using System;
using System.IO;
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



		//<summary>
		//A test for CreateConfig_file
		//</summary>
		[TestMethod]
		public void PixiSimpleConfiguration_CreateConfigfile_Test()
		{
			string strTestFolder = "C:\\PixiTestFolder";
			string strCfgFileName = "CONFIGTEST.XML";

			int Rc = 0;
			PixiSimpleConfigurationXml target;

			//-----------------------------------------------------
			// Test 1 - Create with directory/file do not exists
			//-----------------------------------------------------
			if (Directory.Exists(strTestFolder))
			{
				Directory.Delete(strTestFolder, true);
			}

			target = new PixiSimpleConfigurationXml();
			target.PixiConfigItems.Add(new PixiSimpleConfigItem("Item1", "123"));
			target.PixiConfigItems.Add(new PixiSimpleConfigItem("Item2", "456"));
			target.FileFolder = strTestFolder;
			target.FileName = strCfgFileName;
			Rc = target.CreateConfigfile();
			Assert.AreEqual(0, Rc, "PixiSimplePcConfiguration_CreateConfigfile: Test 1 - CreateConfigFile, Fail when No Directory/file exists");
			target = null;

			//-----------------------------------------------------
			// Test 2 - Create with directory exists, File not exists
			//-----------------------------------------------------
			if (Directory.Exists(strTestFolder))
			{
				if (File.Exists(strTestFolder + "\\" + strCfgFileName))
				{
					File.Delete(strTestFolder + "\\" + strCfgFileName);
				}
			}
			target = new PixiSimpleConfigurationXml();
			target.PixiConfigItems.Add(new PixiSimpleConfigItem("Item1", "123"));
			target.PixiConfigItems.Add(new PixiSimpleConfigItem("Item2", "456"));
			target.FileFolder = strTestFolder;
			target.FileName = strCfgFileName;
			Rc = target.CreateConfigfile();
			Assert.AreEqual(0, Rc, "PixiSimplePcConfiguration_CreateConfigfile: Test 2 - CreateConfigFile, Fail when Directory exists, File do not exist");
			target = null;

			//-----------------------------------------------------
			// Test 3 - Create with input parms
			//-----------------------------------------------------
			if (Directory.Exists(strTestFolder))
			{
				Directory.Delete(strTestFolder, true);
			}
			target = new PixiSimpleConfigurationXml();
			target.PixiConfigItems.Add(new PixiSimpleConfigItem("Item1", "123"));
			target.PixiConfigItems.Add(new PixiSimpleConfigItem("Item2", "456"));
			Rc = target.CreateConfigfile(strTestFolder, strCfgFileName);
			Assert.AreEqual(0, Rc, "PixiSimplePcConfiguration_CreateConfigfile: Test 3 - CreateConfigFile(Folde,File), Fail when input parms");
			Assert.AreEqual(strTestFolder, target.FileFolder, "PixiSimplePcConfiguration_CreateConfigfile: Test 3 - CreateConfigFile(Folde,File), FolderName");
			Assert.AreEqual(strCfgFileName, target.FileName, "PixiSimplePcConfiguration_CreateConfigfile: Test 3 - CreateConfigFile(Folde,File), FileName");
			target = null;

			//-----------------------------------------------------
			// Test 4 - Create with directory exists, File  exists
			//-----------------------------------------------------
			target = new PixiSimpleConfigurationXml();
			target.PixiConfigItems.Add(new PixiSimpleConfigItem("Item1", "123"));
			target.PixiConfigItems.Add(new PixiSimpleConfigItem("Item2", "456"));
			target.FileFolder = strTestFolder;
			target.FileName = strCfgFileName;
			Rc = target.CreateConfigfile();
			Assert.AreEqual(-2, Rc, "PixiSimplePcConfiguration_CreateConfigfile: Test 4 - CreateConfigFile, Fail when Directory exists, File exist. Rc <> -2");
			target = null;

			// Test cleanUp
			Directory.Delete(strTestFolder, true);
		}

		//<summary>
		//A test for ReadConfigfile
		//</summary>
		[TestMethod]
		public void PixiSimpleConfiguration_ReadConfigfile_Test()
		{
			string strTestFolder = "C:\\PixiTestFolder";
			string strCfgFileName = "CONFIGTEST.XML";

			int Rc = 0;
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
			Rc = target.CreateConfigfile(strTestFolder, strCfgFileName);
			Assert.AreEqual(0, Rc, "PixiSimplePcConfiguration_ReadConfigfile: Test 0 - ReadConfigFile, Fail when No Directory/file exists");
			target = null;

			//----------------------------------------------------
			// Test 1 - read with directory exists, File exists
			//-----------------------------------------------------
			target = new PixiSimpleConfigurationXml();
			target.FileFolder = strTestFolder;
			target.FileName = strCfgFileName;
			Rc = target.ReadConfigfile();
			Assert.AreEqual(0, Rc, "PixiSimplePcConfiguration_ReadConfigfile: Test 1 - ReadConfigFile, Fail when Directory exists, File exist");

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
			Rc = target.ReadConfigfile(strTestFolder, strCfgFileName);
			Assert.AreEqual(0, Rc, "PixiSimplePcConfiguration_ReadConfigfile: Test 1 - ReadConfigFile, Fail when Directory exists, File exist");

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
			File.Delete(strTestFolder + "\\" + strCfgFileName);

			target = new PixiSimpleConfigurationXml();
			target.FileFolder = strTestFolder;
			target.FileName = strCfgFileName;
			Rc = target.ReadConfigfile();
			Assert.AreEqual(-3, Rc, "PixiSimplePcConfiguration_ReadConfigfile: Test 3 - ReadConfigFile, Fail when Directory exists, File exist. Rc <> -3");

			//-----------------------------------------------------
			// Test 4 - Read with directory not exists, File not exists
			//-----------------------------------------------------

			target = new PixiSimpleConfigurationXml();
			target.FileFolder = strTestFolder;
			target.FileName = strCfgFileName;
			Rc = target.ReadConfigfile();
			Assert.AreEqual(-3, Rc, "PixiSimplePcConfiguration_ReadConfigfile: Test 4 - ReadConfigFile, Fail when Directory not exists, File exist. Rc <> -3");

			// Test cleanUp
			Directory.Delete(strTestFolder, true);
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
			int Rc = 0;
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
			Rc = target.CreateConfigfile();
			Assert.AreEqual(0, Rc, "PixiSimplePcConfiguration_CreateConfigfile: Test 0 - ReadConfigFile, Pretest");
			target = null;

			//-----------------------------------------------------
			// Test 1 - Save with directory exists, File exists
			//-----------------------------------------------------
			target = new PixiSimpleConfigurationXml();
			target.FileFolder = strTestFolder;
			target.FileName = strCfgFileName;
			Rc = target.SaveConfigfile();
			Assert.AreEqual(0, Rc, "PixiSimplePcConfiguration_ReadConfigfile: Test 1 - SaveConfigFile, Fail when Directory exists, File exist");

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
