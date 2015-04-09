using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetLibrary.Forms.Taskbar
{
    /// <summary>
    /// Heredar de el si se desea controlar eventos personalizados de la barra de tareas.
    /// También se debe cambiar el inicio de la aplicación por ProgramManager.Run(Form);
    /// </summary>
    public class TaskBarJumpListForm : Form
    {
        public TaskBarJumpListForm()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
        }

        #endregion

        public event EventHandler<CommandEventArgs> JumpListCommandReceived;
        public event EventHandler<StartupEventArgs> StarupInstance;

        internal void OnStartupInstance(StartupEventArgs e)
        {
            if (StarupInstance != null)
                StarupInstance(this, e);
        }

        internal void OnJumpListCommandReceived(CommandEventArgs e)
        {
            if (JumpListCommandReceived != null)
                JumpListCommandReceived(this, e);
        }

        protected override void WndProc(ref Message m)
        {
            if (WindowsMessageHelper.WindowMessages.ContainsKey(m.Msg))
            {
                OnJumpListCommandReceived(new CommandEventArgs(WindowsMessageHelper.WindowMessages[m.Msg]));
            }
            else if (m.Msg == WindowsMessageHelper.WM_COPYDATA)
            {
                string arguments = WindowsMessageHelper.GetArguments(m.LParam);
                OnStartupInstance(new StartupEventArgs(false, arguments));
            }
            else
            {
                base.WndProc(ref m);
            }
        }

    }
}
