<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Default.Master" AutoEventWireup="true" CodeBehind="UsuarioXEncuesta.aspx.cs" Inherits="SURVEYTOOLSHP.Content.MenuPrincipal.AdministradorRespuestas.UsuarioXEncuesta" %>

<%@ Register Src="../../../Control/Usuario.ascx" TagName="Usuario" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content_usuario" runat="server">
    <uc1:Usuario ID="Usuario2" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="content_body" runat="server">
    <div class="container-fluid">
        <p style="text-align: left;" class="css_color_titulo css_tamaño_titulo_central">
            <strong>
                <label class="css_fuente_letra">Usuario y Encuestas:</label>
            </strong>
        </p>
        <hr />

        <div class="card">
            <ul class="nav nav-tabs" role="tablist">
                <li class="nav-item">
                    <a class="nav-link active" data-toggle="tab" href="#liEncunesta">Filtar por Encuesta</a>
                </li>
                <li class="nav-item">
                    <a class="nav-link" data-toggle="tab" href="#liUsuario">Filtar por Usuario</a>
                </li>
            </ul>

            <!-- Tab panes -->
            <div class="tab-content">
                <div id="liEncunesta" class="container tab-pane active">
                    <br />

                    <label>Tipo de encuesta : </label>
                    <asp:DropDownList ID="cmb_tipo_encuesta" runat="server"
                        CssClass="form-control" AutoPostBack="True"
                        OnSelectedIndexChanged="cmb_tipo_encuesta_SelectedIndexChanged">
                    </asp:DropDownList>

                    <label>Encuesta :</label>
                    <asp:DropDownList ID="cmb_encuesta" runat="server" CssClass="form-control">
                    </asp:DropDownList>
                </div>
                <div id="liUsuario" class="container tab-pane fade">
                    <br />
                    <label>Usuario : </label>
                    <asp:TextBox ID="txt_filtro_usuario" runat="server"
                        CssClass="form-control"></asp:TextBox>
                </div>
            </div>

            <br />
            <hr />


            <div class="container">
                <asp:Button ID="Button1" runat="server" Text="Filtrar" CssClass="btn btn-primary btn-lg btn-block" OnClick="Button1_Click" />
            </div>

            <hr />
            <br />

            <asp:GridView ID="dgrid_ver_reporte" runat="server" CssClass="table table-bordered"
                OnRowDataBound="dgrid_ver_reporte_RowDataBound"
                OnRowCommand="dgrid_ver_reporte_RowCommand"
                OnPageIndexChanging="dgrid_ver_reporte_PageIndexChanging">
                <Columns>
                    <asp:ButtonField CommandName="Select" Text="Select"
                        ItemStyle-HorizontalAlign="Center"></asp:ButtonField>
                    <asp:ButtonField CommandName="VerRespuesta" Text="Ver Respuesta">
                        <FooterStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:ButtonField>
                </Columns>


            </asp:GridView>

        </div>
    </div>
</asp:Content>
