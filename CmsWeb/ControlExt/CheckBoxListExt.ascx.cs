using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CmsUtils;

namespace CmsWeb.ControlExt
{
    public partial class CheckBoxListExt : System.Web.UI.UserControl
    {
        //checkbox name
        public string Name = string.Empty;
        //是否横向排列，是：横向排列，否：纵向排列
        public bool IsInline = true;
        public string DataTextField = "Text";
        public string DataValueField = "Value";
        public object DataSource = null;

        public string SelectedValue
        {
            get { return hidSelectedValue.Value; }
            set { hidSelectedValue.Value = value; }
        }

        public string[] SelectedValueArray
        {
            get
            {
                if (hidSelectedValue.Value.IsEmpty())
                {
                    return hidSelectedValue.Value.Split(new string[] { "," }, StringSplitOptions.None);
                }
                else
                {
                    return null;
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.rpList.DataSource = DataSource;
                this.rpList.DataBind();
            }
        }

    }
}