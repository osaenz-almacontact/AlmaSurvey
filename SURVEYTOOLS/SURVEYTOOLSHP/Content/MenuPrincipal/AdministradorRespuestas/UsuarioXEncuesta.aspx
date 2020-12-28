<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Default.Master" AutoEventWireup="true" CodeBehind="UsuarioXEncuesta.aspx.cs" Inherits="SURVEYTOOLSHP.Content.MenuPrincipal.AdministradorRespuestas.UsuarioXEncuesta" %>
<%@ Register src="../../../Control/Usuario.ascx" tagname="Usuario" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content_usuario" runat="server">
    <uc1:Usuario ID="Usuario2" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="content_body" runat="server">
<div style="padding:40px 20px 50px 20px;background-color:White" class="css_fuente_letra">
<p class="css_bold css_tamaño_titulo_central css_color_titulo" style="text-align:left;margin:0px 0px 30px 0px">Usuario y Encuestas</p>
<div class="css_border_simple" style="padding:10px">
<div>
<table style="width:100%;" cellpadding=0 cellspacing=0>
<tr>
<td style="width:50%;text-align:center" class="css_bold css_color_letra_conFondo css_tamanio_descripcion">
    <asp:LinkButton ID="LinkButton1" runat="server" Font-Overline="false" 
        CssClass="css_color_letra_blaca css_bold" onclick="LinkButton1_Click" Width="100%">Filtar por Encuesta</asp:LinkButton></td>
<td style="width:50%"></td>
</tr>
<tr>
<td colspan="2">
    <asp:Panel ID="pnl_filtro_X_Encuesta" runat="server" CssClass="css_border_simple" Visible="false">
        <p style="margin:5px 5px 5px 5px" class="css_bold css_tamanio_descripcion css_color_sub_titulo">Tipo de encuesta : 
        &nbsp;<asp:DropDownList ID="cmb_tipo_encuesta" runat="server" 
                Style="vertical-align:middle" Width="200px" AutoPostBack="True" 
                onselectedindexchanged="cmb_tipo_encuesta_SelectedIndexChanged">
        </asp:DropDownList>
    </p>
            <p style="margin:5px 0px 5px 5px" class="css_bold css_tamanio_descripcion css_color_sub_titulo">Encuesta : <asp:DropDownList ID="cmb_encuesta" runat="server" Style="vertical-align:middle" Width="200px">
        </asp:DropDownList>
        &nbsp;</p>


    </asp:Panel>
</td>
</tr>
<tr>
<td style="width:50%;text-align:center" class="css_bold css_color_letra_conFondo css_tamanio_descripcion">
    <asp:LinkButton ID="lnk_btn_usuario_filtro" runat="server" 
        Font-Overline="false" CssClass="css_color_letra_blaca css_bold" 
        onclick="lnk_btn_usuario_filtro_Click" Width="100%">Filtar por Usuario</asp:LinkButton></td>
<td style="width:50%"></td>
</tr>
<tr>
<td colspan="2">

    <asp:Panel ID="pnl_filtro_usuario" runat="server" CssClass="css_border_simple" Visible="false">
    <p style="margin:5px" class="css_bold css_tamanio_descripcion css_color_sub_titulo">     Usuario : 
    <asp:TextBox ID="txt_filtro_usuario" runat="server" 
        Style="vertical-align:middle" Width="238px"></asp:TextBox> </p>
    </asp:Panel>
</td>
</tr>
</table>


<table cellpadding=0 cellspacing=0 style="width:200px;margin:0px 0px 20px 0px">
<tr>
<td valign="top" style="margin:0px;padding:0px;width:50%">
<asp:Button ID="Button1" runat="server" Text="Filtrar" Width="100px" 
        CssClass="css_color_botones css_bold css_fuente_letra css_tamanio_descripcion css_color_letra_blaca" 
        Height="30px" onclick="Button1_Click" />
</td>
<td style="width:50%;text-align:left" valign="top">
    <asp:Image ID="Image1" runat="server" Height="30px" Width="30px" 
        ImageUrl="~/Resources/Iconos/Search.png"/>
</td>
</tr>
</table>
    <center>
  
    <asp:GridView ID="dgrid_ver_reporte" runat="server" Width="95%" 
        AllowPaging="True" CellPadding="4" GridLines="Vertical" PageSize="15" 
        onrowdatabound="dgrid_ver_reporte_RowDataBound" 
        onrowcommand="dgrid_ver_reporte_RowCommand" Font-Size="14px" 
        onpageindexchanging="dgrid_ver_reporte_PageIndexChanging">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:ButtonField CommandName="Select" Text="Select" 
                ItemStyle-HorizontalAlign="Center">
<ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:ButtonField>
            <asp:ButtonField CommandName="VerRespuesta" Text="Ver Respuesta">
            <FooterStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Center" />
            </asp:ButtonField>
        </Columns>
        <EditRowStyle BackColor="#A6A6A7" />
        <FooterStyle BackColor="#A6A6A7" Font-Bold="True" ForeColor="White" />
        <HeaderStyle Font-Bold="True" 
            
            CssClass="css_tamanio_descripcion css_color_letra_conFondo css_fuente_letra" BackColor="#214023" 
            ForeColor="White" HorizontalAlign="Center" />

        <PagerStyle BackColor="#214023" ForeColor="White" HorizontalAlign="Center" CssClass="css_fuente_letra"/>
        <RowStyle BackColor="#E7E9E7" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#F5F7FB" />
        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
        <SortedDescendingCellStyle BackColor="#E9EBEF" />
        <SortedDescendingHeaderStyle BackColor="#4870BE" />

    </asp:GridView>
    </center>
</div>
</div >

</div>
</asp:Content>
