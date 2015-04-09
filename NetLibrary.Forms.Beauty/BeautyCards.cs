using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetLibrary.Forms.Beauty
{
    public class BeautyCards : Panel
    {
        private int _borderWidth = 1;
        private int _shadowOffSet = 10;
        private int _roundCornerRadius = 4;
        private Image _image;
        Point _imageLocation = new Point(4, 4);
        Color _borderColor = Color.Gray;
        Color _gradientStartColor = Color.White;
        Color _gradientEndColor = Color.WhiteSmoke;
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

        public int BorderWidth
        {
            get { return _borderWidth; }
            set { _borderWidth = value; Invalidate(); }
        }
        public int ShadowOffSet
        {
            get
            {
                return _shadowOffSet;
            }
            set { _shadowOffSet = Math.Abs(value); Invalidate(); }
        }
        public int RoundCornerRadius
        {
            get { return _roundCornerRadius; }
            set { _roundCornerRadius = Math.Abs(value); Invalidate(); }
        }
        public Image Image
        {
            get { return _image; }
            set { _image = value; Invalidate(); }
        }
        public Point ImageLocation
        {
            get { return _imageLocation; }
            set { _imageLocation = value; Invalidate(); }
        }
        public Color BorderColor
        {
            get { return _borderColor; }
            set { _borderColor = value; Invalidate(); }
        }
        public Color GradientStartColor
        {
            get { return _gradientStartColor; }
            set { _gradientStartColor = value; Invalidate(); }
        }
        public Color GradientEndColor
        {
            get { return _gradientEndColor; }
            set { _gradientEndColor = value; Invalidate(); }
        }

        public BeautyCards()
        {
            //InitializeComponent();
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.BackColor = Color.Transparent;
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
            if (type.HasFlag(ColorType.F))
            {
                this._borderColor = this._theme.ColorF;
                this._gradientStartColor = this._theme.ColorF;
                this._gradientEndColor = this._theme.ColorF;
            }
            if (type.HasFlag(ColorType.G))
            {
                //this._gradientEndColor = this._theme.ColorG;
            }
            if (type.HasFlag(ColorType.FORE))
                this.ForeColor = this.Theme.ForeColor;

            Invalidate();
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);

            int tmpShadowOffSet = Math.Min(Math.Min(_shadowOffSet, this.Width - 2), this.Height - 2);
            int tmpSoundCornerRadius = Math.Min(Math.Min(_roundCornerRadius, this.Width - 2), this.Height - 2);
            if (this.Width > 1 && this.Height > 1)
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                Rectangle rect = new Rectangle(0, 0, this.Width - tmpShadowOffSet - 1,
                                   this.Height - tmpShadowOffSet - 1);

                Rectangle rectShadow = new Rectangle(tmpShadowOffSet, tmpShadowOffSet,
                                   this.Width - tmpShadowOffSet - 1, this.Height - tmpShadowOffSet - 1);

                GraphicsPath graphPathShadow = A1PanelGraphics.GetRoundPath(rectShadow, tmpSoundCornerRadius);
                GraphicsPath graphPath = A1PanelGraphics.GetRoundPath(rect, tmpSoundCornerRadius);

                if (tmpSoundCornerRadius > 0)
                {
                    using (PathGradientBrush gBrush = new PathGradientBrush(graphPathShadow))
                    {
                        gBrush.WrapMode = WrapMode.Clamp;
                        ColorBlend colorBlend = new ColorBlend(3);
                        colorBlend.Colors = new Color[]{Color.Transparent,
                        Color.FromArgb(180, Color.DimGray),
                        Color.FromArgb(180, Color.DimGray)};

                        colorBlend.Positions = new float[] { 0f, .1f, 1f };

                        gBrush.InterpolationColors = colorBlend;
                        e.Graphics.FillPath(gBrush, graphPathShadow);
                    }
                }

                // Draw backgroup
                LinearGradientBrush brush = new LinearGradientBrush(rect,
                this._gradientStartColor,
                this._gradientEndColor,
                LinearGradientMode.BackwardDiagonal);
                e.Graphics.FillPath(brush, graphPath);
                e.Graphics.DrawPath(new Pen(Color.FromArgb(180, this._borderColor), _borderWidth), graphPath);

                // Draw Image
                if (_image != null)
                    e.Graphics.DrawImageUnscaled(_image, _imageLocation);
            }
        }
    }

    internal class A1PanelGraphics
    {
        public static GraphicsPath GetRoundPath(Rectangle r, int depth)
        {
            GraphicsPath graphPath = new GraphicsPath();

            graphPath.AddArc(r.X, r.Y, depth, depth, 180, 90);
            graphPath.AddArc(r.X + r.Width - depth, r.Y, depth, depth, 270, 90);
            graphPath.AddArc(r.X + r.Width - depth, r.Y + r.Height - depth, depth, depth, 0, 90);
            graphPath.AddArc(r.X, r.Y + r.Height - depth, depth, depth, 90, 90);
            graphPath.AddLine(r.X, r.Y + r.Height - depth, r.X, r.Y + depth / 2);

            return graphPath;
        }
    }

    // A1PanelGlobals class
    internal class A1PanelGlobals
    {
        public const string A1Category = "A1";
    }
}
