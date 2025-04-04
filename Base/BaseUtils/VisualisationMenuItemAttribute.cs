using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Base.BaseUtils
{
    [AttributeUsage(AttributeTargets.Field)]
    public class VisualisationMenuItemAttribute:Attribute
    {
        public VisualisationMenuItemAttribute() { }

        public VisualisationMenuItemAttribute(string strMainMenuId, string strMenuId, string strMenuTitle, string strMnuActionType, string strMenuActionMethodName)
        {
            MainMenuId = strMainMenuId;
            MenuId = strMenuId;
            MenuTitle = strMenuTitle;
            MenuActionType = strMnuActionType;
            MenuActionMethodName = strMenuActionMethodName;
        }
        public string MainMenuId { get; set; }

        public string MenuId { get; set; }

        public string MenuTitle { get; set; }

        public string MenuActionType { get; set; }

        public string MenuActionMethodName { get; set; }
    }
}
