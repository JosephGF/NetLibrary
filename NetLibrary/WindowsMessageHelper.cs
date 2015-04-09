using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace NetLibrary
{
    public class WindowsMessageHelper
    {
        #region Platform Invoke

        public const int WM_COPYDATA = 0x004A;

        private struct CopyDataStruct : IDisposable
        {
            public IntPtr dwData;
            public int cbData;
            public IntPtr lpData;

            public void Dispose()
            {
                if (lpData != IntPtr.Zero)
                {
                    LocalFree(this.lpData);
                    lpData = IntPtr.Zero;
                }
            }
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int SendMessage(IntPtr hWnd, int wMsg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, ref CopyDataStruct lParam);

        [DllImport("user32.dll")]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        private static extern int RegisterWindowMessage(string msgName);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr LocalAlloc(int flag, int size);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr LocalFree(IntPtr p);

        #endregion

        public static string MainFormName { get; set; }

        public static Dictionary<int, string> WindowMessages { get; set; }
        public const string COMMAND_PREFIX = "__JumpListCommand:";

        static WindowsMessageHelper()
        {
            WindowMessages = new Dictionary<int, string>();
        }

        public static int RegisterCommand(string commandName)
        {
            int command = RegisterWindowMessage(commandName);
            WindowMessages[command] = commandName;
            return command;
        }

        public static bool SendMessage(IntPtr handle, int msgId)
        {
            if (handle == IntPtr.Zero)
            {
                handle = FindWindow(null, MainFormName);
                if (handle == IntPtr.Zero) return false;
            }

            long result = SendMessage(handle, msgId, IntPtr.Zero, IntPtr.Zero);

            if (result == 0) return true;
            else return false;
        }

        public static bool SendMessage(IntPtr handle, string args)
        {
            if (handle == IntPtr.Zero)
            {
                handle = FindWindow(null, MainFormName);
                if (handle == IntPtr.Zero) return false;
            }

            WindowsMessageHelper.CopyDataStruct cds = new WindowsMessageHelper.CopyDataStruct();
            try
            {
                cds.cbData = (args.Length + 1) * 2;
                cds.lpData = WindowsMessageHelper.LocalAlloc(0x40, cds.cbData);
                Marshal.Copy(args.ToCharArray(), 0, cds.lpData, args.Length);
                cds.dwData = (IntPtr)1;
                WindowsMessageHelper.SendMessage(handle, WindowsMessageHelper.WM_COPYDATA, IntPtr.Zero, ref cds);
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                cds.Dispose();
            }
        }

        public static string GetArguments(IntPtr lParam)
        {
            string arguments = null;
            try
            {
                CopyDataStruct st = (CopyDataStruct)Marshal.PtrToStructure(lParam, typeof(CopyDataStruct));
                arguments = Marshal.PtrToStringUni(st.lpData);
            }
            catch { }

            return arguments;
        }
    }
}
