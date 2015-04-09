namespace NetLibrary.Forms
{
    partial class ToastForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ToastForm));
            this.pbImagen = new System.Windows.Forms.PictureBox();
            this.lbTitulo = new System.Windows.Forms.Label();
            this.lbText = new System.Windows.Forms.Label();
            this.timerAutoClose = new System.Windows.Forms.Timer(this.components);
            this.lbClose = new System.Windows.Forms.Label();
            this.lbAncla = new System.Windows.Forms.Label();
            this.imageListPush = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pbImagen)).BeginInit();
            this.SuspendLayout();
            // 
            // pbImagen
            // 
            this.pbImagen.Location = new System.Drawing.Point(4, 3);
            this.pbImagen.Name = "pbImagen";
            this.pbImagen.Size = new System.Drawing.Size(85, 85);
            this.pbImagen.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbImagen.TabIndex = 0;
            this.pbImagen.TabStop = false;
            this.pbImagen.Click += new System.EventHandler(this.pbImagen_Click);
            this.pbImagen.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ToastForm_MouseMove);
            // 
            // lbTitulo
            // 
            this.lbTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTitulo.Location = new System.Drawing.Point(103, 3);
            this.lbTitulo.Name = "lbTitulo";
            this.lbTitulo.Size = new System.Drawing.Size(214, 23);
            this.lbTitulo.TabIndex = 1;
            this.lbTitulo.Text = "label1";
            this.lbTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbTitulo.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ToastForm_MouseMove);
            // 
            // lbText
            // 
            this.lbText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbText.Location = new System.Drawing.Point(97, 26);
            this.lbText.Name = "lbText";
            this.lbText.Size = new System.Drawing.Size(220, 62);
            this.lbText.TabIndex = 2;
            this.lbText.Text = "label2";
            this.lbText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbText.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ToastForm_MouseMove);
            // 
            // timerAutoClose
            // 
            this.timerAutoClose.Interval = 3500;
            this.timerAutoClose.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lbClose
            // 
            this.lbClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbClose.ForeColor = System.Drawing.Color.Brown;
            this.lbClose.Image = ((System.Drawing.Image)(resources.GetObject("lbClose.Image")));
            this.lbClose.Location = new System.Drawing.Point(310, 1);
            this.lbClose.Name = "lbClose";
            this.lbClose.Size = new System.Drawing.Size(14, 17);
            this.lbClose.TabIndex = 3;
            this.lbClose.Text = " ";
            this.lbClose.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lbClose.Click += new System.EventHandler(this.lbClose_Click);
            this.lbClose.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ToastForm_MouseMove);
            // 
            // lbAncla
            // 
            this.lbAncla.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbAncla.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbAncla.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbAncla.ForeColor = System.Drawing.Color.Brown;
            this.lbAncla.ImageIndex = 1;
            this.lbAncla.ImageList = this.imageListPush;
            this.lbAncla.Location = new System.Drawing.Point(292, 3);
            this.lbAncla.Name = "lbAncla";
            this.lbAncla.Size = new System.Drawing.Size(14, 17);
            this.lbAncla.TabIndex = 4;
            this.lbAncla.Text = " ";
            this.lbAncla.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lbAncla.Click += new System.EventHandler(this.lbAncla_Click);
            // 
            // imageListPush
            // 
            this.imageListPush.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListPush.ImageStream")));
            this.imageListPush.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListPush.Images.SetKeyName(0, "pushpin1.png");
            this.imageListPush.Images.SetKeyName(1, "pushpin.png");
            // 
            // ToastForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(325, 91);
            this.ControlBox = false;
            this.Controls.Add(this.lbAncla);
            this.Controls.Add(this.lbClose);
            this.Controls.Add(this.lbText);
            this.Controls.Add(this.lbTitulo);
            this.Controls.Add(this.pbImagen);
            this.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ToastForm";
            this.Opacity = 0D;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "ToastForm";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.ToastForm_Load);
            this.Shown += new System.EventHandler(this.ToastForm_Shown);
            this.ResizeEnd += new System.EventHandler(this.ToastForm_ResizeEnd);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ToastForm_MouseMove);
            ((System.ComponentModel.ISupportInitialize)(this.pbImagen)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.Timer timerAutoClose;
        protected System.Windows.Forms.PictureBox pbImagen;
        protected System.Windows.Forms.Label lbTitulo;
        protected System.Windows.Forms.Label lbText;
        protected System.Windows.Forms.Label lbClose;
        protected System.Windows.Forms.Label lbAncla;
        protected System.Windows.Forms.ImageList imageListPush;
    }
}