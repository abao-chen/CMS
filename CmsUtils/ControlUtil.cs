﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            control.Items.Insert(0, new ListItem("请选择", string.Empty));
        }
    }

    /// <summary>
    /// DropDownList数据源实体
    /// </summary>
    public class ItemList
    {
        public ItemList()
        {
        }

        public ItemList(string text, string value)
        {
            this.Text = text;
            this.Value = value;
        }

        public string Text { get; set; }
        public string Value { get; set; }
    }
}
