<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Default.Master" AutoEventWireup="true" CodeBehind="Encuesta.aspx.cs" Inherits="SURVEYTOOLSHP.Content.MenuPrincipal.AdministradorRespuestas.Satisfaccion" EnableEventValidation="false" %>

<%@ Register Src="../../../Control/Usuario.ascx" TagName="Usuario" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content_usuario" runat="server">
    <uc1:Usuario ID="Usuario2" runat="server" />
</asp:Content>
<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="content_body">
    <div class="container-fluid">

        <p style="text-align: left;" class="css_color_titulo css_tamaño_titulo_central">
            <strong>
                <asp:Label ID="lbl_nombre_encuesta" runat="server" Text="" CssClass="css_fuente_letra"></asp:Label></strong>
        </p>
        <hr />
        <div class="form-row">
            <div class="col">
                <label>Eliga fase</label>
                <asp:DropDownList ID="cmb_fase" runat="server"
                    class="form-control"
                    AutoPostBack="True" OnSelectedIndexChanged="cmb_fase_SelectedIndexChanged">
                </asp:DropDownList>
            </div>
            <div class="col">
                <asp:Panel ID="pnl_tipo_encuesta" runat="server" Visible="false">
                    <label>Eliga tipo de encuesta</label>
                    <asp:DropDownList ID="cmb_tipo_encuesta" runat="server"
                        class="form-control"
                        AutoPostBack="True"
                        OnSelectedIndexChanged="cmb_tipo_encuesta_SelectedIndexChanged">
                    </asp:DropDownList>
                </asp:Panel>
            </div>
            <div class="col">
                <asp:Panel ID="pnl_filtro" runat="server" Visible="false">
                    <label>Filtrar por:</label>
                    <asp:RadioButtonList ID="rdl_btn_filtro" runat="server"
                        AutoPostBack="True"
                        OnSelectedIndexChanged="rdl_btn_filtro_SelectedIndexChanged">
                    </asp:RadioButtonList>
                </asp:Panel>
            </div>
            <div class="col">
                <asp:Panel ID="pnl_encuesta" runat="server" Visible="false">
                    <label>Eliga una BU</label>
                    <asp:DropDownList ID="cmb_encuestas" runat="server"
                        class="form-control" AutoPostBack="True"
                        OnSelectedIndexChanged="cmb_encuestas_SelectedIndexChanged">
                    </asp:DropDownList>
                </asp:Panel>
            </div>
        </div>

    </div>


    <div style="width: 100%; padding: 10px; background-color: White;" class="css_fuente_letra">

        <table style="width: 95%;" cellpadding="0;" cellspacing="0;">

            <tr>
                <td colspan="2" style="padding: 10px;" class="css_border_grueso">

                    <p style="margin: 0px 0px 10px 0px; text-align: left;" class="css_color_titulo">
                    </p>


                    <asp:Panel ID="pnl_detalle_respuestas" runat="server" Visible="false">

                        <div>
                            <table style="width: 100%" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="border: 2px solid #214023; padding: 8px; text-align: center">

                                        <p style="text-align: right; margin: 0px 0px 0px 0px; font-size: 12px" class="css_color_sub_titulo css_bold">
                                            Exportar a excel
                                            <asp:ImageButton ID="img_btn_exportar_excel" runat="server"
                                                Style="vertical-align: middle" Height="24px"
                                                ImageUrl="~/Resources/Iconos/logo excel.png" Width="24px"
                                                OnClick="img_btn_exportar_excel_Click" />

                                        </p>

                                        <asp:GridView ID="dgrid_ponderado_total" runat="server" Width="100%"
                                            EmptyDataText="NO FOUND" OnRowCommand="dgrid_ponderado_total_RowCommand"
                                            OnRowDataBound="dgrid_ponderado_total_RowDataBound" AllowPaging="True"
                                            CellPadding="4" ForeColor="#333333" GridLines="None" PageSize="20"
                                            OnPageIndexChanging="dgrid_ponderado_total_PageIndexChanging">
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                                <asp:ButtonField CommandName="VerDetalles" HeaderText="Ver Detalles"
                                                    Text="Select" />
                                            </Columns>
                                            <EditRowStyle BackColor="#2461BF" />
                                            <EmptyDataRowStyle Font-Bold="True" Font-Size="20px" Font-Underline="True"
                                                HorizontalAlign="Center" CssClass="css_color_sub_titulo" />
                                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle Font-Bold="True" Font-Size="18px" BackColor="#214023"
                                                ForeColor="White" />
                                            <PagerStyle BackColor="#214023" Font-Bold="true" ForeColor="White" HorizontalAlign="Center" />
                                            <RowStyle BackColor="#ECFDEF" />
                                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                            <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                            <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                            <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                        </asp:GridView>


                                    </td>
                                </tr>
                            </table>

                        </div>

                    </asp:Panel>
                    <br />
                    <br />


                </td>
            </tr>
        </table>


    </div>
</asp:Content>

