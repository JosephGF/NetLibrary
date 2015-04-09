using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetLibrary.Win32
{
    public class WinHotKeys
    {
	    public const int WM_HOTKEY = 0x0312;//hotkey message identifier
	    public const int HOTKEY_ID = 31197; //identify the hotkey instance

	    [DllImport("user32.dll", SetLastError = true)]
	    public static extern bool RegisterHotKey(
		    IntPtr hWnd, // handle to window    
		    int id, // hot key identifier    
		    KeyModifiers fsModifiers, // key-modifier options    
		    Keys vk    // virtual-key code    
		    );
	    [DllImport("user32.dll", SetLastError = true)]
	    public static extern bool UnregisterHotKey(
		    IntPtr hWnd, // handle to window    
		    int id      // hot key identifier    
		    );

        [Flags]
	    public enum KeyModifiers        //enum to call 3rd parameter of RegisterHotKey easily
	    {
		    None = 0,
		    Alt = 1,
		    Control = 2,
		    Shift = 4,
		    Windows = 8
	    }
	
	    public static bool setHotKey(IntPtr Handle, KeyModifiers Kmds, Keys key)
	    {
		    return RegisterHotKey(Handle, WinHotKeys.HOTKEY_ID, Kmds, key);
	    }
        public static bool unSetHotKey(IntPtr Handle)
	    {
		    return UnregisterHotKey(Handle, WinHotKeys.HOTKEY_ID);
	    }

	    /** Windows Forms override event */
        /*protected override void WndProc(ref Message message)
	    {
		    switch (message.Msg)
		    {
			    case WM_HOTKEY:
				    Keys key = (Keys)(((int)message.LParam >> 16) & 0xFFFF);
				    KeyModifiers modifier = (KeyModifiers)((int)message.LParam & 0xFFFF);
				    //put your on hotkey code here
				    MessageBox.Show("HotKey Pressed :" + modifier.ToString() + " " + key.ToString());
				    //end hotkey code
				    break;
		    }
		    base.WndProc(ref message);
	    }*/
	    //put this code in the onload method of your form
	    //setHotKey(this.Hamdler, KeyModifiers.Alt | KeyModifiers.Shift, Keys.S);
	    //and set up a form closed event and call
        //unSetHotKey(this.Hamdler);
    }
}
