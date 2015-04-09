using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetLibrary.Forms.Controls
{
    public class UIProgressBar : ProgressBar
    {
        public enum ProgressBarState
        {
            Normal,
            Pause,
            Error,
            Personalized
        }

        private ProgressBarState _state = ProgressBarState.Normal;
        private Orientation _orientation = Orientation.Horizontal;
        private Brush _defaultBrush = Brushes.Green;

        /// <summary>
        /// Obtiene o establece el estado de la barra de progreso
        /// </summary>
        public UIProgressBar.ProgressBarState State
        {
            get { return _state; }
            set
            {
                _state = value;
                if (this.State == ProgressBarState.Normal)
                    this.SetStyle(ControlStyles.UserPaint, false);
                else
                    this.SetStyle(ControlStyles.UserPaint, true);
            }
        }

        /// <summary>
        /// Devuelve el porcentaje actual de la barra de progreso
        /// </summary>
        public decimal Percent
        {
            get { return Decimal.Divide(this.Maximum, this.Value) * 100; }
        }

        /// <summary>
        /// Brush con el que se pintará el progressbar cuando el Estado es Personalized
        /// </summary>
        public Brush ProgressBarBrush { get; set; }

        /// <summary>
        /// Obtiene o establece la orientacion de la barra de scroll
        /// </summary>
        public Orientation Orientation { get { return _orientation; } set { _orientation = value; this.Refresh(); } }

        public Color ProgressBarColor
        {
            get { return ((SolidBrush)ProgressBarBrush).Color; }
            set { ProgressBarBrush = new SolidBrush(value); this.Refresh(); }
        }

        public UIProgressBar()
        {
            this.Padding = new Padding(0);
            if (ProgressBarBrush == null) ProgressBarBrush = Brushes.Blue;
        }

        protected override CreateParams CreateParams
        {
            get
            {
                if (this.Orientation == System.Windows.Forms.Orientation.Vertical)
                {
                    CreateParams cp = base.CreateParams;
                    cp.Style |= 0x04;
                    return cp;
                }
                else
                {
                    return base.CreateParams;
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (!this.Visible) return;
            if (this.State == ProgressBarState.Normal)
            {
                base.OnPaint(e);
                return;
            }

            //base.OnPaint(e); return;
            Rectangle rec = e.ClipRectangle;
            float top = this.Padding.Top;
            float left = this.Padding.Left;

            if (Orientation == System.Windows.Forms.Orientation.Horizontal)
            {
                rec.Width = (int)(rec.Width * ((double)Value / Maximum));
            }

            if (Orientation == System.Windows.Forms.Orientation.Vertical)
            {
                top = rec.Height;
                rec.Height = (int)(rec.Height * ((double)Value / Maximum));
                top = top - rec.Height + 1;
            }

            if (ProgressBarRenderer.IsSupported)
                ProgressBarRenderer.DrawHorizontalBar(e.Graphics, e.ClipRectangle);
            //rec.Height = rec.Height - 4;
            e.Graphics.FillRectangle(getColor(), left, top, rec.Width - this.Padding.Right, rec.Height - this.Padding.Bottom);
        }

        private Brush getColor()
        {
            Brush result = Brushes.Green;
            switch (this.State)
            {
                case ProgressBarState.Normal:
                    result = _defaultBrush;
                    break;
                case ProgressBarState.Pause:
                    result = Brushes.Goldenrod;
                    break;
                case ProgressBarState.Error:
                    result = Brushes.Red;
                    break;
                case ProgressBarState.Personalized:
                    result = ProgressBarBrush;
                    break;
            }
            return result;
        }
    }
}
