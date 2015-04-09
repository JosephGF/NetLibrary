using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace NetLibrary.Archives
{
    public class Files
    {
        /// <summary>
        /// Evento que se lanza cuando cambia un fichero que está siendo escuchado por Files.FileWatcher
        /// </summary>
        public static EventHandler<FileSystemEventArgs> FileChanged;
        /// <summary>
        /// Lanza el evento Files.FIleChanged cuando cambia la fecha de acceso/escritura o el nombre del fichero o directorio
        /// </summary>
        /// <param name="file">Fichero que se desea examinar</param>
        /// <returns>Instancia al objecto FileSystemWatcher</returns>
        public static FileSystemWatcher FileWatcher(FileInfo file)
        {
            return Files.FileWatcher(file.FullName);
        }
        /// <summary>
        /// Lanza el evento Files.FIleChanged cuando cambia la fecha de acceso/escritura o el nombre del fichero o directorio
        /// </summary>
        /// <param name="path">Ruta del fichero que se desea examinar</param>
        /// <returns>Instancia al objecto FileSystemWatcher</returns>
        public static FileSystemWatcher FileWatcher(string path)
        {
            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = path;
            watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName;
            watcher.Changed += new FileSystemEventHandler(OnChanged);
            watcher.Created += new FileSystemEventHandler(OnChanged);
            watcher.Deleted += new FileSystemEventHandler(OnChanged);
            watcher.Renamed += new RenamedEventHandler(OnRenamed);
            watcher.EnableRaisingEvents = true;
            return watcher;
        }
        private static void OnChanged(object source, FileSystemEventArgs e)
        {
            if (FileChanged != null)
                FileChanged(source, e);
            //Console.WriteLine("File: " + e.FullPath + " " + e.ChangeType);
        }
        private static void OnRenamed(object source, RenamedEventArgs e)
        {
            if (FileChanged != null)
                FileChanged(source, e);
            //Console.WriteLine("File: {0} renamed to {1}", e.OldFullPath, e.FullPath);
        }

        /// <summary>
        /// Abre el fichero especificado en una nueva ventana con la aplicación por defecto
        /// </summary>
        /// <param name="path">Ruta completa del fichero</param>
        /// <returns></returns>
        public static Process Open(string path)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo(path);
            startInfo.CreateNoWindow = true;
            startInfo.ErrorDialog = true;
            startInfo.WindowStyle = ProcessWindowStyle.Maximized;
            //startInfo.UseShellExecute = false;
            Process process = new Process();
            process.StartInfo = startInfo;
            process.Start();

            return process;
        }

        /// <summary>
        /// Crea una instancia de la clase FileInfo
        /// </summary>
        /// <param name="path">Ruta del fichero</param>
        /// <returns>FileInfo</returns>
        public static FileInfo GetInfo(string path)
        {
            return new FileInfo(path);
        }

        /// <summary>
        /// Comprueba si existe el fichero especificado
        /// </summary>
        /// <param name="pathDirectory">Ruta del fichero</param>
        /// <returns>Booleano que especifica la existencia del fichero</returns>
        public static bool Exists(string file)
        {
            return Files.Exists(file);
        }
        /// <summary>
        /// Comprueba si existen todos los ficheros pasados como parámetro
        /// </summary>
        /// <param name="directories">Lista de ficheros que se desea comprobar</param>
        /// <returns>Booleano que especifica la existencia de los ficheros</returns>
        public static bool ExistsAll(string[] files)
        {
            for (int x = 0; x < files.Length; x++)
                if (!Exists(files[x]))
                    return false;

            return true;
        }

        /// <summary>
        /// Obtiene el icono asociado de un fichero
        /// </summary>
        /// <param name="file">Ruta del fichero</param>
        /// <returns>Devuelve el icono</returns>
        public static Icon getAssociatedIcon(string file)
        {
            FileInfo fInfo = new FileInfo(file);
            return getAssociatedIcon(new FileInfo(file));
        }
        /// <summary>
        /// Obtiene el icono asociado de un fichero
        /// </summary>
        /// <param name="file">Ruta del fichero</param>
        /// <returns>Devuelve el icono</returns>
        public static Icon getAssociatedIcon(FileInfo file)
        {
            Icon ico = null;
            if (file.Exists)
            {
                ico = Icon.ExtractAssociatedIcon(file.FullName);
            }
            return ico;
        }

        /// <summary>
        /// Obtiene todos los ficheros que se encuentren en el directorio especificado (no busca en subdirectorios)
        /// </summary>
        /// <param name="path">Ruta del directorio donde se iniciará la búsqueda</param>
        /// <returns>Lista de los ficheros encontrados</returns>
        public static FileInfo[] GetFiles(string path)
        {
            return GetFiles(path, "*", SearchOption.TopDirectoryOnly);
        }
        /// <summary>
        /// Obtiene todos los ficheros que se encuentren en el directorio especificado (no busca en subdirectorios)
        /// </summary>
        /// <param name="path">Ruta del directorio donde se iniciará la búsqueda</param>
        /// <param name="filter">Filtro usado para buscar [* caracter comodin]</param>
        /// <returns>Lista de los ficheros encontrados</returns>
        public static FileInfo[] GetFiles(string path, string filter)
        {
            return GetFiles(path, filter, SearchOption.TopDirectoryOnly);
        }
        /// <summary>
        /// Obtiene todos los ficheros que se encuentren en el directorio especificado
        /// </summary>
        /// <param name="path">Ruta del directorio donde se iniciará la búsqueda</param>
        /// <param name="searchOpc">Opciones de búsqueda</param>
        /// <returns>Lista de los ficheros encontrados</returns>
        public static FileInfo[] GetFiles(string path, SearchOption searchOpc)
        {
            return GetFiles(path, "*", searchOpc);
        }
        /// <summary>
        /// Obtiene todos los ficheros que se adapten al filtro especificado para el directorio
        /// </summary>
        /// <param name="path">Ruta del directorio donde se iniciará la búsqueda</param>
        /// <param name="filter">Filtro usado para buscar [* caracter comodin]</param>
        /// <param name="searchOpc">Opciones de búsqueda</param>
        /// <returns>Lista de los ficheros encontrados</returns>
        public static FileInfo[] GetFiles(string path, string filter, SearchOption searchOpc)
        {
            return GetFiles(new DirectoryInfo(path), filter, searchOpc);
        }
        /// <summary>
        /// Obtiene todos los ficheros que se adapten al filtro especificado para el directorio
        /// </summary>
        /// <param name="directory">Ruta del directorio donde se iniciará la búsqueda</param>
        /// <param name="filter">Filtro usado para buscar [* caracter comodin]</param>
        /// <param name="searchOpc">Opciones de búsqueda</param>
        /// <returns>Lista de los ficheros encontrados</returns>
        public static FileInfo[] GetFiles(DirectoryInfo directory, string filter, SearchOption searchOpc)
        {
            return directory.GetFiles(filter, searchOpc).OrderBy(f => f.FullName).ToArray();
        }
        /// <summary>
        /// Obtiene todos los ficheros que se adapten a la lista de filtros especificada para el directorio
        /// </summary>
        /// <param name="path">Ruta del directorio donde se iniciará la búsqueda</param>
        /// <param name="filter">Filtro usada para buscar [* caracter comodin]</param>
        /// <param name="searchOpc">Opciones de búsqueda</param>
        /// <returns>Lista de los ficheros encontrados</returns>
        public static FileInfo[] GetFiles(string path, string[] filter, SearchOption searchOpc)
        {
            return GetFiles(new DirectoryInfo(path), filter, searchOpc);
        }
        /// <summary>
        /// Obtiene todos los ficheros que se adapten a la lista de filtros especificada para el directorio
        /// </summary>
        /// <param name="directory">Directorio donde se iniciará la búsqueda</param>
        /// <param name="filter">Lista de filtros usada para buscar [* caracter comodin]</param>
        /// <param name="searchOpc">Opciones de búsqueda</param>
        /// <returns>Lista de los ficheros encontrados</returns>
        public static FileInfo[] GetFiles(DirectoryInfo directory, string[] filter, SearchOption searchOpc)
        {
            List<FileInfo> result = new List<FileInfo>();
            for (int i = 0; i < filter.Length; i++)
                result.AddRange(GetFiles(directory, filter[i], searchOpc));

            return result.OrderBy(f => f.Name).ToArray();
        }

        /// <summary>
        /// Renombra el fichero especificado con el nuevo nombre
        /// </summary>
        /// <param name="pathDirectory">Ruta completa del fichero</param>
        /// <param name="newName">Nuevo nombre del fichero</param>
        public static void Rename(string pathDirectory, string newName)
        {
            FileInfo di = new FileInfo(pathDirectory);
            Rename(di, newName);
        }
        /// <summary>
        /// Renombra el fichero especificado con el nuevo nombre
        /// </summary>
        /// <param name="directory">Directorio origen</param>
        /// <param name="newName">Nuevo nombre del directorio</param>
        public static void Rename(FileInfo file, string newName)
        {
            file.MoveTo(Path.Combine(file.DirectoryName, newName));
        }

        /// <summary>
        /// Obtiene el contenido del fichero como cadena
        /// </summary>
        /// <param name="file">Ruta del fichero</param>
        /// <returns>Texto del fichero</returns>
        public static string GetText(string file)
        {
            return GetText(new FileInfo(file));
        }
        /// <summary>
        /// Obtiene el contenido del fichero como cadena
        /// </summary>
        /// <param name="file">Fichero</param>
        /// <returns>Texto del fichero</returns>
        public static string GetText(FileInfo file)
        {
            if (file.Exists)
            {
                return File.ReadAllText(file.FullName);
            }
            return null;
        }

        /// <summary>
        /// Determines a text file's encoding by analyzing its byte order mark (BOM).
        /// Defaults to ASCII when detection of the text file's endianness fails.
        /// </summary>
        /// <param name="file">The text file to analyze.</param>
        /// <returns>The detected encoding.</returns>
        public static Encoding GetTextFileEncoding(FileInfo file)
        {
            return Files.GetTextFileEncoding(file.FullName);
        }
        /// <summary>
        /// Determines a text file's encoding by analyzing its byte order mark (BOM).
        /// Defaults to ASCII when detection of the text file's endianness fails.
        /// </summary>
        /// <param name="filename">The text file to analyze.</param>
        /// <returns>The detected encoding.</returns>
        public static Encoding GetTextFileEncoding(string filename)
        {
            // Read the BOM
            var bom = new byte[4];
            using (var file = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                file.Read(bom, 0, 4);
            }

            // Analyze the BOM
            if (bom[0] == 0x2b && bom[1] == 0x2f && bom[2] == 0x76) return Encoding.UTF7;
            if (bom[0] == 0xef && bom[1] == 0xbb && bom[2] == 0xbf) return Encoding.UTF8;
            if (bom[0] == 0xff && bom[1] == 0xfe) return Encoding.Unicode; //UTF-16LE
            if (bom[0] == 0xfe && bom[1] == 0xff) return Encoding.BigEndianUnicode; //UTF-16BE
            if (bom[0] == 0 && bom[1] == 0 && bom[2] == 0xfe && bom[3] == 0xff) return Encoding.UTF32;
            return Encoding.ASCII;
        }

        /// <summary>
        /// Reemplaza los caracteres invalidos para un nombre de fichero
        /// </summary>
        /// <param name="fileName">Nombre del directorio</param>
        /// <returns>Nombre con los caracteres especiales eliminados</returns>
        public static string ClearForbiddenChars(string fileName)
        {
            return ClearForbiddenChars(fileName, "");
        }
        /// <summary>
        /// Reemplaza los caracteres invalidos para un nombre de fichero
        /// </summary>
        /// <param name="fileName">Nombre del directorio</param>
        /// <param name="newChar">Caracter por el que se reemplazarán</param>
        /// <returns>Nombre con los caracteres especiales eliminados</returns>
        public static string ClearForbiddenChars(string fileName, string newChar)
        {
            string invalidChars = new string(Path.GetInvalidFileNameChars());
            return Regex.Replace(fileName, "[" + invalidChars + "]+", newChar, RegexOptions.Compiled);
        }
        /// <summary>
        /// Reemplaza los caracteres invalidos para un nombre de fichero
        /// </summary>
        /// <param name="fileName">Nombre del directorio</param>
        /// <returns>Nombre con los caracteres especiales eliminados</returns>
        public static string ClearForbiddenChars(FileInfo fileName)
        {
            return ClearForbiddenChars(fileName, "");
        }
        /// <summary>
        /// Reemplaza los caracteres invalidos para un nombre de fichero
        /// </summary>
        /// <param name="fileName">Nombre del directorio</param>
        /// <param name="newChar">Caracter por el que se reemplazarán</param>
        /// <returns>Nombre con los caracteres especiales eliminados</returns>
        public static string ClearForbiddenChars(FileInfo fileName, string newChar)
        {
            return ClearForbiddenChars(fileName.FullName, newChar);
        }
        
        /// <summary>
        /// Obtiene el mimetype del fichero especificado
        /// </summary>
        /// <param name="fileInfo">Fichero</param>
        /// <returns>MimeType</returns>
        public static string getMimeType(FileInfo fileInfo)
        {
            string mimeType = "application/unknown";
            string ext = fileInfo.Extension;
            Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);
            if (regKey != null && regKey.GetValue("Content Type") != null)
                mimeType = regKey.GetValue("Content Type").ToString();
            return mimeType;
        }

        #region Procesos Asincronos
        public delegate void FileEventHandler(object sender, FileEventArgs e);
        /// <summary>
        /// Se produce cuando se inicia el proceso
        /// </summary>
        public static event FileEventHandler onFileActionStart;
        /// <summary>
        /// Se produce cuando cambia el progreso de la accion
        /// </summary>
        public static event FileEventHandler onFileActionProgressChange;
        /// <summary>
        /// Se produce cuando se termina la accion
        /// </summary>
        public static event FileEventHandler onFileActionComplete;
        /// <summary>
        /// Se produce cuando existe algún error
        /// </summary>
        public static event FileEventHandler onFileActionError;

        /// <summary>
        /// Mueve sobreescribiendo si existe el fichero de destino
        /// </summary>
        /// <param name="file">Fichero a copiar</param>
        /// <param name="path">Ruta especificada</param>
        public static Thread MoveAsync(string file, string path)
        {
            return MoveAsync(new FileInfo(file), path);
        }
        /// <summary>
        /// Mueve sobreescribiendo si existe el fichero de destino
        /// </summary>
        /// <param name="file">Fichero a copiar</param>
        /// <param name="path">Ruta especificada</param>
        public static Thread MoveAsync(FileInfo file, string path)
        {
            Thread startThread = new Thread(() => FileActionAsync(file, path, Action.Move, FileAttributes.Normal));
            startThread.Start();
            //FileActionAsync(file, path, Action.Move, FileAttributes.Normal);

            return startThread;
        }

        /// <summary>
        /// Mueve sobreescribiendo si existe los ficheros de destino
        /// </summary>
        /// <param name="files">Lista de ficheros a mover</param>
        /// <param name="path">Ruta especificada</param>
        public static Thread MoveAsync(string[] files, string path)
        {
            List<FileInfo> lfiles = new List<FileInfo>();
            for (int x = 0; x < files.Length; x++)
            {
                lfiles.Add(new FileInfo(files[x]));
            }

            return MoveAsync(lfiles.ToArray(), path);
        }
        /// <summary>
        /// Mueve sobreescribiendo si existe los ficheros de destino
        /// </summary>
        /// <param name="files">Lista de ficheros a mover</param>
        /// <param name="path">Ruta especificada</param>
        public static Thread MoveAsync(FileInfo[] files, string path)
        {
            Thread startThread = new Thread(() => FileActionAsync(files, path, Action.MultiMove, FileAttributes.Normal));
            startThread.Start();

            return startThread;
        }

        /// <summary>
        /// Copia sobreescribiendo si existe el fichero de destino
        /// </summary>
        /// <param name="file">Fichero a copiar</param>
        /// <param name="path">Ruta especificada</param>
        public static Thread CopyAsync(string file, string path)
        {
            return CopyAsync(new FileInfo(file), path);
        }
        /// <summary>
        /// Copia sobreescribiendo si existe el fichero de destino
        /// </summary>
        /// <param name="file">Fichero a copiar</param>
        /// <param name="path">Ruta especificada</param>
        public static Thread CopyAsync(FileInfo file, string path)
        {
            Thread startThread = new Thread(() => FileActionAsync(file, path, Action.Copy, FileAttributes.Normal));
            startThread.Start();

            return startThread;
        }

        /// <summary>
        /// Copia sobreescribiendo si existe los ficheros de destino
        /// </summary>
        /// <param name="files">Lista de ficheros a copiar</param>
        /// <param name="path">Ruta especificada</param>
        public static Thread CopyAsync(string[] files, string path)
        {
            List<FileInfo> lfiles = new List<FileInfo>();
            for (int x = 0; x < files.Length; x++)
            {
                lfiles.Add(new FileInfo(files[x]));
            }

            return CopyAsync(lfiles.ToArray(), path);
        }
        /// <summary>
        /// Copia sobreescribiendo si existe los ficheros de destino
        /// </summary>
        /// <param name="files">Lista de ficheros a copiar</param>
        /// <param name="path">Ruta especificada</param>
        public static Thread CopyAsync(FileInfo[] files, string path)
        {
            Thread startThread = new Thread(() => FileActionAsync(files, path, Action.MultiCopy, FileAttributes.Normal));
            startThread.Start();

            return startThread;
        }

        private static void FileActionAsync(FileInfo[] file, string path, Action action, FileAttributes attributes)
        {
            for (int x = 0; x < file.Length; x++)
            {
                FileActionAsync(file[x], path, action, attributes, new int[] { x, file.Length });
            }
        }
        private static void FileActionAsync(FileInfo file, string path, Action action, FileAttributes attributes, int[] items = null)
        {
            DirectoryInfo dir = new DirectoryInfo(path);
            float maxbytes = 0;
            int progress = 0;
            int bytesread = 0;
            FileInfo destfile = null;
            try
            {
                destfile = new FileInfo(path + "\\" + file.Name);

                throwActionEvent(onFileActionStart, file, new FileEventArgs(destfile, action, State.Start, 0), items);

                byte[] buffer = new byte[4096]; //4MB buffer
                FileStream fsread = file.Open(FileMode.Open, FileAccess.Read);
                FileStream fswrite = destfile.Open(FileMode.OpenOrCreate, FileAccess.ReadWrite);
                maxbytes = 0;

                while ((bytesread = fsread.Read(buffer, 0, buffer.Length)) > 0)
                {
                    fswrite.Write(buffer, 0, bytesread);
                    maxbytes = maxbytes + bytesread;

                    int porgressAux = Convert.ToInt32(decimal.Divide((decimal)maxbytes, (decimal)file.Length) * 100);

                    if (porgressAux > progress)
                    {
                        progress = porgressAux;
                        throwActionEvent(onFileActionProgressChange, file, new FileEventArgs(destfile, action, State.Start, progress), items);
                    }
                }

                fsread.Flush();
                fswrite.Flush();
                fsread.Close();
                fswrite.Close();
                fsread.Dispose();
                fswrite.Dispose();
                //-------------------
                System.IO.File.SetAttributes(destfile.FullName, attributes);

                switch (action)
                {
                    case Action.Move:
                    case Action.MultiMove:
                        file.Delete();
                        break;
                }

            }
            catch (Exception ex)
            {
                throwActionEvent(onFileActionError, file, new FileEventArgs(destfile, action, ex.Message), items);
                throwActionEvent(onFileActionComplete, file, new FileEventArgs(destfile, action, ex.Message), items);
                return;
                //this.Invoke(OnError, new object[] { err });
                //if (err.result == DialogResult.Cancel);
            }

            throwActionEvent(onFileActionComplete, file, new FileEventArgs(destfile, action, State.Complete, 0), items);
            //could update bytes here also
        }
        private static void throwActionEvent(FileEventHandler evento, object sender, FileEventArgs args, int[] items)
        {
            if (evento == null)
                return;

            if (items != null)
            {
                int idx = items[0] + 1;
                int total = items[1];

                if (args.State == State.Complete && idx != total)
                    return;

                int progreso = Convert.ToInt32(
                                                (decimal.Divide(idx, total) * 100)
                                              + (decimal.Divide(args.Progress, total))
                                              );

                args.Progress = progreso;
            }

            evento(sender, args);
        }
        #endregion
    }
}
