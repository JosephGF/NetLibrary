using Microsoft.WindowsAPICodePack.Shell;
using Microsoft.WindowsAPICodePack;
using Microsoft.WindowsAPICodePack.Taskbar;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows;
using System.ComponentModel.Design.Serialization;

namespace NetLibrary.Forms.Taskbar
{
    public class TaskBarButton
    {
        public TaskBarButton()
        {
            this.Visible = true;
            this.Enabled = true;
        }

        internal TaskBarButton(ThumbnailToolBarButton originalButon)
        {
            this._btn = originalButon;
        }

        private ThumbnailToolBarButton _btn = new ThumbnailToolBarButton(null, "");

        internal ThumbnailToolBarButton OriginalButton { get { return this._btn; } set { this._btn = value; } }
        public bool DismissOnClick { get { return this._btn.DismissOnClick; } set { this._btn.DismissOnClick = value; } }
        public bool Enabled { get { return this._btn.Enabled; } set { this._btn.Enabled = value; } }
        public bool IsInteractive { get { return this._btn.IsInteractive; } set { this._btn.IsInteractive = value; } }
        public bool Visible { get { return this._btn.Visible; } set { this._btn.Visible = value; } }
        public string Tooltip { get { return this._btn.Tooltip; } set { this._btn.Tooltip = value; } }
        public Icon Icon
        {
            get { return this._btn.Icon; }
            set
            {
                this._btn.Icon = value;
            }
        }
        public object Tag { get; set; }
        public string Name { get; set; }

        public event EventHandler<TaskBarButtonsClickedEventArgs> Click;

        #region propiedades ocultas
        new private System.Drawing.Size Size { get; set; }
        new private System.Drawing.Size MinimumSize { get; set; }
        new private System.Drawing.Size MaximumSize { get; set; }
        #endregion
        internal ThumbnailToolBarButton GetButton()
        {
            this._btn.Click += _btn_Click;
            return this._btn;
        }

        private void _btn_Click(object sender, ThumbnailButtonClickedEventArgs e)
        {
            if (this.Click != null)
                this.Click(this, new TaskBarButtonsClickedEventArgs(e));
        }

        public delegate void ButtonCLickEventHandler(object sender, ButtonClickEventArgs e);
        /// <summary>
        /// Se produce cuando se inicia el proceso
        /// </summary>
        public static event ButtonCLickEventHandler onButtonCLickEventHandler;

        public static void AddButton(String tooltip, Icon icon, IntPtr windowsHandle)
        {
            ThumbnailToolBarButton button = new ThumbnailToolBarButton(icon, tooltip);
            button.Click += new EventHandler<ThumbnailButtonClickedEventArgs>(button_Click);
            TaskbarManager.Instance.ThumbnailToolBars.AddButtons(windowsHandle, new ThumbnailToolBarButton[] { button });
        }

        private static void button_Click(object sender, ThumbnailButtonClickedEventArgs e)
        {
            if (onButtonCLickEventHandler != null)
                onButtonCLickEventHandler(sender, new ButtonClickEventArgs(e.ThumbnailButton.Tooltip, e.ThumbnailButton.Icon));
        }
    }
    public class TaskBarButtonsClickedEventArgs : EventArgs
    {
        public TaskBarButtonsClickedEventArgs(ThumbnailButtonClickedEventArgs e)
        {
            this.WindowHandle = e.WindowHandle;
            this.WindowsControl = e.WindowsControl;
        }

        public IntPtr WindowHandle { get; private set; }
        public UIElement WindowsControl { get; private set; }
    }
}
