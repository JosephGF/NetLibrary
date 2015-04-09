using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NetLibrary.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class StringExtension
    {
        /// <summary>
        /// Reemplaza todas las coincidencias duplicadas del caracter especificado
        /// </summary>
        /// <param name="str"></param>
        /// <param name="searchChar">Caracter a buscar</param>
        public static void TrimAll(this string str, string searchChar) {
            RegexOptions options = RegexOptions.None;
            Regex regex = new Regex(@"[ ]{2,}", options);
            str = regex.Replace(str, searchChar);
        }

        /// <summary>
        /// Reemplaza todas las coincidencias duplicadas del caracter especificado
        /// </summary>
        /// <param name="str"></param>
        /// <param name="searchChar">Caracter a buscar</param>
        public static void TrimAll(this string str, char searchChar)
        {
            StringExtension.TrimAll(str, searchChar.ToString());
        }

        /// <summary>
        /// Reemplaza todos lo dobles espacios de la cadena
        /// </summary>
        /// <param name="str"></param>
        public static void TrimAll(this string str)
        {
            StringExtension.TrimAll(str, " ");
        }

        /// <summary>
        /// Codifica la cadena actual a base64
        /// </summary>
        /// <param name="str">Cadena inicial</param>
        /// <returns>Cadena codificada en base 64</returns>
        public static string EncodeBase64(this string str)
        {
            return Serialization.EncodeBase64(str);
        }

        /// <summary>
        /// Codifica la cadena de texto a base64
        /// </summary>
        /// <param name="str">Cadena inicial</param>
        /// <param name="encoding">Codificacion del texto</param>
        /// <returns>Cadena codificada en base 64</returns>
        public static string EncodeBase64(this string str, Encoding encoding)
        {
            return Serialization.EncodeBase64(str, encoding);
        }

        /// <summary>
        /// Decodificación la cadena de texto a base64
        /// </summary>
        /// <param name="str">Cadena inicial</param>
        /// <returns>Cadena decodificada</returns>
        public static string DecodeBase64(this string str)
        {
            return Serialization.DecodeBase64(str);
        }

        /// <summary>
        /// Decodificación la cadena de texto a base64
        /// </summary>
        /// <param name="str">Cadena en base64</param>
        /// <param name="encoding">Codificacion del texto</param>
        /// <returns>Cadena decodificada</returns>
        public static string DecodeBase64(this string str, Encoding encoding)
        {
            return Serialization.DecodeBase64(str, encoding);
        }

        /// <summary>
        /// Devuelve el número de diferencias entre dos strings
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static int LevenshteinDistance(this string source, string target)
        {
            if (String.IsNullOrEmpty(source))
            {
                if (String.IsNullOrEmpty(target)) return 0;
                return target.Length;
            }
            if (String.IsNullOrEmpty(target)) return source.Length;

            if (source.Length > target.Length)
            {
                var temp = target;
                target = source;
                source = temp;
            }

            var m = target.Length;
            var n = source.Length;
            var distance = new int[2, m + 1];
            // Initialize the distance 'matrix'
            for (var j = 1; j <= m; j++) distance[0, j] = j;

            var currentRow = 0;
            for (var i = 1; i <= n; ++i)
            {
                currentRow = i & 1;
                distance[currentRow, 0] = i;
                var previousRow = currentRow ^ 1;
                for (var j = 1; j <= m; j++)
                {
                    var cost = (target[j - 1] == source[i - 1] ? 0 : 1);
                    distance[currentRow, j] = Math.Min(Math.Min(
                                distance[previousRow, j] + 1,
                                distance[currentRow, j - 1] + 1),
                                distance[previousRow, j - 1] + cost);
                }
            }
            return distance[currentRow, m];
        }
    }
}
