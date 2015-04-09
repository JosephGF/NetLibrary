using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace NetLibrary.Images
{
    public class Utils
    {
        public static Image GetThumbnailImage(string fileName, int width = 250, int height = 250)
        {
            Image image = Image.FromFile(fileName);
            Image thumb = Utils.GetThumbnailImage(image, width, height);

            image.Dispose();
            return thumb;
        }
        public static Image GetThumbnailImage(Image image, int width = 250, int height = 250)
        {
            Image thumb = image.GetThumbnailImage(width, height, () => false, IntPtr.Zero);
            return thumb;
        }
        public static Image SetImgOpacity(Image imgPic, float imgOpac)
        {
            if (imgPic == null) return imgPic;

            Bitmap bmpPic = new Bitmap(imgPic.Width, imgPic.Height);
            Graphics gfxPic = Graphics.FromImage(bmpPic);
            ColorMatrix cmxPic = new ColorMatrix();
            cmxPic.Matrix33 = imgOpac;

            ImageAttributes iaPic = new ImageAttributes();
            iaPic.SetColorMatrix(cmxPic, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            gfxPic.DrawImage(imgPic, new Rectangle(0, 0, bmpPic.Width, bmpPic.Height), 0, 0, imgPic.Width, imgPic.Height, GraphicsUnit.Pixel, iaPic);
            gfxPic.Dispose();

            return bmpPic;
        }

        public static Bitmap FixedSize(string fileName, int Width, int Height, bool needToFill)
        {
            Image tmp = Image.FromFile(fileName);
            Bitmap image = Utils.FixedSize((Bitmap)tmp, Width, Height, needToFill);

            tmp.Dispose();
            return image;
        }

        public static Bitmap FixedSize(Bitmap image, int Width, int Height, bool needToFill)
        {
            int sourceWidth = image.Width;
            int sourceHeight = image.Height;
            int sourceX = 0;
            int sourceY = 0;
            double destX = 0;
            double destY = 0;

            double nScale = 0;
            double nScaleW = 0;
            double nScaleH = 0;

            nScaleW = ((double)Width / (double)sourceWidth);
            nScaleH = ((double)Height / (double)sourceHeight);
            if (!needToFill)
            {
                nScale = Math.Min(nScaleH, nScaleW);
            }
            else
            {
                nScale = Math.Max(nScaleH, nScaleW);
                destY = (Height - sourceHeight * nScale) / 2;
                destX = (Width - sourceWidth * nScale) / 2;
            }

            if (nScale > 1)
                nScale = 1;

            int destWidth = (int)Math.Round(sourceWidth * nScale);
            int destHeight = (int)Math.Round(sourceHeight * nScale);

            System.Drawing.Bitmap bmPhoto = null;
            try
            {
                bmPhoto = new System.Drawing.Bitmap(destWidth + (int)Math.Round(2 * destX), destHeight + (int)Math.Round(2 * destY));
            }
            catch (Exception ex)
            {
                throw new ApplicationException(string.Format("destWidth:{0}, destX:{1}, destHeight:{2}, desxtY:{3}, Width:{4}, Height:{5}",
                    destWidth, destX, destHeight, destY, Width, Height), ex);
            }
            using (System.Drawing.Graphics grPhoto = System.Drawing.Graphics.FromImage(bmPhoto))
            {
                grPhoto.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Default;
                grPhoto.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.Default;
                grPhoto.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;

                Rectangle to = new System.Drawing.Rectangle((int)Math.Round(destX), (int)Math.Round(destY), destWidth, destHeight);
                Rectangle from = new System.Drawing.Rectangle(sourceX, sourceY, sourceWidth, sourceHeight);
                //Console.WriteLine("From: " + from.ToString());
                //Console.WriteLine("To: " + to.ToString());
                grPhoto.DrawImage(image, to, from, System.Drawing.GraphicsUnit.Pixel);

                return bmPhoto;
            }
        }

        public static Bitmap MakeGrayscale(string fileName)
        {
            Image tmp = Image.FromFile(fileName);
            Bitmap image = ToGrayColor((Bitmap)tmp);

            tmp.Dispose();
            return image;
        }
        public static Bitmap ToGrayColor(Bitmap original)
        {
            //create a blank bitmap the same size as original
            Bitmap newBitmap = new Bitmap(original.Width, original.Height);

            //get a graphics object from the new image
            Graphics g = Graphics.FromImage(newBitmap);

            //create the grayscale ColorMatrix
            ColorMatrix colorMatrix = new ColorMatrix(
               new float[][] 
      {
         new float[] {.3f, .3f, .3f, 0, 0},
         new float[] {.59f, .59f, .59f, 0, 0},
         new float[] {.11f, .11f, .11f, 0, 0},
         new float[] {0, 0, 0, 1, 0},
         new float[] {0, 0, 0, 0, 1}
      });

            //create some image attributes
            ImageAttributes attributes = new ImageAttributes();

            //set the color matrix attribute
            attributes.SetColorMatrix(colorMatrix);

            //draw the original image on the new image
            //using the grayscale color matrix
            g.DrawImage(original, new Rectangle(0, 0, original.Width, original.Height),
               0, 0, original.Width, original.Height, GraphicsUnit.Pixel, attributes);

            //dispose the Graphics object
            g.Dispose();
            return newBitmap;
        }

        public static Bitmap ToSepiaColor(string fileName)
        {
            Image tmp = Image.FromFile(fileName);
            Bitmap image = ToSepiaColor((Bitmap)tmp);

            tmp.Dispose();
            return image;
        }

        public static Bitmap ToSepiaColor(Bitmap image)
        {// Make the ColorMatrix.
            ColorMatrix cm = new ColorMatrix(new float[][]
    {
        new float[] {0.393f, 0.349f, 0.272f, 0, 0},
        new float[] {0.769f, 0.686f, 0.534f, 0, 0},
        new float[] {0.189f, 0.168f, 0.131f, 0, 0},
        new float[] { 0, 0, 0, 1, 0},
        new float[] { 0, 0, 0, 0, 1}
    });
            ImageAttributes attributes = new ImageAttributes();
            attributes.SetColorMatrix(cm);

            // Draw the image onto the new bitmap while
            // applying the new ColorMatrix.
            Point[] points =
    {
        new Point(0, 0),
        new Point(image.Width - 1, 0),
        new Point(0, image.Height - 1),
    };
            Rectangle rect = new Rectangle(0, 0, image.Width, image.Height);

            // Make the result bitmap.
            Bitmap bm = new Bitmap(image.Width, image.Height);
            using (Graphics gr = Graphics.FromImage(bm))
            {
                gr.DrawImage(image, points, rect,
                    GraphicsUnit.Pixel, attributes);
            }

            // Return the result.
            return bm;
        }
    }
}