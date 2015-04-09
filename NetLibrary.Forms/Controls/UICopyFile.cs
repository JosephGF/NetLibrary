using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Threading;
using System.Text;
using System.IO;
using System.Windows.Forms;
using NetLibrary.Archives;

namespace NetLibrary.Forms.Controls
{
    [ToolboxBitmap(typeof(ProgressBar), "CopyFile")]
    public partial class UICopyFile : UserControl
    {
        public delegate void FileEventHandler(object sender, FileEventArgs e);
        public event FileEventHandler onFileActionProgressChange;
        public event FileEventHandler onFileActionComplete;

        /// <summary>
        /// Fichero que se copiara
        /// </summary>
        public FileInfo Fichero { get; set; }
        /// <summary>
        /// Ruta de destino dónde se copiarán/moveran los archivos
        /// </summary>
        public string Destino { get; set; }
        /// <summary>
        /// Acción que se realizará (Copy|Move)
        /// </summary>
        public NetLibrary.Archives.Action Accion = Archives.Action.Copy;
        /// <summary>
        /// Muestra el progreso en la barra de tareas
        /// </summary>
        //public bool ShowProgressInTaskBar
        //{
        //    get { return this.iuProgressBar1.VisibleInTaskBar; }
        //    set { this.iuProgressBar1.VisibleInTaskBar = value; }
        //}

        private Thread _thread = null;

        public UICopyFile()
        {
            InitializeComponent();
            NetLibrary.Archives.Files.onFileActionProgressChange += new Files.FileEventHandler(Files_onFileActionProgressChange);
            NetLibrary.Archives.Files.onFileActionComplete += new Files.FileEventHandler(Files_onFileActionComplete);
        }

        /// <summary>
        /// Evento que se lanza cuando se completa una accion (Copiar/Mover)
        /// </summary>
        /// <param name="sender">Fichero sobre el que se realiza la acción</param>
        /// <param name="e">Datos del evento</param>
        private void Files_onFileActionComplete(object sender, Archives.FileEventArgs e)
        {
            NetLibrary.Reflection.Manager.Invoke(this.lbText, "Text", "Proceso Finalizado.");

            if (this.onFileActionComplete != null)
                this.onFileActionComplete(sender, e);
        }

        /// <summary>
        /// Evento que se lanza cuando cambia el progreso de la accion (Copiar/Mover)
        /// </summary>
        /// <param name="sender">Fichero sobre el que se realiza la acción</param>
        /// <param name="e">Datos del evento</param>
        private void Files_onFileActionProgressChange(object sender, Archives.FileEventArgs e)
        {
            NetLibrary.Reflection.Manager.Invoke(this.lbText, "Text", "Copiando " + e.File.Name + " - " + e.Progress + "%");
            NetLibrary.Reflection.Manager.Invoke(this.iuProgressBar1, "Value", e.Progress);

            if (this.onFileActionProgressChange != null)
                this.onFileActionProgressChange(sender, e);
        }

        /// <summary>
        /// Inicia la accion
        /// </summary>
        public void start()
        {
            if (Fichero != null && Fichero.Exists)
            {
                switch (Accion)
                {
                    case Archives.Action.Copy:
                        _thread = NetLibrary.Archives.Files.CopyAsync(this.Fichero, this.Destino);
                        break;
                    case Archives.Action.Move:
                        _thread = NetLibrary.Archives.Files.MoveAsync(this.Fichero, this.Destino);
                        break;
                }
            }
        }
    }
}
