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

    public partial class UIListViewFileExplorer : ListView
    {
        public event EventHandler Item_DobleClick;
        //Special Folders
        //  Environment.GetFolderPath();
        //SpecialFolder.MyComputer
        //SpecialFolder.DesktopDirectory
        //SpecialFolder.MyDocuments
        //SpecialFolder.MyImages
        //SpecialFolder.MyMusic

        private string[] GetSpecialDirectory()
        {
            List<string> lista = new List<string>();
            //lista.Add(System.Environment.GetFolderPath(SpecialFolder.MyComputer));
            //lista.Add(System.Environment.GetFolderPath(SpecialFolder.DesktopDirectory));
            //lista.Add(System.Environment.GetFolderPath(SpecialFolder.MyDocuments));
            //lista.Add(System.Environment.GetFolderPath(SpecialFolder.MyImages));
            //lista.Add(System.Environment.GetFolderPath(SpecialFolder.MyMusic));

            return lista.ToArray();
        }

        // Create two ImageList objects.
        ImageList imageListSmall = new ImageList();
        ImageList imageListLarge = new ImageList();
        private string _path = "";

        public enum DisplayType
        {
            Ficheros
            ,
            Carpetas
                , Todo
        }

        private DisplayType _elementosVisibles = DisplayType.Todo;
        public DisplayType ElementosVisibles
        {
            get { return _elementosVisibles; }
            set { _elementosVisibles = value; }
        }

        public string Path
        {
            get { return _path; }
            set
            {
                _path = value;
                ObtenerElelementos(value);
            }
        }

        public UIListViewFileExplorer()
        {
            //Assign the ImageList objects to the ListView.
            this.LargeImageList = imageListLarge;
            this.SmallImageList = imageListLarge;
        }

        private void ObtenerElelementos(string directorio)
        {
            try
            {
                this.Items.Clear();
                this.Columns.Clear();

                if (this.ElementosVisibles == DisplayType.Carpetas || this.ElementosVisibles == DisplayType.Todo)
                {
                    string[] directorios = Directory.GetDirectories(directorio);
                    GenerarDirectorioItem(directorios);
                }


                if (this.ElementosVisibles == DisplayType.Ficheros || this.ElementosVisibles == DisplayType.Todo)
                {
                    string[] ficheros = Directory.GetFiles(directorio);
                    GenerarFicreroItem(ficheros);
                }

                this.Columns.Add("Name", "Nombre");
                this.Columns.Add("Size", "Tamaño");
                this.Columns.Add("Type", "Tipo");
                this.Columns.Add("FMod", "Fecha de modificación");
                this.Columns.Add("FCre", "Fecha de creación");
            }
            catch (Exception ex)
            { }
        }

        private void GenerarDirectorioItem(string[] items)
        {
            //imageListLarge.Images.Add("DIR", Properties.Resources._1_folder_open);
            foreach (string directorio in items)
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(directorio);

                ListViewItem item = new ListViewItem();
                item.ImageKey = "DIR";
                item.Text = directoryInfo.Name;
                //Tamanno
                ListViewItem.ListViewSubItem listViewSubItem = new ListViewItem.ListViewSubItem();
                listViewSubItem.Text = "";
                item.SubItems.Add(listViewSubItem);
                //Tipo
                listViewSubItem = new ListViewItem.ListViewSubItem();
                listViewSubItem.Text = "Carpeta de archivos";
                item.SubItems.Add(listViewSubItem);
                //Fecha Mod.
                listViewSubItem = new ListViewItem.ListViewSubItem();
                listViewSubItem.Text = directoryInfo.LastWriteTime.ToLongDateString();
                item.SubItems.Add(listViewSubItem);
                //Fecha Creacion
                listViewSubItem = new ListViewItem.ListViewSubItem();
                listViewSubItem.Text = directoryInfo.CreationTime.ToLongDateString();

                item.Tag = directoryInfo;
                this.Items.Add(item);
            }
        }

        private void GenerarFicreroItem(string[] items)
        {
            Int32 exeExtension = 0;
            foreach (string fichero in items)
            {
                FileInfo fileInfo = new FileInfo(fichero);
                Icon iconoAux = null;

                string strExtension = fileInfo.Extension;

                if (".exe".Equals(strExtension))
                {
                    exeExtension++;
                    strExtension = strExtension + exeExtension.ToString();
                }

                try
                {
                    iconoAux = NetLibrary.Archives.Files.getAssociatedIcon(fileInfo);
                    imageListSmall.Images.Add(strExtension, iconoAux);

                    //iconoAux = Util.Iconos.Extract(fichero, Iconos.IconSize.Large);
                    imageListLarge.Images.Add(strExtension, iconoAux);
                }
                catch (Exception ex)
                { }


                ListViewItem item = new ListViewItem();
                item.ImageKey = fileInfo.Extension;
                item.Text = fileInfo.Name;
                //Tamanno
                ListViewItem.ListViewSubItem listViewSubItem = new ListViewItem.ListViewSubItem();
                listViewSubItem.Text = fileInfo.Length.ToString();// Util.Conversor.StrTamannoBytes(fileInfo.Length);
                item.SubItems.Add(listViewSubItem);
                //Tipo
                listViewSubItem = new ListViewItem.ListViewSubItem();
                listViewSubItem.Text = ObtenerDescripcionTipoArchivo(fileInfo.Extension);
                item.SubItems.Add(listViewSubItem);
                //Fecha Mod.
                listViewSubItem = new ListViewItem.ListViewSubItem();
                listViewSubItem.Text = ObtenerDescripcionTipoArchivo(fileInfo.LastWriteTime.ToLongDateString());
                item.SubItems.Add(listViewSubItem);
                //Fecha Creacion
                listViewSubItem = new ListViewItem.ListViewSubItem();
                listViewSubItem.Text = ObtenerDescripcionTipoArchivo(fileInfo.CreationTime.ToLongDateString());
                item.SubItems.Add(listViewSubItem);

                item.Tag = fileInfo;
                this.Items.Add(item);
            }
        }

        private string ObtenerDescripcionTipoArchivo(string extension)
        {
            string resultado = "";

            switch (extension.ToUpper())
            {
                case ".EXE":
                    resultado = "Aplicación";
                    break;
                case ".DLL":
                    resultado = "Extensión de la aplicación";
                    break;
                default:
                    resultado = "Archivo " + extension.ToUpper();
                    break;
            }

            return resultado;
        }

        private void ListViewFileExplorerItem_DoubleClick(object sender, EventArgs e)
        {
            if (this.SelectedItems == null || this.SelectedItems[0].Tag == null)
                return;

            if (this.Item_DobleClick != null)
            {
                Item_DobleClick(this.SelectedItems[0].Tag, e);
                return;
            }

            if (this.SelectedItems[0].Tag is DirectoryInfo)
            {
                this.Path = ((DirectoryInfo)this.SelectedItems[0].Tag).FullName;
            }
            else if (this.SelectedItems[0].Tag is FileInfo)
            {
                System.Diagnostics.Process.Start(((FileInfo)this.SelectedItems[0].Tag).FullName);
            }
            else
            {
                //MessageBox.Show("");
            }
        }
    }
}
