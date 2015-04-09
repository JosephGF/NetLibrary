using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.IO;


namespace NetLibrary.Forms.Controls
{
    internal static class UtilDrive
    {
        internal static Image GetDriveImage(DriveInfo driveInfo)
        {
            Image resultado = null;

            switch (driveInfo.DriveType)
            {
                case DriveType.CDRom:
                    break;
                case DriveType.Fixed:
                    break;
                case DriveType.Network:
                    break;
                case DriveType.NoRootDirectory:
                    break;
                case DriveType.Ram:
                    break;
                case DriveType.Removable:
                    break;
                case DriveType.Unknown:
                    break;
            }
            resultado = NetLibrary.Archives.Directories.getAssociatedIcon(driveInfo.RootDirectory).ToBitmap();
            return resultado;
        }
        internal static void DrawItems(DriveInfo drive, DrawItemEventArgs e)
        {
            // Dibujamos el fondo
            e.DrawBackground();
            // Creamos los objetos GDI+
            //Brush brush = new SolidBrush(Color.Black);
            Pen forePen = new Pen(e.ForeColor);
            Brush foreBrush = new SolidBrush(e.ForeColor);
            // Dibujamos el borde del rectángulo

            //Icon icono = Iconos.IconAsociado(drive.Name);
            Image imagen = UtilDrive.GetDriveImage(drive);

            if (imagen != null)
                e.Graphics.DrawImage(imagen, e.Bounds.Left + 2, e.Bounds.Top + 2, e.Bounds.Size.Height - 4, e.Bounds.Size.Height - 4);
            // Rellenamos el rectángulo con el Color seleccionado
            // en la combo
            //e.Graphics.FillRectangle(brush, new Rectangle(e.Bounds.Left + 3, e.Bounds.Top + 3, 18, e.Bounds.Size.Height - 5));
            // Dibujamos el nombre del color
            string label = "Unkown";
            if (drive.IsReady)
                label = drive.VolumeLabel;

            e.Graphics.DrawString(label + " (" + drive.Name + ")", e.Font, foreBrush, e.Bounds.Size.Height + 10, e.Bounds.Top + 2);
            // Eliminamos objetos GDI+
            //brush.Dispose();
            forePen.Dispose();
            foreBrush.Dispose();
        }
    }

    [ToolboxBitmap(typeof(ComboBox), "CbDriver")]
    public partial class UIComboBoxDrive : ComboBox
    {
        public new DriveInfo SelectedItem
        {
            get { return (DriveInfo)base.SelectedItem; }
            set { base.SelectedItem = value; }
        }

        public UIComboBoxDrive()
        {
            //InitializeComponent();
            this.DrawItem += new DrawItemEventHandler(comboBoxDrive_DrawItem);
            this.DrawMode = DrawMode.OwnerDrawVariable;
            this.DropDownStyle = ComboBoxStyle.DropDownList;
            //this.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            //this.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;

            //this.Items.AddRange(DriveInfo.GetDrives());
        }

        private void comboBoxDrive_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index == -1) return;
            if (!(this.Items[e.Index] is DriveInfo)) return;
            DriveInfo drive = (DriveInfo)this.Items[e.Index];

            UtilDrive.DrawItems(drive, e);
        }
    }

    [ToolboxBitmap(typeof(ListBox), "lbDriver")]
    public partial class UIListBoxDrive : ListBox
    {
        public UIListBoxDrive()
        {
            //InitializeComponent();
            this.DrawItem += new DrawItemEventHandler(comboBoxDrive_DrawItem);
            this.DrawMode = DrawMode.OwnerDrawVariable;
            //this.DropDownStyle = ComboBoxStyle.DropDown;
            //this.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            //this.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;

            //this.Items.AddRange(DriveInfo.GetDrives());
        }

        public DriveInfo SelectedItem
        {
            get { return (DriveInfo)base.SelectedItem; }
            set { base.SelectedItem = value; }
        }

        private void comboBoxDrive_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (!(this.Items[e.Index] is DriveInfo)) return;
            DriveInfo drive = (DriveInfo)this.Items[e.Index];

            UtilDrive.DrawItems(drive, e);
        }
    }
}
