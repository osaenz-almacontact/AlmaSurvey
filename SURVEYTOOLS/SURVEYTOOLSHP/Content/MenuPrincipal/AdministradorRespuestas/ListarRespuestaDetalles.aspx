<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Default.Master" AutoEventWireup="true" CodeBehind="ListarRespuestaDetalles.aspx.cs" Inherits="SURVEYTOOLSHP.Content.MenuPrincipal.AdministradorRespuestas.ListarRespuestaDetalles" %>
<%@ Register src="../../../Control/Usuario.ascx" tagname="Usuario" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content_usuario" runat="server">
    <uc1:Usuario ID="Usuario2" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="content_body" runat="server">
    <div class="css_fuente_letra" style="padding:20px 50px 20px 50px;background-color:White;">
<p class="css_bold css_color_titulo css_tamaño_titulo_central" style="text-align:left">Reporte personal de respuestas 
    Para :  
    <asp:Label ID="txt_nombre_encuesta" runat="server" Text=""></asp:Label>
</p>
<asp:Panel ID="pnl_detalles" runat="server" Visible="true">
 
    
<table style="width:98%" cellpadding=0 cellspacing=0>

<tr>
<td>
    <hr color="#585964" />
    </td>
</tr>
<tr>
<td style="padding:10px;text-align:left;">
    <asp:Panel ID="pnl_Respuesta" runat="server" Visible="true">
        <div style="margin:10px 0px 0px 0px">
         <p style="text-align:right;margin:0px 0px 0px 0px;padding:0px 20px 0px 0x;font-size:12px" class="css_color_sub_titulo css_bold">
    Exportar a excel
        <asp:ImageButton ID="img_btn_exportar_excel" runat="server" 
            Style="vertical-align:middle" Height="24px" 
            ImageUrl="~/Resources/Iconos/logo excel.png" Width="24px" 
           onclick="img_btn_exportar_excel_Click"/>

    </p>
            <center>
                <asp:GridView ID="dgridRespuesta" runat="server" AllowPaging="True" 
                    CssClass="css_fuente_letra" GridLines="None" 
                    onpageindexchanging="dgridRespuesta_PageIndexChanging" 
                    onrowdatabound="dgridRespuesta_RowDataBound" PageSize="20" Width="98%" 
                    CellPadding="4" ForeColor="#333333">
                    <AlternatingRowStyle BackColor="White" />
                    <EditRowStyle BackColor="#2461BF" />
                    <EmptyDataRowStyle Font-Bold="True" Font-Size="20pt" ForeColor="#585964" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle CssClass="css_tamanio_descripcion" BackColor="#214023" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"  
                       />
                    <PagerStyle BackColor="#214023" Font-Bold="true" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#ECFDEF" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                </asp:GridView>
            </center>
        </div>
        <div class="css_border_simple" style="margin:20px 0px 0px 0px;padding:10px">
      <p style="margin:0px 0px 10px 0px;text-align:right">
      Ver por evaluador 
          <asp:ImageButton ID="img_btn_ver_respuestas_evaluados" runat="server" 
              Style="vertical-align:middle" Height="15px" Width="15px" 
              ImageUrl="~/Resources/Iconos/Add.png" 
              onclick="img_btn_ver_respuestas_evaluados_Click"/>
      </p>
      <div>
      <center>
          <asp:GridView ID="dgrid_evaluador_respuestas" runat="server" Width="100%" 
              CellPadding="4" ForeColor="#333333" GridLines="None" AllowPaging="True" Visible="false"
              PageSize="12" 
              onpageindexchanging="dgrid_evaluador_respuestas_PageIndexChanging">
              <AlternatingRowStyle BackColor="White" />
              <EditRowStyle BackColor="#2461BF" />
              <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
              <HeaderStyle BackColor="#214023" Font-Bold="True" ForeColor="White" />
              <PagerStyle BackColor="#214023" ForeColor="White" HorizontalAlign="Center" />
              <RowStyle BackColor="#ECFDEF" />
              <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
              <SortedAscendingCellStyle BackColor="#F5F7FB" />
              <SortedAscendingHeaderStyle BackColor="#6D95E1" />
              <SortedDescendingCellStyle BackColor="#E9EBEF" />
              <SortedDescendingHeaderStyle BackColor="#4870BE" />
          </asp:GridView>
      </center>
      
      </div>
        </div>
        <p class="css_bold css_color_sub_titulo css_tamanio_subtitulos" 
            style="text-align:right;padding:0px 10px 0px 0px;margin:10px 0px 10px 0px">
            TOTAL:
            <asp:Label ID="lbl_total_ponderacion_individual" runat="server" Text=""></asp:Label>
        </p>
        <hr size="5px" color="#A6A6A7"/>
        <div style="margin:0px;border:2px solid #A6A6A7">
            <center>
                <iframe src="http://bog03nmca/ReportServer/Pages/ReportViewer.aspx?%2fSURVEYHPTOOLS%2fSURVEY_RESPUESTA_SATISFACCION&amp;rs:Command=Render" 
                    style="width:98%; height: 726px;"></iframe>
            </center>
        </div>
    </asp:Panel>
    </td>
</tr>
</table>
   </asp:Panel>


    </div>
</asp:Content>
