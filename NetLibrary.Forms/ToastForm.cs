using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using NetLibrary.Forms.Extensions;

namespace NetLibrary.Forms
{
    public enum ToastPosition
    {
        TOP_LEFT
        ,TOP_RIGHT
        ,BOTTOM_LEFT
        ,BOTTOM_RIGHT 
    }

    public partial class ToastForm : Form
    {
        public event EventHandler<EventArgs> OnFadeOutEnd;
        public event EventHandler<EventArgs> OnFadeInEnd;
        new public string Text { get { return this.lbText.Text; } set { this.lbText.Text = value; } }
        public Image Image { get { return this.pbImagen.Image; } set { this.pbImagen.Image = value; } }
        public String Title { get { return this.lbTitulo.Text; } set { this.lbTitulo.Text = value; } }

        public bool CloseButton { get { return this.lbClose.Visible; } set { this.lbClose.Visible = value;} }
        public bool AnchorButton { get { return this.lbAncla.Visible; } set { this.lbAncla.Visible = value; } }

        public Double DefaultOpacity { get; set; }

        private bool _autoclose = true;
        public bool AutoClose { get { return this._autoclose; } set { this._autoclose = value; this.lbAncla.ImageIndex = value == true ? 1 : 0; } }

        public ToastForm()
        {
            InitializeComponent();
            initialize();
        }

        public ToastForm(String title, String description, Image image)
        {
            InitializeComponent();
            this.Title = title;
            this.Text = description;
            this.Image = image;
            initialize();
        }

        public ToastForm(String title, String description, string image)
        {
            InitializeComponent();
            this.Title = title;
            this.Text = description;
            this.pbImagen.LoadAsync(image);
            initialize();
        }

        protected void setPosition()
        {
            int x = Screen.PrimaryScreen.WorkingArea.Width - this.Width - 5;
            int y = Screen.PrimaryScreen.WorkingArea.Height - this.Height - 5;
            
            foreach(Form f in Application.OpenForms)
            {
                if (f is ToastForm && !f.Equals(this) && f.Visible == true)
                    y -= f.Height +5;
            }
            this.Location = new Point(x, y);
        }

        private void initialize()
        {
            setPosition();
            this.LostFocus += ToastForm_LostFocus;
            this.GotFocus += ToastForm_GotFocus;
            FadeFormExtension.OnFadeInEnd += FadeFormExtension_OnFadeInEnd;
            FadeFormExtension.OnFadeOutEnd += FadeFormExtension_OnFadeOutEnd;

            if (this.DefaultOpacity == 0) this.DefaultOpacity = 0.8;
        }

        /// <summary>
        /// Do not activate the window just show it
        /// </summary>
        protected override bool ShowWithoutActivation
        {
            get { return true; }
        }

        private void FadeFormExtension_OnFadeOutEnd(object sender, EventArgs e)
        {
            this.Visible = false;
            if (sender == this && OnFadeOutEnd != null)
            {
                OnFadeOutEnd(this, e);
            }
        }

        private void FadeFormExtension_OnFadeInEnd(object sender, EventArgs e)
        {
            if (sender == this && OnFadeInEnd != null)
            {
                OnFadeInEnd(this, e);
            }
        }

        void ToastForm_GotFocus(object sender, EventArgs e)
        {
            this.Opacity = 1;
            this.timerAutoClose.Stop();
        }

        void ToastForm_LostFocus(object sender, EventArgs e)
        {
            this.Opacity = this.DefaultOpacity;
            if (this.AutoClose)
            {
                this.timerAutoClose.Start();
            }
        }

        public void LoadImage(String uri)
        {
            try
            {
                this.pbImagen.LoadAsync(uri);
            }
            catch (Exception ex) { }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.timerAutoClose.Stop();
            // if (false.Equals(this.Tag))
            //     this.Tag = true;
            // else
            this.CloseToast();
        }

        protected virtual void CloseToast()
        {
            this.Close();
        }

        private void ToastForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.AutoClose)
            {
                //this.Tag = false;
                this.timerAutoClose.Stop();
                this.Focus();
            }
        }

        private void lbClose_Click(object sender, EventArgs e)
        {
            this.timerAutoClose.Stop();
            this.CloseToast();
        }

        private void ToastForm_Load(object sender, EventArgs e)
        {
            this.timerAutoClose.Enabled = this.AutoClose;
        }

        private void pbImagen_Click(object sender, EventArgs e)
        {

        }

        private void ToastForm_Shown(object sender, EventArgs e)
        {
            this.FadeIn(this.DefaultOpacity);
        }

        public void FadeIn(double opacity)
        {
            this.FadeIn(opacity, 20, 800, false);
        }
        public void FadeIn()
        {
            this.SuspendLayout();
            if (!this.Visible)
            {
                this.Visible = true;
                this.Opacity = 0;
            }
            this.ResumeLayout();
            this.FadeIn(this.DefaultOpacity);
        }

        public void FadeOut()
        {
            this.FadeOut(0);
        }
        public void FadeOut(double opacity)
        {
            this.FadeOut(opacity, 20, 800, false);
        }

        private void lbAncla_Click(object sender, EventArgs e)
        {
            this.AutoClose = !this.AutoClose;
        }

        private void ToastForm_ResizeEnd(object sender, EventArgs e)
        {
            setPosition();
        }
    }
}
