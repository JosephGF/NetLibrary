using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NetLibrary;

namespace Tester
{
    public partial class FrmNetLibrary : Form
    {
        public FrmNetLibrary()
        {
            InitializeComponent();
        }

        private void btnToBase64_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int limit = Convert.ToInt32(txNGuids.Text);
            for (int x = 0; x < limit; x++)
            {
                this.trxDGuid.Text += NetLibrary.GuidDate.NewGuid().ToString("B") + Environment.NewLine;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new NetLibrary.Forms.AboutForm().Show();
        }

        private void btnProcessGuid_Click(object sender, EventArgs e)
        {
            try
            {
                string guid = this.txGuid.Text;
                GuidDate gDate = (GuidDate)new Guid(guid);
                this.txDate.Text = gDate.DateTime.ToString("dd-MM-yyyy hh:mm:ss.fff");
                this.txData.Text = gDate.Data.ToString();
                this.txErrors.Text = "Sin Errores";
            }
            catch (NetLibrary.GuidDate.InvalidDateGuidException ex)
            {
                this.txErrors.Text = ex.Message;
                this.txDate.Text = ex.Date.ToString("dd-MM-yyyy hh:mm:ss.fff");
                this.txData.Text = ex.Data.ToString();
            }
        }
    }
}