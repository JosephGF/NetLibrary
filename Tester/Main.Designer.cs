namespace Tester
{
    partial class Main
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
            this.btnWin32 = new System.Windows.Forms.Button();
            this.btnFormsMVC = new System.Windows.Forms.Button();
            this.btnWinForms = new System.Windows.Forms.Button();
            this.btnEntityFramework = new System.Windows.Forms.Button();
            this.btnWinConsole = new System.Windows.Forms.Button();
            this.btnImages = new System.Windows.Forms.Button();
            this.btnNetLibrary = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnWin32
            // 
            this.btnWin32.Location = new System.Drawing.Point(12, 287);
            this.btnWin32.Name = "btnWin32";
            this.btnWin32.Size = new System.Drawing.Size(93, 74);
            this.btnWin32.TabIndex = 0;
            this.btnWin32.Text = "NetLibrary\r\nWin32";
            this.btnWin32.UseVisualStyleBackColor = true;
            this.btnWin32.Click += new System.EventHandler(this.btnWin32_Click);
            // 
            // btnFormsMVC
            // 
            this.btnFormsMVC.Location = new System.Drawing.Point(124, 12);
            this.btnFormsMVC.Name = "btnFormsMVC";
            this.btnFormsMVC.Size = new System.Drawing.Size(93, 74);
            this.btnFormsMVC.TabIndex = 0;
            this.btnFormsMVC.Text = "NetLibrary\r\nForms\r\nMVC";
            this.btnFormsMVC.UseVisualStyleBackColor = true;
            this.btnFormsMVC.Click += new System.EventHandler(this.btnFormsMVC_Click);
            // 
            // btnWinForms
            // 
            this.btnWinForms.Location = new System.Drawing.Point(228, 12);
            this.btnWinForms.Name = "btnWinForms";
            this.btnWinForms.Size = new System.Drawing.Size(93, 74);
            this.btnWinForms.TabIndex = 0;
            this.btnWinForms.Text = "NetLibrary\r\nWindows\r\nForms\r\n";
            this.btnWinForms.UseVisualStyleBackColor = true;
            this.btnWinForms.Click += new System.EventHandler(this.btnWinForms_Click);
            // 
            // btnEntityFramework
            // 
            this.btnEntityFramework.Location = new System.Drawing.Point(12, 92);
            this.btnEntityFramework.Name = "btnEntityFramework";
            this.btnEntityFramework.Size = new System.Drawing.Size(93, 74);
            this.btnEntityFramework.TabIndex = 0;
            this.btnEntityFramework.Text = "NetLibrary\r\nDataBase";
            this.btnEntityFramework.UseVisualStyleBackColor = true;
            this.btnEntityFramework.Click += new System.EventHandler(this.btnEntityFramework_Click);
            // 
            // btnWinConsole
            // 
            this.btnWinConsole.Location = new System.Drawing.Point(468, 287);
            this.btnWinConsole.Name = "btnWinConsole";
            this.btnWinConsole.Size = new System.Drawing.Size(93, 74);
            this.btnWinConsole.TabIndex = 0;
            this.btnWinConsole.Text = "NetLibrary\r\nDeveloper";
            this.btnWinConsole.UseVisualStyleBackColor = true;
            this.btnWinConsole.Click += new System.EventHandler(this.btnWinConsole_Click);
            // 
            // btnImages
            // 
            this.btnImages.Location = new System.Drawing.Point(340, 12);
            this.btnImages.Name = "btnImages";
            this.btnImages.Size = new System.Drawing.Size(93, 74);
            this.btnImages.TabIndex = 0;
            this.btnImages.Text = "NetLibrary\r\nImages\r\n";
            this.btnImages.UseVisualStyleBackColor = true;
            this.btnImages.Click += new System.EventHandler(this.btnImages_Click);
            // 
            // btnNetLibrary
            // 
            this.btnNetLibrary.Location = new System.Drawing.Point(12, 12);
            this.btnNetLibrary.Name = "btnNetLibrary";
            this.btnNetLibrary.Size = new System.Drawing.Size(93, 74);
            this.btnNetLibrary.TabIndex = 1;
            this.btnNetLibrary.Text = "NetLibrary";
            this.btnNetLibrary.UseVisualStyleBackColor = true;
            this.btnNetLibrary.Click += new System.EventHandler(this.btnNetLibrary_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(573, 373);
            this.Controls.Add(this.btnNetLibrary);
            this.Controls.Add(this.btnWinConsole);
            this.Controls.Add(this.btnEntityFramework);
            this.Controls.Add(this.btnFormsMVC);
            this.Controls.Add(this.btnImages);
            this.Controls.Add(this.btnWinForms);
            this.Controls.Add(this.btnWin32);
            this.Name = "Main";
            this.Text = "Main";
            this.Load += new System.EventHandler(this.Main_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnWin32;
        private System.Windows.Forms.Button btnFormsMVC;
        private System.Windows.Forms.Button btnWinForms;
        private System.Windows.Forms.Button btnEntityFramework;
        private System.Windows.Forms.Button btnWinConsole;
        private System.Windows.Forms.Button btnImages;
        private System.Windows.Forms.Button btnNetLibrary;
    }
}

