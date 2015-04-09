using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetLibrary.Debugger
{
    public partial class FrmException : Form
    {
        /// <summary>
        /// Deshabilita le botón de cerrar [X] del formulario
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                const int CP_NOCLOSE = 0x200;
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE;
                return myCp;
            }
        }

        /// <summary>
        /// Obtiene el objeto de error actual
        /// </summary>
        public DebugErrorData Error { get; protected set; }

        public FrmException()
        {
            InitializeComponent();
        }

        protected virtual void Initialize()
        {
            this.pbScreenshot.Image = this.Error.Screenshot;
            this.txDetalles.Text = this.Error.ToString();
            this.txSistema.Text = this.Error.SystemInformation.ToString();
            this.rtbResumen.Text = Debug.GetDefaultExportText(this.Error);
        }

        new private void Show() { base.Show(); }

        new private void Show(IWin32Window owner) { base.Show(owner); }

        /// <summary>
        /// Muestra el formulario con los datos de la excepción especifiada
        /// </summary>
        /// <param name="ex">Excepicion ocurrida</param>
        /// <returns></returns>
        public DialogResult Show(Exception ex)
        {
            return this.Show(new DebugErrorData(ex));
        }

        /// <summary>
        /// Muestra el formulario con los datos de la excepción especifiada
        /// </summary>
        /// <param name="dEx">Datos de la excepción</param>
        /// <returns></returns>
        public DialogResult Show(DebugErrorData dEx)
        {
            this.Error = dEx;

            Initialize();
            this.Show();
            return this.DialogResult;
        }

        new private DialogResult ShowDialog() { return base.ShowDialog(); }

        new private DialogResult ShowDialog(IWin32Window owner) { return base.ShowDialog(owner); }

        /// <summary>
        /// Abre el formulario con la información de la excepcion
        /// </summary>
        /// <param name="ex">Excepcion producida</param>
        /// <returns></returns>
        public DialogResult ShowDialog(Exception ex)
        {
            return this.Show(new DebugErrorData(ex));
        }

        /// <summary>
        /// Abre el formulario con la información de la excepcion
        /// </summary>
        /// <param name="dEx">Objeto con la excepcion tratada</param>
        /// <returns></returns>
        public DialogResult ShowDialog(DebugErrorData dEx)
        {
            this.Error = dEx;

            Initialize();
            this.Show();
            return this.DialogResult;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }
    }
}
