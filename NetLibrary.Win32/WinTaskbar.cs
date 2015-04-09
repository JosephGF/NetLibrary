using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace NetLibrary.Win32
{
    public class WinTaskbar
    {
        private const int SW_HIDE = 0;
        private const int SW_SHOW = 1;

        protected static IntPtr Handle
        {
            get
            {
                return Win32.FindWindow("Shell_TrayWnd", "");
            }
        }

        private WinTaskbar()
        {
            // hide ctor
        }

        public static void Show()
        {
            Win32.ShowWindow(Handle, SW_SHOW);
        }

        public static void Hide()
        {
            Win32.ShowWindow(Handle, SW_HIDE);
        }
    }
}
