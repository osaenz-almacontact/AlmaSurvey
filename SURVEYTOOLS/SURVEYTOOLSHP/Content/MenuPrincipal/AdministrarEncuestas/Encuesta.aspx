<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Default.Master" AutoEventWireup="true" CodeBehind="Encuesta.aspx.cs" Inherits="SURVEYTOOLSHP.Content.MenuPrincipal.AdministrarEncuesta.Satisfaccion" %>

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
                <asp:Label ID="lbl_nombre_encuesta" runat="server" Text="" CssClass="css_fuente_letra"></asp:Label>
            </strong>
        </p>
        <hr />
        <div class="card">
            <asp:GridView ID="dgrid_Preguntas" runat="server"
                AllowPaging="True" PageSize="20" OnRowCommand="dgrid_Preguntas_RowCommand"
                EmptyDataText="NO FOUND" OnRowDataBound="dgrid_Preguntas_RowDataBound"
                OnPageIndexChanging="dgrid_Preguntas_PageIndexChanging"
                OnSelectedIndexChanged="dgrid_Preguntas_SelectedIndexChanged1" CssClass="table table-striped">
                <Columns>
                    <asp:ButtonField ButtonType="Link" CommandName="Editar" HeaderText="Editar"
                        Text='<i class="material-icons" style="color:#d39e00; margin-top:2px">autorenew</i>' ControlStyle-CssClass="btn btn-light btn-circle" />
                    <asp:ButtonField ButtonType="Link" CommandName="Deshabilitar" HeaderText="Deshabilitar"
                        Text='<i class="material-icons" style="color:#dc3545; margin-top:2px">highlight_off</i>' ControlStyle-CssClass="btn btn-light btn-circle"/>
                    <asp:ButtonField ButtonType="Link" CommandName="Habilitar" HeaderText="Habilitar"
                        Text='<i class="material-icons" style="color:#28a745; margin-top:2px">done_all</i>' ControlStyle-CssClass="btn btn-light btn-circle"/>
                </Columns>
                <%--<EditRowStyle BackColor="#2461BF" />
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
            <div style="padding: 2px; margin: 20px 0px 0px 0px; border: 2px solid #A6A6A7; text-align: right;">
                <p style="font-size: 20px; text-align: right; border: 1px solid #CFCFCF; color: #A6A6A7">
                    <strong>
                        <asp:LinkButton ID="LinkButton1" ForeColor="#A6A6A7" Font-Underline="false"
                            runat="server" OnClick="LinkButton1_Click">Adicionar Pregunta >></asp:LinkButton></strong>
                </p>
            </div>
        </div>

        <div style="width: 380px; position: fixed; top: 120px; right: 0px;" class="css_fuente_letra">
            <asp:Panel ID="pnl_VerCaracterisitacas_Preguntas" runat="server" Width="100%" Visible="false" BackColor="White">
                <div style="width: 100%; border: 2px solid #214023" cellpadding="0" cellspacing="0" class="card border-success">
                    <div class="card-header" style="text-align: center">
                        <b>
                            <asp:Label ID="lbl_titulo_editar_pregunta" runat="server" Text="Editar Pregunta"></asp:Label></b>
                    </div>
                    <div class="card-body">
                        <div class="form-row">
                            <div class="col-12">
                                <asp:Panel ID="pnl_pregunta" runat="server" Visible="true">
                                    <p style="margin: 0px 0px 10px 0px">
                                        <asp:Label ID="lbl_pregunta" runat="server" Text="Pregunta : "></asp:Label>
                                        <asp:TextBox ID="txt_pregunta" runat="server" Style="vertical-align: middle"
                                            Height="69px" Rows="5" TextMode="MultiLine" Width="280px" CssClass="form-control"></asp:TextBox>
                                    </p>
                                </asp:Panel>
                            </div>
                            <div class="col-12">
                                <label>Tipo de Pregunta : </label>
                                <asp:DropDownList ID="cmb_tipo_pregunta" runat="server"
                                    Style="vertical-align: middle" Width="170px" AutoPostBack="True"
                                    OnSelectedIndexChanged="cmb_tipo_pregunta_SelectedIndexChanged" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                            <div class="col-12">
                                <asp:Panel ID="pnl_red_cmb" runat="server" Visible="false">
                                    <asp:Label ID="lbl_Respuestas" runat="server" Text="Lista de respuestas :"></asp:Label>
                                    <asp:TextBox ID="txt_darRespuesta" runat="server" Style="vertical-align: middle"
                                        Height="83px" Width="353px" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                </asp:Panel>
                            </div>

                            <div class="col" style="margin-top: 10px">

                                <asp:Button ID="btnEnviarPregunta" runat="server" Text="Enviar" Width="151px"
                                    CssClass="btn btn-info"
                                    OnClick="btnEnviarPregunta_Click" />

                                <asp:Button ID="Button1" runat="server" Text="Ocultar" Width="151px"
                                    CssClass="btn btn-info"
                                    OnClick="Button1_Click" />

                            </div>
                        </div>
                    </div>
                </div>

            </asp:Panel>


        </div>
    </div>
</asp:Content>
