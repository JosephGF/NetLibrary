namespace NetLibrary.Forms.Beauty
{
    partial class BeautyFormAbout : BeautyForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        public System.ComponentModel.IContainer components = null;

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
            this.pbIcon = new System.Windows.Forms.PictureBox();
            this.lbTitle = new System.Windows.Forms.Label();
            this.lbVersion = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lbAutor = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lbDescription = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lbCopyright = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.windowBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgForm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbIcon)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // windowBar
            // 
            this.windowBar.Size = new System.Drawing.Size(429, 24);
            // 
            // btnHelp
            // 
            this.btnHelp.Location = new System.Drawing.Point(333, 0);
            // 
            // btnMinimize
            // 
            this.btnMinimize.Location = new System.Drawing.Point(357, 0);
            // 
            // btMaximize
            // 
            this.btMaximize.Location = new System.Drawing.Point(381, 0);
            // 
            // btnCerrar
            // 
            this.btnCerrar.Location = new System.Drawing.Point(405, 0);
            // 
            // lbTitulo
            // 
            this.lbTitulo.Size = new System.Drawing.Size(309, 24);
            this.lbTitulo.Text = "About";
            // 
            // pbIcon
            // 
            this.pbIcon.Location = new System.Drawing.Point(9, 31);
            this.pbIcon.Name = "pbIcon";
            this.pbIcon.Size = new System.Drawing.Size(64, 64);
            this.pbIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbIcon.TabIndex = 0;
            this.pbIcon.TabStop = false;
            // 
            // lbTitle
            // 
            this.lbTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTitle.Location = new System.Drawing.Point(84, 29);
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Size = new System.Drawing.Size(333, 23);
            this.lbTitle.TabIndex = 1;
            this.lbTitle.Text = "Titulo";
            this.lbTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbVersion
            // 
            this.lbVersion.AutoSize = true;
            this.lbVersion.Location = new System.Drawing.Point(351, 54);
            this.lbVersion.Name = "lbVersion";
            this.lbVersion.Size = new System.Drawing.Size(40, 13);
            this.lbVersion.TabIndex = 2;
            this.lbVersion.Text = "0.0.0.0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(319, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Ver.";
            // 
            // lbAutor
            // 
            this.lbAutor.AutoSize = true;
            this.lbAutor.Location = new System.Drawing.Point(161, 64);
            this.lbAutor.Name = "lbAutor";
            this.lbAutor.Size = new System.Drawing.Size(100, 13);
            this.lbAutor.TabIndex = 2;
            this.lbAutor.Text = "Joseph Girón Flores";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(120, 59);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Autor:";
            // 
            // lbDescription
            // 
            this.lbDescription.AutoSize = true;
            this.lbDescription.Location = new System.Drawing.Point(3, 0);
            this.lbDescription.Name = "lbDescription";
            this.lbDescription.Size = new System.Drawing.Size(40, 13);
            this.lbDescription.TabIndex = 2;
            this.lbDescription.Text = "0.0.0.0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(120, 88);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Description:";
            // 
            // lbCopyright
            // 
            this.lbCopyright.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbCopyright.Location = new System.Drawing.Point(0, 157);
            this.lbCopyright.Name = "lbCopyright";
            this.lbCopyright.Size = new System.Drawing.Size(429, 13);
            this.lbCopyright.TabIndex = 2;
            this.lbCopyright.Text = "0.0.0.0";
            this.lbCopyright.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.lbDescription);
            this.panel1.Location = new System.Drawing.Point(123, 105);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(294, 42);
            this.panel1.TabIndex = 3;
            // 
            // BeautyFormAbout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(429, 170);
            this.Controls.Add(this.lbCopyright);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbAutor);
            this.Controls.Add(this.lbVersion);
            this.Controls.Add(this.lbTitle);
            this.Controls.Add(this.pbIcon);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1920, 1040);
            this.MinimizeBox = false;
            this.Name = "BeautyFormAbout";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "About";
            this.Controls.SetChildIndex(this.pbIcon, 0);
            this.Controls.SetChildIndex(this.lbTitle, 0);
            this.Controls.SetChildIndex(this.lbVersion, 0);
            this.Controls.SetChildIndex(this.lbAutor, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.lbCopyright, 0);
            this.Controls.SetChildIndex(this.windowBar, 0);
            this.windowBar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imgForm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbIcon)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.PictureBox pbIcon;
        public System.Windows.Forms.Label lbTitle;
        public System.Windows.Forms.Label lbVersion;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.Label lbAutor;
        public System.Windows.Forms.Label label4;
        public System.Windows.Forms.Label lbDescription;
        public System.Windows.Forms.Label label6;
        public System.Windows.Forms.Label lbCopyright;
        public System.Windows.Forms.Panel panel1;

    }
}