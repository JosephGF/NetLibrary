using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tester.View.Home
{
    public class Manager : NetLibrary.Forms.Mvc.View
    {
        private System.Windows.Forms.Button btnIndex;
    
        public Manager()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.btnIndex = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnIndex
            // 
            this.btnIndex.Location = new System.Drawing.Point(3, 3);
            this.btnIndex.Name = "btnIndex";
            this.btnIndex.Size = new System.Drawing.Size(75, 23);
            this.btnIndex.TabIndex = 0;
            this.btnIndex.Text = "Volver";
            this.btnIndex.UseVisualStyleBackColor = true;
            this.btnIndex.Click += new System.EventHandler(this.btnIndex_Click);
            // 
            // Manager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Controls.Add(this.btnIndex);
            this.Name = "Manager";
            this.BackColor = System.Drawing.Color.Green;
            this.ResumeLayout(false);

        }

        private void btnIndex_Click(object sender, EventArgs e)
        {
            this.Action("Index");
        }
    }
}
