<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RadioBoxListExt.ascx.cs" Inherits="CmsWeb.ControlExt.RadioBoxListExt" %>
<asp:Repeater ID="rpList" runat="server">
    <ItemTemplate>
        <% if (!IsInline)
            {%>
        <div class="radio">
            <%   }%>
            <label class='<%=IsInline?"radio-inline":"" %>'>
                <input type="radio" name="<%= Name %>" value="<%#Eval(DataValueField) %>"><%#Eval(DataTextField) %>
            </label>
            <% if (!IsInline)
                {%>
        </div>
        <%   }%>
    </ItemTemplate>
</asp:Repeater>
<asp:HiddenField ID="hidSelectedValue" runat="server" />

<!-- jQuery -->
<script src="/Scripts/bootstrap/vendor/jquery/jquery.min.js"></script>
<script type="text/javascript">
    $(function () {
        $("input[type='radio'][name='<%= Name %>']").click(function () {
            $("#<%=hidSelectedValue.ClientID%>").val($(this).val());
        });

        if ($("#<%=hidSelectedValue.ClientID%>").val() != "") {
            var selectedValue = $("#<%=hidSelectedValue.ClientID%>").val();
            $("input[type='radio'][name='<%= Name %>']").each(function () {
                if (selectedValue == $(this).val()) {
                    $(this).attr("checked", true);
                }
            });
        }
    });
</script>
