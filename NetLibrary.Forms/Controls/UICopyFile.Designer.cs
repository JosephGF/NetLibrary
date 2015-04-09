namespace NetLibrary.Forms.Controls
{
    partial class UICopyFile
    {
        /// <summary> 
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar 
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbText = new System.Windows.Forms.Label();
            this.iuProgressBar1 = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // lbText
            // 
            this.lbText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbText.Location = new System.Drawing.Point(0, 0);
            this.lbText.Name = "lbText";
            this.lbText.Size = new System.Drawing.Size(336, 47);
            this.lbText.TabIndex = 1;
            this.lbText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // iuProgressBar1
            // 
            this.iuProgressBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.iuProgressBar1.Location = new System.Drawing.Point(0, 30);
            this.iuProgressBar1.Name = "iuProgressBar1";
            //this.iuProgressBar1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            //this.iuProgressBar1.ProgressBarColor = System.Drawing.Color.Blue;
            this.iuProgressBar1.Size = new System.Drawing.Size(336, 17);
            //this.iuProgressBar1.State = NetLibrary.Controls.UIProgressBar.ProgressBarState.Normal;
            this.iuProgressBar1.TabIndex = 2;
            //this.iuProgressBar1.VisibleInTaskBar = false;
            // 
            // IUCopyFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.iuProgressBar1);
            this.Controls.Add(this.lbText);
            this.Name = "IUCopyFile";
            this.Size = new System.Drawing.Size(336, 47);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbText;
        private System.Windows.Forms.ProgressBar iuProgressBar1;
    }
}
