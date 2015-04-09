using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetLibrary.Forms.Controls
{
    [ToolboxBitmap(typeof(PictureBox))]
    public class UIGifPictureBox : PictureBox
    {
        private FrameDimension dimension;
        private int _frameCount;
        private int _currentFrame = -1;
        private int _step = 1;
        private Timer _timer = new Timer();


        /// <summary>
        /// Obtiene o establece si se iniciara la animación automaticamente
        /// </summary>
        public bool AutoStart { get; set; }

        /// <summary>
        /// Obtiene o establece si se invertirá el orden de los frames al llegar al último
        /// </summary>
        public bool Reverse { get; set; }

        /// <summary>
        /// Obtiene o establece si se reproducirá en bucle la secuencia de frames
        /// </summary>
        public bool Loop { get; set; }

        /// <summary>
        /// Obtiene o establece el intermalo en el que cambia de frame la animación
        /// </summary>
        public int Intervar
        {
            get { return _timer.Interval; }
            set { _timer.Interval = value; }
        }

        public UIGifPictureBox()
        {
            _timer.Tick += _timer_Tick;
        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            this.Image = this.GetNextFrame();
        }

        /// <summary>
        /// Obtiene o establece la imagen que se muestra en el control
        /// </summary>
        public new Image Image
        {
            get { return base.Image; }
            set
            {
                this.StopAnimation();

                base.Image = value;
                dimension = new FrameDimension(base.Image.FrameDimensionsList[0]); //gets the GUID
                _frameCount = base.Image.GetFrameCount(dimension); //total frames in the animation

                if (_frameCount > 1 && this.AutoStart)
                    this.StartAnimation();
            }
        }
        private Image GetNextFrame()
        {
            _currentFrame += _step;

            //if the animation reaches a boundary...
            if (_currentFrame >= _frameCount || _currentFrame < 1)
            {
                if (this.Reverse)
                {
                    _step *= -1; //...reverse the count
                    _currentFrame += _step; //apply it
                }
                else
                    _currentFrame = 0; //...or start over
            }
            return GetFrame(_currentFrame);
        }
        private Image GetFrame(int index)
        {
            this.Image.SelectActiveFrame(dimension, index); //find the frame
            return (Image)this.Image.Clone(); //return a copy of it
        }

        public void StartAnimation()
        {
            _timer.Start();
        }
        public void PauseAnimation()
        {
            _timer.Stop();
        }
        public void StopAnimation()
        {
            _currentFrame = 0;
            _timer.Stop();
        }

        protected override void Dispose(bool disposing)
        {
            this.StopAnimation();
            base.Dispose(disposing);
        }
    }
}
