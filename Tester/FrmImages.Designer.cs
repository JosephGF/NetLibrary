namespace Tester
{
    partial class FrmImages
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
            this.txSource = new System.Windows.Forms.TextBox();
            this.txDestino = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnResize = new System.Windows.Forms.Button();
            this.txHeight = new System.Windows.Forms.TextBox();
            this.txWidth = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // txSource
            // 
            this.txSource.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txSource.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystemDirectories;
            this.txSource.Location = new System.Drawing.Point(12, 39);
            this.txSource.Name = "txSource";
            this.txSource.Size = new System.Drawing.Size(741, 20);
            this.txSource.TabIndex = 0;
            this.txSource.Text = "D:\\tutiempo\\new";
            // 
            // txDestino
            // 
            this.txDestino.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txDestino.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystemDirectories;
            this.txDestino.Location = new System.Drawing.Point(12, 82);
            this.txDestino.Name = "txDestino";
            this.txDestino.Size = new System.Drawing.Size(741, 20);
            this.txDestino.TabIndex = 1;
            this.txDestino.Text = "D:\\tutiempo\\Convert";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Source";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Destino";
            // 
            // btnResize
            // 
            this.btnResize.Location = new System.Drawing.Point(12, 479);
            this.btnResize.Name = "btnResize";
            this.btnResize.Size = new System.Drawing.Size(94, 23);
            this.btnResize.TabIndex = 4;
            this.btnResize.Text = "Redimensionar";
            this.btnResize.UseVisualStyleBackColor = true;
            this.btnResize.Click += new System.EventHandler(this.btnResize_Click);
            // 
            // txHeight
            // 
            this.txHeight.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txHeight.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystemDirectories;
            this.txHeight.Location = new System.Drawing.Point(43, 453);
            this.txHeight.Name = "txHeight";
            this.txHeight.Size = new System.Drawing.Size(94, 20);
            this.txHeight.TabIndex = 5;
            this.txHeight.Text = "400";
            // 
            // txWidth
            // 
            this.txWidth.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txWidth.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystemDirectories;
            this.txWidth.Location = new System.Drawing.Point(206, 453);
            this.txWidth.Name = "txWidth";
            this.txWidth.Size = new System.Drawing.Size(94, 20);
            this.txWidth.TabIndex = 6;
            this.txWidth.Text = "600";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 456);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(25, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Alto";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(143, 456);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Ancho";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pictureBox1.Location = new System.Drawing.Point(353, 108);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(400, 400);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            // 
            // FrmImages
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(765, 514);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.txWidth);
            this.Controls.Add(this.txHeight);
            this.Controls.Add(this.btnResize);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txDestino);
            this.Controls.Add(this.txSource);
            this.Name = "FrmImages";
            this.Text = "FrmDrawing";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txSource;
        private System.Windows.Forms.TextBox txDestino;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnResize;
        private System.Windows.Forms.TextBox txHeight;
        private System.Windows.Forms.TextBox txWidth;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}