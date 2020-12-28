<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Default.Master" AutoEventWireup="true" CodeBehind="RespuestaEspecifica.aspx.cs" Inherits="SURVEYTOOLSHP.Content.MenuPrincipal.AdministradorRespuestas.RespuestaEspecifica" %>
<%@ Register src="../../../Control/Usuario.ascx" tagname="Usuario" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content_usuario" runat="server">
    <uc1:Usuario ID="Usuario2" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="content_body" runat="server">
<div style="padding:20px 20px 20px 20px;background-color:White;" class="css_fuente_letra" >
<p style="margin:0px 0px 20px 0px" class="css_color_titulo css_tamaño_titulo_central css_bold">Respuesta Especifica</p>
<p style="margin:0px 0px 10px 0px;text-align:justify" class="css_tamanio_subtitulos css_color_sub_titulo">El usuario <strong>  
    <asp:Label ID="lbl_nombre_usuario" runat="server" Text=""></asp:Label>&nbsp;</strong>para la encuesta <strong> 
        <asp:Label ID="lbl_nom_encuesta" runat="server"></asp:Label>  </strong> contestó lo siguiente : </p>
    <div class="css_border_simple" style="padding:10px">
    <center>
    <div style="width:90%">
    <center>
        <asp:GridView ID="dgrid_respuesta_especifica" runat="server" Width="95%" CellPadding="4" 
            ForeColor="#333333" GridLines="None" AllowPaging="True" PageSize="20" 
            onrowdatabound="dgrid_respuesta_especifica_RowDataBound" 
            onpageindexchanging="dgrid_respuesta_especifica_PageIndexChanging">
            <AlternatingRowStyle BackColor="White" />
            <EditRowStyle BackColor="#2461BF" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <HeaderStyle CssClass="css_tamanio_descripcion" BackColor="#214023" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
            <PagerStyle CssClass="css_bold" BackColor="#214023" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#ECFDEF" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
        </asp:GridView>
    </center>
    
    </div>
    <p style="margin:10px 0px 10px 0px" class="css_color_titulo css_tamanio_subtitulos">Para 
        <asp:Label ID="lbl_nombre_evaluado" runat="server" Text="Label" Font-Bold="true"> </asp:Label> tuvo un promedio de 
        <asp:Label ID="lbl_ponderacion" runat="server" Text="Label" Font-Bold="true"></asp:Label> según 
        <asp:Label ID="Lbl_usuario" runat="server" Text="" Font-Bold="true"></asp:Label></p>
        <p style="margin:10px 0px 10px 0px;text-align:right;">
        <asp:LinkButton ID="lbl_volver" runat="server" Text="Volver" Style="padding:5px;text-align:center" 
                Font-Bold="true" Width="100px" 
                CssClass="css_border_simple css_color_sub_titulo css_tamanio_subtitulos" onclick="lbl_volver_Click"></asp:LinkButton>
        </p>
        </center>
    </div>

</div>
</asp:Content>
