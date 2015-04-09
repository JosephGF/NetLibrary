using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NetLibrary.Forms.Beauty
{
    [ToolboxBitmap(typeof(DataGridView), "System.Windows.Forms")]
    public class BeautyGrid : DataGridView
    {
        public BeautyGrid()
        {
            this.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            this.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            this.EnableHeadersVisualStyles = false;
        }

        private BeautyTheme _theme = null;

        public BeautyTheme Theme
        {
            get { return _theme; }
            set
            {
                _theme = value;
                if (_theme == null) return;

                _theme.onChangeTheme += _theme_onChangeTheme;
                _theme.onChangeColor += _theme_onChangeColor;
                this.Draw(ColorType.ALL);
            }
        }

        void _theme_onChangeColor(object sender, ColorEventArgs e)
        {
            Draw(e.Type);
        }

        void _theme_onChangeTheme(object sender, ThemeEventArgs e)
        {
            Draw(ColorType.ALL);
        }

        private void Draw(ColorType type)
        {
            if (type.HasFlag(ColorType.B))
                this.DefaultCellStyle.SelectionBackColor = this.Theme.ColorB;

            if (type.HasFlag(ColorType.E))
                this.RowHeadersDefaultCellStyle.BackColor = this.Theme.ColorE;
            if (type.HasFlag(ColorType.F))
            {
                this.GridColor = this._theme.ColorF;
                this.ColumnHeadersDefaultCellStyle.BackColor = this.Theme.ColorF;
                this.AlternatingRowsDefaultCellStyle.BackColor = this.Theme.ColorF;
            }
            if (type.HasFlag(ColorType.G))
                this.BackgroundColor = this._theme.ColorG;

            if (type.HasFlag(ColorType.FORE))
            {
                this.ColumnHeadersDefaultCellStyle.ForeColor = this.Theme.ForeColor;
                this.RowHeadersDefaultCellStyle.ForeColor = this.Theme.ForeColor;
                this.ForeColor = this._theme.ForeColor;
            }
        }

        private void InitializeComponent()
        {
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // BeautyGrid
            // 
            this.ParentChanged += new System.EventHandler(this.BeautyGrid_ParentChanged);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        private void BeautyGrid_ParentChanged(object sender, EventArgs e)
        {
            if (this._theme == null)
            {
                Form form = this.FindForm();
                if (form.GetType() == typeof(BeautyForm))
                {
                    this._theme = ((BeautyForm)form).Theme;
                }
            }
        }
    }
}
