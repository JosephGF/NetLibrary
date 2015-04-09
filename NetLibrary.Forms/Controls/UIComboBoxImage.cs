using System.Windows.Forms;
using System.Drawing;
using System;
using System.Collections.Generic;

namespace NetLibrary.Forms.Controls
{
    internal static class UtilCombo
    {
        internal static void DrawItems(UIComboBoxImage combo, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;
            Image imagen = combo.getImagen(e.Index);
            string label = combo.Items[e.Index].Text;

            DrawItems(label, imagen, e);
        }
        internal static void DrawItems(UIListBoxImage list, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;
            Image imagen = list.getImagen(e.Index);
            string label = list.Items[e.Index].Text;

            DrawItems(label, imagen, e);
        }

        internal static void DrawItems(String label, Image imagen, DrawItemEventArgs e)
        {
            // Dibujamos el fondo
            e.DrawBackground();
            // Creamos los objetos GDI+
            //Brush brush = new SolidBrush(Color.Black);
            Pen forePen = new Pen(e.ForeColor);
            Brush foreBrush = new SolidBrush(e.ForeColor);
            // Dibujamos el borde del rectángulo

            if (imagen != null)
                e.Graphics.DrawImage(imagen, e.Bounds.Left + 2, e.Bounds.Top + 2, e.Bounds.Size.Height - 4, e.Bounds.Size.Height - 4);
            // Rellenamos el rectángulo con el Color seleccionado
            // en la combo
            //e.Graphics.FillRectangle(brush, new Rectangle(e.Bounds.Left + 3, e.Bounds.Top + 3, 18, e.Bounds.Size.Height - 5));
            // Dibujamos el nombre del color

            e.Graphics.DrawString(label, e.Font, foreBrush, e.Bounds.Size.Height + 10, e.Bounds.Top + 2);
            // Eliminamos objetos GDI+
            //brush.Dispose();
            forePen.Dispose();
            foreBrush.Dispose();
        }
    }

    [ToolboxBitmap(typeof(ComboBox), "CbDriver")]
    public class UIComboBoxImage : ComboBox
    {
        public ImageList ImageList { get; set; }
        public new ComboBoxItem[] Items { get { return _items.ToArray(); } set { _items = new List<ComboBoxItem>(value); base.DataSource = _items; } }
        private List<ComboBoxItem> _items = new List<ComboBoxItem>();

        public UIComboBoxImage()
        {
            this.DisplayMember = "Text";
            this.DrawItem += new DrawItemEventHandler(IUComboBoxImage_DrawItem);
            this.DrawMode = DrawMode.OwnerDrawFixed;
            this.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        public void AddItem(ComboBoxItem item)
        {
            _items.Add(item);
            this.Items = _items.ToArray();
        }

        void IUComboBoxImage_DrawItem(object sender, DrawItemEventArgs e)
        {
            UtilCombo.DrawItems(this, e);
        }

        /// <summary>
        /// Obtiene la imagen asociada al item
        /// </summary>
        /// <param name="index">index del item</param>
        /// <returns>Imagen asociada</returns>
        public Image getImagen(int index)
        {
            Image img = null;

            if (this.Items.Length >= index)
            {
                int idx = Items[index].Index;
                if (idx > -1)
                    img = ImageList.Images[idx];
                else
                    img = Items[index].Image;
            }

            return img;
        }
    }

    [ToolboxBitmap(typeof(ListBox), "lbDriver")]
    public class UIListBoxImage : ListBox
    {
        public ImageList ImageList { get; set; }
        public new ComboBoxItem[] Items { get { return _items.ToArray(); } set { _items = new List<ComboBoxItem>(value); base.DataSource = _items; } }
        private List<ComboBoxItem> _items = new List<ComboBoxItem>();

        public UIListBoxImage()
        {
            this.DisplayMember = "Text";
            this.DrawItem += new DrawItemEventHandler(UIListBoxImage_DrawItem);
            this.DrawMode = DrawMode.OwnerDrawFixed;
        }

        void UIListBoxImage_DrawItem(object sender, DrawItemEventArgs e)
        {
            UtilCombo.DrawItems(this, e);
        }

        /// <summary>
        /// Obtiene la imagen asociada al item
        /// </summary>
        /// <param name="index">index del item</param>
        /// <returns>Imagen asociada</returns>
        public Image getImagen(int index)
        {
            Image img = null;

            if (this.Items.Length >= index)
            {
                int idx = Items[index].Index;
                if (idx > -1)
                    img = ImageList.Images[idx];
                else
                    img = Items[index].Image;
            }

            return img;
        }
    }

    [Serializable]
    public class ComboBoxItem
    {
        public int Index { get; set; }
        public Image Image { get; set; }
        public string Text { get; set; }
        public object Tag { get; set; }

        public ComboBoxItem()
        {
            this.Index = -1;
        }
    }
}
