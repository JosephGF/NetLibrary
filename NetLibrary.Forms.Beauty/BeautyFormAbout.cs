using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using NetLibrary.Forms;
using System.Reflection;

namespace NetLibrary.Forms.Beauty
{
    public partial class BeautyFormAbout : BeautyForm
    {
        private AssemblyInfo _assemblyInfo;
        public AssemblyInfo AssemblyInfo
        {
            get { return _assemblyInfo; }
            set { _assemblyInfo = value; Initialize(); }
        }

        public BeautyFormAbout(Assembly assembly)
            : base()
        {
            init(assembly);
        }

        public BeautyFormAbout(Assembly assembly, BeautyTheme theme)
            : base(theme)
        {
            init(assembly);
        }


        public BeautyFormAbout(AssemblyInfo assemblyInfo, BeautyTheme theme)
            : base(theme)
        {
            InitializeComponent();
            this.AssemblyInfo = assemblyInfo;
            Initialize();
        }

        public BeautyFormAbout(AssemblyInfo assemblyInfo) : this(assemblyInfo, null) { }

        public BeautyFormAbout()
            : base()
        {
            init(null);
        }

        public BeautyFormAbout(BeautyTheme theme)
            : base(theme)
        {
            init(null);
        }

        private void init(Assembly assembly)
        {
            if (assembly == null) 
                assembly = Assembly.GetEntryAssembly();

            InitializeComponent();
            this.AssemblyInfo = new Forms.AssemblyInfo(assembly);
            Initialize();
        }

        protected void Initialize()
        {
            if (this.AssemblyInfo == null)
                return;

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
