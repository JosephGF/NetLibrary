using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Text;

namespace NetLibrary.Forms.Controls
{
    [ToolboxBitmap(typeof(ComboBox), "CbFont")]
    public class UIComboBoxFonts : ComboBox
    {
        private Single _textSize = 6;
        private InstalledFontCollection installedFonts = new InstalledFontCollection();

        public Single TextSize
        {
            get { return _textSize; }
            set
            {
                _textSize = value;
                this.ItemHeight = Convert.ToInt32(_textSize + 16);

            }
        }

        public UIComboBoxFonts()
        {
            this.DataSource = installedFonts.Families;
            this.DisplayMember = "Name";
            this.DrawMode = DrawMode.OwnerDrawFixed;

            this.DrawItem += new DrawItemEventHandler(comboBox_DrawItem);
        }

        private void comboBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            FontUtil.DrawItem(this.Items[e.Index], _textSize, this.ForeColor, e);
        }
    }

    [ToolboxBitmap(typeof(ListBox), "CbFont")]
    public class UIListBoxFonts : ListBox
    {
        private Single _textSize = 6;
        private InstalledFontCollection installedFonts = new InstalledFontCollection();

        public Single TextSize
        {
            get { return _textSize; }
            set
            {
                _textSize = value;
                this.ItemHeight = Convert.ToInt32(_textSize + 6);
            }
        }

        public UIListBoxFonts()
        {
            this.DataSource = installedFonts.Families;
            this.DisplayMember = "Name";
            this.DrawMode = DrawMode.OwnerDrawFixed;

            this.DrawItem += new DrawItemEventHandler(listBox_DrawItem);
        }

        private void listBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            FontUtil.DrawItem(this.Items[e.Index], _textSize, this.ForeColor, e);
        }
    }

    internal static class FontUtil
    {
        internal static void DrawItem(object item, float _textSize, Color foreColor, DrawItemEventArgs e)
        {
            //FontFamily family = installedFonts.Families[e.Index];
            if (!(item is FontFamily)) return;
            if (e.Index < 0) return;


            FontFamily family = (FontFamily)item;

            FontStyle style = FontStyle.Regular;

            if (!family.IsStyleAvailable(style))
                style = FontStyle.Bold;
            if (!family.IsStyleAvailable(style))
                style = FontStyle.Italic;

            Font fnt = new Font(family, _textSize, style);

            Brush brush;
            if (e.State == DrawItemState.Selected)
            {
                brush = new SolidBrush(Color.White);
            }
            else
            {
                brush = new SolidBrush(foreColor);
            }

            e.DrawBackground();
            e.Graphics.DrawString(family.GetName(0), fnt, brush, e.Bounds.Location);

            // Eliminamos objetos GDI+
            brush.Dispose();
        }
    }
}

