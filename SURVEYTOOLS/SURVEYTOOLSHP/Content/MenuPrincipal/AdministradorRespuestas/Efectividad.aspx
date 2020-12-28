<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Default.Master" AutoEventWireup="true" CodeBehind="Efectividad.aspx.cs" Inherits="SURVEYTOOLSHP.Content.MenuPrincipal.AdministradorRespuestas.Eficiencia" EnableEventValidation="false" %>

<%@ Register Src="../../../Control/Usuario.ascx" TagName="Usuario" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content_usuario" runat="server">
    <uc1:Usuario ID="Usuario2" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="content_body" runat="server">
    <div style="padding: 50px 20px 50px 20px; background-color: White" class="css_fuente_letra">
        <div>

            <table style="width: 95%" cellpadding="0px" cellspacing="0px">
                <tr>
                    <td style="width: 50%; text-align: center" class="css_tamaño_titulo_central css_bold css_color_letra_conFondo">Efectividad
                    </td>
                    <td style="width: 50%;"></td>
                </tr>
                <tr>
                    <td colspan="2" valign="top" style="padding: 10px" class="css_border_simple">
                        <p style="margin: 0px 0px 10px 0px">
                            <strong><a class="css_color_titulo css_tamanio_descripcion">Fase: </a></strong>
                            <asp:DropDownList ID="cmb_fase" runat="server"
                                Style="vertical-align: middle" Width="200px"
                                AutoPostBack="True" OnSelectedIndexChanged="cmb_fase_SelectedIndexChanged">
                            </asp:DropDownList>
                        </p>
                        <asp:Panel ID="pnl_con_data" runat="server" Visible="false">


                            <p style="margin: 0px 0px 10px 0px">
                                <strong><a class="css_color_titulo css_tamanio_descripcion">Eliga el tipo de encuesta: </a></strong>
                                <asp:DropDownList ID="cmb_tipo_encuesta" runat="server"
                                    Style="vertical-align: middle" Width="200px"
                                    OnSelectedIndexChanged="cmb_tipo_encuesta_SelectedIndexChanged"
                                    AutoPostBack="True">
                                </asp:DropDownList>
                            </p>

                            <p style="margin: 0px 0px 10px 0px; text-align: left" class="css_color_titulo css_tamanio_descripcion">
                                <strong>Filtrar por: </strong>
                                <asp:RadioButtonList ID="rdl_btn_filtro" runat="server"
                                    CssClass="css_bold css_fuente_letra" Style="color: Black;" AutoPostBack="True"
                                    OnSelectedIndexChanged="rdl_btn_filtro_SelectedIndexChanged"
                                    RepeatDirection="Horizontal">
                                </asp:RadioButtonList>
                            </p>
                            <asp:Panel ID="pnl_encueta" runat="server" Visible="false">
                                <p style="margin: 0px 0px 10px 0px">
                                    <strong><a class="css_color_titulo css_tamanio_descripcion">Eliga un valor:</a></strong>
                                    <asp:DropDownList ID="cmb_encuesta" runat="server"
                                        Style="vertical-align: middle" Width="200px"
                                        OnSelectedIndexChanged="cmb_encuesta_SelectedIndexChanged"
                                        AutoPostBack="True">
                                    </asp:DropDownList>
                                </p>
                            </asp:Panel>

                            <asp:Panel ID="pnl_isr_vista" runat="server" Visible="false">
                                <p style="margin: 0px 0px 10px 0px">
                                    <strong><a class="css_color_titulo css_tamanio_descripcion">Eliga un isr: </a></strong>
                                    <asp:DropDownList ID="cmb_isr" runat="server" Style="vertical-align: middle"
                                        Width="200px" AutoPostBack="True"
                                        OnSelectedIndexChanged="cmb_isr_SelectedIndexChanged1">
                                    </asp:DropDownList>
                                </p>

                            </asp:Panel>

                            <asp:Panel ID="pnl_ponderacion" runat="server" Visible="false">
                                <p style="margin: 10px 0px 0px 0px; text-align: left" class="css_color_titulo css_tamanio_subtitulos">
                                    El  
                                    <asp:Label ID="lbl_pondera_respondidos" runat="server" Text="" Font-Bold="true" ForeColor="#9B9C9D" Font-Size="25px"></asp:Label>
                                    de personas que han respondido la encuesta  
            <asp:Label ID="lbl_nom_encuesta" runat="server" Text="" CssClass="css_bold"></asp:Label>
                                    para 
            <asp:Label ID="lbl_nom_evaluado" runat="server" Text="" CssClass="css_bold"></asp:Label>
                                    . 
                                </p>
                            </asp:Panel>

                            <div style="margin: 20px 0px 0px 0px">
                                <center>
                                    <p style="text-align: right; margin: 0px 0px 0px 0px; font-size: 12px; padding: 0px 0px 0px 0px" class="css_color_sub_titulo css_bold">
                                        Exportar a excel
        <asp:ImageButton ID="img_btn_exportar_excel" runat="server"
            Style="vertical-align: middle" Height="24px"
            ImageUrl="~/Resources/Iconos/logo excel.png" Width="24px"
            OnClick="img_btn_exportar_excel_Click" />

                                    </p>
                                    <asp:GridView ID="dgrid_eficiencia" runat="server" Width="95%"
                                        AllowPaging="True" PageSize="20"
                                        OnRowCommand="dgrid_eficiencia_RowCommand"
                                        OnRowDataBound="dgrid_eficiencia_RowDataBound"
                                        OnPageIndexChanging="dgrid_eficiencia_PageIndexChanging" CellPadding="4"
                                        ForeColor="#333333" GridLines="None">

                                        <Columns>
                                            <asp:ButtonField CommandName="Select" Text="Enviar Correo" />
                                            <asp:ButtonField CommandName="ReenviarCorreo" Text="Reenviar Correo" />
                                            <asp:ButtonField CommandName="VerRespuesta" Text="Ver Respuesta" />
                                        </Columns>
                                        <AlternatingRowStyle BackColor="White" />
                                        <EditRowStyle BackColor="#2461BF" />
                                        <EmptyDataRowStyle Font-Bold="True" Font-Size="18px" ForeColor="Silver"
                                            HorizontalAlign="Center" CssClass="css_color_titulo css_fuente_letra" />
                                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle Font-Size="16px" CssClass="css_bold" BackColor="#214023" ForeColor="White"
                                            Font-Bold="True" />
                                        <PagerStyle CssClass="css_bold" BackColor="#214023" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#ECFDEF" />
                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                    </asp:GridView>

                                </center>
                            </div>


                            <asp:Panel ID="pnl_envio_correo" runat="server" Visible="false">

                                <p style="margin: 20px 0px 0px 0px; padding: 10px; border: 2px solid #A6A6A7; color: #A6A6A7; text-align: right" class="css_bold css_tamanio_subtitulos">
                                    Enviar correo a los usuario que faltan por responder esta encuesta 
        <asp:LinkButton ID="LinkButton1" runat="server" ForeColor="#A6A6A7"
            Font-Underline="true" Font-Bold="true" OnClick="LinkButton1_Click"> aquí >></asp:LinkButton>
                                </p>
                            </asp:Panel>
                        </asp:Panel>

                        <asp:Panel ID="pnl_view_noAsignado" runat="server" Visible="false">
                            <p class="css_color_sub_titulo css_tamanio_subtitulos css_bold" style="text-align: center">No se han ingresado los usuario que tiene que responder este tipo de encuestas</p>
                        </asp:Panel>
                    </td>

                </tr>
            </table>
        </div>
    </div>
</asp:Content>
