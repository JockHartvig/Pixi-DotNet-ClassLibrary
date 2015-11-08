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
            throw new System.ArgumentException("Property '" + pItemName + "' not found in config file");
        }

        public void CreateConfigfile()
        {
            // returns Rc:
            //   PixiConfigFileAllreadyExistException - Configuration file allready exists
            //   System.ArgumentException - FilePath or FileFolder not filled with a value

            // Check if FileName and FileFolder is filled with a value.
            if (FileName == "" | FileFolder == "")
            {
                throw new System.ArgumentException("Properties FileFolder and FileName cannot be blank when creating config file");
            }

            try
            {
                // Call public Function that have input parameters
                CreateConfigfile(FileFolder, FileName);
            }
            catch (System.ArgumentException ex)
            {
                throw new System.ArgumentException("Parameters FileFolder and FileName cannot be blank when creating config file", ex);
            }
            catch (PixiConfigFileAllreadyExistException ex)
            {
                throw new PixiConfigFileAllreadyExistException("Configuration file allready exists", ex);
            }
        }

        public void CreateConfigfile(string pFileFolder, string pFileName)
        {
            // Exceptions:
            //   PixiConfigFileAllreadyExistException - Configuration file allready exists
            //   System.ArgumentException - FilePath or FileFolder not filled with a value
            string strFilePath = "";

            // Check if FileName and FileFolder is filled with a value.
            if (pFileName == "" | pFileFolder == "")
            {
                throw new System.ArgumentException("Parameters FileFolder and FileName cannot be blank");
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
                throw new PixiConfigFileAllreadyExistException("Configuration file allready exists");
            }
        }

        public void ReadConfigfile()
        {
            // Exceptions:
            //   PixiConfigFileNotExistException - Configuration file do not exist
            //   System.ArgumentException - Properties FileFolder and FileName cannot be blank when reading config file

            // Check if FileName and FileFolder is filled with a value.
            if (FileName == "" | FileFolder == "")
            {
                throw new System.ArgumentException("Properties FileFolder and FileName cannot be blank when reading config file");
            }

            // Call public Function that have input parameters
            try
            {
                ReadConfigfile(FileFolder, FileName);
            }
            catch (System.ArgumentException ex)
            {
                throw new System.ArgumentException("Parameters FileFolder and FileName cannot be blank when reading config file", ex);
            }

        }

        public void ReadConfigfile(string pFileFolder, string pFileName)
        {
            // Exceptions:
            //   PixiConfigFileNotExistException - Configuration file do not exist
            //   System.ArgumentException - Parameters FileFolder and FileName cannot be blank
            string strFilePath = "";

            // Check if FileName and FileFolder is filled with a value.
            if (pFileName == "" | pFileFolder == "")
            {
                throw new System.ArgumentException("Parameters FileFolder and FileName cannot be blank");
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
                throw new PixiConfigFileNotExistException("Configuration file do not exists");
            }
        }

        public void SaveConfigfile()
        {
            // Exceptions:
            //   System.PixiConfigFileNotExistException - Configuration file do not exist
            //   System.ArgumentException - FilePath or FileFolder not filled with a value
            string FilePath = "";

            // Check if FileName and FileFolder is filled with a value.
            if (FileName == "" | FileFolder == "")
            {
                throw new System.ArgumentException("Parameters FileFolder and FileName cannot be blank");
            }

            try
            {
                FilePath = FileFolder + "\\" + FileName;
                SaveConfigfile(FileFolder, FileName);
            }
            catch (System.ArgumentException ex)
            {
                throw new System.ArgumentException("Parameters FileFolder and FileName cannot be blank when save config file", ex);
            }
        }

        public void SaveConfigfile(string pFileFolder, string pFileName)
        {
            // Exceptions:
            //   PixiConfigFileNotExistException - Configuration file do not exist
            //   System.ArgumentException - FilePath or FileFolder not filled with a value
            string strFilePath = "";

            // Check if FileName and FileFolder is filled with a value.
            if (pFileName == "" | pFileFolder == "")
            {
                throw new System.ArgumentException("Parameters FileFolder and FileName cannot be blank");
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
                throw new PixiConfigFileNotExistException("Configuration file do not exists");
            }

        }
    } // End  public class PixiSimpleConfiguration 

    [Serializable()]
    public class PixiConfigFileAllreadyExistException : System.Exception
    {
        public PixiConfigFileAllreadyExistException() : base() { }
        public PixiConfigFileAllreadyExistException(string message) : base(message) { }
        public PixiConfigFileAllreadyExistException(string message, System.Exception inner) : base(message, inner) { }

        // A constructor is needed for serialization when an
        // exception propagates from a remoting server to the client. 
        protected PixiConfigFileAllreadyExistException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context)
        { }
    }

    [Serializable()]
    public class PixiConfigFileNotExistException : System.Exception
    {
        public PixiConfigFileNotExistException() : base() { }
        public PixiConfigFileNotExistException(string message) : base(message) { }
        public PixiConfigFileNotExistException(string message, System.Exception inner) : base(message, inner) { }

        // A constructor is needed for serialization when an
        // exception propagates from a remoting server to the client. 
        protected PixiConfigFileNotExistException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context)
        { }
    }

}  // End Namespace
