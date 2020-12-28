<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Default.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="SURVEYTOOLSHP.Default" %>

<%@ Register Src="../Control/Usuario.ascx" TagName="Usuario" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content_usuario" runat="server">
    <uc1:Usuario ID="Usuario2" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="content_body" runat="server">
    <br />
    <div class="container-fluid">

        <p style="text-align: left;" class="css_color_titulo css_tamaño_titulo_central"><strong>SURVEY TOOLS</strong></p>
        <hr />
        <p style="text-align: left; margin: 0px 0px 30px 0px" class="css_color_sub_titulo css_tamanio_subtitulos">
            <strong>Bienvenido: </strong>
            <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
        </p>
        <div class="card border-secondary">

            <div style="padding: 0px 10px 20px 10px; text-align: justify; margin: 0px 0px 20px 0px" class="css_border_simple">
                <%--<p style="font-size: 17px" class="css_color_sub_titulo">
                    <a style="font-size: 20px; margin-top: 0px" class="css_color_titulo;"><strong>Estimado Colaborador </strong></a>Solicitamos su ayuda diligenciando el siguiente cuestionario, cuyos resultados pretenden conocer su nivel de satisfacción frente a los servicios ofrecidos y la calidad del servicio prestado. La información que nos proporcionara será utilizada para la mejora de nuestro servicio. <strong>Sus repuestas serán confidenciales.</strong>
                </p>--%>
                <p style="font-size: 17px" class="css_color_sub_titulo">
                    <a style="font-size: 20px; margin-top: 0px" class="css_color_titulo;"><strong>Estimado Colaborador </strong></a>Para ALMACONTACT es muy importante conocer su opinión, pues ello nos permite comprender las fortalezas de nuestra gestión e identificar las oportunidades de mejora de nuestros procesos.
                    La información suministrada es de carácter <strong>estrictamente confidencial </strong>y sus respuestas serán analizadas en conjunto con la del resto de los colaboradores. <br />
                    Le agradecemos de antemano su amable disposición y honestidad frente a este ejercicio.
                </p>
                <asp:Panel ID="pnl_encuestas_pendientes" runat="server" Visible="true">
                    <p style="font-size: 17px; margin: 0px 0px 10px 0px" class="css_color_titulo">
                        <strong>Acontinuación se presenta las encuestas a solucionar:</strong>
                    </p>
                    <div class="form-row">
                        <div class="col-md-4">
                            <label>Eliga una fase: </label>
                            <asp:DropDownList ID="cmb_fase" runat="server" CssClass="form-control"
                                AutoPostBack="True" OnSelectedIndexChanged="cmb_fase_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                        <asp:Panel ID="pnl_encuestas_tipos" runat="server" Visible="true" CssClass="col-md-8">
                            <div class="col">
                                <label>Eliga el tipo de encuesta:&nbsp;&nbsp; </label>
                                <asp:DropDownList ID="cmb_tipo_encuesta" runat="server"
                                    CssClass="form-control"
                                    AutoPostBack="True"
                                    OnSelectedIndexChanged="cmb_tipo_encuesta_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                            <div class="col">
                                <label>Eliga la encuesta a contestar </label>
                                <asp:DropDownList ID="cmb_unidad_Negocio" runat="server" Style="vertical-align: middle" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                            <div class="col">
                                <br />
                                <asp:Button ID="btnEnviar" runat="server" Text="Contestar Encuesta" CssClass="btn btn-info"
                                    OnClick="btnEnviar_Click" />
                            </div>
                        </asp:Panel>


                        <asp:Panel ID="pnl_fase_editada" runat="server" Visible="false">
                            <p class="css_bold css_tamanio_subtitulos css_tamanio_descripcion css_color_sub_titulo" style="margin: 10px 0px 0px 10px; text-align: center">
                                Gracias por su colaboración, señor usuario no tiene encuestas pendientes para esta fase.
                            </p>
                        </asp:Panel>
                    </div>
                </asp:Panel>
                <asp:Panel ID="pnl_encuestas_no_pendientes" runat="server" Visible="false">
                    <p class="css_bold css_tamanio_subtitulos css_tamanio_descripcion css_color_sub_titulo" style="margin: 10px 0px 0px 10px; text-align: center">
                        Gracias por su colaboración, señor usuario no tiene encuestas pendientes por responder.
                    </p>

                </asp:Panel>


                <div style="padding: 2px; margin: 20px 0px 0px 0px; border: 2px solid #1B95E0; text-align: right;">
                    <p style="margin: 0px; text-align: right; color: #1B95E0" class="css_tamanio_subtitulos">
                        <asp:LinkButton ID="LinkButton1" runat="server" CssClass="css_fuente_letra"
                            Font-Bold="True" Font-Underline="False" ForeColor="#1B95E0"
                            OnClick="LinkButton1_Click">Ver encuestas por responder >></asp:LinkButton>
                    </p>
                </div>
                <br />
                <asp:Panel ID="pnl_EncuestasXResponder" runat="server" Visible="false">
                    <div class="card border-secondary">
                        <div class="card-header" style="text-align: center">
                            <b>TUS ENCUESTAS</b>
                        </div>
                        <div class="card-body">
                            <asp:GridView ID="dgrid_misEncuestas" runat="server" CssClass="table table-bordered"
                                OnRowCommand="dgrid_misEncuestas_RowCommand"
                                OnRowDataBound="dgrid_misEncuestas_RowDataBound">
                                <Columns>
                                    <asp:ButtonField CommandName="Responder" HeaderText="RESPONDER"
                                        Text="Responder">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:ButtonField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </asp:Panel>

            </div>


        </div>

    </div>


</asp:Content>
