<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Default.Master" AutoEventWireup="true" CodeBehind="AdministrarCuentas.aspx.cs" Inherits="SURVEYTOOLSHP.Content.MenuPrincipal.Cuentas.AdministrarCuentas" %>

<%@ Register Src="../../../Control/Usuario.ascx" TagName="Usuario" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content_usuario" runat="server">
    <uc1:Usuario ID="Usuario2" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="content_body" runat="server">
    <link href="https://fonts.googleapis.com/css2?family=Material+Icons" rel="stylesheet" />
    <style>
        .btn-circle.btn-xl {
            width: 70px;
            height: 70px;
            padding: 10px 16px;
            border-radius: 55px;
            font-size: 24px;
            line-height: 1.33;
        }

        .btn-circle {
            width: 40px;
            height: 40px;
            padding: 6px 0px;
            border-radius: 25px;
            text-align: center;
            font-size: 12px;
            line-height: 1.42857;
        }
    </style>
    <br />
    <br />
    <div class="container-fluid">
        <p style="text-align: left;" class="css_color_titulo css_tamaño_titulo_central">
            <strong>
                <label>Administración de cuentas</label>
            </strong>
        </p>
        <hr />
        <div class="card">
            <div class="card-header">
                <div class="form-row">
                    <div class="col">
                        <label>Usuario : </label>
                        <asp:TextBox ID="txt_filtro_por_usuario" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col">
                        <asp:LinkButton ID="lnk_btn_filtro" runat="server" Font-Overline="false"
                            Font-Bold="true" CssClass="btn btn-info" Style="text-align: right; margin-top: 30px"
                            OnClick="lnk_btn_filtro_Click">Filtrar>></asp:LinkButton>
                    </div>
                    <p style="text-align: right; margin: 0px 0px 0px 0px; font-size: 12px" class="css_color_sub_titulo css_bold">
                        Exportar a excel
                    <asp:ImageButton ID="img_btn_exportar_excel" runat="server"
                        Style="vertical-align: middle" Height="24px"
                        ImageUrl="~/Resources/Iconos/logo excel.png" Width="24px"
                        OnClick="img_btn_exportar_excel_Click" />
                    </p>
                </div>

            </div>

            <div class="card-body">
                <asp:GridView ID="dgrid_Cuentas" runat="server" AllowPaging="True"
                    EmptyDataText="NO FOUND" PageSize="20"
                    OnRowCommand="dgrid_Cuentas_RowCommand"
                    OnRowDataBound="dgrid_Cuentas_RowDataBound"
                    OnPageIndexChanging="dgrid_Cuentas_PageIndexChanging" CellPadding="4"
                    CssClass="table table-striped">
                    <Columns>
                        <asp:ButtonField ButtonType="Link" CommandName="Editar" HeaderText="Editar"
                            Text='<i class="material-icons" style="color:#d39e00; margin-top:2px">autorenew</i>' />
                        <asp:ButtonField ButtonType="Link" CommandName="AgregarISR"
                            HeaderText="Agregar Evaluado" ItemStyle-HorizontalAlign="Center"
                            Text='<i class="material-icons" style="color:#28a745; margin-top:2px">add_task</i>' />
                        <asp:ButtonField ButtonType="Link" CommandName="Desahabilitar"
                            HeaderText="Desactivar" ItemStyle-HorizontalAlign="Center"
                            Text='<i class="material-icons" style="color:#dc3545; margin-top:2px">highlight_off</i>' />
                    </Columns>
                    <%-- <EditRowStyle BackColor="#2461BF" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#214023" Font-Bold="True" Font-Size="18px"
                        ForeColor="White" />
                    <PagerStyle BackColor="#214023" ForeColor="White" HorizontalAlign="Center" Font-Bold="true" />
                    <RowStyle BackColor="#ECFDEF" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                    <SortedDescendingHeaderStyle BackColor="#4870BE" />--%>
                </asp:GridView>

            </div>
            <div style="text-align: right; margin: 20px 0px 0px 0px" class="css_fuente_letra">

                <p style="text-align: right; margin: 10px 0px 0px 0px">
                    <asp:LinkButton ID="LinkButton1" runat="server"
                        Style="text-align: right; padding: 10px" ForeColor="#A6A6A7"
                        Font-Underline="False" Font-Size="18px"
                        CssClass="css_fuente_letra css_bold css_tamanio_subtitulos"
                        OnClick="LinkButton1_Click">Agregar un solo usuario >></asp:LinkButton>

                </p>
                <hr size="5px" />
                <asp:Panel ID="pnl_importar_excel" runat="server">
                    <p style="margin: 0px">
                        <strong>Importar archivo excel</strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:ImageButton ID="Image1" runat="server" Height="40px" Width="40px"
                            Style="vertical-align: middle"
                            ImageUrl="~/Resources/Iconos/logo excel.png" OnClick="Image1_Click" />
                    </p>

                </asp:Panel>
                <hr size="5px" />
                <hr size="5px" />
                <asp:Panel ID="pnl_importar_archivo_excel" Visible="false" runat="server">
                    <div class="container-fluid">
                        <div class="form-row">
                            <div class="col" style="text-align: right">
                                <label>Eliga fase : </label>
                                <asp:DropDownList ID="cmb_fase" runat="server"
                                    CssClass="form-control"
                                    AutoPostBack="True">
                                </asp:DropDownList>
                            </div>
                            <div class="col" style="text-align: right">
                                <label>Eliga tipo de encuesta : </label>
                                <asp:DropDownList ID="cmb_tipo_encuesta_nuevo_usuario" runat="server"
                                    CssClass="form-control"
                                    AutoPostBack="True"
                                    OnSelectedIndexChanged="cmb_tipo_encuesta_nuevo_usuario_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                            <div class="col" style="text-align: right">
                                <label>Eliga encuesta :</label>
                                <asp:DropDownList ID="cmb_encuesta_nuevo_usuario" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                            <div class="col" style="text-align: right">
                                <label>Seleccione un archivo</label>
                                <asp:FileUpload ID="file_upload_excel" runat="server" CssClass="form-control" />
                            </div>
                            <div class="col" style="text-align: right; margin-top:20px;">
                                <asp:LinkButton ID="lnk_importar_archivo" runat="server"
                                    Font-Underline="False" Visible="true" OnClick="lnk_importar_archivo_Click"
                                    CssClass="btn btn-info"><strong>Importar Archivo >></strong></asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <br />
                    <br />
                </asp:Panel>


            </div>
        </div>


    </div>

    <div style="position: fixed; width: 500px; top: 125px; right: 0px; z-index: 2;" class="css_fuente_letra">
        <asp:Panel ID="pnl_agregar_isr" runat="server" Visible="false">
            <div style="width: 100%; background-color: White;" class="card border-success">
                <div class="card-header" style="text-align: center">
                    <b>
                        <label>ISRs</label>
                    </b>
                </div>
                <div class="card-body">
                    <div class="form-row">
                        <div class="col-6">
                            <label>Tipo de encuesta</label>
                            <asp:DropDownList ID="cmb_tipo_de_encuesta_isr" runat="server" CssClass="form-control" AutoPostBack="true"
                                OnSelectedIndexChanged="cmb_tipo_de_encuesta_isr_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                        <div class="col-6">
                            <label>Encuesta</label>
                            <asp:DropDownList ID="cmb_encuesta_cambiar_isr" runat="server"
                                CssClass="form-control" AutoPostBack="True"
                                OnSelectedIndexChanged="cmb_encuesta_cambiar_isr_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>

                        <div class="col-12" style="margin-top: 15px">
                            <label>ISR Evaluados:</label>
                            <asp:TextBox ID="txt_isr_nuevos_editados" runat="server"
                                TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col" style="margin-top: 15px">
                            <asp:Button ID="btn_Enviar_Cambio_ISR" runat="server" Text="Editar"
                                CssClass="btn btn-info"
                                Width="153px" OnClick="btn_Enviar_Cambio_ISR_Click" />

                            <asp:Button ID="btn_ocultar" runat="server" Text="Ocultar"
                                CssClass="btn btn-info"
                                Width="143px" OnClick="btn_ocultar_Click" />

                        </div>

                    </div>
                </div>
            </div>
        </asp:Panel>
    </div>
</asp:Content>
