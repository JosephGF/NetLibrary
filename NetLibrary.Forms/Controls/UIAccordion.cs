using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetLibrary.Forms.Controls
{
    public partial class UIAccordion : UserControl
    {
        private List<UIAccordionCategory> _categories = new List<UIAccordionCategory>();
        public List<UIAccordionCategory> Categories { get { return _categories; } set { _categories = value; CreateUIAccordionCategories(); } }
        public List<UIAccordionItems> Items
        {
            get
            {
                List<UIAccordionItems> items = new List<UIAccordionItems>();
                foreach (var categoria in this.Categories)
                {
                    items.AddRange(categoria.Items);
                }

                return items;
            }
        }
        public UIAccordion()
        {
            InitializeComponent();
            CreateUIAccordionCategories();
        }

        private void pnCategory_Paint(object sender, PaintEventArgs e)
        {

        }

        private void CreateUIAccordionCategories()
        {
            this.Controls.Clear();
            if (this.Categories != null)
            {
                foreach (UIAccordionCategory category in this.Categories)
                {
                    this.Controls.Add(category.Control);
                }
            }
        }
    }


    public class UIAccordionCategory
    {
        public UIAccordionCategory()
        {
            this.Items = new List<UIAccordionItems>();
        }
        public Image Image { get; set; }
        public string Text { get; set; }

        public List<UIAccordionItems> Items { get; set; }

        public Control Control
        {
            get
            {
                Panel pnCategory = new Panel();
                Panel pnTitulo = new Panel();
                Label lbTitulo = new Label();
                PictureBox pbImage = new PictureBox();


                pnTitulo.Dock = System.Windows.Forms.DockStyle.Top;
                pnTitulo.Location = new System.Drawing.Point(0, 0);
                pnTitulo.Size = new System.Drawing.Size(115, 46);
                pnTitulo.TabIndex = 2;

                lbTitulo.Dock = System.Windows.Forms.DockStyle.Fill;
                //lbTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                //lbTitulo.Location = new System.Drawing.Point(27, 0);
                //lbTitulo.Name = "lbCategoryText";
                lbTitulo.Size = new System.Drawing.Size(88, 46);
                lbTitulo.TabIndex = 3;
                lbTitulo.Text = this.Text;
                lbTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

                pbImage.Dock = System.Windows.Forms.DockStyle.Left;
                pbImage.Image = (this.Image);
                pbImage.Location = new System.Drawing.Point(0, 0);
                //pbImage.Name = "pbCategoryImage";
                pbImage.Size = new System.Drawing.Size(27, 46);
                pbImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
                pbImage.TabIndex = 2;
                pbImage.TabStop = false;

                Panel pnContent = new Panel();
                pnContent.Dock = System.Windows.Forms.DockStyle.Top;
                pnContent.Location = new System.Drawing.Point(0, 0);
                pnContent.AutoSize = true;

                List<Control> items = new List<Control>();

                if (this.Items != null)
                {
                    for (int i = this.Items.Count - 1; i >= 0; i--)
                    {
                        UIAccordionItems item = this.Items[i];
                        items.Add(item.Control);
                    }
                }

                pnContent.Controls.AddRange(items.ToArray());

                pnTitulo.Controls.Add(lbTitulo);
                pnTitulo.Controls.Add(pbImage);
                pnCategory.Controls.Add(pnTitulo);
                pnCategory.Controls.Add(pnContent);
                return pnContent;
            }
        }
    }


    public class UIAccordionItems
    {
        // Declare the delegate (if using non-generic pattern).
        public delegate void OnClick(object tag, EventArgs e);

        // Declare the event.
        public event OnClick onClick;

        private Control _control = null;
        public Image Image { get; set; }
        public string Text { get; set; }
        public object Tag { get; set; }

        public bool Visible
        {
            get { return this._control.Visible; }
            set { this._control.Visible = value; }
        }

        public Control Control
        {
            get
            {
                if (_control != null)
                    return Control;

                Panel pnItem = new Panel();
                Label lbTitulo = new Label();
                PictureBox pbImage = new PictureBox();

                pnItem.Dock = System.Windows.Forms.DockStyle.Top;
                pnItem.Location = new System.Drawing.Point(0, 46);
                pnItem.Size = new System.Drawing.Size(0, 50);
                pnItem.TabIndex = 3;
                pnItem.Click += item_Click;
                pnItem.Tag = this.Tag;

                pbImage.Dock = System.Windows.Forms.DockStyle.Fill;
                pbImage.Image = (this.Image);
                pbImage.Location = new System.Drawing.Point(0, 0);
                //pbImage.Size = new System.Drawing.Size(115, 65);
                pbImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
                pbImage.TabIndex = 2;
                pbImage.TabStop = false;
                pbImage.Click += item_Click;
                pbImage.Tag = this.Tag;

                lbTitulo.BackColor = System.Drawing.Color.Transparent;
                lbTitulo.Dock = System.Windows.Forms.DockStyle.Bottom;
                lbTitulo.Location = new System.Drawing.Point(0, 65);
                //lbTitulo.Size = new System.Drawing.Size(115, 20);
                lbTitulo.TabIndex = 1;
                lbTitulo.Text = this.Text;
                lbTitulo.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
                lbTitulo.Click += item_Click;
                lbTitulo.Tag = this.Tag;

                pnItem.Controls.Add(pbImage);
                pnItem.Controls.Add(lbTitulo);

                this._control = pnItem;
                return pnItem;
            }
        }

        public void item_Click(object sender, EventArgs e)
        {
            if (onClick != null)
                onClick(((Control)sender).Tag, e);
        }
    }
}
