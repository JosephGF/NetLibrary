namespace Tester
{
    partial class FrmNetLibrary
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
            this.btnToBase64 = new System.Windows.Forms.Button();
            this.txBase64 = new System.Windows.Forms.TextBox();
            this.trxDGuid = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.txNGuids = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.txGuid = new System.Windows.Forms.TextBox();
            this.txData = new System.Windows.Forms.TextBox();
            this.txDate = new System.Windows.Forms.TextBox();
            this.btnProcessGuid = new System.Windows.Forms.Button();
            this.txErrors = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnToBase64
            // 
            this.btnToBase64.Location = new System.Drawing.Point(12, 472);
            this.btnToBase64.Name = "btnToBase64";
            this.btnToBase64.Size = new System.Drawing.Size(122, 23);
            this.btnToBase64.TabIndex = 0;
            this.btnToBase64.Text = "Clipboard To Base64";
            this.btnToBase64.UseVisualStyleBackColor = true;
            this.btnToBase64.Click += new System.EventHandler(this.btnToBase64_Click);
            // 
            // txBase64
            // 
            this.txBase64.Location = new System.Drawing.Point(12, 501);
            this.txBase64.Name = "txBase64";
            this.txBase64.Size = new System.Drawing.Size(122, 20);
            this.txBase64.TabIndex = 1;
            // 
            // trxDGuid
            // 
            this.trxDGuid.Location = new System.Drawing.Point(434, 30);
            this.trxDGuid.Name = "trxDGuid";
            this.trxDGuid.Size = new System.Drawing.Size(247, 356);
            this.trxDGuid.TabIndex = 2;
            this.trxDGuid.Text = "";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(434, 392);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(247, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Generate DateGuids";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txNGuids
            // 
            this.txNGuids.Location = new System.Drawing.Point(629, 4);
            this.txNGuids.Name = "txNGuids";
            this.txNGuids.Size = new System.Drawing.Size(52, 20);
            this.txNGuids.TabIndex = 4;
            this.txNGuids.Text = "1000";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(589, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Guids";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(122, 23);
            this.button2.TabIndex = 0;
            this.button2.Text = "about";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // txGuid
            // 
            this.txGuid.Location = new System.Drawing.Point(434, 422);
            this.txGuid.Name = "txGuid";
            this.txGuid.Size = new System.Drawing.Size(247, 20);
            this.txGuid.TabIndex = 6;
            // 
            // txData
            // 
            this.txData.Location = new System.Drawing.Point(568, 448);
            this.txData.Name = "txData";
            this.txData.Size = new System.Drawing.Size(112, 20);
            this.txData.TabIndex = 6;
            // 
            // txDate
            // 
            this.txDate.Location = new System.Drawing.Point(434, 448);
            this.txDate.Name = "txDate";
            this.txDate.Size = new System.Drawing.Size(128, 20);
            this.txDate.TabIndex = 6;
            // 
            // btnProcessGuid
            // 
            this.btnProcessGuid.Location = new System.Drawing.Point(434, 498);
            this.btnProcessGuid.Name = "btnProcessGuid";
            this.btnProcessGuid.Size = new System.Drawing.Size(246, 23);
            this.btnProcessGuid.TabIndex = 0;
            this.btnProcessGuid.Text = "Process Guid Date";
            this.btnProcessGuid.UseVisualStyleBackColor = true;
            this.btnProcessGuid.Click += new System.EventHandler(this.btnProcessGuid_Click);
            // 
            // txErrors
            // 
            this.txErrors.Location = new System.Drawing.Point(433, 474);
            this.txErrors.Name = "txErrors";
            this.txErrors.Size = new System.Drawing.Size(247, 20);
            this.txErrors.TabIndex = 6;
            // 
            // FrmNetLibrary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(693, 529);
            this.Controls.Add(this.txDate);
            this.Controls.Add(this.txData);
            this.Controls.Add(this.txErrors);
            this.Controls.Add(this.txGuid);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txNGuids);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.trxDGuid);
            this.Controls.Add(this.txBase64);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnProcessGuid);
            this.Controls.Add(this.btnToBase64);
            this.Name = "FrmNetLibrary";
            this.Text = "FrmNetLibrary";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnToBase64;
        private System.Windows.Forms.TextBox txBase64;
        private System.Windows.Forms.RichTextBox trxDGuid;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txNGuids;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox txGuid;
        private System.Windows.Forms.TextBox txData;
        private System.Windows.Forms.TextBox txDate;
        private System.Windows.Forms.Button btnProcessGuid;
        private System.Windows.Forms.TextBox txErrors;
    }
}