using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using webAppChat.classes;

namespace webAppChat
{
    public partial class index : util
    {
        const string cntMsgFull = "Chat room is full!";
        const string cntMsgNickReq = "Nickname is required!";
        const string cntMsgNickUsed = "Nickname has already been used!";
        const int cntMaxUsers = 7;
        string nickName;

        protected void Page_Load(object sender, EventArgs e)
        {
            //
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            bool flagMain = false;

            lblStatus.Text = string.Empty;

            if (Page.IsValid)
            {
                if (validateConnected())
                {
                    switch (validateSession())
                    {
                        case 0:
                            addSession();
                            flagMain = true;
                            break;
                        case 1:
                            flagMain = true;
                            break;
                        case 2:
                            lblStatus.Text = cntMsgNickUsed;
                            break;
                    }

                    if (flagMain)
                    {
                        Response.Redirect("main.aspx");
                    }
                }
                else 
                {
                    lblStatus.Text = cntMsgFull;
                }
            }
            else 
            {
                lblStatus.Text = cntMsgNickReq;
            }
        }

        private void addSession()
        {
            bool lExists = false;

            // registering nickname in session
            if (Session["nickname"] != null)
            {
                Session["nickname"] = nickName;
            }
            else
            {
                Session.Add("nickname", nickName);
            }

            // updating users table
            foreach (DataRow dtRow in ((DataTable)Application["dtUsers"]).Rows)
            {
                if (dtRow["sessionId"].ToString() == Session.SessionID)
                {
                    lExists = true;
                    dtRow["nickname"] = nickName;
                    break;
                }
            }

            // adding user if doesn't exist
            if (!lExists)
            {
                DataRow dtRow = ((DataTable)Application["dtUsers"]).NewRow();
                dtRow["sessionId"] = Session.SessionID;
                dtRow["nickname"] = nickName;
                dtRow["toxLevel"] = 0;
                dtRow["toxLevelStr"] = "";
                ((DataTable)Application["dtUsers"]).Rows.Add(dtRow);
            }

        }

        private bool validateConnected()
        {
           return !(((DataTable)Application["dtUsers"]).Rows.Count >= cntMaxUsers);
        }

        private int validateSession()
        {
            int valReturn = 0;

            nickName = txtNickname.Text.ToLower().Trim();

            foreach (DataRow dtRow in ((DataTable)Application["dtUsers"]).Rows)
            {
                if (dtRow["nickname"].ToString() == nickName)
                {
                    if (dtRow["sessionId"].ToString() == Session.SessionID)
                    {
                        valReturn = 1; // do nothing
                    }
                    else
                    {
                        valReturn = 2; // Nickname has already been used!
                    }
                    break;
                }
            }

            return valReturn;
        }

    }
}