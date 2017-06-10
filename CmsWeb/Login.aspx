<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="CmsWeb.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="description" content="" />
    <meta name="author" content="" />

    <title>SB Admin 2 - Bootstrap Admin Theme</title>

    <!-- Bootstrap Core CSS -->
    <link href="~/Scripts/bootstrap/vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet" />

    <!-- MetisMenu CSS -->
    <link href="~/Scripts/bootstrap/vendor/metisMenu/metisMenu.min.css" rel="stylesheet" />

    <!-- Custom CSS -->
    <link href="~/Scripts/bootstrap/dist/css/sb-admin-2.css" rel="stylesheet" />

    <!-- Custom Fonts -->
    <link href="~/Scripts/bootstrap/vendor/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css" />

    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
        <script src="~/Scripts/bootstrap/js/html5shiv.js"></script>
        <script src="~/Scripts/bootstrap/js/respond.min.js"></script>
    <![endif]-->
</head>
<body>
    <form id="form1" runat="server">

        <div class="container">
            <div class="row">
                <div class="col-md-4 col-md-offset-4">
                    <div class="login-panel panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title">Please Sign In</h3>
                        </div>
                        <div class="panel-body">
                            <%--<form role="form">--%>
                                <fieldset>
                                    <div class="form-group">
                                        <asp:TextBox ID="txtEmail" runat="server" class="form-control" placeholder="E-mail" name="email" type="email" autofocus></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <asp:TextBox runat="server" ID="txtPassword" class="form-control" placeholder="Password" name="password" type="password" value=""></asp:TextBox>
                                    </div>
                                    <div class="checkbox">
                                        <label>
                                            <input name="remember" type="checkbox" value="Remember Me">Remember Me
                                       
                                        </label>
                                    </div>
                                    <!-- Change this to a button or input when using this as a form -->
                                    <asp:Button ID="btnLogin" runat="server" Text="Login" class="btn btn-lg btn-success btn-block" OnClick="btnLogin_Click" />
                                </fieldset>
                            <%--</form>--%>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- jQuery -->
        <script src="/Scripts/bootstrap/vendor/jquery/jquery.min.js"></script>

        <!-- Bootstrap Core JavaScript -->
        <script src="/Scripts/bootstrap/vendor/bootstrap/js/bootstrap.min.js"></script>

        <!-- Metis Menu Plugin JavaScript -->
        <script src="/Scripts/bootstrap/vendor/metisMenu/metisMenu.min.js"></script>

        <!-- Custom Theme JavaScript -->
        <script src="/Scripts/bootstrap/dist/js/sb-admin-2.js"></script>
    </form>
</body>
</html>
