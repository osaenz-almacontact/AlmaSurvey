<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Default.Master" AutoEventWireup="true" CodeBehind="CuentaDetalles.aspx.cs" Inherits="SURVEYTOOLSHP.Content.MenuPrincipal.Cuentas.CuentaDetalles" %>

<%@ Register Src="../../../Control/Usuario.ascx" TagName="Usuario" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content_usuario" runat="server">
    <uc1:Usuario ID="Usuario2" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="content_body" runat="server">
    <div class="container-fluid">
        <p style="text-align: left" class="css_tamaño_titulo_central css_color_titulo">
            <strong>Usario
                <asp:Label ID="lbl_nombre_usuario" runat="server" Text=""></asp:Label></strong>
        </p>
        <hr />

        <asp:Panel ID="pnlEditarUsuario" runat="server">

            <div class="card border-secondary">
                <div class="card-header" style="text-align: center">
                    <label><b>Editar Usuario:</b></label>
                </div>
                <div class=" card-body">
                    <div class="form-row">
                        <div class="col">
                            <label style="text-align: left; width: 50%">Usuario:</label>
                            <asp:TextBox ID="txt_usu_nombre" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col">
                            <label style="text-align: left; width: 50%">login:</label>
                            <asp:TextBox ID="txt_login" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col">
                            <label style="text-align: left; width: 50%">Contraseña:</label>
                            <p style="margin: 0px">
                                <asp:TextBox ID="txt_contrasenia" runat="server" CssClass="form-control" Style="vertical-align: middle"></asp:TextBox>
                                &nbsp;<asp:ImageButton ID="ImageButton1" runat="server"
                                    Style="vertical-align: middle" Height="30px"
                                    ImageUrl="~/Resources/Iconos/Tag.png" OnClick="ImageButton1_Click"
                                    ToolTip="nueva contraseña" Width="31px" />
                            </p>
                        </div>
                        <div class="col">
                            <label style="text-align: left; width: 50%">Correo:</label>
                            <asp:TextBox ID="txt_correo" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col">
                            <label style="text-align: left; width: 50%">Estado:</label>
                            <asp:DropDownList ID="cmb_estado" runat="server" CssClass="form-control">
                                <asp:ListItem Value="1">Activo</asp:ListItem>
                                <asp:ListItem Value="0">Desactivo</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col">
                            <label style="text-align: left; width: 50%">Permisos:</label>
                            <asp:DropDownList ID="cmb_Permisos" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </div>

                    </div>
                </div>
            </div>
            <br />
            <h4><strong>Encuestas</strong></h4>
            <hr />
            <strong>Fase </strong>
            <asp:DropDownList
                ID="cmb_fase" runat="server" CssClass="form-control" Style="vertical-align: middle"
                Width="208px" OnSelectedIndexChanged="cmb_fase_SelectedIndexChanged"
                AutoPostBack="True">
            </asp:DropDownList>
            <br />
            <div class="row">
                <div class="col-md-6">
                    <div class="card border-secondary">
                        <div class="card-header" style="text-align: center">
                            <label><b>Satisfacción</b></label>
                        </div>
                        <div class="card-body">
                            <asp:CheckBoxList ID="chk_list_encuestas_satisfaccion" runat="server" Width="400px" Style="vertical-align: middle; text-align: left" Font-Size="12px" CssClass="css_fuente_letra css_bold">
                            </asp:CheckBoxList>
                        </div>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="card border-secondary">
                        <div class="card-header" style="text-align: center">
                            <label><b>Servicio</b></label>
                        </div>
                        <div class="card-body">
                            <asp:CheckBoxList ID="chk_lst_encuesta_servicio" runat="server" Width="400px" Style="vertical-align: middle; text-align: left" Font-Size="12px" CssClass="css_fuente_letra css_bold">
                            </asp:CheckBoxList>
                        </div>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="card border-secondary">
                        <div class="card-header" style="text-align: center">
                            <label><b>Liderazgo</b></label>
                        </div>
                        <div class="card-body">
                            <asp:CheckBoxList ID="chk_lst_encuesta_liderazgo" runat="server" Width="400px" Style="vertical-align: middle; text-align: left" Font-Size="12px" CssClass="css_fuente_letra css_bold">
                            </asp:CheckBoxList>
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <div class="card border-secondary">
                <div class="row">
                    <div class="col-md-6" style="text-align: center">
                        <asp:Button ID="btn_Enviar_cambios" runat="server" Text="Enviar" CssClass="btn btn-primary"
                            OnClick="btn_Enviar_cambios_Click" />
                    </div>
                    <div class="col-md-6" style="text-align: center">
                        <asp:Button ID="btn_ocultar_panel" runat="server" Text="Volver" Width="112px"
                            CssClass="btn btn-primary" OnClick="btn_ocultar_panel_Click" />
                    </div>
                </div>
            </div>
        </asp:Panel>
    </div>

</asp:Content>
