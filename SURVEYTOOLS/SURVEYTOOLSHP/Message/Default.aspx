<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SURVEYTOOLSHP.Message.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<style type="text/css">
body
{
    width:100%;
    padding:0px;margin:0px;
   background-color:#D2D2D2;
    }
</style>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table style="width:100%;border-bottom:solid 2px #464649">
    <tr><td valign="top" style="text-align:left;padding:20px;">
        <asp:Image ID="img_logo_survey" runat="server" 
            ImageUrl="~/Resources/SurveyLogo.png" Height="150px" Width="174px" />
    </td>
    <td style="width:100%;color:#464649;font-size:16px;font-family:HP Simplified" valign="middle" ><strong><asp:Label ID="Label1" runat="server" Text=""></asp:Label></strong></td>
    </tr>
    </table>
    </div>
    <div style="width:100%;">
    <center>
    <table style="width:400px;margin:50px;background-color:White;border:solid 5px #464649" cellpadding="0" cellspacing="0">
    <tr>
    <td style="padding:10px;border-right:solid 2px #464649" >
        <asp:Image ID="img_mensaje" runat="server" Height="64px" Width="68px" />
        </td>
    <td valign="middle" style="padding:10px">
     <p style="margin:0px 0px 10px 0px;text-align:justify">
     <asp:Label ID="lbl_mensaje_titulo" runat="server" Text="" Font-Size="20px" Font-Bold="true" ForeColor="#C10C0C"></asp:Label>
     </p> 
       <p style="margin:0px 0px 10px 0px;text-align:left">
     <asp:Label ID="lbl_mensaje_descripcion" runat="server" Font-Size="17px" Text="" Font-Bold="true">
     
     </asp:Label>
     </p>     
    </td>
    </tr>
    </table>
    </center>
    </div>
    </form>
    
</body>
</html>
