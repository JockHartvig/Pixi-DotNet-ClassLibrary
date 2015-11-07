using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Pixi.Configuration
{

    //---------------------------------------------
    //  Class PixiSimpleConfigurationXml
    //---------------------------------------------
    public class PixiSimpleConfigurationXml
    {
        //------------------------------------
        // Class Properties
        //------------------------------------

        private string mFileFolder = "";
        private string mFileName = "";
        public string FileFolder
        {
            get { return mFileFolder; }
            set
            {
              mFileFolder = value;
             FilePath = mFileFolder + "x" + mFileName;
            }
        }
        public string FileName
        {
            get { return mFileName; }
            set {
                mFileName = value;
                FilePath = mFileFolder + "\\" + mFileName;
            }
        }
        public string FilePath { get; set; } = "";
        public List<PixiSimpleConfigItem> PixiConfigItems = new List<PixiSimpleConfigItem>();

        //------------------------------------
        // Class Constructors
        //------------------------------------
        public PixiSimpleConfigurationXml()
        {
        }

        //--------------------------------------------------------
        // Class public Methods
        // Rc:
        //   -1 - ItemName not found in ConfigItems collection
        //   -2 - Config file allready exists
        //   -3 - Config file not found
        //   -9 - FilePath or FileFolder not filled with a value
        //--------------------------------------------------------

        public object GetItemValue(string pItemName)
        {
            // Loop through the Class public Property ConfigItems Collection.
            foreach (var obj in PixiConfigItems)
            {
                if (obj.ItemName == pItemName)
                {
                    return obj.ItemValue;
                }
            }
            return null;
        }

        public int SetItemValue(string pItemName, string pItemValue)
        {
            // Loop through the Class public Property ConfigItems Collection.
            foreach (PixiSimpleConfigItem obj in PixiConfigItems)
            {
                if (obj.ItemName == pItemName)
                {
                    obj.ItemValue = pItemValue;
                    return 0;
                }
            }
            return -1;
        }

        public int CreateConfigfile()
        {
            // returns Rc:
            //   -2 - Configuration file allready exists
            //   -9 - FilePath or FileFolder not filled with a value
            int Rc = 0;

            // Check if FileName and FileFolder is filled with a value.
            if (FileName == "" | FileFolder == "")
            {
                return -9;
            }

            // Call public Function that have input parameters
            Rc = CreateConfigfile(FileFolder, FileName);

            return Rc;
        }

        public int CreateConfigfile(string pFileFolder, string pFileName)
        {
            // Rc:
            //   -2 - Configuration file allready exists
            //   -9 - FilePath or FileFolder not filled with a value
            int Rc = 0;
            string strFilePath = "";

            // Check if FileName and FileFolder is filled with a value.
            if (pFileName == "" | pFileFolder == "")
            {
                return -9;
            }

            strFilePath = pFileFolder + "\\" + pFileName;

            // Check if the file exists.                
            // if exists return Rc -2.                
            if (System.IO.File.Exists(strFilePath) == false)
            {
                // Check if Folder exists.
                if (System.IO.Directory.Exists(pFileFolder) == false)
                {
                    // Create Folder if it does not exist.
                    Directory.CreateDirectory(pFileFolder);
                }

                // Assign to public Properties class variables
                FileFolder = pFileFolder;
                FileName = pFileName;
                FilePath = strFilePath;

                //declare our xml writersettings object.                        
                XmlWriterSettings xmlSettings = new XmlWriterSettings();
                // xmlwritersettings use indention for the xml.                        
                xmlSettings.Indent = true;
                // Create the .xml document.                        
                XmlWriter XmlWrt = XmlWriter.Create(strFilePath, xmlSettings);
                // Write the Xml declaration.                                
                XmlWrt.WriteStartDocument();
                // Write a comment.                                
                XmlWrt.WriteComment("XML - Pixi Simple Configuration file.");
                // Write the root element.                                
                XmlWrt.WriteStartElement("Settings");
                foreach (PixiSimpleConfigItem PixiConfigItem in PixiConfigItems)
                {
                    XmlWrt.WriteStartElement(PixiConfigItem.ItemName);
                    XmlWrt.WriteString(PixiConfigItem.ItemValue);
                    XmlWrt.WriteEndElement();
                }
                // Close the XmlTextWriter.                                
                XmlWrt.WriteEndDocument();
                XmlWrt.Close();
            }
            else
            {
                return -2;
            }
            return Rc;
        }

        public int ReadConfigfile()
        {
            // Rc:
            //   -3 - Configuration file do not exist
            //   -9 - FilePath or FileFolder not filled with a value
            int Rc = 0;

            // Check if FileName and FileFolder is filled with a value.
            if (FileName == "" | FileFolder == "")
            {
                return -9;
            }

            // Call public Function that have input parameters
            Rc = ReadConfigfile(FileFolder, FileName);

            return Rc;
        }
        public int ReadConfigfile(string pFileFolder, string pFileName)
        {
            // Rc:
            //   -3 - Configuration file do not exist
            //   -9 - FilePath or FileFolder not filled with a value
            int Rc = 0;
            string strFilePath = "";

            // Check if FileName and FileFolder is filled with a value.
            if (pFileName == "" | pFileFolder == "")
            {
                return -9;
            }

            strFilePath = pFileFolder + "\\" + pFileName;

            // Check if the file exists.                
            // if Not return Rc -1.                
            if (System.IO.File.Exists(strFilePath))
            {
                // Declare the XML document reader
                XmlReader Document = new XmlTextReader(strFilePath);
                while (Document.Read())
                {
                    // NodeType = Document.NodeType;
                    if (Document.NodeType == XmlNodeType.Element)
                    {
                        foreach (PixiSimpleConfigItem PixiConfigItem in PixiConfigItems)
                        {
                            if (Document.Name == PixiConfigItem.ItemName)
                            {
                                PixiConfigItem.ItemValue = Document.Value;
                            }
                        }
                    }
                }
                Document.Close();
            }
            else
            {
                return -3;
            }
            return Rc;
        }

        public int SaveConfigfile()
        {
            // Rc:
            //   -3 - Configuration file do not exist
            //   -9 - FilePath or FileFolder not filled with a value
            int Rc = 0;
            string FilePath = "";

            // Check if FileName and FileFolder is filled with a value.
            if (FileName == "" | FileFolder == "")
            {
                return -9;
            }

            FilePath = FileFolder + "\\" + FileName;
            Rc = SaveConfigfile(FileFolder, FileName);
    
            return Rc;
        }

        public int SaveConfigfile(string pFileFolder, string pFileName)
        {
            // Rc:
            //   -3 - Configuration file do not exist
            //   -9 - FilePath or FileFolder not filled with a value
            string strFilePath = "";

            // Check if FileName and FileFolder is filled with a value.
            if (pFileName == "" | pFileFolder == "")
            {
                return -9;
            }

            FileFolder = pFileFolder;
            FileName = pFileName;
            strFilePath = pFileFolder + "\\" + pFileName;

            // Check if the file exists.                
            // if Not return Rc -1.                
            if (System.IO.File.Exists(strFilePath) == true)
            {
                // declare  xmlwritersettings object.                        
                XmlWriterSettings xmlSettings = new XmlWriterSettings();
                // xmlwritersettings use indention for the xml.                        
                xmlSettings.Indent = true;
                // Create the .xml document.                        
                XmlWriter XmlWrt = XmlWriter.Create(strFilePath, xmlSettings);
                // Write the Xml declaration.                                
                XmlWrt.WriteStartDocument();
                // Write a comment.                                
                XmlWrt.WriteComment("XML Database.");
                // Write the root element.                                
                XmlWrt.WriteStartElement("Settings");
                // Write the Config Items
                foreach (PixiSimpleConfigItem PixiConfigItem in PixiConfigItems)
                {
                    XmlWrt.WriteStartElement(PixiConfigItem.ItemName);
                    XmlWrt.WriteString(PixiConfigItem.ItemValue);
                    XmlWrt.WriteEndElement();
                }
                // Close the XmlTextWriter.                                
                XmlWrt.WriteEndDocument();
                XmlWrt.Close();
            }
            else
            { 
                return -3;
            }

            return 0;
        }
    } // End  public class PixiSimpleConfiguration 
}  // End Namespace
