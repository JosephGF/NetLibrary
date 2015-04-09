using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace NetLibrary.Forms.Beauty
{
    public partial class BeautyForm : Form
    {
        new public bool MaximizeBox
        {
            get { return base.MaximizeBox; }
            set { this.btMaximize.Visible = base.MaximizeBox = value; }
        }

        new public bool MinimizeBox
        {
            get { return base.MinimizeBox; }
            set { this.btnMinimize.Visible = base.MinimizeBox = value; }
        }

        new public bool HelpButton
        {
            get { return base.HelpButton; }
            set { this.btnHelp.Visible = base.HelpButton = value; }
        }

        private BeautyTheme _theme = null;

        public new string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
                this.lbTitulo.Text = base.Text;
            }
        }
        public new Icon Icon
        {
            get
            {
                return base.Icon;
            }
            set
            {
                base.Icon = value;
                this.imgForm.Image = base.Icon.ToBitmap();
            }
        }

        public Color TitleBarBackgroundColor
        {
            get { return this.windowBar.BackColor; }
            set { this.windowBar.BackColor = value; }
        }
        public Color TitleBarForeColor
        {
            get { return this.windowBar.ForeColor; }
            set { this.windowBar.ForeColor = value; }
        }

        public BeautyTheme Theme
        {
            get { return this._theme; }
            set
            {
                this._theme = value;
                if (_theme == null) return;

                this._theme.onChangeColor += _theme_onChangeColor;
                this._theme.onChangeTheme += _theme_onChangeTheme;
                this.Draw(ColorType.ALL);
            }
        }

        private void _theme_onChangeTheme(object sender, ThemeEventArgs e)
        {
            Draw(ColorType.ALL);
        }

        void _theme_onChangeColor(object sender, ColorEventArgs e)
        {
            Draw(e.Type);
        }

        private void Draw(ColorType type)
        {
            if (type.HasFlag(ColorType.A))
                //if (this.TitleBarForeColor == null || this.TitleBarForeColor.IsEmpty)
                this.windowBar.ForeColor = this._theme.ColorA;
            if (type.HasFlag(ColorType.D))
                //if (this.TitleBarBackgroundColor == null || this.TitleBarBackgroundColor.IsEmpty)
                this.windowBar.BackColor = this._theme.ColorD;
            if (type.HasFlag(ColorType.G))
            {
                this.BackColor = this._theme.ColorG;
            }
            if (type.HasFlag(ColorType.FORE))
                this.ForeColor = this._theme.ForeColor;
        }

        public BeautyForm() : this(null) { }

        public BeautyForm(BeautyTheme theme)
        {
            InitializeComponent();
            this.Theme = theme;
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
            this.DialogResult = DialogResult.Cancel;
        }

        private void btMaximize_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
                this.WindowState = FormWindowState.Maximized;
            else
                this.WindowState = FormWindowState.Normal;
        }

        private void lbTitulo_DoubleClick(object sender, EventArgs e)
        {
            this.btMaximize_Click(sender, e);
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        #region MoveForm
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd,
                         int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        #endregion

        private void lbTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void BeautyForm_Load(object sender, EventArgs e)
        {
            this.MaximumSize = Screen.PrimaryScreen.WorkingArea.Size;
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {

        }

        private void buttonTitleBar_MouseLeave(object sender, EventArgs e)
        {
            ((Control)sender).BackColor = Color.Transparent;
        }

        private void buttonTitleBar_MouseEnter(object sender, EventArgs e)
        {
            Color cBase = ((Control)sender).Parent.BackColor;
            ((Control)sender).BackColor = NetLibrary.Drawing.Color.Luminace(cBase, 20);
        }
    }
}
