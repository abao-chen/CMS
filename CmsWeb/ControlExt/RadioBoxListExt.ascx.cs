using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CmsWeb.ControlExt
{
    public partial class RadioBoxListExt : System.Web.UI.UserControl
    {
        //radiobox name
        public string Name = string.Empty;
        //是否横向排列，是：横向排列，否：纵向排列
        public bool IsInline = true;
        public string DataTextField = "Text";
        public string DataValueField = "Value";
        public object DataSource = null;
        //是否启用
        public bool Enabled = true;

        public string SelectedValue
        {
            get { return hidSelectedValue.Value; }
            set { hidSelectedValue.Value = value; }
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