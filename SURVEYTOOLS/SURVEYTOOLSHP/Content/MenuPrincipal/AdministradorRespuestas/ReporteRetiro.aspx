<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Default.Master" AutoEventWireup="true" CodeBehind="ReporteRetiro.aspx.cs" Inherits="SURVEYTOOLSHP.Content.MenuPrincipal.AdministradorRespuestas.ReporteRetiro"  EnableEventValidation="false" %>

<%@ Register Src="~/Control/Usuario.ascx" TagName="Usuario" TagPrefix="uc1"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content_usuario" runat="server">
    <uc1:Usuario ID="Usuario2" runat="server" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="content_body" runat="server">
    <br />
    <br />
    <div class="container-fluid">
        <p style="text-align: left;" class="css_color_titulo css_tamaño_titulo_central">
            <strong>
                <label>Reporte Retiro</label>
            </strong>
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
            <%--<div class="col">
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
            </div>--%>
        </div>
        <hr />
        <div class="row">
            <div class="col-12">

                <asp:Panel ID="pnl_detalle_respuestas" runat="server" Visible="false">

                    <div>
                        <table style="width: 100%" cellpadding="0" cellspacing="0">
                            <tr>
                                <td  padding: 8px; text-align: center">

                                    <p style="text-align: right; margin: 0px 0px 0px 0px; font-size: 12px" class="css_color_sub_titulo css_bold">
                                        Exportar a excel
                                            <asp:ImageButton ID="img_btn_exportar_excel" runat="server"
                                                Style="vertical-align: middle" Height="24px"
                                                ImageUrl="~/Resources/Iconos/logo excel.png" Width="24px"
                                                OnClick="img_btn_exportar_excel_Click" />

                                    </p>

                                    <asp:GridView ID="dgrid_reporte_retiro" runat="server" CssClass="table table-bordered"
                                        EmptyDataText="NO FOUND" AllowPaging="True"
                                        CellPadding="4"  PageSize="20"
                                        OnPageIndexChanging="dgrid_reporte_retiro_PageIndexChanging">
                                        <Columns>
                                            <asp:ButtonField CommandName="VerDetalles" HeaderText="Ver Detalles"
                                                Text="Select" />
                                        </Columns>                              
                    
                                    </asp:GridView>


                                </td>
                            </tr>
                        </table>

                    </div>

                </asp:Panel>
                <br />
                <br />
            </div>

        </div>

    </div>
</asp:Content>
