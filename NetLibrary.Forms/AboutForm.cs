using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace NetLibrary.Forms
{
    public partial class AboutForm : Form
    {
        public AssemblyInfo AssemblyInfo { get; set; }

        public AboutForm(Assembly assembly)
        {
            InitializeComponent();
            this.AssemblyInfo = new Forms.AssemblyInfo(assembly);
            Initialize();
        }

        public AboutForm(AssemblyInfo assemblyInfo)
        {
            InitializeComponent();
            this.AssemblyInfo = assemblyInfo;
            Initialize();
        }

        public AboutForm()
        {
            InitializeComponent();
            
            try
            {
                Assembly _assembly = Assembly.GetEntryAssembly();
                this.AssemblyInfo = new Forms.AssemblyInfo(_assembly);
                Initialize();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        protected void Initialize()
        {
            if (this.AssemblyInfo == null) return;

            try
            {
                this.lbTitle.Text = this.AssemblyInfo.Name;
                this.lbVersion.Text = this.AssemblyInfo.Version;
                this.lbAutor.Text = this.AssemblyInfo.Author;
                this.lbCopyright.Text = this.AssemblyInfo.Copyright;
                this.pbIcon.Image = new Icon(this.AssemblyInfo.Icon, 128, 128).ToBitmap();
            }
            catch (Exception ex)
            {
                Debugger.Debug.WriteLine(ex);
            }
        }
    }
}
