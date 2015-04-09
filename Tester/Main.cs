using NetLibrary.Debugger;
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
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void btnWin32_Click(object sender, EventArgs e)
        {
            new FrmWin32().ShowDialog();
        }

        private void btnFormsMVC_Click(object sender, EventArgs e)
        {
            NetLibrary.Forms.Mvc.Configuration.initialize();
        }

        private void btnWinForms_Click(object sender, EventArgs e)
        {
            NetLibrary.Forms.Animations.Animate(this.btnWinForms, NetLibrary.Forms.Animations.Effect.Center, 1000, 180);
        }

        private void btnEntityFramework_Click(object sender, EventArgs e)
        {
            new FrmDataBase().ShowDialog();
        }

        private void Main_Load(object sender, EventArgs e)
        {
        }

        private void btnWinConsole_Click(object sender, EventArgs e)
        {
            Debug.OpenConsole();
            Debug.WriteLine("Add your command");
            string command = Debug.ReadLine();
            MessageBox.Show(command);
        }

        private void btnImages_Click(object sender, EventArgs e)
        {
            new FrmImages().Show();
        }

        private void btnNetLibrary_Click(object sender, EventArgs e)
        {
            new FrmNetLibrary().Show();
        }
    }
}
