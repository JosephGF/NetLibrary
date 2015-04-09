using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetLibrary.Win32
{
    public class Win32
    {
        public static bool IsUserAdministrator
        {
            get
            {
                bool isAdmin;
                try
                {
                    WindowsIdentity user = WindowsIdentity.GetCurrent();
                    WindowsPrincipal principal = new WindowsPrincipal(user);
                    isAdmin = principal.IsInRole(WindowsBuiltInRole.Administrator);
                }
                catch (UnauthorizedAccessException ex)
                {
                    isAdmin = false;
                }
                catch (Exception ex)
                {
                    isAdmin = false;
                }
                return isAdmin;
            }
        }

        public static void RestartAsAdmin()
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.UseShellExecute = true;
            startInfo.WorkingDirectory = Environment.CurrentDirectory;
            startInfo.FileName = Application.ExecutablePath;
            startInfo.Verb = "runas";
            try
            {
                Process p = Process.Start(startInfo);
            }
            catch (System.ComponentModel.Win32Exception ex)
            {
                return;
            }

            Application.Exit();
        }


        [DllImport("User32.Dll")]
        internal static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        internal static extern IntPtr GetWindow(IntPtr hwnd, long wCmd);

        [DllImport("user32", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        internal static extern IntPtr ShowWindow(IntPtr hWnd, long nCmdShow);

        [DllImport("user32", EntryPoint = "FindWindowExA", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        internal static extern IntPtr GetWindowEx(IntPtr hWnd1, IntPtr hWnd2, string lpsz1, string lpsz2);
        
        [DllImport("user32.dll")]
        internal static extern IntPtr EnableWindow(IntPtr hwnd, long fEnable);

        [DllImport("User32.Dll")]
        internal static extern IntPtr GetClassName(IntPtr hwnd, StringBuilder lpClassName, int nMaxCount);


        [DllImport("user32")]
        internal static extern IntPtr SendMessage(IntPtr hWnd, UInt32 msg, UInt32 wParam, UInt32 lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        internal static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        internal static extern IntPtr SendMessageTimeout(int hWnd, int Msg, int wParam, string lParam, int fuFlags, int uTimeout, out int lpdwResult);


        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        internal static extern IntPtr SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);
    }
}
