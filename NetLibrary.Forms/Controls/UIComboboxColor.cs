using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
using System.Text;

namespace NetLibrary.Forms.Controls
{
    [ToolboxBitmap(typeof(ComboBox), "CbColor")]
    public class UIComboBoxColors : ComboBox
    {
        private bool _knownColors = false;

        /// <summary>
        /// El valor de los items es de tipo Color
        /// </summary>
        public UIComboBoxColors()
        {
            this.DrawItem += new DrawItemEventHandler(comboBoxColor_DrawItem);
            this.DrawMode = DrawMode.OwnerDrawFixed;
            this.DisplayMember = "Name";
            this.DropDownStyle = ComboBoxStyle.DropDownList;


            string[] colorNames = System.Enum.GetNames(typeof(KnownColor));

            List<Color> listaColores = new List<Color>();

            for (Int32 index = 27; index < 167; index++)
            {
                listaColores.Add(Color.FromName(colorNames[index]));
            }

            this.DataSource = listaColores;
        }

        /// <summary>
        /// Determina si se deben usar Color o knownColors como valor de los items
        /// </summary>
        /// <param name="usarKnownColors"></param>
        public UIComboBoxColors(bool usarKnownColors)
        {
            if (usarKnownColors)
            {
                _knownColors = true;
                this.DrawItem += new DrawItemEventHandler(comboBoxColor_DrawItem);
                this.DrawMode = DrawMode.OwnerDrawFixed;
                this.DropDownStyle = ComboBoxStyle.DropDownList;

                List<KnownColor> listaKnownColors = new List<KnownColor>();

                for (Int32 x = 28; x <= 167; x++)
                {
                    listaKnownColors.Add((KnownColor)x);
                }

                this.DataSource = listaKnownColors;
            }
            else
            {
                this.DrawItem += new DrawItemEventHandler(comboBoxColor_DrawItem);
                this.DrawMode = DrawMode.OwnerDrawFixed;
                this.DisplayMember = "Name";
                this.DropDownStyle = ComboBoxStyle.DropDownList;


                string[] colorNames = System.Enum.GetNames(typeof(KnownColor));

                List<Color> listaColores = new List<Color>();

                for (Int32 index = 27; index < 167; index++)
                {
                    listaColores.Add(Color.FromName(colorNames[index]));
                }

                this.DataSource = listaColores;
            }
        }

        /// <summary>
        /// Los items son del tipo Colors
        /// </summary>
        /// <param name="listaColores"></param>
        public UIComboBoxColors(List<Color> listaColores)
        {
            this.DrawItem += new DrawItemEventHandler(comboBoxColor_DrawItem);
            this.DrawMode = DrawMode.OwnerDrawFixed;
            this.DisplayMember = "Name";
            this.DropDownStyle = ComboBoxStyle.DropDownList;

            this.DataSource = listaColores;
        }

        /// <summary>
        /// El valor de los items son del tipo KnownColor en ver de Colors
        /// </summary>
        /// <param name="listaColores"></param>
        public UIComboBoxColors(List<KnownColor> listaColores)
        {
            _knownColors = true;
            this.DrawItem += new DrawItemEventHandler(comboBoxColor_DrawItem);
            this.DrawMode = DrawMode.OwnerDrawFixed;
            this.DropDownStyle = ComboBoxStyle.DropDownList;

            this.DataSource = listaColores;
        }

        private void comboBoxColor_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index == -1) return;
            UtilColors.DrawColors(this.Items[e.Index], this.Font, _knownColors, e);
        }
    }

    [ToolboxBitmap(typeof(ListBox), "lbColor")]
    public class UIListBoxColors : ListBox
    {
        private bool _knownColors = false;

        /// <summary>
        /// El valor de los items es de tipo Color
        /// </summary>
        public UIListBoxColors()
        {
            this.DrawItem += new DrawItemEventHandler(ListBoxColor_DrawItem);
            this.DrawMode = DrawMode.OwnerDrawFixed;
            this.DisplayMember = "Name";


            string[] colorNames = System.Enum.GetNames(typeof(KnownColor));

            List<Color> listaColores = new List<Color>();

            for (Int32 index = 27; index < 167; index++)
            {
                listaColores.Add(Color.FromName(colorNames[index]));
            }

            this.DataSource = listaColores;
        }

        /// <summary>
        /// Determina si se deben usar Color o knownColors como valor de los items
        /// </summary>
        /// <param name="usarKnownColors"></param>
        public UIListBoxColors(bool usarKnownColors)
        {
            if (usarKnownColors)
            {
                _knownColors = true;
                this.DrawItem += new DrawItemEventHandler(ListBoxColor_DrawItem);
                this.DrawMode = DrawMode.OwnerDrawFixed;

                List<KnownColor> listaKnownColors = new List<KnownColor>();

                for (Int32 x = 28; x <= 167; x++)
                {
                    listaKnownColors.Add((KnownColor)x);
                }

                this.DataSource = listaKnownColors;
            }
            else
            {
                this.DrawItem += new DrawItemEventHandler(ListBoxColor_DrawItem);
                this.DrawMode = DrawMode.OwnerDrawFixed;
                this.DisplayMember = "Name";


                string[] colorNames = System.Enum.GetNames(typeof(KnownColor));

                List<Color> listaColores = new List<Color>();

                for (Int32 index = 27; index < 167; index++)
                {
                    listaColores.Add(Color.FromName(colorNames[index]));
                }

                this.DataSource = listaColores;
            }
        }

        /// <summary>
        /// Los items son del tipo Colors
        /// </summary>
        /// <param name="listaColores"></param>
        public UIListBoxColors(List<Color> listaColores)
        {
            this.DrawItem += new DrawItemEventHandler(ListBoxColor_DrawItem);
            this.DrawMode = DrawMode.OwnerDrawFixed;
            this.DisplayMember = "Name";

            this.DataSource = listaColores;
        }

        /// <summary>
        /// El valor de los items son del tipo KnownColor en ver de Colors
        /// </summary>
        /// <param name="listaColores"></param>
        public UIListBoxColors(List<KnownColor> listaColores)
        {
            _knownColors = true;
            this.DrawItem += new DrawItemEventHandler(ListBoxColor_DrawItem);
            this.DrawMode = DrawMode.OwnerDrawFixed;

            this.DataSource = listaColores;
        }

        private void ListBoxColor_DrawItem(object sender, DrawItemEventArgs e)
        {
            UtilColors.DrawColors(this.Items[e.Index], this.Font, _knownColors, e);
        }
    }

    internal static class UtilColors
    {
        internal static void DrawColors(object item, Font font, bool _knownColors, DrawItemEventArgs e)
        {
            if (!(item is Color) && !(item is KnownColor)) return;
            Color color = new Color();

            if (_knownColors)
            {
                color = Color.FromKnownColor((KnownColor)item);
            }
            else
            {
                color = (Color)item;
            }
            // Dibujamos el fondo
            e.DrawBackground();
            // Creamos los objetos GDI+
            Brush brush = new SolidBrush(color);
            Pen forePen = new Pen(e.ForeColor);
            Brush foreBrush = new SolidBrush(e.ForeColor);
            // Dibujamos el borde del rectángulo
            e.Graphics.DrawRectangle(forePen, new Rectangle(e.Bounds.Left + 2, e.Bounds.Top + 2, 19, e.Bounds.Size.Height - 4));
            // Rellenamos el rectángulo con el Color seleccionado
            // en la combo
            e.Graphics.FillRectangle(brush, new Rectangle(e.Bounds.Left + 3, e.Bounds.Top + 3, 18, e.Bounds.Size.Height - 5));
            // Dibujamos el nombre del color
            e.Graphics.DrawString(color.Name, font, foreBrush, e.Bounds.Left + 25, e.Bounds.Top + 2);
            // Eliminamos objetos GDI+
            brush.Dispose();
            forePen.Dispose();
            foreBrush.Dispose();
        }
    }
}
