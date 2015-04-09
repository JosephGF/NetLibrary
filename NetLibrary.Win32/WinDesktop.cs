using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace NetLibrary.Win32
{
    public sealed class WinDesktop
    {
        WinDesktop() { }

        public class Wallpaper
        {
            const int SPI_SETDESKWALLPAPER = 20;
            const int SPIF_UPDATEINIFILE = 0x01;
            const int SPIF_SENDWININICHANGE = 0x02;

            public enum Style : int
            {
                Tiled,
                Centered,
                Stretched
            }
            public static void Set(Uri uri, Style style)
            {
                System.IO.Stream s = new System.Net.WebClient().OpenRead(uri.ToString());

                System.Drawing.Image img = System.Drawing.Image.FromStream(s);
                Set(img, style);
            }


            public static void Set(System.Drawing.Image img, Style style)
            {
                string tmpPath = Path.Combine(Path.GetTempPath(), "wallpaper.bmp");
                img.Save(tmpPath, System.Drawing.Imaging.ImageFormat.Bmp);
                Set(tmpPath, style);
            }
            public static void Set(string path, Style style)
            {
                SetStyle(style);
                Win32.SystemParametersInfo(SPI_SETDESKWALLPAPER,
                    0,
                    path,
                    SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE);
            }

            public static void SetStyle(Style style)
            {
                RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop", true);
                if (style == Style.Stretched)
                {
                    key.SetValue(@"WallpaperStyle", 2.ToString());
                    key.SetValue(@"TileWallpaper", 0.ToString());
                }

                if (style == Style.Centered)
                {
                    key.SetValue(@"WallpaperStyle", 1.ToString());
                    key.SetValue(@"TileWallpaper", 0.ToString());
                }

                if (style == Style.Tiled)
                {
                    key.SetValue(@"WallpaperStyle", 1.ToString());
                    key.SetValue(@"TileWallpaper", 1.ToString());
                }
            }
        }

        public class Windows
        {
            const int WM_COMMAND = 0x111;
            const int MIN_ALL = 419;
            const int MIN_ALL_UNDO = 416;

            public static void MinimizeAll()
            {
                IntPtr lHwnd = Win32.FindWindow("Shell_TrayWnd", null);
                Win32.SendMessage((IntPtr)lHwnd, WM_COMMAND, (IntPtr)MIN_ALL, IntPtr.Zero);
            }
            public static void Maximize()
            {
                IntPtr lHwnd = Win32.FindWindow("Shell_TrayWnd", null);
                Win32.SendMessage((IntPtr)lHwnd, WM_COMMAND, (IntPtr)MIN_ALL_UNDO, IntPtr.Zero);
            }
        }

        public class Icons
        {
             private const long GW_CHILD = 5;
             private const long SW_HIDE = 0;
             private const long SW_SHOW = 5;

             public static void ShowDesktopIcons(bool bVisible)
             {
                 IntPtr hWnd_DesktopIcons = Win32.FindWindow("Progman", "Program Manager");
                 if (bVisible)
                 {
                     Win32.ShowWindow(hWnd_DesktopIcons, SW_SHOW);
                 }
                 else
                 {
                     Win32.ShowWindow(hWnd_DesktopIcons, SW_HIDE);
                 }
             }

             public static void EnableDesktop(bool bEnable)
             {
                 IntPtr hWnd_Desktop = Win32.FindWindow("Progman", "Program Manager");
                 Win32.EnableWindow(hWnd_Desktop, bEnable == true ? 1 : 0);
             }

            enum GetWindow_Cmd : long
            {
                GW_HWNDFIRST = 0,
                GW_HWNDLAST = 1,
                GW_HWNDNEXT = 2,
                GW_HWNDPREV = 3,
                GW_OWNER = 4,
                GW_CHILD = 5,
                GW_ENABLEDPOPUP = 6
            }

            private const int WM_COMMAND = 0x111;

            public static void ToggleDesktopIcons()
            {
                var toggleDesktopCommand = new IntPtr(0x7402);
                IntPtr hWnd = (IntPtr)Win32.GetWindow(Win32.FindWindow("Progman", "Program Manager"), (long)GetWindow_Cmd.GW_CHILD);
                Win32.SendMessage(hWnd, WM_COMMAND, toggleDesktopCommand, IntPtr.Zero);
            }

        }
    }
}
