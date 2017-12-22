<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DataPickerExt.ascx.cs" Inherits="CmsWeb.ControlExt.DataPickerExt" %>
<!-- datetimepicker -->
<script src="/Scripts/bootstrap/vendor/datetimepicker/js/bootstrap-datetimepicker.min.js"></script>
<script type="text/javascript" src="/Scripts/bootstrap/vendor/datetimepicker/js/locales/bootstrap-datetimepicker.fr.js" charset="UTF-8"></script>
<div class="input-group date form_date" data-date-format="<%=Format.ToLower() %>" data-link-field="<%=hidDate.ClientID %>">
    <input type="text" id="<%=this.ID %>" name="<%=this.Name %>" placeholder="<%=this.PlaceHolder %>" SearchAttr="<%=this.SearchAttr %>" class="form-control" readonly="readonly"/>
    <asp:HiddenField ID="hidDate" runat="server" />
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