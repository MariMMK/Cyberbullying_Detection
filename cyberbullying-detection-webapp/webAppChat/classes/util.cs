using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Web;
using System.Xml.Linq;

namespace webAppChat.classes
{
    public class util : System.Web.UI.Page
    {
        public void showMessage(string pMessage)
        {
            Response.Write("<Script Language='JavaScript'>parent.alert('" + pMessage + "');</Script>");
        }

        internal void scrollDown(string pControlID) 
        {
            StringBuilder sbScript = new StringBuilder();
            sbScript.Append("<script type = 'text/javascript'>");
            sbScript.Append(string.Format("document.getElementById('{0}').scrollTop += 1000000", pControlID));
            sbScript.Append("</script>");

            Page.ClientScript.RegisterStartupScript(Page.GetType(), "scrollDown", sbScript.ToString(), false);
        }
    }
}