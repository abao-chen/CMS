using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CmsWeb.ControlExt
{
    public partial class EditorExt : System.Web.UI.UserControl
    {
        public string Text
        {
            get
            {
                return EditorValue?.Value;
            }
            set
            {
                if (this.EditorValue != null)
                {
                    this.EditorValue.Value = value;
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}