using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace CmsUtils
{
    public static class ControlUtil
    {
        /// <summary>
        /// 绑定控件数据源
        /// </summary>
        /// <param name="control">控件对象</param>
        /// <param name="dataSource">数据源</param>
        /// <param name="isAddBlank">是否增加空选项，默认为false</param>
        public static void BindListControl(ListControl control, List<ItemList> dataSource, bool isAddBlank = false)
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

        /// <summary>
        /// 绑定控件数据源
        /// </summary>
        /// <param name="control">控件对象</param>
        /// <param name="dataSource">数据源</param>
        /// <param name="textField">文本字段</param>
        /// <param name="valueField">值字段</param>
        /// <param name="isAddBlank">是否增加空选项，默认为false</param>
        public static void BindListControl<T>(ListControl control, List<T> dataSource, string textField, string valueField, bool isAddBlank = false)
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