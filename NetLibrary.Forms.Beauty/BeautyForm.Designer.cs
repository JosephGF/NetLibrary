namespace NetLibrary.Forms.Beauty
{
    partial class BeautyForm
    {
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
        public void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.windowBar = new System.Windows.Forms.Panel();
            this.lbTitulo = new System.Windows.Forms.Label();
            this.imgForm = new System.Windows.Forms.PictureBox();
            this.btnHelp = new System.Windows.Forms.Label();
            this.btnMinimize = new System.Windows.Forms.Label();
            this.btMaximize = new System.Windows.Forms.Label();
            this.btnCerrar = new System.Windows.Forms.Label();
            this.ctxMenuTitleBar = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.restaurarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moverToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tamañoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.minimizarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.maximizarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.cerrarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.windowBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgForm)).BeginInit();
            this.ctxMenuTitleBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // windowBar
            // 
            this.windowBar.Controls.Add(this.lbTitulo);
            this.windowBar.Controls.Add(this.imgForm);
            this.windowBar.Controls.Add(this.btnHelp);
            this.windowBar.Controls.Add(this.btnMinimize);
            this.windowBar.Controls.Add(this.btMaximize);
            this.windowBar.Controls.Add(this.btnCerrar);
            this.windowBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.windowBar.Location = new System.Drawing.Point(0, 0);
            this.windowBar.Name = "windowBar";
            this.windowBar.Size = new System.Drawing.Size(893, 24);
            this.windowBar.TabIndex = 0;
            // 
            // lbTitulo
            // 
            this.lbTitulo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTitulo.Location = new System.Drawing.Point(24, 0);
            this.lbTitulo.Name = "lbTitulo";
            this.lbTitulo.Size = new System.Drawing.Size(773, 24);
            this.lbTitulo.TabIndex = 5;
            this.lbTitulo.Text = "label1";
            this.lbTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbTitulo.DoubleClick += new System.EventHandler(this.lbTitulo_DoubleClick);
            this.lbTitulo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lbTitulo_MouseDown);
            // 
            // imgForm
            // 
            this.imgForm.Dock = System.Windows.Forms.DockStyle.Left;
            this.imgForm.Location = new System.Drawing.Point(0, 0);
            this.imgForm.Name = "imgForm";
            this.imgForm.Size = new System.Drawing.Size(24, 24);
            this.imgForm.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imgForm.TabIndex = 4;
            this.imgForm.TabStop = false;
            // 
            // btnHelp
            // 
            this.btnHelp.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnHelp.Location = new System.Drawing.Point(797, 0);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(24, 24);
            this.btnHelp.TabIndex = 3;
            this.btnHelp.Text = "?";
            this.btnHelp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnHelp.MouseEnter += new System.EventHandler(this.buttonTitleBar_MouseEnter);
            this.btnHelp.MouseLeave += new System.EventHandler(this.buttonTitleBar_MouseLeave);
            // 
            // btnMinimize
            // 
            this.btnMinimize.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnMinimize.Location = new System.Drawing.Point(821, 0);
            this.btnMinimize.Name = "btnMinimize";
            this.btnMinimize.Size = new System.Drawing.Size(24, 24);
            this.btnMinimize.TabIndex = 2;
            this.btnMinimize.Text = "_";
            this.btnMinimize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnMinimize.Click += new System.EventHandler(this.btnMinimize_Click);
            this.btnMinimize.MouseEnter += new System.EventHandler(this.buttonTitleBar_MouseEnter);
            this.btnMinimize.MouseLeave += new System.EventHandler(this.buttonTitleBar_MouseLeave);
            // 
            // btMaximize
            // 
            this.btMaximize.Dock = System.Windows.Forms.DockStyle.Right;
            this.btMaximize.Location = new System.Drawing.Point(845, 0);
            this.btMaximize.Name = "btMaximize";
            this.btMaximize.Size = new System.Drawing.Size(24, 24);
            this.btMaximize.TabIndex = 1;
            this.btMaximize.Text = "M";
            this.btMaximize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btMaximize.Click += new System.EventHandler(this.btMaximize_Click);
            this.btMaximize.MouseEnter += new System.EventHandler(this.buttonTitleBar_MouseEnter);
            this.btMaximize.MouseLeave += new System.EventHandler(this.buttonTitleBar_MouseLeave);
            // 
            // btnCerrar
            // 
            this.btnCerrar.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCerrar.Location = new System.Drawing.Point(869, 0);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(24, 24);
            this.btnCerrar.TabIndex = 0;
            this.btnCerrar.Text = "X";
            this.btnCerrar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            this.btnCerrar.MouseEnter += new System.EventHandler(this.buttonTitleBar_MouseEnter);
            this.btnCerrar.MouseLeave += new System.EventHandler(this.buttonTitleBar_MouseLeave);
            // 
            // ctxMenuTitleBar
            // 
            this.ctxMenuTitleBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.restaurarToolStripMenuItem,
            this.moverToolStripMenuItem,
            this.tamañoToolStripMenuItem,
            this.minimizarToolStripMenuItem,
            this.maximizarToolStripMenuItem,
            this.toolStripSeparator1,
            this.cerrarToolStripMenuItem});
            this.ctxMenuTitleBar.Name = "ctxMenuTitleBar";
            this.ctxMenuTitleBar.Size = new System.Drawing.Size(129, 142);
            // 
            // restaurarToolStripMenuItem
            // 
            this.restaurarToolStripMenuItem.Name = "restaurarToolStripMenuItem";
            this.restaurarToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.restaurarToolStripMenuItem.Text = "Restaurar";
            // 
            // moverToolStripMenuItem
            // 
            this.moverToolStripMenuItem.Name = "moverToolStripMenuItem";
            this.moverToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.moverToolStripMenuItem.Text = "Mover";
            // 
            // tamañoToolStripMenuItem
            // 
            this.tamañoToolStripMenuItem.Enabled = false;
            this.tamañoToolStripMenuItem.Name = "tamañoToolStripMenuItem";
            this.tamañoToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.tamañoToolStripMenuItem.Text = "Tamaño";
            // 
            // minimizarToolStripMenuItem
            // 
            this.minimizarToolStripMenuItem.Name = "minimizarToolStripMenuItem";
            this.minimizarToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.minimizarToolStripMenuItem.Text = "Minimizar";
            // 
            // maximizarToolStripMenuItem
            // 
            this.maximizarToolStripMenuItem.Name = "maximizarToolStripMenuItem";
            this.maximizarToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.maximizarToolStripMenuItem.Text = "Maximizar";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(125, 6);
            // 
            // cerrarToolStripMenuItem
            // 
            this.cerrarToolStripMenuItem.Name = "cerrarToolStripMenuItem";
            this.cerrarToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.cerrarToolStripMenuItem.Text = "Cerrar";
            // 
            // BeautyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(893, 554);
            this.Controls.Add(this.windowBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "BeautyForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "BeautyForm";
            this.Load += new System.EventHandler(this.BeautyForm_Load);
            this.windowBar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imgForm)).EndInit();
            this.ctxMenuTitleBar.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.Panel windowBar;
        protected System.Windows.Forms.Label btnHelp;
        protected System.Windows.Forms.Label btnMinimize;
        protected System.Windows.Forms.Label btMaximize;
        protected System.Windows.Forms.Label btnCerrar;
        protected System.Windows.Forms.Label lbTitulo;
        protected System.Windows.Forms.PictureBox imgForm;
        private System.Windows.Forms.ContextMenuStrip ctxMenuTitleBar;
        private System.Windows.Forms.ToolStripMenuItem restaurarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem moverToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tamañoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem minimizarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem maximizarToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem cerrarToolStripMenuItem;
    }
}