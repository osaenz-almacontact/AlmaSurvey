<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Default.Master" AutoEventWireup="true" CodeBehind="Encuesta.aspx.cs" Inherits="SURVEYTOOLSHP.Content.MenuPrincipal.Encuesta.Encuesta" %>

<%@ Register Src="../../../Control/Usuario.ascx" TagName="Usuario" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content_usuario" runat="server">
    <uc1:Usuario ID="Usuario2" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="content_body" runat="server">
    <script src="../../../Scripts/jquery-3.4.1.min.js"></script>
    <script>
        $(document).ready(function () {
            Horas = 0;
            Minutos = 30;
            Segundos = 0;
            countDown()

            function countDown() {
                Segundos = Segundos - 1;
                if (Segundos < 0) {
                    Segundos = 59;
                    Minutos = Minutos - 1;
                }

                document.getElementById("second").innerHTML = Segundos;
                if (Minutos < 0) {

                    Minutos = 59;
                    Horas = Horas - 1;
                }

                document.getElementById("minute").innerHTML = Minutos;
                document.getElementById("hour").innerHTML = Horas;

                if (Horas < 0) {

                    //final
                    document.getElementById("second").innerHTML = 0;
                    document.getElementById("minute").innerHTML = 0;
                    document.getElementById("hour").innerHTML = 0;
                }
                else {
                    setTimeout(countDown, 1000);
                }

                if (document.getElementById("second").innerHTML == 0 && document.getElementById("minute").innerHTML == 0 && document.getElementById("hour").innerHTML == 0) {
                    $('#<%=BtnCierreContador.ClientID%>').click();
                }
            }

        });
    </script>
    <button id="BtnCierreContador" runat="server" onserverclick="BtnCierreContador_ServerClick"></button>
    <br />
    <div class="container-fluid">
        <p style="text-align: left;" class="css_color_titulo css_tamaño_titulo_central">
            <strong>
                <label sclass="css_fuente_letra">RECUERDA:</label>
            </strong>
        </p>
        <hr />
        <div id="DivMensajeStandar" runat="server">
            <p class="css_tamanio_subtitulos css_color_sub_titulo" style="margin: 0px 0px 10px 0px; text-align: justify">
                Para el Almacontact conocer su opinión es una prioridad para  conocer qué piensa sobre su relación comercial y/o el servicio prestado y de esta manera retroalimentar la gestión interna. Le agradecemos su objetividad y disposición para contestar la encuesta.
            </p>
            <p class="css_tamanio_subtitulos css_color_sub_titulo" style="margin: 0px 0px 10px 0px; text-align: justify">
                La información que nos proporcionara será utilizada para la mejora de nuestro servicio.  
            </p>
            <p class="css_color_sub_titulo" style="margin: 0px 0px 40px 0px; text-align: justify; font-size: 20px">
                <strong>Tus repuestas serán confidenciales.</strong>
            </p>
        </div>
        <div id="DivMensajeLiderazgo" runat="server">
            <p class="css_tamanio_subtitulos css_color_sub_titulo" style="margin: 0px 0px 10px 0px; text-align: justify">
                <b>Estimado Colaborador,</b><br />
                Teniendo en cuenta la experiencia vivida en ALMACONTACT con su líder inmediato, le agradecemos compartir sus impresiones de acuerdo con las siguientes afirmaciones.<br />
                En este ejercicio no existen respuestas buenas o malas. Por favor lea atentamente el contenido de cada afirmación y señale de forma rápida su nivel de acuerdo con cada una.<br />
            </p>
            <p class="css_color_sub_titulo" style="margin: 0px 0px 40px 0px; text-align: justify; font-size: 20px">
                <strong>Sus repuestas serán estrictamente confidenciales.</strong>
            </p>
        </div>
        <div id="DivMensajeAutoevaluacion" runat="server">
            <p class="css_tamanio_subtitulos css_color_sub_titulo" style="margin: 0px 0px 10px 0px; text-align: justify">
                <b>Estimado Colaborador,</b><br />
                Teniendo en cuenta su experiencia como líder en ALMACONTACT, le agradecemos compartir las percepciones que tiene de sí mismo en relación con su rol frente a las personas y equipos de trabajo que usted lidera.<br />
                En este ejercicio no existen respuestas buenas o malas. Esta valoración es estrictamente confidencial y objetiva, cuyo propósito es únicamente conocer su percepción.<br />
                Por favor lea atentamente el contenido de cada afirmación y señale de forma rápida su nivel de acuerdo con cada una.

            </p>
            <p class="css_color_sub_titulo" style="margin: 0px 0px 40px 0px; text-align: justify; font-size: 20px">
                <strong>Sus repuestas serán estrictamente confidenciales.</strong>
            </p>
        </div>
        <div id="DivMensajeClima" runat="server">
            <p class="css_tamanio_subtitulos css_color_sub_titulo" style="margin: 0px 0px 10px 0px; text-align: justify">
                <b>Estimado Colaborador,</b><br />
                Gracias por compartir sus impresiones acerca de los aspectos relacionados a continuación. 
                Para ALMACONTACT es muy importante conocer su opinión, pues ello nos permite comprender las fortalezas de nuestra gestión e identificar las oportunidades de mejora de nuestros procesos para mantener un clima de trabajo agradable para todos.
                <br />
                La información suministrada por usted será de carácter estrictamente confidencial y sus respuestas serán analizadas en conjunto con la del resto de los colaboradores.<br />
                En este ejercicio no existen respuestas buenas o malas. Por favor lea atentamente el contenido de las afirmaciones y señale de forma rápida su nivel de acuerdo con cada una.

            </p>
            <p class="css_color_sub_titulo" style="margin: 0px 0px 40px 0px; text-align: justify; font-size: 20px">
                <strong>Muchas gracias.</strong>
            </p>
        </div>
        <div id="DivMensajeRetiro" runat="server">
            <p class="css_tamanio_subtitulos css_color_sub_titulo" style="margin: 0px 0px 10px 0px; text-align: justify">
                Gracias por permitirnos conocer tu opinión, el propósito de esta encuesta es obtener la mayor información posible para poder implementar acciones de mejoras de nuestros procesos internos. 
                Por favor responde de la forma mas sincera posible las siguientes preguntas. 
                Te recordamos que no existen respuestas buenas o malas y que los datos proporcionados son completamente personales y confidenciales.
            </p>
            <p class="css_color_sub_titulo" style="margin: 0px 0px 40px 0px; text-align: justify; font-size: 20px">
                <strong>Sus repuestas serán confidenciales.</strong>
            </p>
        </div>
        <div style="position: fixed; width: 300px; top: 205px; right: 0px; z-index: 2;" class="css_fuente_letra">
            <asp:Panel ID="pnl_agregar_isr" runat="server">
                <div style="width: 100%; background-color: White;" class="card text-white bg-warning">
                    <div class="card-body">

                        <div class="timer">
                            <div>
                                <span class="hours" id="hour"></span>
                                <div class="smalltext">Horas</div>
                            </div>
                            <div>
                                <span class="minutes" id="minute"></span>
                                <div class="smalltext">Minutos</div>
                            </div>
                            <div>
                                <span class="seconds" id="second"></span>
                                <div class="smalltext">Segundos</div>
                            </div>
                            <p id="time-up"></p>
                        </div>
                    </div>

                </div>

            </asp:Panel>
        </div>



        <div class="card border-light">
            <div class="card-header">
                <b>
                    <asp:Label ID="lbl_nombre_encuesta" runat="server" Text=""></asp:Label>
                </b>
            </div>
            <div class="card-body">
                <asp:Panel ID="pnl_Encuesta" runat="server">
                    <div class="form-row">
                        <div class="col">
                            <label>Fase</label>
                            <asp:DropDownList
                                ID="cmb_fase" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                        <div class="col">
                            <label>Site</label>
                            <asp:DropDownList
                                ID="cmb_site" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                        <div class="col">
                            <label>Operacion</label>
                            <asp:DropDownList
                                ID="cmb_operacion" runat="server" CssClass="form-control" OnSelectedIndexChanged="cmb_operacion_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>
                        </div>
                        <div class="col">
                            <label>Servicio</label>
                            <asp:DropDownList
                                ID="cmb_servicio" runat="server" CssClass="form-control" OnSelectedIndexChanged="cmb_servicio_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>
                        </div>
                        <div class="col">
                            <asp:Label ID="lbl_evaluado" Text="" runat="server" />
                            <asp:DropDownList
                                ID="cmb_cagar_combo_evaluado" runat="server" CssClass="form-control" Style="margin-top: 7px">
                            </asp:DropDownList>
                        </div>

                    </div>
                    <br />
                    <asp:Table ID="Table1" runat="server" CssClass="table">
                    </asp:Table>
                    <p style="margin: 5px 0px 0px 0px; text-align: left;">
                        <asp:Button ID="btnEnviarFormulario" runat="server"
                            Text="Enviar" CssClass="btn btn-info"
                            OnClick="btnEnviarFormulario_Click" />
                    </p>

                </asp:Panel>

            </div>
        </div>

    </div>

</asp:Content>
