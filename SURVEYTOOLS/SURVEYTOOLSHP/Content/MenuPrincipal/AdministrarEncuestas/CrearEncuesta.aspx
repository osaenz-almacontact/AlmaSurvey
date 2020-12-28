<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Default.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="CrearEncuesta.aspx.cs" Inherits="SURVEYTOOLSHP.Content.MenuPrincipal.AdministrarEncuestas.CrearEncuesta" %>

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
                <label>ENCUESTAS</label>
            </strong>
        </p>
        <hr />

        <div class="card">
            <div class="card-header">
                <p style="text-align: right; margin: 0px 0px 0px 0px; font-size: 12px" class="css_color_sub_titulo css_bold">
                    Exportar a excel
                <asp:ImageButton ID="img_btn_exportar_excel" runat="server"
                    Style="vertical-align: middle" Height="24px"
                    ImageUrl="~/Resources/Iconos/logo excel.png" Width="24px"
                    OnClick="img_btn_exportar_excel_Click" />
                </p>
            </div>

            <div class="card-body">
                <asp:GridView ID="dgrid_Encuestas" runat="server"
                    OnRowCommand="dgrid_Encuestas_RowCommand"
                    OnRowDataBound="dgrid_Encuestas_RowDataBound"
                    CssClass="table table-striped">
                    <Columns>
                        <asp:ButtonField ButtonType="Link" CommandName="Editar" HeaderText="Editar"
                            Text='<i class="material-icons" style="color:#d39e00; margin-top:2px">autorenew</i>' ControlStyle-CssClass="btn btn-light btn-circle" />
                        <asp:ButtonField ButtonType="Link" CommandName="Activar" HeaderText="Activar"
                            Text='<i class="material-icons" style="color:#28a745; margin-top:2px">done_all</i>' ControlStyle-CssClass="btn btn-light btn-circle" />
                        <asp:ButtonField ButtonType="Link" CommandName="Desactivar" HeaderText="Desactivar"
                            Text='<i class="material-icons" style="color:#dc3545; margin-top:2px">highlight_off</i>' ControlStyle-CssClass="btn btn-light btn-circle" ItemStyle-HorizontalAlign="Center" />
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
            <div class="card-footer">
                <label><b>CREAR ENCUESTA</b></label>
                <hr />
                <div class="form-row">
                    <div class="col">
                        <label>Nombre de la encuesta : </label>
                        <asp:TextBox ID="txt_nombre_encuesta" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col">
                        <label>Tipo de Encuesta :</label>
                        <asp:DropDownList ID="cmb_tipo_encuesta" runat="server" CssClass="form-control" AutoPostBack="True"
                            OnSelectedIndexChanged="cmb_tipo_encuesta_SelectedIndexChanged1">
                        </asp:DropDownList>
                    </div>
                    <div class="col">
                        <label>Copiar preguntas de otra encuesta:</label>
                        <asp:RadioButtonList ID="rdl_list_copiar_Preguntas" runat="server"
                            RepeatDirection="Horizontal" AutoPostBack="True" CssClass="form-check"
                            OnSelectedIndexChanged="rdl_list_copiar_Preguntas_SelectedIndexChanged">
                            <asp:ListItem Value="1">Si</asp:ListItem>
                            <asp:ListItem Value="0">No</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <div class="col">
                        <asp:Panel ID="pnl_otra_encuestas" runat="server" Visible="false">
                            <label>Encuesta: </label>
                            <asp:DropDownList ID="cmb_encestas_seleccion" runat="server" CssClass="form-control"></asp:DropDownList>

                        </asp:Panel>
                    </div>
                    <div class="col" style="margin-top:10px">
                        <asp:Button ID="Button1" runat="server" Text="Crear"
                            CssClass="btn btn-info"
                            OnClick="Button1_Click" />
                    </div>
                </div>
            </div>
        </div>

    </div>

    <div style="position: fixed; right: 0px; top: 140px; width: 400px; z-index: 1;" class="css_fuente_letra">
        <asp:Panel ID="pnl_EditarEncuesta" runat="server" BackColor="White" CssClass="css_border_simple" Width="100%" Visible="false">
            <table style="width: 100%;" cellpadding="0" cellspacing="0">
                <tr>
                    <td colspan="2" class="css_color_letra_conFondo css_bold css_tamanio_subtitulos" style="text-align: center">Editar Encuesta
                    </td>
                </tr>
                <tr>
                    <td style="width: 50%; padding: 10px" valign="top"><strong>Nombre de encuesta :</strong>
                    </td>
                    <td style="width: 50%; padding: 10px" valign="top">
                        <asp:TextBox ID="txt_nombre_editar_encuesta" runat="server" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 50%; padding: 10px" valign="top"><strong>Tipo de encuesta</strong></td>
                    <td style="width: 50%; padding: 10px" valign="top">
                        <asp:DropDownList ID="cmb_Tipo_encuesta_editar" runat="server" Style="vertical-align: middle">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width: 50%; padding: 10px" valign="top"><strong>Estado de la encuesta</strong></td>
                    <td style="width: 50%; padding: 10px" valign="top">
                        <asp:DropDownList ID="cmb_estado_encuesta" runat="server" Style="vertical-align: middle">
                            <asp:ListItem Value="1">Activo</asp:ListItem>
                            <asp:ListItem Value="0">Desactivo</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>

                <tr>
                    <td style="width: 50%; padding: 10px">
                        <asp:Button ID="btn_enviar_cambio" runat="server" Text="Enviar"
                            CssClass="css_color_botones css_bold css_color_letra_blaca css_tamanio_descripcion css_fuente_letra"
                            Width="123px" OnClick="btn_enviar_cambio_Click" /></td>
                    <td style="width: 50%; padding: 10px">
                        <asp:Button ID="btn_ocultar" runat="server" Text="Ocultar"
                            CssClass="css_color_botones css_color_letra_blaca css_tamanio_descripcion css_bold css_fuente_letra"
                            Width="129px" OnClick="btn_ocultar_Click" /></td>
                </tr>
            </table>
        </asp:Panel>
    </div>
</asp:Content>
