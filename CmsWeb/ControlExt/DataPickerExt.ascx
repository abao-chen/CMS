<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DataPickerExt.ascx.cs" Inherits="CmsWeb.ControlExt.DataPickerExt" %>
<!-- jQuery -->
<script src="/Scripts/bootstrap/vendor/jquery/jquery.min.js"></script>
<!-- Bootstrap Core JavaScript -->
<script src="/Scripts/bootstrap/vendor/bootstrap/js/bootstrap.min.js"></script>
<!-- datetimepicker -->
<script src="/Scripts/bootstrap/vendor/datetimepicker/js/bootstrap-datetimepicker.min.js"></script>
<script type="text/javascript" src="/Scripts/bootstrap/vendor/datetimepicker/js/locales/bootstrap-datetimepicker.fr.js" charset="UTF-8"></script>
<div class="input-group date form_date" data-date-format="<%=Format.ToLower() %>" data-link-field="<%=hidDate.ClientID %>">
    <input type="text" id="<%=this.ID %>" name="<%=this.Name %>" placeholder="<%=this.PlaceHolder %>" class="form-control" readonly="readonly"/>
    <asp:HiddenField ID="hidDate" runat="server" />
    <%--<asp:TextBox runat="server" ID="txtDate" ReadOnly="True" CssClass="form-control" placeholder="<%=PlaceHolder %>"></asp:TextBox>--%>
    <span class="input-group-addon"><span class="glyphicon glyphicon-remove"></span></span>
    <span class="input-group-addon"><span class="glyphicon glyphicon-th"></span></span>
</div>
<script type="text/javascript">
    $(function() {
        $(".form_date").datetimepicker({
            weekStart: 1,
            todayBtn: 1,
            autoclose: 1,
            todayHighlight: 1,
            startView: 2,
            forceParse: 0,
            minView: 2
        });

        var hidValue = $("#<%= hidDate.ClientID%>").val();
        if (hidValue && hidValue != "") {
            $("#<%=this.ID%>").val(hidValue);
        }
    });
</script>