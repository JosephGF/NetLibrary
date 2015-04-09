namespace Tester
{
    partial class FrmWin32
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
            this.btnToggleTaskbar = new System.Windows.Forms.Button();
            this.btnDisableTaskmng = new System.Windows.Forms.Button();
            this.btnShowTaskbar = new System.Windows.Forms.Button();
            this.btnChangeDesktop = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnToggleTaskbar
            // 
            this.btnToggleTaskbar.Location = new System.Drawing.Point(13, 13);
            this.btnToggleTaskbar.Name = "btnToggleTaskbar";
            this.btnToggleTaskbar.Size = new System.Drawing.Size(107, 23);
            this.btnToggleTaskbar.TabIndex = 0;
            this.btnToggleTaskbar.Text = "hide taskbar";
            this.btnToggleTaskbar.UseVisualStyleBackColor = true;
            this.btnToggleTaskbar.Click += new System.EventHandler(this.btnToggleTaskbar_Click);
            // 
            // btnDisableTaskmng
            // 
            this.btnDisableTaskmng.Location = new System.Drawing.Point(13, 82);
            this.btnDisableTaskmng.Name = "btnDisableTaskmng";
            this.btnDisableTaskmng.Size = new System.Drawing.Size(107, 23);
            this.btnDisableTaskmng.TabIndex = 0;
            this.btnDisableTaskmng.Text = "toggle taskmng";
            this.btnDisableTaskmng.UseVisualStyleBackColor = true;
            this.btnDisableTaskmng.Click += new System.EventHandler(this.btnDisableTaskmng_Click);
            // 
            // btnShowTaskbar
            // 
            this.btnShowTaskbar.Location = new System.Drawing.Point(13, 42);
            this.btnShowTaskbar.Name = "btnShowTaskbar";
            this.btnShowTaskbar.Size = new System.Drawing.Size(107, 23);
            this.btnShowTaskbar.TabIndex = 0;
            this.btnShowTaskbar.Text = "show taskbar";
            this.btnShowTaskbar.UseVisualStyleBackColor = true;
            this.btnShowTaskbar.Click += new System.EventHandler(this.btnShowTaskbar_Click);
            // 
            // btnChangeDesktop
            // 
            this.btnChangeDesktop.Location = new System.Drawing.Point(12, 125);
            this.btnChangeDesktop.Name = "btnChangeDesktop";
            this.btnChangeDesktop.Size = new System.Drawing.Size(107, 23);
            this.btnChangeDesktop.TabIndex = 0;
            this.btnChangeDesktop.Text = "change desktop";
            this.btnChangeDesktop.UseVisualStyleBackColor = true;
            this.btnChangeDesktop.Click += new System.EventHandler(this.btnChangeDesktop_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(13, 170);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(107, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Show Desktop icons";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 199);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(107, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "Hide Desktop icons";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // FrmWin32
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(135, 320);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnChangeDesktop);
            this.Controls.Add(this.btnDisableTaskmng);
            this.Controls.Add(this.btnShowTaskbar);
            this.Controls.Add(this.btnToggleTaskbar);
            this.Name = "FrmWin32";
            this.Text = "FrmWin32";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnToggleTaskbar;
        private System.Windows.Forms.Button btnDisableTaskmng;
        private System.Windows.Forms.Button btnShowTaskbar;
        private System.Windows.Forms.Button btnChangeDesktop;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}