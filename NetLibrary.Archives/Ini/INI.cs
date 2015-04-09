
using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace NetLibrary.Archives.INI
{
    public class INI
    {

        private string sBuffer; // Para usarla en las funciones GetSection(s)
        private string _pathFile; // Nombre del fichero INI

        public INI(string pathFile)
        {
            _pathFile = pathFile;
        }

        /// <summary>
        /// Crea una nueva instancia de ini
        /// </summary>
        /// <param name="pathFile">Ruta del fichero</param>
        /// <returns></returns>
        public static INI Open(string pathFile)
        {
            return new INI(pathFile);
        }
        //--- Declaraciones para leer ficheros INI ---
        /// <summary>
        /// Leer todas las secciones de un fichero INI, esto seguramente no funciona en Win95
        /// Esta función no estaba en las declaraciones del API que se incluye con el VB
        /// </summary>
        /// <param name="lpszReturnBuffer"></param>
        /// <param name="nSize"></param>
        /// <param name="lpFileName"></param>
        /// <returns></returns>
        [DllImport("Kernel32.dll", CharSet = CharSet.Auto)]
        private static extern int GetPrivateProfileSectionNames(
            string lpszReturnBuffer,  // address of return buffer
            int nSize,             // size of return buffer
            string lpFileName         // address of initialization filename
        );

        /// <summary>
        /// Leer una sección completa
        /// </summary>
        /// <param name="lpAppName"></param>
        /// <param name="lpReturnedString"></param>
        /// <param name="nSize"></param>
        /// <param name="lpFileName"></param>
        /// <returns></returns>
        [DllImport("Kernel32.dll", CharSet = CharSet.Auto)]
        private static extern int GetPrivateProfileSection(
            string lpAppName,         // address of section name
            string lpReturnedString,  // address of return buffer
            int nSize,             // size of return buffer
            string lpFileName         // address of initialization filename
        );

        /// <summary>
        /// Leer una clave de un fichero INI 
        /// </summary>
        /// <param name="lpAppName"></param>
        /// <param name="lpKeyName"></param>
        /// <param name="lpDefault"></param>
        /// <param name="lpReturnedString"></param>
        /// <param name="nSize"></param>
        /// <param name="lpFileName"></param>
        /// <returns></returns>
        [DllImport("Kernel32.dll", CharSet = CharSet.Auto)]
        private static extern int GetPrivateProfileString(
            string lpAppName,        // points to section name
            string lpKeyName,        // points to key name
            string lpDefault,        // points to default string
            string lpReturnedString, // points to destination buffer
            int nSize,            // size of destination buffer
            string lpFileName        // points to initialization filename
        );

        /// <summary>
        /// Leer una clave de un fichero INI 
        /// </summary>
        /// <param name="lpAppName"></param>
        /// <param name="lpKeyName"></param>
        /// <param name="lpDefault"></param>
        /// <param name="lpReturnedString"></param>
        /// <param name="nSize"></param>
        /// <param name="lpFileName"></param>
        /// <returns></returns>
        [DllImport("Kernel32.dll", CharSet = CharSet.Auto)]
        private static extern int GetPrivateProfileString(
            string lpAppName,        // points to section name
            int lpKeyName,        // points to key name
            string lpDefault,        // points to default string
            string lpReturnedString, // points to destination buffer
            int nSize,            // size of destination buffer
            string lpFileName        // points to initialization filename
            );

        /// <summary>
        /// Escribir una clave de un fichero INI (también para borrar claves y secciones)
        /// </summary>
        /// <param name="lpAppName"></param>
        /// <param name="lpKeyName"></param>
        /// <param name="lpString"></param>
        /// <param name="lpFileName"></param>
        /// <returns></returns>
        [DllImport("Kernel32.dll", CharSet = CharSet.Auto)]
        private static extern int WritePrivateProfileString(
            string lpAppName,  // pointer to section name
            string lpKeyName,  // pointer to key name
            string lpString,   // pointer to string to add
            string lpFileName  // pointer to initialization filename
        );

        /// <summary>
        /// Escribir una clave de un fichero INI (también para borrar claves y secciones)
        /// </summary>
        /// <param name="lpAppName"></param>
        /// <param name="lpKeyName"></param>
        /// <param name="lpString"></param>
        /// <param name="lpFileName"></param>
        /// <returns></returns>
        [DllImport("Kernel32.dll", CharSet = CharSet.Auto)]
        private static extern int WritePrivateProfileString(
            string lpAppName,  // pointer to section name
            string lpKeyName,  // pointer to key name
            int lpString,   // pointer to string to add
            string lpFileName  // pointer to initialization filename
            );

        /// <summary>
        /// Escribir una clave de un fichero INI (también para borrar claves y secciones)
        /// </summary>
        /// <param name="lpAppName"></param>
        /// <param name="lpKeyName"></param>
        /// <param name="lpString"></param>
        /// <param name="lpFileName"></param>
        /// <returns></returns>
        [DllImport("Kernel32.dll", CharSet = CharSet.Auto)]
        private static extern int WritePrivateProfileString(
            string lpAppName,  // pointer to section name
            int lpKeyName,  // pointer to key name
            int lpString,   // pointer to string to add
            string lpFileName  // pointer to initialization filename
            );

        /// <summary>
        /// Borrar una clave o entrada de un fichero INI                  (16/Feb/99)
        /// Si no se indica sKey, se borrará la sección indicada en sSection
        /// En otro caso, se supone que es la entrada (clave) lo que se quiere borrar
        ///
        /// Para borrar una sección se debería usar IniDeleteSection
        /// </summary>
        /// <param name="sSection">La seccion sobre la que se desea buscar</param>
        /// <param name="sKey">La clave que se desea borrar</param>
        public void IniDeleteKey(string sSection, string sKey)
        {
            string sIniFile = _pathFile;
            //--------------------------------------------------------------------------
            //
            if (sKey == "")
            {
                // Borrar una sección
                WritePrivateProfileString(sSection, 0, 0, sIniFile);
            }
            else
            {
                // Borrar una entrada
                WritePrivateProfileString(sSection, sKey, 0, sIniFile);
            }
        }

        /// <summary>
        /// Borrar una sección
        /// </summary>
        /// <param name="sIniFile">El fichero INI</param>
        /// <param name="sSection">La sección que se desea borrar</param>
        public void IniDeleteSection(string sSection)
        {
            string sIniFile = _pathFile;
            //--------------------------------------------------------------------------
            WritePrivateProfileString(sSection, 0, 0, sIniFile);
        }

        /// <summary>
        /// Devuelve el valor de una clave de un fichero INI
        /// </summary>
        /// <param name="sSection">La sección de la que se quiere leer</param>
        /// <param name="sKeyName">Clave</param>
        /// <param name="sDefault">Valor opcional que devolverá si no se encuentra la clave</param>
        /// <returns></returns>
        public string IniGet(string sSection, string sKeyName, string sDefault)
        {
            string sIniFile = _pathFile;
            int ret;
            string sRetVal;
            //
            sRetVal = new string(' ', 255);
            //
            ret = GetPrivateProfileString(sSection, sKeyName, sDefault, sRetVal, sRetVal.Length, sIniFile);
            if (ret == 0)
            {
                return sDefault;
            }
            else
            {
                return sRetVal.Substring(0, ret);
            }
        }

        /// <summary>
        /// Guarda los datos de configuración
        /// </summary>
        /// <param name="sSection">La sección de la que se quiere leer</param>
        /// <param name="sKeyName">Clave</param>
        /// <param name="sValue">Valor a guardar</param>
        public void IniWrite(string sSection, string sKeyName, string sValue)
        {
            string sIniFile = _pathFile;
            WritePrivateProfileString(sSection, sKeyName, sValue, sIniFile);
        }

        /// <summary>
        /// Lee una sección entera de un fichero INI                      (27/Feb/99)
        /// Adaptada para devolver un array de string                     (04/Abr/01)
        ///
        /// Esta función devolverá un array de índice cero con las claves y valores de la sección
        /// </summary>
        /// <param name="sFileName">Nombre del fichero INI</param>
        /// <param name="sSection">Nombre de la sección a leer</param>
        /// <returns>
        /// Un array con el nombre de la clave y el valor
        ///   Para leer los datos:
        ///       For i = 0 To UBound(elArray) -1 Step 2
        ///           sClave = elArray(i)
        ///           sValor = elArray(i+1)
        ///       Next
        /// </returns>
        public string[,] IniGetSection(string sSection)
        {
            string sIniFile = _pathFile;
            string[,] aSeccion;
            int n;
            //
            aSeccion = new string[0, 0];
            //
            // El tamaño máximo para Windows 95
            sBuffer = new string('\0', 32767);
            //
            n = GetPrivateProfileSection(sSection, sBuffer, sBuffer.Length, sIniFile);
            //
            if (n > 0)
            {
                // Cortar la cadena al número de caracteres devueltos
                // menos los dos últimos que indican el final de la cadena
                sBuffer = sBuffer.Substring(0, n - 2).TrimEnd();
                //
                // Cada una de las entradas estará separada por un Chr$(0)
                // y cada valor estará en la forma: clave = valor
                string[] aSeccionAux = sBuffer.Split(new char[] { '\0' });
                aSeccion = new string[aSeccionAux.Length, 2];
                for (int idx = 0; idx < aSeccionAux.Length; idx++)
                {
                    var value = aSeccionAux[idx].Split(new char[] { '=' });
                    aSeccion[idx, 0] = value[0];
                    aSeccion[idx, 1] = value[1];
                }
            }
            // Devolver el array
            return aSeccion;
        }

        /// <summary>
        /// Devuelve todas las secciones de un fichero INI                (27/Feb/99)
        /// Adaptada para devolver un array de string                     (04/Abr/01)
        ///
        /// Esta función devolverá un array con todas las secciones del fichero
        /// </summary>
        /// <param name="sFileName">Nombre del fichero INI</param>
        /// <returns>
        ///   Un array con todos los nombres de las secciones
        ///   La primera sección estará en el elemento 1,
        ///   por tanto, si el array contiene cero elementos es que no hay secciones
        /// </returns>
        public string[] IniGetSections()
        {
            string sIniFile = _pathFile;
            int n;
            string[] aSections;
            //
            aSections = new string[0];
            //
            // El tamaño máximo para Windows 95
            sBuffer = new string('\0', 32767);
            //
            // Esta función del API no está definida en el fichero TXT
            n = GetPrivateProfileSectionNames(sBuffer, sBuffer.Length, sIniFile);
            //
            if (n > 0)
            {
                // Cortar la cadena al número de caracteres devueltos
                // menos los dos últimos que indican el final de la cadena
                sBuffer = sBuffer.Substring(0, n - 2).TrimEnd();
                aSections = sBuffer.Split('\0');
            }
            // Devolver el array
            return aSections;
        }
    }
}