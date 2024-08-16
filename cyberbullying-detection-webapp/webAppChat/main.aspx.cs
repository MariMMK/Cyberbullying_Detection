using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Net;
using System.Web.UI;
using System.Web.UI.WebControls;
using webAppChat.classes;
using System.IO;
using System.Net.Http;
using System.Security.Policy;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using Newtonsoft.Json;
using System.Reflection;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Web.UI.WebControls.WebParts;

namespace webAppChat
{
    public partial class main : util
    {
        toxicMessage toxicMsg;

        const decimal cntToxLevelHigh = 80;
        const decimal cntToxLevelMild = 50;
        const string cntMsgTox1 = "You sent a toxic message!";
        const string cntMsgWarn = "Some messages may offend people's sensibilities!";

        decimal toxLevelGlobal = 0;
        decimal toxLevelMessage = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["nickname"] == null)
            {
                Response.Redirect("index.aspx");
            }
            else
            {
                setDataSource_Users();
                setDataSource_Chat();
                scrollDown("divgv");
            }
        }

        protected void lnkButSignOff_Click(object sender, EventArgs e)
        {
            bool flagReset = ((DataTable)Application["dtUsers"]).Rows.Count == 1;

            toxicMsg = new toxicMessage(Session["nickname"].ToString());

            if (!toxicMsg.reset(flagReset))
            {
                showMessage("Error reseting user statistics");
            }

            Session.Abandon();

            Response.Redirect("index.aspx");
        }

        protected void gvUsers_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (DataBinder.Eval(e.Row.DataItem, "nickname").ToString() == Session["nickname"].ToString())
                {
                    e.Row.Font.Bold = true;
                }

                if ((Decimal)DataBinder.Eval(e.Row.DataItem, "toxLevel") >= cntToxLevelHigh)
                {
                    e.Row.Cells[3].CssClass = "text-danger";
                }
                else if ((Decimal)DataBinder.Eval(e.Row.DataItem, "toxLevel") >= cntToxLevelMild)
                {
                    e.Row.Cells[3].CssClass = "text-warning";
                }
                else
                {
                    e.Row.Cells[3].CssClass = "text-success";
                }
            }
        }
        protected void gvChat_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if ((Decimal)DataBinder.Eval(e.Row.DataItem, "value") >= cntToxLevelHigh)
                {
                    e.Row.CssClass = "table-danger";
                }
                else if ((Decimal)DataBinder.Eval(e.Row.DataItem, "value") >= cntToxLevelMild)
                {
                    e.Row.CssClass = "table-warning";
                }
                else
                {
                    e.Row.CssClass = "table-success";
                }
            }
        }
        
        protected void btnSend_Click(object sender, EventArgs e)
        {
            string strMessage;

            lblStatus.Text = "";
            lblStatus.CssClass = "";

            if (Page.IsValid)
            {
                strMessage = txtMsg.Text.Replace("\x022", "'").Trim();

                // calls to NLP model
                toxicMsg = new toxicMessage(Session["nickname"].ToString(), strMessage);

                if (toxicMsg.validate())
                {
                    toxLevelGlobal = toxicMsg.resultAllMsgs;
                    toxLevelMessage = toxicMsg.resultThisMsg;

                    if (toxLevelMessage >= cntToxLevelHigh)
                    {
                        lblStatus.Text = cntMsgTox1;
                        lblStatus.CssClass = "lblStatus_banned";
                    }
                    else if (toxLevelMessage >= cntToxLevelMild)
                    {
                        lblStatus.Text = cntMsgWarn;
                        lblStatus.CssClass = "lblStatus_warning";
                    }

                    // adding message to chatroom
                    addMessage(Session["nickname"].ToString() + " says:", strMessage, toxLevelMessage);
                    setDataSource_Chat();
                    scrollDown("divgv");

                    // updating toxicity level of user
                    foreach (DataRow dtRow in ((DataTable)Application["dtUsers"]).Rows)
                    {
                        if (dtRow["sessionId"].ToString() == Session.SessionID)
                        {
                            dtRow["toxLevel"] = toxLevelGlobal;
                            dtRow["toxLevelStr"] = levelBar(toxLevelGlobal);
                            break;
                        }
                    }

                    setDataSource_Users();

                    txtMsg.Text = "";
                    txtMsg.Focus();
                }
                else
                {
                    showMessage("Error invoking service");
                }
            }
            else
            {
                showMessage("Message is required");
            }
        }

        private void addMessage(string nickname, string message, decimal value)
        {
            DataRow rowMsg = ((DataTable)Application["dtChat"]).NewRow();
            rowMsg["nickname"] = nickname;
            rowMsg["message"]  = message;
            rowMsg["value"] = value;
            ((DataTable)Application["dtChat"]).Rows.Add(rowMsg);
        }

        private void setDataSource_Users() 
        {
            gvUsers.DataSource = (DataTable)Application["dtUsers"];
            gvUsers.DataBind();
        }

        private void setDataSource_Chat()
        {
            gvChat.DataSource = (DataTable)Application["dtChat"];
            gvChat.DataBind();
        }

        private string levelBar(Decimal pLevel)
        {
            return new string('|', (int)(20 * (pLevel / 100))) + (char)13 + pLevel.ToString("0.00\\%");
        }

        protected void timeUser_Tick(object sender, EventArgs e)
        {
            setDataSource_Users();
        }

        protected void timeChat_Tick(object sender, EventArgs e)
        {
            setDataSource_Chat();
        }
    }
}