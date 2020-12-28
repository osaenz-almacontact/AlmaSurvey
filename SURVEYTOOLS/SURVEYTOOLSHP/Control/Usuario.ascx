<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Usuario.ascx.cs" Inherits="SURVEYTOOLSHP.Control.Usuario" %>
<div class="row">
    <div class="col-sm-7">
        <strong>
            <asp:Label ID="lbl_usuario" runat="server" ForeColor="White" Style="font-size: 12px;"></asp:Label>
        </strong>
        <asp:Label runat="server" ForeColor="White" Style="font-size: 15px;" Text="/"></asp:Label>
        <span class="navbar-text">
            <asp:Label ID="lbl_permiso" runat="server" ForeColor="White" Style="font-size: 12px;"></asp:Label>
        </span>
    </div>
    <div class="col-sm-5">
        <p style="margin: 5px 0px 0px 0px; text-align: left">
            <strong>
                <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-danger" OnClick="LinkButton1_Click">Cerrar Sesión</asp:LinkButton>
            </strong>
        </p>
    </div>
</div>


