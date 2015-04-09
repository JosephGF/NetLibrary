namespace NetLibrary.Forms.Controls
{
    partial class UIItemList
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
            this.pbImagen = new System.Windows.Forms.PictureBox();
            this.lbTitulo = new System.Windows.Forms.Label();
            this.lbDesc = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbImagen)).BeginInit();
            this.SuspendLayout();
            // 
            // pbImagen
            // 
            this.pbImagen.Location = new System.Drawing.Point(4, 4);
            this.pbImagen.Name = "pbImagen";
            this.pbImagen.Size = new System.Drawing.Size(48, 50);
            this.pbImagen.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbImagen.TabIndex = 0;
            this.pbImagen.TabStop = false;
            this.pbImagen.Click += new System.EventHandler(this.contolClick_Click);
            // 
            // lbTitulo
            // 
            this.lbTitulo.AutoSize = true;
            this.lbTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTitulo.Location = new System.Drawing.Point(58, 4);
            this.lbTitulo.Name = "lbTitulo";
            this.lbTitulo.Size = new System.Drawing.Size(60, 25);
            this.lbTitulo.TabIndex = 1;
            this.lbTitulo.Text = "Titulo";
            this.lbTitulo.Click += new System.EventHandler(this.contolClick_Click);
            // 
            // lbDesc
            // 
            this.lbDesc.AutoSize = true;
            this.lbDesc.Location = new System.Drawing.Point(55, 41);
            this.lbDesc.Name = "lbDesc";
            this.lbDesc.Size = new System.Drawing.Size(63, 13);
            this.lbDesc.TabIndex = 2;
            this.lbDesc.Text = "Descripcion";
            this.lbDesc.Click += new System.EventHandler(this.contolClick_Click);
            // 
            // Contacto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lbDesc);
            this.Controls.Add(this.lbTitulo);
            this.Controls.Add(this.pbImagen);
            this.Name = "Contacto";
            this.Size = new System.Drawing.Size(346, 61);
            this.Click += new System.EventHandler(this.contolClick_Click);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Contacto_MouseDown);
            this.MouseEnter += new System.EventHandler(this.Contacto_MouseHover);
            this.MouseLeave += new System.EventHandler(this.Contacto_MouseLeave);
            this.MouseHover += new System.EventHandler(this.Contacto_MouseHover);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Contacto_MouseUp);
            ((System.ComponentModel.ISupportInitialize)(this.pbImagen)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbImagen;
        private System.Windows.Forms.Label lbTitulo;
        private System.Windows.Forms.Label lbDesc;
    }
}
