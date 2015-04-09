using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetLibrary.Drawing
{
    public class Color
    {
        /// <summary>
        /// Obtiene un color cambiando el porcentaje indicado del rgb del color pasado como parámetro
        /// </summary>
        /// <param name="origin">Color de origen</param>
        /// <param name="percent">Porcentaje para obtener el nuevo color (Puede ser negativo)</param>
        /// <exception cref="exception">Porcentaje incorrecto</exception>
        /// <returns>Color resultante</returns>
        public static System.Drawing.Color Luminace(System.Drawing.Color origin, int percent)
        {
            if (percent > 100 || percent < -100)
                throw new Exception("El porcentaje debe ser entre 100 y -100");

            double aux = Math.Round(255 * (percent / 100.0));

            Byte a = origin.A;
            Byte r = origin.R;
            Byte g = origin.G;
            Byte b = origin.B;

            r = (Byte)Math.Round((double)Math.Min(255, Math.Max(r + aux, 0)));
            g = (Byte)Math.Round((double)Math.Min(255, Math.Max(g + aux, 0)));
            b = (Byte)Math.Round((double)Math.Min(255, Math.Max(b + aux, 0)));
            return System.Drawing.Color.FromArgb(a, r, g, b);
        }
    }
}
