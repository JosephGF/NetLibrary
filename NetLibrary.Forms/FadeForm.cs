using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetLibrary.Forms
{
    public class FadeForm : Form
    {
        public delegate void FadeFormHandle(object sender, EventArgs e);
        public event FadeFormHandle OnFadeOutEnd;
        public void FadeOut()
        {
            NetLibrary.Forms.Extensions.FadeFormExtension.FadeOut(this, 0);

            if (OnFadeOutEnd != null)
                OnFadeOutEnd(this, new EventArgs());
            else
                this.Dispose();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // FadeForm
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "FadeForm";
            this.ResumeLayout(false);

        }
    }
}
