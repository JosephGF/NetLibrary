using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Drawing;

namespace NetLibrary.Forms.Controls
{
    public class UIPictureBoxButton : PictureBox
    {
        private bool _autogenerate = true;
        public bool AutoGenerate { get { return _autogenerate; } set { _autogenerate = value; AutoGenerateImages(); } }
        public Image OnHoverImage { get; set; }
        public Image OnFocusImage { get; set; }
        private Image DefaultImage { get; set; }
        private Image BaseImage { get; set; }

        new public Image InitialImage
        {
            get { return base.InitialImage; } 
            set {
                base.InitialImage = value;
                if (_autogenerate && (this.DefaultImage == null || !this.DefaultImage.Equals(value))) 
                    AutoGenerateImages(value);

                this.DefaultImage = value; 
            } 
        }

        public UIPictureBoxButton() 
        {
            this.GotFocus += UIPictureBoxButton_GotFocus;
            this.LostFocus += UIPictureBoxButton_LostFocus;
            this.MouseEnter += UIPictureBoxButton_MouseEnter;
            this.MouseLeave += UIPictureBoxButton_MouseLeave;
        }

        public void AutoGenerateImages()
        {
            this.AutoGenerateImages(this.DefaultImage);
        }

        public void AutoGenerateImages(Image image)
        {
            this.OnFocusImage = image;
            this.OnHoverImage = NetLibrary.Images.Utils.SetImgOpacity(image, (float)0.8);
            base.Image = this.BaseImage = NetLibrary.Images.Utils.SetImgOpacity(image, (float)0.5);
        }

        void UIPictureBoxButton_MouseLeave(object sender, EventArgs e)
        {
            if (!this.Focused)
            {
                base.Image = this.BaseImage;
            }
        }

        void UIPictureBoxButton_MouseEnter(object sender, EventArgs e)
        {
            if (!this.Focused)
            {
                base.Image = this.OnHoverImage ?? this.DefaultImage;
            }
        }

        void UIPictureBoxButton_LostFocus(object sender, EventArgs e)
        {
            base.Image = this.BaseImage  ?? this.DefaultImage;
        }

        void UIPictureBoxButton_GotFocus(object sender, EventArgs e)
        {
            base.Image = this.OnFocusImage ?? this.DefaultImage;
        }

        new public void Dispose()
        {
            if (this.OnHoverImage != null) this.OnHoverImage.Dispose();
            if (this.OnFocusImage != null) this.OnFocusImage.Dispose();
            if (this.DefaultImage != null) this.DefaultImage.Dispose();
            if (this.BaseImage != null) this.BaseImage.Dispose();
            

            base.Dispose();
        }
    }
}
