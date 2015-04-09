using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tester
{
    public partial class FrmWin32 : Form
    {
        public FrmWin32()
        {
            InitializeComponent();
            NetLibrary.Win32.WinControls.AddShieldToButton(this.btnDisableTaskmng);
        }

        private void btnToggleTaskbar_Click(object sender, EventArgs e)
        {
            NetLibrary.Win32.WinTaskbar.Hide();
        }

        private void btnDisableTaskmng_Click(object sender, EventArgs e)
        {

            if (NetLibrary.Win32.Win32.IsUserAdministrator)
                NetLibrary.Win32.WinTaskmanager.ToggleTaskManager();
            else
                NetLibrary.Win32.Win32.RestartAsAdmin();

        }

        private void btnShowTaskbar_Click(object sender, EventArgs e)
        {
            NetLibrary.Win32.WinTaskbar.Show();
        }

        private void btnChangeDesktop_Click(object sender, EventArgs e)
        {
            Uri uri = new Uri(@"http://fc07.deviantart.net/fs70/f/2010/304/c/9/saw_v_2_by_hassoomi-d31v9zz.jpg");
            NetLibrary.Win32.WinDesktop.Wallpaper.Set(uri, NetLibrary.Win32.WinDesktop.Wallpaper.Style.Stretched);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NetLibrary.Win32.WinDesktop.Windows.MinimizeAll();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            NetLibrary.Win32.WinDesktop.Windows.Maximize();
        }
    }
}
