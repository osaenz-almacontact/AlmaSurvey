<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Default.Master" AutoEventWireup="true" CodeBehind="CrearUsuario.aspx.cs" Inherits="SURVEYTOOLSHP.Content.MenuPrincipal.Cuentas.CrearUsuario" %>
<%@ Register src="../../../Control/Usuario.ascx" tagname="Usuario" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content_usuario" runat="server">
    w<uc1:Usuario ID="Usuario2" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="content_body" runat="server">
<div style="padding:10px 20px 10px 20px;background-color:White;">
<div>
<asp:Panel ID="pnlEditarUsuario" runat="server"  BackColor="White"  CssClass="css_fuente_letra">
   <div>
   <center>
   
     <table style="width:80%" cellpadding=0 cellspacing=0 class="css_border_simple">
    <tr>
    <td class="css_color_letra_conFondo css_tamanio_subtitulos css_bold" style="text-align:center">
     <asp:Label ID="Label1" runat="server" Text="Crear Usuario"></asp:Label>
    </td>
    </tr>
    <tr>
    <td style="padding:15px">
    <div>
    <table style="width:100%" cellpadding=0 cellspacing=0>
    <tr>
    <td class="css_bold" style="text-align:left;width:50%">Usuario</td>
    <td style="width:50%;text-align:left">
        <asp:TextBox ID="txt_usu_nombre" runat="server" Width="220px"></asp:TextBox> </td>
    </tr>
     <tr>
    <td class="css_bold" style="text-align:left;width:50%">login</td>
    <td style="width:50%;text-align:left">
        <asp:TextBox ID="txt_login" runat="server" Width="220px"></asp:TextBox> </td>
    </tr>
     <tr>
    <td class="css_bold" style="text-align:left;width:50%">Contraseña</td>
    <td style="width:50%;text-align:left">
      <p>  <asp:TextBox ID="txt_contrasenia" runat="server" Width="158px" 
              Style="vertical-align:middle" Enabled="False"></asp:TextBox> 
          &nbsp;<asp:ImageButton ID="ImageButton1" runat="server" 
              Style="vertical-align:middle" Height="30px" 
              ImageUrl="~/Resources/Iconos/Tag.png" onclick="ImageButton1_Click" 
              ToolTip="nueva contraseña" Width="31px"/></p> </td>
    </tr>
         <tr>
    <td class="css_bold" style="text-align:left;width:50%">Correo</td>
    <td style="width:50%;text-align:left">
        <asp:TextBox ID="txt_correo" runat="server" Width="220px"></asp:TextBox> </td>
    </tr>
    <tr>
    <td class="css_bold" style="text-align:left;width:50%">Estado</td>
    <td style="width:50%;text-align:left">
        <asp:DropDownList ID="cmb_estado" runat="server" Width="180px">
            <asp:ListItem Value="1">Activo</asp:ListItem>
            <asp:ListItem Value="0">Desactivo</asp:ListItem>
        </asp:DropDownList> </td>
    </tr>
    <tr>
    <td class="css_bold" style="text-align:left;width:50%">Permisos</td>
    <td style="width:50%;text-align:left">
        <asp:DropDownList ID="cmb_Permisos" runat="server" Width="180px">
        </asp:DropDownList> </td>
    </tr>
    
    <tr>
    <td>
    <br />
    </td>
    </tr>
    <tr>
    
    <td style="width:50%;text-align:center;" class="css_tamanio_descripcion css_color_letra_conFondo">
    <strong>Encuestas</strong>
    </td>
    </tr>
   <tr>
   <td colspan="2" style="padding:5px;text-align:left;" class="css_border_top_simple css_border_left_simple css_border_right_simple">
   <p style="margin:0px;text-align:left;" class="css_color_sub_titulo"><strong>Fase: </strong> 
       <asp:DropDownList ID="cmb_fase" runat="server" Style="vertical-align:middle" 
       Width="173px" AutoPostBack="True" 
           onselectedindexchanged="cmb_fase_SelectedIndexChanged">
       </asp:DropDownList>
   </p>
   <p style="text-align:left;margin:0px;font-size:12px;color:#C01C1C">
       <strong><asp:Label ID="lbl_val_fase" runat="server" Text="***falta por seleccionar***" Visible="false"></asp:Label></strong></p>
   </td>
   </tr>
  <tr>
  <td valign="top" style="width:50%;padding:5px" class="css_border_left_simple css_border_bottom_simple">
 <div>
  <table style="width:100%" cellpadding="0" cellspacing="0">
  <tr>
  <td style="width:50%">   <p class="css_color_letra_conFondo" style="margin:0px"><strong>Satisfacción</strong></p></td>
  <td style="width:50%"></td>
  </tr>
  <tr>
  <td valign="top" colspan="2" class="css_border_simple" style="text-align:left;">
  <asp:CheckBoxList ID="chk_list_encuestas_satisfaccion" runat="server" Width="100%" Style="vertical-align:middle;" Font-Size="12px" CssClass="css_fuente_letra css_bold">
        </asp:CheckBoxList>
  </td>
  </tr>
  </table>
 </div>
  </td>
  <td valign="top" style="width:50%;padding:5px" class="css_border_right_simple css_border_bottom_simple">
  <div>
  <table style="width:100%" cellpadding="0" cellspacing="0">
  <tr>
  <td style="width:50%">    <p class="css_color_letra_conFondo" style="margin:0px"><strong>Servicio</strong></p></td>
  <td style="width:50%"></td>
  </tr>
  <tr>
  <td valign="top" colspan="2" class="css_border_simple" style="text-align:left">
   <asp:CheckBoxList ID="chk_lst_encuesta_servicio" runat="server" Width="100%" Style="vertical-align:middle;" Font-Size="12px" CssClass="css_fuente_letra css_bold">
        </asp:CheckBoxList>
  </td>
  </tr>
  </table>
  </div>
  </td>
  </tr>
    


    <tr class="css_border_simple">
    <td style="width:50%;padding:20px 10px 10px 10px"><center>
        <asp:Button ID="btn_Enviar_cambios" runat="server" Text="Enviar" CssClass="css_bold css_color_letra_blaca css_color_botones css_fuente_letra css_tamanio_descripcion"
            Width="122px" onclick="btn_Enviar_cambios_Click" />
    </center></td>
    <td style="width:50%;padding:20px 10px 10px 10px"><center>
    <asp:Button ID="btn_ocultar_panel" runat="server" Text="Atras" Width="112px" 
            CssClass="css_bold css_color_letra_blaca css_color_botones css_fuente_letra css_tamanio_descripcion" 
            onclick="btn_ocultar_panel_Click"/>
    </center></td>
    </tr>
    </table>
        
    </div>
    </td>
    </tr>
    </table>
   
   </center>
   </div>


    </asp:Panel>
</div>

</div>
</asp:Content>
