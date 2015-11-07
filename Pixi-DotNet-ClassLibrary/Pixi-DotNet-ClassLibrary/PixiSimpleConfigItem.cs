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

        public string ItemName { get; set; }
        public string ItemValue { get; set; }

        //------------------------------------
        // Class Constructors
        //------------------------------------
        public PixiSimpleConfigItem(string pItemName, string pItemValue)
        {
            ItemName = pItemName;
            ItemValue = pItemValue;
        }
    }
}
