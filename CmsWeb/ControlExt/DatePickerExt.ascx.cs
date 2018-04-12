using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CmsUtils;

namespace Cms.Web.Admin.ControlExt
{
    public partial class DatePickerExt : System.Web.UI.UserControl
    {
        public string Name = string.Empty;
        public string PlaceHolder = string.Empty;
        public string Format = "yyyy/MM/dd";
        public string SearchAttr = string.Empty;

        public string TextValue
        {
            get { return this.hidDate.Value; }
            set { this.hidDate.Value = value; }
        }

        public DateTime? DateValue
        {
            get
            {
                if (!hidDate.Value.IsEmpty())
                {
                    return Convert.ToDateTime(this.hidDate.Value);
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (value != null)
                {
                    this.hidDate.Value = Convert.ToDateTime(value).ToString(Format);
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}