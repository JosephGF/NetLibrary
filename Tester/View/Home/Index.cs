using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NetLibrary.Forms.Mvc;

namespace Tester.View.Home
{
    public class Index : NetLibrary.Forms.Mvc.View
    {
        private System.Windows.Forms.Button btnNextView;
    
        public Index()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.btnNextView = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnNextView
            // 
            this.btnNextView.Location = new System.Drawing.Point(3, 3);
            this.btnNextView.Name = "btnNextView";
            this.btnNextView.Size = new System.Drawing.Size(82, 23);
            this.btnNextView.TabIndex = 0;
            this.btnNextView.Text = "Ir a Manager";
            this.btnNextView.UseVisualStyleBackColor = true;
            this.btnNextView.Click += new System.EventHandler(this.btnNextView_Click);
            // 
            // Index
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Controls.Add(this.btnNextView);
            this.Name = "Index";
            this.BackColor = System.Drawing.Color.Red;
            this.ResumeLayout(false);

        }

        private void btnNextView_Click(object sender, EventArgs e)
        {
            this.Action("Manager");
        }
    }
}
