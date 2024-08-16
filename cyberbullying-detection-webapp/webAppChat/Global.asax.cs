using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI;

namespace webAppChat
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            Application["dtUsers"] = new DataTable();
            ((DataTable)Application["dtUsers"]).Columns.Add("sessionId",   System.Type.GetType("System.String"));
            ((DataTable)Application["dtUsers"]).Columns.Add("nickname",    System.Type.GetType("System.String"));
            ((DataTable)Application["dtUsers"]).Columns.Add("toxLevel",    System.Type.GetType("System.Decimal"));
            ((DataTable)Application["dtUsers"]).Columns.Add("toxLevelStr", System.Type.GetType("System.String"));

            Application["dtChat"] = new DataTable();
            ((DataTable)Application["dtChat"]).Columns.Add("nickname", System.Type.GetType("System.String"));
            ((DataTable)Application["dtChat"]).Columns.Add("message",  System.Type.GetType("System.String"));
            ((DataTable)Application["dtChat"]).Columns.Add("value",    System.Type.GetType("System.Decimal"));
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            //
        }
        protected void Session_End(object sender, EventArgs e)
        {
            foreach (DataRow dtRow in ((DataTable)Application["dtUsers"]).Rows)
            {
                if (dtRow["sessionId"].ToString() == Session.SessionID)
                { 
                    dtRow.Delete();
                    break;
                }
            }
        }

        protected void Application_End(object sender, EventArgs e)
        {
            Application["dtUsers"] = null;
            Application["dtChat"]  = null;
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            //
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            //
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            //
        }

    }
}