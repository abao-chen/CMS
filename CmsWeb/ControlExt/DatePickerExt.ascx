<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DatePickerExt.ascx.cs" Inherits="CmsWeb.ControlExt.DatePickerExt" %>
<!-- datetimepicker -->
<script src="/Scripts/bootstrap/vendor/datetimepicker/js/bootstrap-datetimepicker.min.js"></script>
<script src="/Scripts/bootstrap/vendor/datetimepicker/js/locales/bootstrap-datetimepicker.zh-CN.js" charset="UTF-8"></script>
<div class="input-group date form_date" data-date-format="<%=Format.ToLower() %>" data-link-field="<%=hidDate.ClientID %>">
    <input type="text" id="<%=this.ID %>" name="<%=this.Name %>" placeholder="<%=this.PlaceHolder %>" searchattr="<%=this.SearchAttr %>" class="form-control" readonly="readonly" />
    <asp:HiddenField ID="hidDate" runat="server" />
    <span class="input-group-addon"><span class="glyphicon glyphicon-remove"></span></span>
    <span class="input-group-addon"><span class="glyphicon glyphicon-th"></span></span>
</div>
<script type="text/javascript">
    $(function () {
        $(".form_date[data-link-field='<%=hidDate.ClientID %>']").datetimepicker({
            language:"zh-CN",
            weekStart: 1,
            todayBtn: 1,
            autoclose: 1,
            todayHighlight: 1,
            startView: 2,
            forceParse: 0,
            minView: 2
        }).on("hide", function (event) {
            event.preventDefault();
            event.stopPropagation();
        });

        var hidValue = $("#<%= hidDate.ClientID%>").val();
        if (hidValue && hidValue != "") {
            $("#<%=this.ID%>").val(hidValue);
        }
    });
</script>
