using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetLibrary.Forms.Controls
{
    public partial class UIItemList : UserControl
    {
        #region Events
        public delegate void ClickEventHandler(object sender, EventArgs re);
        public event ClickEventHandler onClick;

        public delegate void CheckedEventHandler(object sender, ItemCheckEventArgs re);
        public event CheckedEventHandler onCheckedChange;
        #endregion

        #region Public Properties

        public PictureBox Picture
        {
            get { return this.pbImagen; }
        }
        public Color MouseHoverColor
        {
            get { return _mouseHoverColor; }
            set { _mouseHoverColor = value; }
        }
        public Color MouseClickColor
        {
            get { return _mouseClickColor; }
            set { _mouseClickColor = value; }
        }
        public Color CheckedColor
        {
            get { return _checkedColor; }
            set { _checkedColor = value; }
        }
        public Color BackColor
        {
            get { return base.BackColor; }
            set { base.BackColor = value; _backColor = value; }
        }

        public bool Checked
        {
            get { return _isChecked == CheckState.Checked; }
            set
            {
                if (value)
                    _isChecked = CheckState.Checked;
                else
                    _isChecked = CheckState.Unchecked;
            }
        }
        public CheckState CheckState
        {
            get { return _isChecked; }
            set { 
                checkedChange(value); 
                _isChecked = value; 
            }
        } 
        public string Title
        {
            get { return this.lbTitulo.Text; }
            set { this.lbTitulo.Text = value; }
        }
        public Color TitleForeColor
        {
            get { return this.lbTitulo.ForeColor; }
            set { this.lbTitulo.ForeColor = value; }
        }
        public string Description
        {
            get { return this.lbDesc.Text; }
            set { this.lbDesc.Text = value; }
        }
        public Color DescriptionForeColor
        {
            get { return this.lbDesc.ForeColor; }
            set { this.lbDesc.ForeColor = value; }
        }
        #endregion

        #region Private Properties
        private Color _mouseHoverColor = SystemColors.Highlight;
        private Color _mouseClickColor = SystemColors.ButtonHighlight;
        private Color _backColor = SystemColors.Control;
        private Color _checkedColor = SystemColors.MenuHighlight;
        private CheckState _isChecked = CheckState.Unchecked;
        #endregion

        public UIItemList()
        {
            InitializeComponent();
            _backColor = this.BackColor;
        }

        #region Public Methods
        #endregion

        #region Private Methods
        #endregion

        #region Interface Events
        private void checkedChange(CheckState newState)
        {
            ItemCheckEventArgs args = new ItemCheckEventArgs(0, newState, _isChecked);
            SetDefaultColor();
            onCheckedChange(this, args);
        }

        private void contolClick_Click(object sender, EventArgs e)
        {
            if (onClick != null) onClick(this, e);
        }

        private void Contacto_MouseHover(object sender, EventArgs e)
        {
            base.BackColor = this.MouseHoverColor;
        }

        private void Contacto_MouseDown(object sender, MouseEventArgs e)
        {
            base.BackColor = this.MouseClickColor;
        }

        private void Contacto_MouseUp(object sender, MouseEventArgs e)
        {
            SetDefaultColor();
        }

        private void Contacto_MouseLeave(object sender, EventArgs e)
        {
            SetDefaultColor();
        }

        private void SetDefaultColor()
        {
            if (_isChecked == System.Windows.Forms.CheckState.Checked)
                base.BackColor = this.CheckedColor;
            else
                base.BackColor = this.BackColor;
        }
        #endregion
    }
}
