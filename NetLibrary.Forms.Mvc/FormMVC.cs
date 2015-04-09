using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetLibrary.Forms.Mvc
{
    public sealed partial class FormMVC : Form
    {
        public View View { get; internal set; }
        public FormMVC()
        {
            InitializeComponent();
        }

        public FormMVC(View view)
        {
            this.LoadView(view);
        }

        private void LoadView(View view)
        {
            this.View = view;

            if (!view.IsPartial)
            {
                //foreach (Control c in this.Controls)
                this.Controls.Clear();
                view.Dock = System.Windows.Forms.DockStyle.Fill;
            }

            this.Controls.Add(view);
        }

        private void FormMVC_ControlAdded(object sender, ControlEventArgs e)
        {
            if (e.Control is IView)
            {

            }
            else
            {
                throw new Exception("Only can be added NetLibrary.Forms.Mvc.IView implementations.");
            }
        }
    }
}
