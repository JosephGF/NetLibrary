using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetLibrary.Forms.Mvc
{
    public interface IMvcControl
    {
        void OnCreate(View view);
        Control GetControl();
    }
}
