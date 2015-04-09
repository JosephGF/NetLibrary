using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetLibrary.Win32
{
    public class WinTaskmanager
    {
        public static void State(bool enable)
        {
            if (Win32.IsUserAdministrator)
            {
                RegistryKey objRegistryKey = Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Policies\System");

                if (enable && objRegistryKey.GetValue("DisableTaskMgr") != null)
                    objRegistryKey.DeleteValue("DisableTaskMgr");
                else
                    objRegistryKey.SetValue("DisableTaskMgr", "1");

                objRegistryKey.Close();
            }
        }

        public static void ToggleTaskManager()
        {
            if (Win32.IsUserAdministrator)
            {
                RegistryKey objRegistryKey = Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Policies\System");
                State(!(objRegistryKey.GetValue("DisableTaskMgr") == null));
            }
        }
    }
}
