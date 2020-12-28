<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Default.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SURVEYTOOLSHP.Content.MenuPrincipal.AdministrarEncuestas.Default" %>

<%@ Register Src="../../../Control/Usuario.ascx" TagName="Usuario" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content_usuario" runat="server">
    <uc1:Usuario ID="Usuario2" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="content_body" runat="server">
    <br />
    <br />
    <div class="container-fluid">
        <p style="text-align: left;" class="css_color_titulo css_tamaño_titulo_central"><strong>ADMINISTRAR ENCUESTAS</strong></p>
        <hr />
        <div class="card">
            <asp:GridView ID="dgrid_Encuesta" runat="server" Width="100%" CellPadding="4"
                GridLines="None" AllowPaging="True"
                OnRowCommand="dgridView_RowCommand" PageSize="15"
                OnPageIndexChanging="dgrid_Encuesta_PageIndexChanging"
                OnRowDataBound="dgrid_Encuesta_RowDataBound" CssClass="table table-striped">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:ButtonField CommandName="Select" Text="Select" />
                </Columns>
                <%--<EditRowStyle BackColor="#2461BF" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#214023" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
            <PagerStyle BackColor="#214023" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#ECFDEF" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />--%>
            </asp:GridView>
        </div>

    </div>
</asp:Content>
