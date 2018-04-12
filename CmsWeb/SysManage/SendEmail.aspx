<%@ Page Title="" Language="C#" ValidateRequest="false" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true" CodeBehind="SendEmail.aspx.cs" Inherits="Cms.Web.Admin.SysManage.SendEmail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    发送邮件
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="form-group">
                                <label>主题：</label>
                                <asp:TextBox ID="txtSubject" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-12">
                            <div class="form-group">
                                <label>收件人：</label>
                                <asp:TextBox ID="txtAddressee" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-12">
                            <div class="form-group">
                                <label>抄送：</label>
                                <asp:TextBox ID="txtCC" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-lg-12">
                            <div class="form-group">
                                <label>正文：</label>
                                <Cms:EditorExt runat="server" ID="txtBody" />
                            </div>
                        </div>
                        <div class="col-lg-12">
                            <asp:Button ID="btnSend" runat="server" Text="发送邮件" CssClass="btn btn-default" OnClick="btnSend_OnClick" OnClientClick="return getEmailBody();" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <script type="text/javascript">
        $(function () {
            $('#form').bootstrapValidator({
                message: 'This value is not valid',
                feedbackIcons: {
                    valid: 'glyphicon glyphicon-ok',
                    invalid: 'glyphicon glyphicon-remove',
                    validating: 'glyphicon glyphicon-refresh'
                },
                fields: {
                    "<%=txtSubject.UniqueID%>": {
                        validators: {
                            notEmpty: {}
                        }
                    },
                    "<%=txtAddressee.UniqueID%>": {
                        validators: {
                            notEmpty: {}
                        }
                    },
                    "<%=txtBody.UniqueID%>": {
                        validators: {
                            notEmpty: {}
                        }
                    }
                }
            });
        });
    </script>
</asp:Content>
