using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Pixi.Configuration;

namespace SampleConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {

            string CfgFolderName = "C:\\PixiTestFolder";
            string CopyFolder = "C:\\PixiTestCopyFolder";
            string CfgFileName = "CONFIGTEST.XML";


            PixiSimpleConfigurationXml SamplePixiConfig = new PixiSimpleConfigurationXml();

            SamplePixiConfig.FileName = CfgFileName;
            SamplePixiConfig.FileFolder = CfgFolderName;
            SamplePixiConfig.PixiConfigItems.Add(new PixiSimpleConfigItem("Item1", "123"));
            SamplePixiConfig.PixiConfigItems.Add(new PixiSimpleConfigItem("Item2", "456"));

            if (Directory.Exists(SamplePixiConfig.FileFolder)    )
            {
                Directory.Delete(SamplePixiConfig.FileFolder,true);
            }

            SamplePixiConfig.CreateConfigfile();

            SamplePixiConfig.ReadConfigfile();

            foreach ( PixiSimpleConfigItem xx in SamplePixiConfig.PixiConfigItems)
            {
                Console.WriteLine($"Item: {xx.ItemName}={xx.ItemValue}");
                
            }
            Console.WriteLine("Press any key");
            Console.ReadLine();
            File.Delete(SamplePixiConfig.FilePath);
        }
    }
}
