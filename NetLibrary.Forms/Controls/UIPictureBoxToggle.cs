using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;

namespace NetLibrary.Forms.Controls
{
    public class UIPictureBoxToggle : PictureBox
    {
        public event EventHandler<EventArgs> OnImageChange;
        private int _imageIndex;
        public int ImageIndex { 
            get { return this._imageIndex; }
            set { this._imageIndex = value; this.ChangeImageIndex(this._imageIndex); }
        }
        private ImageList _imageList = null;
        public ImageList ImageList { 
            get { return this._imageList; } 
            set { this._imageList = value; this.ChangeImageIndex(this._imageIndex); }
        }

        public UIPictureBoxToggle()
        {
            this.Click += UIPictureBoxToggle_Click;
        }

        public void UIPictureBoxToggle_Click(object sender, EventArgs e)
        {
            this._imageIndex++;
            this.ChangeImageIndex(this._imageIndex);
        }
        private void ChangeImageIndex(int index)
        {
            if (this.ImageList == null || this.ImageList.Images.Count == 0) return;
            if (this.ImageList.Images.Count <= index) index = 0;

            this.Image = this.ImageList.Images[index];
            this._imageIndex = index;

            if (OnImageChange != null) OnImageChange(this, new EventArgs());
        }
    }
}
