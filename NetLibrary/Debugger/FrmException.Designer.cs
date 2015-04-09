namespace NetLibrary.Debugger
{
    partial class FrmException
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
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmException));
            this.btnClose = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lbTitulo = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabDetalle = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txDetalles = new System.Windows.Forms.TextBox();
            this.tabCaptura = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pbScreenshot = new System.Windows.Forms.PictureBox();
            this.tabSistemaInfo = new System.Windows.Forms.TabPage();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txSistema = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.tabResumen = new System.Windows.Forms.TabPage();
            this.panel4 = new System.Windows.Forms.Panel();
            this.rtbResumen = new System.Windows.Forms.RichTextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.btnRestart = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabDetalle.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabCaptura.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbScreenshot)).BeginInit();
            this.tabSistemaInfo.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tabResumen.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(527, 327);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(122, 23);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Cerrar Aplicación";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(4, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(50, 43);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // lbTitulo
            // 
            this.lbTitulo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTitulo.Location = new System.Drawing.Point(74, 13);
            this.lbTitulo.Name = "lbTitulo";
            this.lbTitulo.Size = new System.Drawing.Size(575, 33);
            this.lbTitulo.TabIndex = 2;
            this.lbTitulo.Text = "Opps, Se ha producido un error grave.";
            this.lbTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabDetalle);
            this.tabControl1.Controls.Add(this.tabCaptura);
            this.tabControl1.Controls.Add(this.tabSistemaInfo);
            this.tabControl1.Controls.Add(this.tabResumen);
            this.tabControl1.Location = new System.Drawing.Point(10, 50);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(637, 271);
            this.tabControl1.TabIndex = 4;
            // 
            // tabDetalle
            // 
            this.tabDetalle.Controls.Add(this.panel1);
            this.tabDetalle.Location = new System.Drawing.Point(4, 22);
            this.tabDetalle.Name = "tabDetalle";
            this.tabDetalle.Padding = new System.Windows.Forms.Padding(3);
            this.tabDetalle.Size = new System.Drawing.Size(629, 245);
            this.tabDetalle.TabIndex = 0;
            this.tabDetalle.Text = "Detalles";
            this.tabDetalle.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txDetalles);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(623, 239);
            this.panel1.TabIndex = 0;
            // 
            // txDetalles
            // 
            this.txDetalles.BackColor = System.Drawing.SystemColors.Control;
            this.txDetalles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txDetalles.Location = new System.Drawing.Point(0, 0);
            this.txDetalles.Multiline = true;
            this.txDetalles.Name = "txDetalles";
            this.txDetalles.ReadOnly = true;
            this.txDetalles.Size = new System.Drawing.Size(623, 239);
            this.txDetalles.TabIndex = 0;
            // 
            // tabCaptura
            // 
            this.tabCaptura.Controls.Add(this.panel2);
            this.tabCaptura.Location = new System.Drawing.Point(4, 22);
            this.tabCaptura.Name = "tabCaptura";
            this.tabCaptura.Padding = new System.Windows.Forms.Padding(3);
            this.tabCaptura.Size = new System.Drawing.Size(629, 245);
            this.tabCaptura.TabIndex = 1;
            this.tabCaptura.Text = "Captura";
            this.tabCaptura.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.pbScreenshot);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(623, 239);
            this.panel2.TabIndex = 1;
            // 
            // pbScreenshot
            // 
            this.pbScreenshot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbScreenshot.Location = new System.Drawing.Point(0, 0);
            this.pbScreenshot.Name = "pbScreenshot";
            this.pbScreenshot.Size = new System.Drawing.Size(623, 239);
            this.pbScreenshot.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbScreenshot.TabIndex = 0;
            this.pbScreenshot.TabStop = false;
            // 
            // tabSistemaInfo
            // 
            this.tabSistemaInfo.Controls.Add(this.panel3);
            this.tabSistemaInfo.Location = new System.Drawing.Point(4, 22);
            this.tabSistemaInfo.Name = "tabSistemaInfo";
            this.tabSistemaInfo.Size = new System.Drawing.Size(629, 245);
            this.tabSistemaInfo.TabIndex = 2;
            this.tabSistemaInfo.Text = "Sistema";
            this.tabSistemaInfo.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.txSistema);
            this.panel3.Controls.Add(this.textBox2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(629, 245);
            this.panel3.TabIndex = 1;
            // 
            // txSistema
            // 
            this.txSistema.BackColor = System.Drawing.SystemColors.Control;
            this.txSistema.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txSistema.Location = new System.Drawing.Point(0, 0);
            this.txSistema.Multiline = true;
            this.txSistema.Name = "txSistema";
            this.txSistema.ReadOnly = true;
            this.txSistema.Size = new System.Drawing.Size(629, 245);
            this.txSistema.TabIndex = 2;
            // 
            // textBox2
            // 
            this.textBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox2.Location = new System.Drawing.Point(0, 0);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(629, 245);
            this.textBox2.TabIndex = 1;
            // 
            // tabResumen
            // 
            this.tabResumen.Controls.Add(this.panel4);
            this.tabResumen.Location = new System.Drawing.Point(4, 22);
            this.tabResumen.Name = "tabResumen";
            this.tabResumen.Size = new System.Drawing.Size(629, 245);
            this.tabResumen.TabIndex = 3;
            this.tabResumen.Text = "Resumen";
            this.tabResumen.UseVisualStyleBackColor = true;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.rtbResumen);
            this.panel4.Controls.Add(this.textBox3);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(629, 245);
            this.panel4.TabIndex = 1;
            // 
            // rtbResumen
            // 
            this.rtbResumen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbResumen.Location = new System.Drawing.Point(0, 0);
            this.rtbResumen.Name = "rtbResumen";
            this.rtbResumen.Size = new System.Drawing.Size(629, 245);
            this.rtbResumen.TabIndex = 2;
            this.rtbResumen.Text = "";
            // 
            // textBox3
            // 
            this.textBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox3.Location = new System.Drawing.Point(0, 0);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(629, 245);
            this.textBox3.TabIndex = 1;
            // 
            // btnRestart
            // 
            this.btnRestart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRestart.Location = new System.Drawing.Point(399, 327);
            this.btnRestart.Name = "btnRestart";
            this.btnRestart.Size = new System.Drawing.Size(122, 23);
            this.btnRestart.TabIndex = 0;
            this.btnRestart.Text = "Reiniciar Aplicación";
            this.btnRestart.UseVisualStyleBackColor = true;
            this.btnRestart.Click += new System.EventHandler(this.btnRestart_Click);
            // 
            // FrmException
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(654, 356);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.lbTitulo);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnRestart);
            this.Controls.Add(this.btnClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "FrmException";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Error";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabDetalle.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabCaptura.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbScreenshot)).EndInit();
            this.tabSistemaInfo.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.tabResumen.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.TabControl tabControl1;
        protected System.Windows.Forms.TabPage tabDetalle;
        protected System.Windows.Forms.Panel panel1;
        protected System.Windows.Forms.TabPage tabCaptura;
        protected System.Windows.Forms.Panel panel2;
        protected System.Windows.Forms.TabPage tabSistemaInfo;
        protected System.Windows.Forms.Panel panel3;
        protected System.Windows.Forms.TabPage tabResumen;
        protected System.Windows.Forms.Panel panel4;
        protected System.Windows.Forms.TextBox txDetalles;
        protected System.Windows.Forms.TextBox textBox2;
        protected System.Windows.Forms.TextBox textBox3;
        protected System.Windows.Forms.PictureBox pbScreenshot;
        protected System.Windows.Forms.TextBox txSistema;
        protected System.Windows.Forms.Button btnClose;
        protected System.Windows.Forms.PictureBox pictureBox1;
        protected System.Windows.Forms.Label lbTitulo;
        protected System.Windows.Forms.Button btnRestart;
        protected System.Windows.Forms.RichTextBox rtbResumen;
    }
}