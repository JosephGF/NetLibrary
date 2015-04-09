namespace NetLibrary.Developer
{
    partial class Console
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Console));
            this.pnBottom = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.pnBody = new System.Windows.Forms.Panel();
            this.txLog = new System.Windows.Forms.TextBox();
            this.pnBottom.SuspendLayout();
            this.pnBody.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnBottom
            // 
            this.pnBottom.Controls.Add(this.label1);
            this.pnBottom.Controls.Add(this.textBox1);
            this.pnBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnBottom.Location = new System.Drawing.Point(0, 395);
            this.pnBottom.Name = "pnBottom";
            this.pnBottom.Size = new System.Drawing.Size(891, 29);
            this.pnBottom.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Dock = System.Windows.Forms.DockStyle.Right;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(849, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 29);
            this.label1.TabIndex = 1;
            this.label1.Text = "<<";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.BackColor = System.Drawing.Color.Black;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.ForeColor = System.Drawing.Color.Lime;
            this.textBox1.Location = new System.Drawing.Point(12, 8);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(838, 13);
            this.textBox1.TabIndex = 0;
            this.textBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            // 
            // pnBody
            // 
            this.pnBody.AutoScroll = true;
            this.pnBody.Controls.Add(this.txLog);
            this.pnBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnBody.Location = new System.Drawing.Point(0, 0);
            this.pnBody.Name = "pnBody";
            this.pnBody.Size = new System.Drawing.Size(891, 395);
            this.pnBody.TabIndex = 1;
            // 
            // txLog
            // 
            this.txLog.BackColor = System.Drawing.Color.Black;
            this.txLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txLog.ForeColor = System.Drawing.Color.Lime;
            this.txLog.Location = new System.Drawing.Point(0, 0);
            this.txLog.Multiline = true;
            this.txLog.Name = "txLog";
            this.txLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txLog.Size = new System.Drawing.Size(891, 395);
            this.txLog.TabIndex = 0;
            // 
            // Console
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(891, 424);
            this.Controls.Add(this.pnBody);
            this.Controls.Add(this.pnBottom);
            this.ForeColor = System.Drawing.Color.Lime;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Console";
            this.Text = "Debug Console";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Console_FormClosing);
            this.pnBottom.ResumeLayout(false);
            this.pnBottom.PerformLayout();
            this.pnBody.ResumeLayout(false);
            this.pnBody.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnBottom;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Panel pnBody;
        private System.Windows.Forms.TextBox txLog;
    }
}