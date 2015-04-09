using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Drawing;

namespace NetLibrary.Forms.Beauty
{
    [Flags()]
    public enum ColorType
    {
        A, B, C, D, E, F, G, FORE,
        ALL = A | B | C | D | E | F | G | FORE
    };

    public class BeautyTheme : Component
    {
        public delegate void ChangedEventHandler(object sender, ColorEventArgs e);
        public delegate void ThemeGenerateHandler(object sender, ThemeEventArgs e);
        public event ChangedEventHandler onChangeColor;
        public event ThemeGenerateHandler onChangeTheme;
        //public event ColorsUpdated;

        private Color _baseColor;
        private int _desfase = 10;
        private Color _colorA;
        private Color _colorB;
        private Color _colorC;
        private Color _colorD;
        private Color _colorE;
        private Color _colorF;
        private Color _colorG;
        private Color _foreColor = Color.Black;

        public Color AutoGenerateFromBaseColor
        {
            get { return this._baseColor; }
            set
            {
                this._baseColor = value;
                GenerateTheme();
            }
        }

        /// <summary>
        /// Desfase en % de los colores
        /// </summary>
        public int Desfase { get { return _desfase; } set { _desfase = value; GenerateTheme(); } }
        public Color ColorA
        {
            get { return this._colorA; }
            set
            {
                if (onChangeColor != null)
                    onChangeColor(this, new ColorEventArgs(value, this._colorA, ColorType.A));

                this._colorA = value;
            }
        }
        public Color ColorB
        {
            get { return this._colorB; }
            set
            {
                if (onChangeColor != null)
                    onChangeColor(this, new ColorEventArgs(value, this._colorB, ColorType.B));

                this._colorB = value;
            }
        }
        public Color ColorC
        {
            get { return this._colorC; }
            set
            {
                if (onChangeColor != null)
                    onChangeColor(this, new ColorEventArgs(value, this._colorC, ColorType.C));

                this._colorC = value;
            }
        }
        public Color ColorD
        {
            get { return this._colorD; }
            set
            {
                if (onChangeColor != null)
                    onChangeColor(this, new ColorEventArgs(value, this._colorD, ColorType.D));
                this._colorD = value;
            }
        }
        public Color ColorE
        {
            get { return this._colorE; }
            set
            {
                if (onChangeColor != null)
                    onChangeColor(this, new ColorEventArgs(value, this._colorE, ColorType.E));
                this._colorE = value;
            }
        }
        public Color ColorF
        {
            get { return this._colorF; }
            set
            {
                if (onChangeColor != null)
                    onChangeColor(this, new ColorEventArgs(value, this._colorF, ColorType.F));
                this._colorF = value;
            }
        }
        public Color ColorG
        {
            get { return this._colorG; }
            set
            {
                if (onChangeColor != null)
                    onChangeColor(this, new ColorEventArgs(value, this._colorG, ColorType.G));
                this._colorG = value;
            }
        }
        public Color ForeColor
        {
            get { return this._foreColor; }
            set
            {
                if (onChangeColor != null)
                    onChangeColor(this, new ColorEventArgs(value, this._foreColor, ColorType.FORE));
                this._foreColor = value;
            }
        }

        public BeautyTheme()
        {
            this.Desfase = 10;
        }

        private void GenerateTheme()
        {
            if (this._baseColor.IsEmpty)
                this._baseColor = Color.Gray;

            this._colorA = NetLibrary.Drawing.Color.Luminace(this._baseColor, -3 * this.Desfase);
            this._colorB = NetLibrary.Drawing.Color.Luminace(this._baseColor, -2 * this.Desfase);
            this._colorC = NetLibrary.Drawing.Color.Luminace(this._baseColor, -1 * this.Desfase);
            this._colorD = this._baseColor;
            this._colorE = NetLibrary.Drawing.Color.Luminace(this._baseColor, 1 * this.Desfase);
            this._colorF = NetLibrary.Drawing.Color.Luminace(this._baseColor, 2 * this.Desfase);
            this._colorG = NetLibrary.Drawing.Color.Luminace(this._baseColor, 3 * this.Desfase);

            if (onChangeTheme != null)
                onChangeTheme(this, new ThemeEventArgs(this.Desfase, this._baseColor));
        }
    }

    public class ColorEventArgs : EventArgs
    {
        public ColorEventArgs(Color newC, Color oldC, ColorType type)
        {
            this.OldColor = oldC;
            this.NewColor = newC;
            this.Type = type;
        }

        public Color OldColor { get; set; }
        public Color NewColor { get; set; }
        public ColorType Type { get; set; }
    }

    public class ThemeEventArgs : EventArgs
    {
        public ThemeEventArgs(int desfase, Color baseColor)
        {
            this.Desfase = desfase;
            this.BaseColor = baseColor;
        }
        public int Desfase { get; set; }
        public Color BaseColor { get; set; }
    }

}
