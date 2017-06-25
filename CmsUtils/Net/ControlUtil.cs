using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace CmsUtils
{
    public static class ControlUtil
    {
        public static void BindDropDownList(DropDownList control, List<ItemList> dataSource, bool isAddBlank)
        {
            control.DataTextField = "Text";
            control.DataValueField = "Value";
            control.DataSource = dataSource;
            control.DataBind();
            if (isAddBlank)
            {
                control.Items.Insert(0, new ListItem("请选择", string.Empty));
            }
        }

        public static void BindDropDownList<T>(DropDownList control, List<T> dataSource,string textField, string valueField, bool isAddBlank)
        {
            control.DataTextField = textField;
            control.DataValueField = valueField;
            control.DataSource = dataSource;
            control.DataBind();
            if (isAddBlank)
            {
                control.Items.Insert(0, new ListItem("请选择", string.Empty));
            }
        }
    }

    /// <summary>
    ///     DropDownList数据源实体
    /// </summary>
    public class ItemList
    {
        public ItemList()
        {
        }

        public ItemList(string text, string value)
        {
            Text = text;
            Value = value;
        }

        public string Text { get; set; }
        public string Value { get; set; }
    }
}