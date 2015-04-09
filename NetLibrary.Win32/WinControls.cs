using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetLibrary.Win32
{
    public class WinControls
    {

        internal const int BCM_FIRST = 0x1600; //Normal button
        internal const int BCM_SETSHIELD = (BCM_FIRST + 0x000C); //Elevated button
        public static void AddShieldToButton(Button b)
        {
            b.FlatStyle = FlatStyle.System;
            Win32.SendMessage(b.Handle, BCM_SETSHIELD, 0, 0xFFFFFFFF);
        }
    }
}
