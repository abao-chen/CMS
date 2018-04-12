<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Cms.Web.Admin.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="description" content="" />
    <meta name="author" content="" />

    <title>CMS-Login</title>

    <!-- Bootstrap Core CSS -->
    <link href="~/Scripts/bootstrap/vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet" />

    <!-- MetisMenu CSS -->
    <link href="~/Scripts/bootstrap/vendor/metisMenu/metisMenu.min.css" rel="stylesheet" />
    <!-- bootstrapValidator -->
    <link href="~/Scripts/bootstrap/vendor/bootstrap-validator/css/bootstrapValidator.css" rel="stylesheet" />
    <!-- toastr -->
    <link href="~/Scripts/bootstrap/vendor/toastr/css/toastr.min.css" rel="stylesheet" />
    <link href="~/Scripts/bootstrap/vendor/toastr/css/toastr.less" rel="stylesheet" />
    <link href="~/Scripts/bootstrap/vendor/toastr/css/toastr.scss" rel="stylesheet" />

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
    <form id="form" runat="server">

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
                                    <asp:TextBox ID="txtAccount" runat="server" class="form-control" placeholder="用户账号" name="email" Text="" autofocus></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <asp:TextBox runat="server" ID="txtPassword" class="form-control" placeholder="用户密码" name="password" type="password" Text=""></asp:TextBox>
                                </div>
                                <div class="form-group input-group" style="display: none;">
                                    <asp:TextBox runat="server" ID="txtValidateCode" class="form-control" placeholder="验证码" name="validateCode" Text="1"></asp:TextBox>
                                    <span class="input-group-btn">
                                        <asp:Image ID="ImgValidateCode" runat="server" ImageUrl="~/API/ValidateCodeApi.aspx" ToolTip="点击刷新" />
                                    </span>
                                </div>
                                <div class="checkbox">
                                    <label>
                                        <asp:CheckBox ID="cbxRemeber" runat="server"  Text="Remember Me"/>
                                        <%--<input name="remember" type="checkbox" value="Remember Me" />Remember Me--%>
                                    </label>
                                </div>
                                <!-- Change this to a button or input when using this as a form -->
                                <asp:Button ID="btnLogin" runat="server" Text="Login" class="btn btn-lg btn-success btn-block" data-loading-text="登陆中..." OnClick="btnLogin_Click" />
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

        <!-- bootstrapValidator -->
        <script src="/Scripts/bootstrap/vendor/bootstrap-validator/js/bootstrapValidator.js"></script>
        <script src="/Scripts/bootstrap/vendor/bootstrap-validator/js/language/zh_CN.js"></script>

        <!-- toastr -->
        <script src="/Scripts/bootstrap/vendor/toastr/js/toastr.min.js"></script>

        <!-- Metis Menu Plugin JavaScript -->
        <script src="/Scripts/bootstrap/vendor/metisMenu/metisMenu.min.js"></script>

        <!-- Custom Theme JavaScript -->
        <script src="/Scripts/bootstrap/dist/js/sb-admin-2.js"></script>
        <script type="text/javascript">
            //表单验证
            $(function () {
                $('#form').bootstrapValidator({
                    message: 'This value is not valid',
                    feedbackIcons: {
                        valid: 'glyphicon glyphicon-ok',
                        invalid: 'glyphicon glyphicon-remove',
                        validating: 'glyphicon glyphicon-refresh'
                    },
                    fields: {
                        <%=txtAccount.UniqueID%>: {
                            validators: {
                                notEmpty: {}
                            }
                        },
                        <%=txtPassword.UniqueID%>: {
                            validators: {
                                notEmpty: {}
                            }
                        },
                        <%=txtValidateCode.UniqueID%>: {
                            validators: {
                                notEmpty: {}
                            }
                        }
                    }
                });

                $(document).keydown(function(event){   
                    if (event.keyCode == 13) {     
                        $('form').each(function() {       
                            event.preventDefault();     
                        });
                        $("#<%=btnLogin.ClientID%>").click();
                    }
                });
            });

            $(function () {
                $("#<%=ImgValidateCode.ClientID%>").click(function () {
                    var imgSrc = "/API/ValidateCodeApi.aspx?rnd=" + new Date().getTime();
                    $(this).attr("src", imgSrc);
                });
            });
        </script>
    </form>
</body>
</html>
