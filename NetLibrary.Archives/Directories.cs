using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace NetLibrary.Archives
{
    public class Directories
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct SHFILEINFO
        {
            public IntPtr hIcon;
            public IntPtr iIcon;
            public uint dwAttributes;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szDisplayName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string szTypeName;
        };

        class Win32
        {
            public const uint SHGFI_ICON = 0x100;
            public const uint SHGFI_LARGEICON = 0x0; // 'Large icon
            public const uint SHGFI_SMALLICON = 0x1; // 'Small icon

            [DllImport("shell32.dll")]
            public static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, ref SHFILEINFO psfi, uint cbSizeFileInfo, uint uFlags);
        }

        /// <summary>
        /// Obtiene el icono asociado de un directorio
        /// </summary>
        /// <param name="file">Ruta del directorio</param>
        /// <returns>Devuelve el icono, null si no se encuentra</returns>
        public static Icon getAssociatedIcon(string directory)
        {
            SHFILEINFO shinfo = new SHFILEINFO();
            IntPtr hImgSmall = Win32.SHGetFileInfo(directory, 0, ref shinfo, (uint)Marshal.SizeOf(shinfo), Win32.SHGFI_ICON | Win32.SHGFI_SMALLICON);
            DirectoryInfo fInfo = new DirectoryInfo(directory);
            if (shinfo.hIcon.ToInt32().Equals(0)) return null;
            Icon icon = System.Drawing.Icon.FromHandle(shinfo.hIcon);
            return icon;
        }

        /// <summary>
        /// Obtiene el icono asociado de un fichero
        /// </summary>
        /// <param name="file">Ruta del fichero</param>
        /// <returns>Devuelve el icono</returns>
        public static Icon getAssociatedIcon(DirectoryInfo directory)
        {
            return getAssociatedIcon(directory.FullName);
        }

        /// <summary>
        /// Crea una instancia de la clase DirectoryInfo para la ruta especificada
        /// </summary>
        /// <param name="path">Directorio</param>
        /// <returns>Instancia de la clase DirectoryInfo</returns>
        public static DirectoryInfo GetInfo(string path)
        {
            return new DirectoryInfo(path);
        }

        /// <summary>
        /// Comprueba si existe el directorio especificado
        /// </summary>
        /// <param name="pathDirectory">Ruta del Directorio</param>
        /// <returns>Booleano que especifica la existencia del directorio</returns>
        public static bool Exists(string pathDirectory)
        {
            return Directory.Exists(pathDirectory);
        }

        /// <summary>
        /// Comprueba si existen todos los directorios pasados como parámetro
        /// </summary>
        /// <param name="directories">Lista de directorios que se desea comprobar</param>
        /// <returns></returns>
        public static bool ExistsAll(string[] directories)
        {
            for (int x = 0; x < directories.Length; x++)
                if (!Exists(directories[x]))
                    return false;

            return true;
        }

        /// <summary>
        /// Crea todas las carpetas necesarias hasta completar la ruta especificada
        /// </summary>
        /// <param name="path">Ruta a crear</param>
        public static void CreatePath(string path)
        {
            Directory.CreateDirectory(path);
        }

        /// <summary>
        /// Obtiene todos los subdirectorios de la ruta especifiada
        /// </summary>
        /// <param name="pathDirectory">Ruta del Directorio</param>
        /// <returns>Lista de directorios encontrados</returns>
        public static DirectoryInfo[] GetDirectories(string path)
        {
            string[] directories = Directory.GetDirectories(path);
            List<DirectoryInfo> result = new List<DirectoryInfo>();
            for (int x = 0; x < directories.Length; x++)
            {
                result.Add(new DirectoryInfo(directories[x]));
            }

            return result.ToArray();
        }

        /// <summary>
        /// Renombra el directorio especificado con el nuevo nombre
        /// </summary>
        /// <param name="pathDirectory">Ruta completa del directorio</param>
        /// <param name="newName">Nuevo nombre del directorio</param>
        public static void Rename(string pathDirectory, string newName)
        {
            DirectoryInfo di = new DirectoryInfo(pathDirectory);
            Rename(di, newName);
        }

        /// <summary>
        /// Renombra el directorio especificado con el nuevo nombre
        /// </summary>
        /// <param name="directory">Directorio origen</param>
        /// <param name="newName">Nuevo nombre del directorio</param>
        public static void Rename(DirectoryInfo directory, string newName)
        {
            directory.MoveTo(Path.Combine(directory.Parent.FullName, newName));
        }

        /// <summary>
        /// Reemplaza los caracteres invalidos para un directorio
        /// </summary>
        /// <param name="dirName">Nombre del directorio</param>
        /// <param name="newChar">Caracter por el que se reemplazarán</param>
        /// <returns>Nombre con los caracteres especiales eliminados</returns>
        public static string ClearForbiddenChars(string dirName, string newChar)
        {
            string invalidChars = new string(Path.GetInvalidFileNameChars());
            return Regex.Replace(dirName, "[" + invalidChars + "]+", newChar, RegexOptions.Compiled);
        }

        /// <summary>
        /// Reemplaza los caracteres invalidos para un directorio
        /// </summary>
        /// <param name="dirName">Nombre del directorio</param>
        /// <param name="newChar">Caracter por el que se reemplazarán</param>
        /// <returns>Nombre con los caracteres especiales eliminados</returns>
        public static string ClearForbiddenChars(DirectoryInfo dirName, string newChar)
        {
            return ClearForbiddenChars(dirName.FullName, newChar);
        }

        /// <summary>
        /// Obtiene la ruta de la carpeta temporal
        /// </summary>
        /// <returns>Path de la ruta temporal</returns>
        public static string GetTempPath()
        {
            return Path.GetTempPath();
        }

        public static DirectoryInfo[] GetEmtpyDirectories(string rootPath)
        {
            return GetEmtpyDirectories(new DirectoryInfo(rootPath));
        }

        public static DirectoryInfo[] GetEmtpyDirectories(DirectoryInfo rootPath)
        {
            List<DirectoryInfo> lDirectories = new List<DirectoryInfo>();
            foreach(DirectoryInfo dInfo in rootPath.GetDirectories())
                if (dInfo.GetDirectories().Length == 0 && dInfo.GetFiles().Length == 0)
                    lDirectories.Add(dInfo);
                else 
                    lDirectories.AddRange(GetEmtpyDirectories(dInfo));

            return lDirectories.ToArray();
        }
    }
}
