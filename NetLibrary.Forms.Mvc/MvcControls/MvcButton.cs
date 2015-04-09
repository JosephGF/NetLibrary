using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetLibrary.Forms.Mvc.MvcControls
{
    public class MvcButton : System.Windows.Forms.Button, IMvcButton
    {
        public Control GetControl() { return this; }

        public void OnCreate(View view)
        {
            
        }
    }
}
