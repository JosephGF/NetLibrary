using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetLibrary.Forms.Mvc.MvcControls
{

    public class MvcInputTextBox : System.Windows.Forms.TextBox, IMvcInput
    {
        public Control GetControl() { return this; }

        public virtual bool Validate()
        {
            return true;
        }

        public string Value
        {
            get
            {
                return this.Text;
            }
            set
            {
                this.Text = value;
            }
        }

        public void OnCreate(View view)
        {
        }
    }
   
    public class MvcInputMaskedTextBox : System.Windows.Forms.MaskedTextBox, IMvcInput
    {
        public Control GetControl() { return this; }

        public virtual bool Validate()
        {
            return true;
        }

        public string Value
        {
            get
            {
                return this.Text;
            }
            set
            {
                this.Text = value;
            }
        }

        public void OnCreate(View view)
        {
        }
    }
    
    public class MvcInputRichTextBox : System.Windows.Forms.RichTextBox, IMvcInput
    {
        public Control GetControl() { return this; }

        public virtual bool Validate()
        {
            return true;
        }

        public string Value
        {
            get
            {
                return this.Text;
            }
            set
            {
                this.Text = value;
            }
        }


        public void OnCreate(View view)
        {
        }
    }
}
