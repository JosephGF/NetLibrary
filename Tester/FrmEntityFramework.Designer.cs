namespace Tester
{
    partial class FrmDataBase
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
            this.txSql = new System.Windows.Forms.TextBox();
            this.btnGenerateSQL = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txSql
            // 
            this.txSql.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txSql.Location = new System.Drawing.Point(12, 41);
            this.txSql.Multiline = true;
            this.txSql.Name = "txSql";
            this.txSql.Size = new System.Drawing.Size(605, 235);
            this.txSql.TabIndex = 0;
            // 
            // btnGenerateSQL
            // 
            this.btnGenerateSQL.Location = new System.Drawing.Point(12, 12);
            this.btnGenerateSQL.Name = "btnGenerateSQL";
            this.btnGenerateSQL.Size = new System.Drawing.Size(97, 23);
            this.btnGenerateSQL.TabIndex = 1;
            this.btnGenerateSQL.Text = "Generar SQL";
            this.btnGenerateSQL.UseVisualStyleBackColor = true;
            this.btnGenerateSQL.Click += new System.EventHandler(this.btnGenerateSQL_Click);
            // 
            // FrmEntityFramework
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(629, 289);
            this.Controls.Add(this.btnGenerateSQL);
            this.Controls.Add(this.txSql);
            this.Name = "FrmEntityFramework";
            this.Text = "FrmEntityFramework";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txSql;
        private System.Windows.Forms.Button btnGenerateSQL;
    }
}