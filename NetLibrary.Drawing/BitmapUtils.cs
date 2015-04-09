using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetLibrary.Drawing
{
    public class BitmapUtils
    {
        /// <summary>
        /// Realiza una captura de pantalla
        /// </summary>
        /// <returns>Mapa de bits que representa la captuara de pantalla</returns>
        public static Bitmap ScreenCapture()
        {
            Bitmap bmpScreenshot = new Bitmap(Screen.PrimaryScreen.Bounds.Width,
                               Screen.PrimaryScreen.Bounds.Height,
                               PixelFormat.Format16bppRgb555);

            // Create a graphics object from the bitmap.
            var gfxScreenshot = Graphics.FromImage(bmpScreenshot);

            // Take the screenshot from the upper left corner to the right bottom corner.
            gfxScreenshot.CopyFromScreen(Screen.PrimaryScreen.Bounds.X,
                                        Screen.PrimaryScreen.Bounds.Y,
                                        0,
                                        0,
                                        Screen.PrimaryScreen.Bounds.Size,
                                        CopyPixelOperation.SourceCopy);

            return bmpScreenshot;
        }

        /// <summary>
        /// Codifica la cadena de texto a base64
        /// </summary>
        /// <param name="bitmap">Imagen inicial</param>
        /// <param name="encoding">Codificacion del texto</param>
        /// <returns>Cadena codificada en base 64</returns>
        public static string EncodeBase64(Bitmap bitmap)
        {
            byte[] bytes = BitmapUtils.ToByteArray(bitmap);
            return System.Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// Decodifica una cadena en base64 a un mapa de bits
        /// </summary>
        /// <param name="bitmat64">Cadena en base64 que representa la imagen</param>
        /// <returns>Imagen convertida, null si no es una imagen</returns>
        public static Bitmap DecodeBase64(string bitmat64)
        {
            byte[] bytes = System.Convert.FromBase64String(bitmat64);
            return BitmapUtils.ToBitmap(bytes);
        }

        /// <summary>
        /// Covert a bitmap to a byte array
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns>
        /// byte array, when bitmap could be converted
        /// null, when bitmap is null
        /// null, when bitmap could not be converted to byte array
        /// </returns>
        public static byte[] ToByteArray(Bitmap bitmap)
        {
            ImageConverter convert = new ImageConverter();
            return (byte[])convert.ConvertTo(bitmap, typeof(byte[]));
        }

        /// <summary>
        /// Convierte un array de bytes a un mapa de bits
        /// </summary>
        /// <param name="byteArray">Array de bytes que representa la imagen</param>
        /// <returns>null si no es una imagen</returns>
        public static Bitmap ToBitmap(byte[] byteArray)
        {
            using (MemoryStream ms = new MemoryStream(byteArray))
            {
                return Image.FromStream(ms) as Bitmap;
            }
        }
    }
}
