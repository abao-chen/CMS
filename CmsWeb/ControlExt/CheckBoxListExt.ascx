<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CheckBoxListExt.ascx.cs" Inherits="CmsWeb.ControlExt.CheckBoxListExt" %>
<asp:Repeater ID="rpList" runat="server">
    <ItemTemplate>
        <% if (!IsInline)
            {%>
        <div class="checkbox">
            <%   }%>
            <label class='<%=IsInline?"checkbox-inline":"" %>'>
                <input type="checkbox" id="<%=this.ID %>_<%#Eval(DataValueField) %>" name="<%= Name %>" value="<%#Eval(DataValueField) %>" <%= Enabled ?"":"disabled" %>><label style="font-weight: 400; padding-left: 0px;" for="<%=this.ID %>_<%#Eval(DataValueField) %>"><%#Eval(DataTextField) %></label>
            </label>
            <% if (!IsInline)
                {%>
        </div>
        <%   }%>
    </ItemTemplate>
</asp:Repeater>
<asp:HiddenField ID="hidSelectedValue" runat="server" />

<script type="text/javascript">
    $(function () {
        $("input[type='checkbox'][name='<%= Name %>']").click(function () {
            var selectedValue = "";
            $("input[type='checkbox'][name='<%= Name %>']").each(function () {
                if ($(this).is(":checked")) {
                    selectedValue += selectedValue == "" ? $(this).val() : "," + $(this).val();
                }
            });
            $("#<%=hidSelectedValue.ClientID%>").val(selectedValue);
        });

        if ($("#<%=hidSelectedValue.ClientID%>").val() != "") {
            var selectedItems = $("#<%=hidSelectedValue.ClientID%>").val().split(",");
            $("input[type='checkbox'][name='<%= Name %>']").each(function () {
                if (selectedItems.indexOf($(this).val()) >= 0) {
                    $(this).attr("checked", true);
                }
            });
        }
    });
</script>
