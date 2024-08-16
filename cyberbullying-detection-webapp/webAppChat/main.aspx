<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="main.aspx.cs" Inherits="webAppChat.main" Async="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <meta charset="UTF-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <!-- <meta http-equiv="refresh" content="10" /> -->

    <title>Chat room</title>

    <link rel = "stylesheet" href = "css/main.css" />
    <link rel         = "stylesheet"
          href        = "https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css"
          integrity   = "sha384-T3c6CoIi6uLrA9TneNEoa7RxnatzjcDSCmG1MXxSR1GAsXEV/Dwwykc2MPK8M2HN"
          crossorigin = "anonymous" /> 
</head>
<body>
    <form id="form1" runat="server">
       <asp:ScriptManager ID="ScriptManager1" runat="server">
       </asp:ScriptManager>

       <nav class = "navbar navbar-expand-lg navbar-primary mt-3 mb-3">
          <div class = "container">
              <img src = "img/lambton.png" />
              <div class = "navbar-nav ms-auto">
                  <asp:LinkButton ID       = "lnkButSignOff"
                                  runat    = "server"
                                  CssClass = "btn-link"
                                  OnClick  = "lnkButSignOff_Click">Sign-Off
                  </asp:LinkButton>
              </div>
          </div>
       </nav>

       <div class = "container mb-2">
          <div class = "row">
             <div class = "col-xs-3 col-sm-3 col-md-3 col-lg-3 col-xl-3 py-2">
                 <h5 class = "textOverBlue">Connected users</h5>
                 <div class = "bg-white"
                      style = "height:570px;">
                     <asp:UpdatePanel ID    = "upUsers"
                                      runat = "server">
                         <ContentTemplate>
                             <asp:Timer ID       = "timeUser"
                                        runat    = "server"
                                        Interval = "1000"
                                        OnTick   = "timeUser_Tick">
                             </asp:Timer>
                             <asp:GridView ID                   = "gvUsers"
                                           runat                = "server"
                                           AutoGenerateColumns  = "False"
                                           OnRowDataBound       = "gvUsers_RowDataBound"
                                           CssClass             = "table table-condensed table-responsive">
                                 <Columns>
                                     <asp:BoundField DataField  = "sessionId"
                                                     HeaderText = "Session ID"
                                                     ReadOnly   = "True"
                                                     Visible    = "False">
                                     </asp:BoundField>
                                     <asp:BoundField DataField  = "nickname"
                                                     HeaderText = "Nickname"
                                                     ReadOnly   = "True">
                                     <ItemStyle CssClass = "column_usr_nick"/>
                                     </asp:BoundField>
                                     <asp:BoundField DataField  = "toxLevel"
                                                     HeaderText = "toxLevel"
                                                     ReadOnly   = "True"
                                                     Visible    = "False">
                                     </asp:BoundField>
                                     <asp:BoundField DataField  = "toxLevelStr"
                                                     HeaderText = "Toxicity level" 
                                                     ReadOnly   = "True">
                                     <HeaderStyle CssClass = "text-start" />
                                     <ItemStyle   CssClass = "column_usr_level" />
                                     </asp:BoundField>
                                 </Columns>
                             </asp:GridView>
                         </ContentTemplate>
                     </asp:UpdatePanel>
                 </div>
             </div>
             <div class = "col-xs-9 col-sm-9 col-md-9 col-lg-9 col-xl-9 py-2">
                 <h5 class = "textOverBlue">Chat room</h5>

                  <div id    = "divgv"
                       class = "bg-white"
                       style = "height:400px;overflow-y:scroll;">
 
                     <asp:UpdatePanel ID    = "upChat"
                                      runat = "server">
                         <ContentTemplate>
                             <asp:Timer ID       = "timeChat"
                                        runat    = "server"
                                        Interval = "1000"
                                        OnTick   = "timeChat_Tick">
                             </asp:Timer>
                             <asp:GridView ID                  = "gvChat"
                                           runat               = "server"
                                           AutoGenerateColumns = "False"
                                           OnRowDataBound      = "gvChat_RowDataBound"             
                                           CssClass            = "table table-condensed table-responsive" >
                                 <PagerSettings Visible = "False" />
                                 <RowStyle            BackColor = "#F5F5F5" Height = "66.5px" />
                                 <AlternatingRowStyle BackColor = "#FFFFFF" Height = "66.5px" />
                                 <Columns>
                                     <asp:BoundField DataField  = "nickname"
                                                     HeaderText = "nickname"
                                                     ReadOnly   = "True"
                                                     ShowHeader = "False" >
                                        <HeaderStyle CssClass = "column_hide" />
                                        <ItemStyle   CssClass = "column_nick" />
                                     </asp:BoundField>
                             
                                     <asp:BoundField DataField  = "message"
                                                     HeaderText = "message"
                                                     ReadOnly   = "True"
                                                     ShowHeader = "False" >
                                        <HeaderStyle CssClass = "column_hide" />
                                        <ItemStyle   CssClass = "column_msg"  />
                                     </asp:BoundField>
                             
                                     <asp:BoundField DataField  = "value"
                                                     HeaderText = "value"
                                                     ReadOnly   = "True"
                                                     ShowHeader = "False" >
                                        <HeaderStyle CssClass = "column_hide" />
                                        <ItemStyle   CssClass = "column_hide" />
                                     </asp:BoundField>
                                 </Columns>
                             </asp:GridView>
                         </ContentTemplate>
                     </asp:UpdatePanel>
                 </div>

                 <div class = "mb-3">
                      <asp:Label ID       = "Label1"
                                 runat    = "server"
                                 Text     = "Message:"
                                 CssClass = "form-label textOverBlue">
                      </asp:Label>
                      <asp:TextBox ID        = "txtMsg"
                                   runat     = "server"
                                   CssClass  = "form-control form-text"
                                   MaxLength = "100">
                      </asp:TextBox>
                      <asp:RequiredFieldValidator ID                = "rfvMsg"
                                                  runat             = "server"
                                                  ControlToValidate = "txtMsg"
                                                  ErrorMessage      = "Message is required"
                                                  ForeColor         = "#CC0000"
                                                  ValidationGroup   = "valMsg"
                                                  SetFocusOnError   = "True">
                      </asp:RequiredFieldValidator>
                      <asp:Button ID              = "btnSend"
                                  runat           = "server"
                                  Text            = "Send"
                                  ValidationGroup = "valMsg"
                                  OnClick         = "btnSend_Click"
                                  CssClass        = "form-control btn btn-primary" >
                      </asp:Button>
                      <asp:Label ID    = "lblStatus"
                                 runat = "server"
                                 Text  = "">
                      </asp:Label>
                 </div>
             </div>
           </div>
       </div>
    </form>
</body>

<script type = "text/javascript" src = "js/main.js">
</script>

</html>
