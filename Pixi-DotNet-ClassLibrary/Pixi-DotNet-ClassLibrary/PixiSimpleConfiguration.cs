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
    //  Class PixiSimpleConfigItem
    //---------------------------------------------
    public class PixiSimpleConfigItem
    {
        //------------------------------------
        // Class Properties
        //------------------------------------
        private string _ItemName = "";
        private string _ItemValue = "";


        public String ItemName
        {
            get { return _ItemName; }
            set { _ItemName = value; }
        }

        public string ItemValue
        {
            get { return _ItemValue; }
            set { _ItemValue = value; }
        }

        //------------------------------------
        // Class Constructors
        //------------------------------------
        public PixiSimpleConfigItem(string pItemName, string pItemValue)
        {
            ItemName = pItemName;
            ItemValue = pItemValue;
        }

    }

    //---------------------------------------------
    //  Class PixiSimpleConfiguration
    //---------------------------------------------
    public class PixiSimpleConfiguration
    {
        //------------------------------------
        // Class Properties
        //------------------------------------
        private string _FileName = "";
        private string _FileFolder = "";
        private string _FilePath = "";

        public string FileFolder
        {
            get { return _FileFolder; }
            set {
                _FileFolder = value;
                _FilePath = _FileFolder + "x" + _FileName;
            }
        }

        public string FileName
        {
            get { return _FileName; }
            set {
                _FileName = value;
                _FilePath = _FileFolder + "\\" + _FileName;
            }
        }

        public string FilePath
        {
            get { return _FilePath; }
        }

        public List<PixiSimpleConfigItem> PixiConfigItems = new List<PixiSimpleConfigItem>();

        //------------------------------------
        // Class Constructors
        //------------------------------------
        public PixiSimpleConfiguration()
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
            if (_FileName == "" | _FileFolder == "")
            {
                return -9;
            }

            // Call public Function that have input parameters
            Rc = CreateConfigfile(_FileFolder, _FileName);

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
            //if (System.IO.File.exist(strFilePath) == false)
            //{
            //    // Check if Folder exists.
            //    if (FileIO.FileSystem.DirectoryExists(pFileFolder) = false)
            //    {
            //        // Create Folder if it does not exist.
            //        FileIO.FileSystem.CreateDirectory(pFileFolder);
            //    }

            //    // Assign to public Properties class variables
            //    _FileFolder = pFileFolder;
            //    _FileName = pFileName;
            //    _FilePath = strFilePath;

            //    //declare our xml writersettings object.                        
            //    private XmlWriterSettings xmlSettings = new XmlWriterSettings();
            //    // xmlwritersettings use indention for the xml.                        
            //    xmlSettings.indent = true;
            //    // Create the .xml document.                        
            //    private XmlWriter XmlWrt = XmlWriter.Create(strFilePath, xmlSettings)
            //    // Write the Xml declaration.                                
            //    XmlWrt.WriteStartDocument();
            //    // Write a comment.                                
            //    XmlWrt.WriteComment("XML - Pixi Simple Configuration file.");
            //    // Write the root element.                                
            //    XmlWrt.WriteStartElement("Settings");
            //    foreach (PixiConfigItem In ConfigItems)
            //    {
            //        XmlWrt.WriteStartElement(PixiConfigItem.ItemName);
            //        XmlWrt.WriteString(PixiConfigItem.ItemValue.ToString);
            //        XmlWrt.WriteEndElement();
            //    }
            //    // Close the XmlTextWriter.                                
            //    XmlWrtWriteEndDocument();
            //    XmlWrt.Close();
            //else
            //    return -2;
            //}

            return Rc;
        }

        public int ReadConfigfile()
        {
            // Rc:
            //   -3 - Configuration file do not exist
            //   -9 - FilePath or FileFolder not filled with a value
            int Rc = 0;

            // Check if FileName and FileFolder is filled with a value.
            if (_FileName == "" | _FileFolder == "")
            {
                return -9;
            }

            // Call public Function that have input parameters
            Rc = ReadConfigfile(_FileFolder, _FileName);

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
                // declare  xmlwritersettings object.                        
                XmlWriterSettings xmlSettings = new XmlWriterSettings();
                // xmlwritersettings use indention for the xml.                        
                xmlSettings.Indent = true;
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
            else
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
            if (_FileName == "" | _FileFolder == "")
            {
                return -9;
            }

            FilePath = _FileFolder + "\\" + _FileName;
            Rc = SaveConfigfile(FileFolder, _FileName);
    
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

            _FileFolder = pFileFolder;
            _FileName = pFileName;
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
            else
                return -3;
            }

            return 0;
        }
    } // End  public class PixiSimpleConfiguration 
}  // End Namespace
