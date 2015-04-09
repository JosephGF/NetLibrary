using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetLibrary.Forms.Mvc.MvcControls
{
    public class MvcInputCheckBox : System.Windows.Forms.CheckBox, IMvcInput
    {
        public Control GetControl() { return this; }

        public bool Validate()
        {
            return true;
        }

        public string Value
        {
            get
            {
                return base.Checked.ToString();
            }
            set
            {
                base.Checked = Boolean.Parse(value);
            }
        }

        public void OnCreate(View view)
        {
        }
    }
}
