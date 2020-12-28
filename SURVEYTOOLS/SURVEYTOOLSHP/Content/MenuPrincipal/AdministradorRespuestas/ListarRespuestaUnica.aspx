<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Default.Master" AutoEventWireup="true" CodeBehind="ListarRespuestaUnica.aspx.cs" Inherits="SURVEYTOOLSHP.Content.MenuPrincipal.AdministradorRespuestas.ListarRespuestaUnica" %>
<%@ Register src="../../../Control/Usuario.ascx" tagname="Usuario" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content_usuario" runat="server">
    <uc1:Usuario ID="Usuario2" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="content_body" runat="server">
    <div style="padding:20px;background-color:White;" class="css_fuente_letra">
<div>
<center>
<table style="width:95%;" cellpadding="0" cellspacing="0">
<tr>
<td style="width:50%;text-align:center" class="css_tamaño_titulo_central css_color_letra_conFondo">
<strong>
LISTAR RESPUESTA
</strong>
</td>
<td style="width:50%">

</td>
</tr>
<tr>
<td colspan="2" class="css_border_grueso" style="padding:10px;text-align:left;">
<p style="margin:0px 0px 10px 0px;text-align:left" class="css_color_titulo">
<strong>Eliga tipo de encuesta</strong> 
    <asp:DropDownList ID="cmb_tipo_encuesta" runat="server" 
        CssClass="css_fuente_letra" Style="vertical-align:middle" Width="200px" 
        AutoPostBack="True" 
        onselectedindexchanged="cmb_tipo_encuesta_SelectedIndexChanged">
    </asp:DropDownList>

</p>
<p style="margin:0px 0px 10px 0px;text-align:left" class="css_color_titulo">
<strong>Filtar por :</strong> 
    <asp:RadioButtonList ID="rdl_filtrar" runat="server" 
        CssClass="css_fuente_letra" Style="vertical-align:middle" 
        onselectedindexchanged="rdl_filtrar_SelectedIndexChanged" 
        AutoPostBack="True">

    </asp:RadioButtonList>

</p>
    <asp:Panel ID="pnl_unidad" runat="server" Visible="false">
    <p style="margin:0px 0px 10px 0px;text-align:left" class="css_color_titulo">
<strong>BU </strong> 
  <asp:DropDownList ID="cmb_encuesta" runat="server" CssClass="css_fuente_letra" 
            Style="vertical-align:middle" Width="200px" AutoPostBack="True" 
            onselectedindexchanged="cmb_encuesta_SelectedIndexChanged">
    </asp:DropDownList>
</p>
    </asp:Panel>


    <asp:Panel ID="pnl_isr" Visible="false" runat="server">
    <p style="margin:0px 0px 10px 0px;text-align:left" class="css_color_titulo">
    <strong>Eliga un evaluado</strong> 
    <asp:DropDownList ID="cmb_isr" runat="server" CssClass="css_fuente_letra" 
            Style="vertical-align:middle" Width="200px" AutoPostBack="True" 
            onselectedindexchanged="cmb_isr_SelectedIndexChanged">
    </asp:DropDownList>
</p>
    </asp:Panel>
   

   <div>
       <asp:Panel ID="pnl_grid" Visible="false" runat="server">
   
    <asp:GridView ID="dgrid_respuesta" runat="server" Width="100%" 
           CellPadding="4" ForeColor="#333333"  GridLines="None" AllowPaging="True" 
               onrowupdating="dgrid_respuesta_RowUpdating">
        <AlternatingRowStyle BackColor="White" />
        <EditRowStyle BackColor="#2461BF" />
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#214023" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#214023" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#ECFDEF" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#F5F7FB" />
        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
        <SortedDescendingCellStyle BackColor="#E9EBEF" />
        <SortedDescendingHeaderStyle BackColor="#4870BE" />

    </asp:GridView>
    <p style="margin:20px 0px 0px 0px;padding:10px;text-align:right" class="css_color_sub_titulo css_border_simple css_tamanio_subtitulos css_bold"> 
      Total :  <asp:Label ID="lbl_ponderado_total" runat="server" Text=""></asp:Label> </p>
    </asp:Panel>
        </div>

</td>
</tr>
</table>
</center>
</div>

</div>
</asp:Content>
