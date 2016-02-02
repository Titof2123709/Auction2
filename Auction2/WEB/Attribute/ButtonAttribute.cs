using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WEB.Attribute
{
    public class ButtonAttribute : ActionMethodSelectorAttribute
    {
        public string ButtonName { get; set; }
        public ButtonAttribute(string buttonName)
        {
            ButtonName = buttonName;
        }
        public override bool IsValidForRequest(ControllerContext controllerContext, System.Reflection.MethodInfo methodInfo)
        {
           var x = controllerContext.Controller.ValueProvider.GetValue(ButtonName);
          return x!=null;
        }

    }
}