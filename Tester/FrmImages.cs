using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tester
{
    public partial class FrmImages : Form
    {
        public FrmImages()
        {
            InitializeComponent();
        }

        private void btnResize_Click(object sender, EventArgs e)
        {
            string[] files = Directory.GetFiles(this.txSource.Text, "*.jpg", SearchOption.TopDirectoryOnly);

            foreach (string fImg in files)
            {
                var bmp = NetLibrary.Images.Utils.FixedSize((Bitmap)Bitmap.FromFile(fImg), Convert.ToInt32(this.txWidth.Text), Convert.ToInt32(this.txHeight.Text), true);
                string path = this.txDestino.Text + "\\" + Path.GetFileName(fImg);
                bmp.Save(path, System.Drawing.Imaging.ImageFormat.Jpeg);
                bmp.Dispose();
            }
        }
    }
}
