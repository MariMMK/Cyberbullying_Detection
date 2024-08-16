<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="webAppChat.index" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <meta charset="UTF-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    
    <title>Toxic Chat</title>

    <link rel = "stylesheet" href = "css/index.css" />
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
              <img src   = "img/lambton.png"
                   alt   = "lambton.png"
                   style = "vertical-align:middle;" />
          </div>
       </nav>

       <div class = "container mb-3">
          <div class = "row">
             <div class = "col-xs-9 col-sm-9 col-md-9 col-lg-9 col-xl-9 py-2">
                <h1 class = "titleH1OverBlue">AML-2304 Natural Language Processing</h1>
                <h4 class = "titleOverBlue">DSMM 2024S-T3</h4>
                <br />
                <h3 class = "titleOverBlue">Professor: Bhavik Gandhi</h3>
                <br />
                <h3 class = "titleOverBlue">Group 2</h3>
                <div class = "row">
                    <div class = "col-xs-3 col-sm-3 col-md-3 col-lg-3 col-xl-3 py-2">
                       <ul class = "ulmembers">
                          <li>Marzieh Mohammadi</li>
                          <li>Adriana Peñaranda</li>
                          <li>Kirandeep Kaur</li>
                          <li>Jaisy Joy</li>
                       </ul>
                    </div>
                    <div class = "col-xs-3 col-sm-3 col-md-3 col-lg-3 col-xl-3 py-2">
                       <ul class = "ulmembers">
                          <li>Carlos Rey</li>
                          <li>Leonardo Gil</li>
                          <li>Haldo Somoza</li>
                          <li>Eduardo Williams</li>
                       </ul>
                    </div>
                    <div class = "col-xs-6 col-sm-6 col-md-6 col-lg-6 col-xl-6 py-2">
                    </div>
                </div>
             </div>
             <div class = "col-xs-3 col-sm-3 col-md-3 col-lg-3 col-xl-3 py-2"
                  style = "display:flex;align-items:center;" >
                <div class = "row">
                   <div class="mb-3">
                      <asp:Label ID       = "Label1"
                                 runat    = "server"
                                 Text     = "Nickname:"
                                 CssClass = "form-label textOverWhite">
                      </asp:Label>
                      <asp:TextBox ID        = "txtNickname"
                                   runat     = "server"
                                   CssClass  = "form-control form-text text-lowercase"
                                   MaxLength = "15">
                      </asp:TextBox>
                      <ajaxToolkit:FilteredTextBoxExtender ID              = "txtNickname_FilteredTextBoxExtender"
                                                           runat           = "server"
                                                           BehaviorID      = "txtNickname_FilteredTextBoxExtender"
                                                           TargetControlID = "txtNickname"
                                                           FilterMode      = "InvalidChars"
                                                           InvalidChars    = " ">
                      </ajaxToolkit:FilteredTextBoxExtender>
                      <asp:RequiredFieldValidator ID                = "rfvNickname"
                                                  runat             = "server"
                                                  ControlToValidate = "txtNickname"
                                                  ErrorMessage      = "Nickname is required"
                                                  ForeColor         = "#CC0000"
                                                  ValidationGroup   = "valNickname"
                                                  SetFocusOnError   = "True"
                                                  Display           = "Dynamic">
                      </asp:RequiredFieldValidator>
                   </div>
                   <div class = "d-grid gap-2 mb-3">
                      <asp:Button ID              = "btnLogin"
                                  runat           = "server"
                                  Text            = "Sign-In"
                                  ValidationGroup = "valNickname"
                                  OnClick         = "btnLogin_Click"
                                  CssClass        = "form-control btn btn-primary" >
                      </asp:Button>
                   </div>
                   <div class = "mb-3">
                       <asp:Label ID    = "lblStatus"
                                  runat = "server"
                                  Text  = "">
                       </asp:Label>
                   </div>
                </div>
             </div>
         </div>
       </div>
    </form>
</body>
</html>
