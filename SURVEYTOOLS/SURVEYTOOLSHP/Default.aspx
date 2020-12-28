<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SURVEYTOOLSHP.Defualt" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>LOGIN</title>

    <!-- Bootstrap core CSS -->
    <link href="App_Themes/Default/CSS/bootstrap.min.css" rel="stylesheet" />
    <!-- Theme -->
    <link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon" />
    <link href="App_Themes/Default/LoginStyle.css" rel="stylesheet" />
    <link href="https://fonts.googleapis.com/css2?family=Material+Icons" rel="stylesheet" />
</head>
<body>
    <style>
        footer {
            text-align: center;
            font-family: sans-serif;
            color: whitesmoke;
            width: 100%;
            width: 100%;
            bottom: 0;
            position: fixed;
        }
    </style>
    <div id="layoutAuthentication">
        <div id="layoutAuthentication_content">
            <main>
                <div class="container-fluid">
                    <div class="row no-gutter">
                        <div class="col-md-4 col-lg-6 bg-image oscurecer">
                            <div class="row">
                                <img src="App_Themes/Images/logo.png" class="rounded mx-auto d-block" style="max-height: 100px; margin-top: 400px" />
                            </div>

                        </div>

                        <div class="col-md-8 col-lg-6">
                            <div class="login d-flex align-items-center py-5">
                                <div class="container">


                                    <form id="form1" runat="server">
                                        <div class="row">
                                            <div class="col-md-9 col-lg-8 mx-auto">
                                                <asp:Panel ID="pnl_Login" runat="server">

                                                    <h3 class="login-heading mb-4">ALMA-SURVEY</h3>

                                                    <!-------------------------->
                                                    <div class="form-label-group">
                                                        <asp:TextBox ID="txt_Usuario" runat="server" class="form-control"></asp:TextBox>
                                                        <label class="small mb-1" for="txt_Usuario" style="margin-bottom: 4px">Usuairo</label>
                                                    </div>
                                                    <div class="form-label-group">

                                                        <asp:TextBox ID="txt_contrasenia" runat="server"
                                                            TextMode="Password" MaxLength="8" class="form-control"></asp:TextBox>
                                                        <label class="small mb-1" for="txt_contrasenia" style="margin-bottom: 4px">Password</label>
                                                    </div>
                                                    <div class="form-label-group">
                                                        <asp:Button ID="Button1"
                                                            runat="server" Text="Ingresar" CssClass="btn btn-lg btn-primary btn-block btn-login text-uppercase font-weight-bold mb-2" OnClick="Button1_Click" />

                                                    </div>
                                                </asp:Panel>
                                                <asp:Panel ID="pnl_Mensaje" runat="server" Visible="false">
                                                    <div class="alert alert-danger" role="alert">
                                                        <strong>ERROR DE AUTENTICACIÓN</strong>

                                                        <asp:Label ID="lbl_Mensaje" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
                                                    </div>

                                                </asp:Panel>
                                            </div>
                                        </div>

                                    </form>

                                </div>
                            </div>

                            <div id="layoutAuthentication_footer">
                                <footer class="py-4 bg-light mt-auto">
                                    <div class="container-fluid">
                                        <div class="d-flex align-items-center justify-content-between small">
                                            <div class="text-muted">
                                                <asp:Label ID="lbl_Pagina" runat="server" Text=""></asp:Label>
                                            </div>
                                            <div>
                                                <a href="#">Privacy Policy</a>
                                                &middot;
                                <a href="#">Terms &amp; Conditions</a>
                                            </div>
                                        </div>
                                    </div>
                                </footer>
                            </div>
                        </div>
                    </div>
                </div>
            </main>
        </div>
    </div>


</body>
</html>
