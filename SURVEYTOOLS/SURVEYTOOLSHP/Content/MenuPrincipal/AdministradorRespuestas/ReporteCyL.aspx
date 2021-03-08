<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Default.Master" AutoEventWireup="true" CodeBehind="ReporteCyL.aspx.cs" Inherits="SURVEYTOOLSHP.Content.MenuPrincipal.AdministradorRespuestas.ReporteCyL" %>

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
                <label>Data CyL</label>
            </strong>
        </p>
        <hr />
        <div class="row">
            <div class="col-6">
                <div class="card">
                    <div class="card-header">
                        <div class="form-row">
                            <div class="col" align='center'>
                                <h4>
                                    <label>Clima</label></h4>
                            </div>
                           
                        </div>

                    </div>
                    <div class="card-body">

                        <div class="row">
                            <div class="col-10">
                                <div class="progress">
                                    <div class="progress-bar bg-success" style="width: 0%" id="BarEncuestasRespondidas" runat="server">
                                        <asp:Label ID="LabProgreso" Text="text" runat="server" />
                                    </div>

                                    <div class="progress-bar bg-danger" style="width: 0%" id="BarEncuestasFaltantes" runat="server">
                                        <asp:Label ID="LabFaltante" Text="text" runat="server" />
                                    </div>

                                    <div class="progress-bar bg-danger" style="width: 0%" id="Div1" runat="server">
                                    </div>
                                </div>
                                <hr />
                                <div class="row">
                                    <div class="col-10">
                                        <h4>
                                            <label>Respondidas:</label></h4>
                                    </div>
                                    <div class="col-2" align='center'>
                                         <h5><asp:Label ID="LabRespondidas" Text="text" runat="server" /></h5>
                                    </div>
                                    <div class="col-10">
                                        <h4>
                                            <label>Por Responder:</label></h4>
                                    </div>
                                    <div class="col-2" align='center'>
                                        <h5><asp:Label ID="LabPorResponder" Text="text" runat="server" /></h5>
                                    </div>
                                </div>


                            </div>
                            <div class="col-2" align='center'>
                                <h1>
                                    <asp:Label ID="LabPorcentajeClima" Text="text" runat="server" /></h1>
                                <hr />
                                <h2>
                                    <asp:Label ID="LabTotalClima" Text="text" runat="server" /></h2>
                            </div>
                        </div>
                        <hr />
                        <div class="row">
                            <div class="col-12">
                                <asp:GridView ID="dgrid_encuestasClima" runat="server" CssClass="table table-bordered">
                                <Columns>
                                   
                                </Columns>
                            </asp:GridView>
                            </div>
                        </div>

                    </div>
                </div>
            </div>

            <div class="col-6">
                <div class="card">
                    <div class="card-header">
                        <div class="form-row">
                            <div class="col" align='center'>
                                <h4>
                                    <label>Liderazgo</label></h4>
                            </div>
                            
                        </div>

                    </div>
                    <div class="card-body">

                        <div class="row">
                            <div class="col-10">
                                <div class="progress">
                                    <div class="progress-bar bg-success" style="width: 0%" id="BarEncuestasRespondidasLid" runat="server">
                                        <asp:Label ID="LabProgresoLid" Text="text" runat="server" />
                                    </div>

                                    <div class="progress-bar bg-danger" style="width: 0%" id="BarEncuestasFaltantesLid" runat="server">
                                        <asp:Label ID="LabFaltanteLid" Text="text" runat="server" />
                                    </div>

                                    <div class="progress-bar bg-danger" style="width: 0%"  runat="server">
                                    </div>
                                </div>
                                <hr />
                                <div class="row">
                                    <div class="col-10">
                                        <h4>
                                            <label>Respondidas:</label></h4>
                                    </div>
                                    <div class="col-2" align='center'>
                                         <h5><asp:Label ID="LabRespondidasLid" Text="text" runat="server" /></h5>
                                    </div>
                                    <div class="col-10">
                                        <h4>
                                            <label>Por Responder:</label></h4>
                                    </div>
                                    <div class="col-2" align='center'>
                                        <h5><asp:Label ID="LabPorResponderLid" Text="text" runat="server" /></h5>
                                    </div>
                                </div>


                            </div>
                            <div class="col-2" align='center'>
                                <h1>
                                    <asp:Label ID="LabPorcentajeLiderazgo" Text="text" runat="server" /></h1>
                                <hr />
                                <h2>
                                    <asp:Label ID="LabTotalLiderazgo" Text="text" runat="server" /></h2>
                            </div>
                        </div>
                        <hr />
                        <div class="row">
                            <div class="col-12">
                                <asp:GridView ID="dgrid_encuestasLiderazgo" runat="server" CssClass="table table-bordered">
                                <Columns>
                                   
                                </Columns>
                            </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

    </div>
</asp:Content>
