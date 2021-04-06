<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Default.Master" AutoEventWireup="true" CodeBehind="ReporteRetiro.aspx.cs" Inherits="SURVEYTOOLSHP.Content.MenuPrincipal.AdministradorRespuestas.ReporteRetiro" EnableEventValidation="false" %>

<%@ Register Src="~/Control/Usuario.ascx" TagName="Usuario" TagPrefix="uc1" %>
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
        <script type="text/javascript">

            function FechaInicio() {

                var DateFechaInicio = document.getElementById('DataFechaInicio').value;
                document.getElementById('<%= TxtFechaInicio.ClientID %>').value = DateFechaInicio;

            }

            function FechaCierre() {

                var DateFechaFin = document.getElementById('DataFechaCierre').value;
                document.getElementById('<%= TxtFechaCierre.ClientID %>').value = DateFechaFin;

             }

        </script>



        <div class="form-row">
            <div class="col">
                <label>Eliga fase</label>
                <asp:DropDownList ID="cmb_fase" runat="server"
                    class="form-control"
                    AutoPostBack="True" OnSelectedIndexChanged="cmb_fase_SelectedIndexChanged">
                </asp:DropDownList>
            </div>
            <div class="col">
                <label>Eliga Operacion</label>
                <asp:DropDownList ID="cmb_operacion" runat="server"
                    class="form-control">
                </asp:DropDownList>
            </div>
            <div class="col">
                <label>Filtrar Site:</label>
                <asp:DropDownList ID="cmb_site" runat="server"
                    class="form-control">
                </asp:DropDownList>
            </div>
            <div class="col">
                <label>Filtrar Lider:</label>
                <asp:DropDownList ID="cmb_lider" runat="server"
                    class="form-control">
                </asp:DropDownList>
            </div>
            <div class="col">
                <label>Eliga Fecha Inicio</label>
                <input placeholder="Selected date" type="date" id="DataFechaInicio" class="form-control datepicker" onchange="FechaInicio()"/>
                <input placeholder="Selected date" type="text" id="TxtFechaInicio" runat="server" class="form-control datepicker" style="display: none"/>
            </div>
            <div class="col">
                <label>Eliga Fecha Cierre </label>
                <input placeholder="Selected date" type="date" id="DataFechaCierre" class="form-control datepicker" onchange="FechaCierre()"/>
                <input placeholder="Selected date" type="text" id="TxtFechaCierre" runat="server" class="form-control datepicker" style="display: none"/>
            </div>

        </div>
    </div>
    <br />
    <div class="form-row">
        <asp:Button ID="BtnFiltrar" runat="server" Text="Filtrar" CssClass="btn btn-primary btn-lg btn-block" OnClick="BtnFiltrar_Click" />
    </div>
    <hr />

    <div class="row">
        <div class="col-12">

            <asp:Panel ID="pnl_detalle_respuestas" runat="server" Visible="false">
                <div>
                    <table style="width: 100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="padding: 8px; text-align: center">

                                <p style="text-align: right; margin: 0px 0px 0px 0px; font-size: 12px" class="css_color_sub_titulo css_bold">
                                    Exportar a excel
                                            <asp:ImageButton ID="img_btn_exportar_excel" runat="server"
                                                Style="vertical-align: middle" Height="24px"
                                                ImageUrl="~/Resources/Iconos/logo excel.png" Width="24px"
                                                OnClick="img_btn_exportar_excel_Click" />

                                </p>


                                <div class="accordion" id="accordion">
                                    <asp:Repeater ID="RptPreguntas" runat="server" OnItemDataBound="RptRespuestas_ItemDataBound">
                                        <ItemTemplate>
                                            <div class="card">
                                                <div class="card-header" id="headingOne">
                                                    <h2 class="mb-0">
                                                        <asp:TextBox ID="TxtIdPregunta" Enabled="false" Visible="false" Text='<%#Eval("PREGUNTA") %>' runat="server"></asp:TextBox>
                                                        <button class="btn btn-link btn-block text-left collapsed" type="button" data-toggle="collapse" data-target="#<%#Eval("ID") %>" aria-expanded="true" aria-controls="<%#Eval("ID") %>">
                                                            <%--<button class="btn btn-link btn-block text-left" type="button" data-toggle="collapse" data-target="11" aria-expanded="true" aria-controls="<%#Eval("ID") %>">--%>
                                                            <%#Eval("PREGUNTA") %>
                                                        </button>
                                                    </h2>
                                                </div>

                                                <%--<div id="<%#Eval("ID") %>" class="collapse show" aria-labelledby="<%#Eval("ID") %>" data-parent="#accordion">--%>
                                                <div id="<%#Eval("ID") %>" class="collapse show" aria-labelledby="headingOne" data-parent="#accordion">
                                                    <div class="card-body">
                                                        <asp:Repeater ID="RptRespuestas" runat="server">
                                                            <HeaderTemplate>
                                                                <table class="table">
                                                                    <thead class="thead-light">
                                                                        <tr>
                                                                            <th scope="col">RESPUESTA</th>
                                                                            <th scope="col">CONTADOR</th>
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td><%#Eval("RESPUESTA") %></td>
                                                                    <td><%#Eval("CONTADOR") %></td>
                                                                </tr>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                </tbody>
                                                                    </table>
                                                            </FooterTemplate>
                                                            <SeparatorTemplate>, </SeparatorTemplate>
                                                        </asp:Repeater>
                                                    </div>
                                                </div>

                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>

                                </div>


                            </td>
                        </tr>
                    </table>
                </div>
            </asp:Panel>
            <br />
            <br />
        </div>
    </div>

</asp:Content>
