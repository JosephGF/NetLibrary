using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Windows.Forms;

namespace NetLibrary.Debugger
{
    public class DebuggerErrorEvenArgs : System.Threading.ThreadExceptionEventArgs
    {
        public DebuggerErrorEvenArgs(Exception ex) : base(ex)
        {
            this.DebugException = new DebugErrorData(ex);
        }

        public DebuggerErrorEvenArgs(DebugErrorData dEx) : base(dEx.Exception)
        {
            this.DebugException = dEx;
        }

        /// <summary>
        /// Cancela la propagación del evento
        /// </summary>
        public bool Cancel { get; set; }

        public DebugErrorData DebugException { get; private set; }
    }

    public class Debug
    {
        /// <summary>
        /// Se lanza cuando se produce una excepción sin controlar
        /// </summary>
        public static event EventHandler<DebuggerErrorEvenArgs> onDebuggerError;

        /// <summary>
        /// Tipos de ficheros que se exportan
        /// </summary>
        public enum ErrorToFile
        {
            None,
            Json,
            Xml,
            Html,
            /// <summary>
            /// Texto plano
            /// </summary>
            Plain
        }

        /// <summary>
        /// Used to debugger dll errors
        /// </summary>
        internal static bool InternalDebug { get; set; }

        /// <summary>
        /// Indica si se exportará el texto escrito por Debug.WriteLine al fichero debugger.log (True por defecto)
        /// </summary>
        public static bool WriteLog { get; set; }

        /// <summary>
        /// Instancia del formulario de excepciones no controladas
        /// </summary>
        public static FrmException FormException { get; set; }

        /// <summary>
        /// Ruta del log que se escribirá
        /// </summary>
        public static DirectoryInfo DirectoryLogs { get; set; }

        /// <summary>
        /// Indica si se exportarán los errores a un fichero xml
        /// </summary>
        public static ErrorToFile ErrorsExportFormat { get; set; }

        private static Stream _standardOutput = Console.OpenStandardOutput();

        static Debug()
        {
            DirectoryLogs = new DirectoryInfo(Application.StartupPath);
            ErrorsExportFormat = ErrorToFile.Xml;
            WriteLog = true;

            if (Environment.GetCommandLineArgs().Contains("/console"))
                Debug.OpenConsole();

            if (Environment.GetCommandLineArgs().Contains("/debugger"))
            {
                Debug.InternalDebug = true;
                Debug.WriteDebugger("Debug for debugger is enabled");
            }
        }

        /// <summary>
        /// Inicializael debug para que espere excepciones no controladas 
        /// </summary>
        public static void initialize()
        {
            Debug.initialize(new FrmException());
        }

        /// <summary>
        /// Inicializael debug para que espere excepciones no controladas 
        /// </summary>
        /// <param name="customForm">Formulario heredado para customizar el original</param>
        public static void initialize(FrmException customForm)
        {
            StartExceptionListener();
            Debug.FormException = customForm;
        }

        private static void StartExceptionListener()
        {
            //Excepciones en el thread principal
            Application.ThreadException += Application_ThreadException;

            //Excepciones en otros threads
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }


        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Debug.onException(sender, (Exception)e.ExceptionObject);
        }

        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            Debug.onException(sender, e.Exception);
        }

        private static void onException(object sender, Exception ex)
        {
            Debug.WriteDebugger("Debug -> onException -> Start");
            DebugErrorData dErr = new DebugErrorData(ex);


            Debug.WriteDebugger("Debug -> onException - Generate export text");
            string exportText = Debug.GetDefaultExportText(dErr);

            if (onDebuggerError != null)
            {
                DebuggerErrorEvenArgs args = new DebuggerErrorEvenArgs(dErr);
                onDebuggerError(sender, args);
                if (args.Cancel) return;
            }


            if (ErrorsExportFormat != ErrorToFile.None && Debug.DirectoryLogs != null && Debug.DirectoryLogs.Exists)
            {
                Debug.WriteDebugger("Debug -> onException - Save file exception.");
                Debug.SaveFileException(dErr);
            }

            Debug.WriteDebugger("Debug -> onException - Open form");
            Debug.FormException.ShowDialog(dErr);
            Debug.WriteLine(ex);
        }

        internal static void WriteDebugger(string text)
        {
            if (Debug.InternalDebug)
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Debug.WriteLine("Debugger: " + text);
            }
        }

        /// <summary>
        /// Obtiene el texto en el formato establecido en Debug.ErrorsExportFormat (XML, JSON, HTML o Texto Plano)
        /// </summary>
        /// <param name="dErr">Error actual</param>
        /// <returns>Texto en el formato actual</returns>
        public static string GetDefaultExportText(DebugErrorData dErr)
        {
            string exportText = "";

            switch (Debug.ErrorsExportFormat)
            {
                case ErrorToFile.Xml:
                    exportText = dErr.XMLData;
                    break;
                case ErrorToFile.Json:
                    exportText = dErr.JSONData;
                    break;
                case ErrorToFile.Html:
                    exportText = dErr.HTMLData;
                    break;
                default:
                    exportText = dErr.ToString() + Environment.NewLine + dErr.SystemInformation.ToString();
                    break;
            }

            return exportText;
        }

        /// <summary>
        /// Obtiene la extension asociada al formato
        /// </summary>
        /// <param name="format">Formato de salida</param>
        /// <returns></returns>
        public static string GetExtension(ErrorToFile format)
        {
            switch (format)
            {
                case ErrorToFile.Html:
                    return ".html";
                case ErrorToFile.Json:
                    return ".json";
                case ErrorToFile.Xml:
                    return ".xml";
                case ErrorToFile.Plain:
                default:
                    return ".txt";
            }
        }

        /// <summary>
        /// Guarda un fichero con los datos de la excepción
        /// </summary>
        /// <param name="ex">Excepcion que se desea enviar</param>
        /// <returns></returns>
        public static bool SaveFileException(Exception ex)
        {
            return Debug.SaveFileException(new DebugErrorData(ex));
        }

        /// <summary>
        /// Envia un email con los datos de la excepción
        /// </summary>
        /// <param name="dErr">Excepcion que se desea enviar</param>
        /// <returns></returns>
        public static bool SaveFileException(DebugErrorData dErr)
        {
            string exportText = Debug.GetDefaultExportText(dErr);

            string file = Debug.DirectoryLogs + "\\" + DateTime.Now.Ticks + ".err";
            File.WriteAllText(file, exportText);

            return true;
        }

        /// <summary>
        /// Realiza un Console.WriteLiLine además de exportarlo a un fichero si está configrado
        /// </summary>
        /// <param name="text">Texto que se escribirá en la salida por defecto de la aplicación</param>
        /// <param name="foreColor">Color del texto</param>
        /// <param name="ignoreExprt">Indica si el texto ignorara el log de salida (information.log)</param>
        public static void WriteLine(string text, ConsoleColor foreColor, bool ignoreExprt = false)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Debug.WriteLine(text);
        }

        /// <summary>
        /// Realiza un Console.WriteLiLine además de exportarlo a un fichero si está configrado
        /// </summary>
        /// <param name="text">Texto que se escribirá en la salida por defecto de la aplicación</param>
        /// <param name="foreColor">Color del texto</param>
        /// <param name="backgroundColor">Color de fondo</param>
        /// <param name="ignoreExprt">Indica si el texto ignorara el log de salida (information.log)</param>
        public static void WriteLine(string text, ConsoleColor foreColor, ConsoleColor backgroundColor, bool ignoreExprt = false)
        {
            Console.ForegroundColor = foreColor;
            Console.BackgroundColor = backgroundColor;
            Debug.WriteLine(text);
        }

        /// <summary>
        /// Realiza un Console.WriteLiLine además de exportarlo a un fichero si está configrado
        /// </summary>
        /// <param name="text">Texto que se escribirá en la salida por defecto de la aplicación</param>
        /// <param name="ignoreExprt">Indica si el texto ignorara el log de salida (information.log)</param>
        public static void WriteLine(string text, bool ignoreExprt = false)
        {
            text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss") + " " + text.Trim();
            if (Debug.WriteLog && !ignoreExprt)
            {
                if (String.IsNullOrEmpty(text)) return;

                if (DirectoryLogs.Exists)
                    File.AppendAllText(DirectoryLogs.FullName + "\\information.log", text + Environment.NewLine);
            }

            try
            {
                Console.WriteLine(text);
            }
            catch (IOException ex)
            {
                MessageBox.Show("WriteLine dejó de fucionar, reinicie la aplicación" + Environment.NewLine + ex.Message);
            }

            Console.ResetColor();
        }

        /// <summary>
        /// Realiza un Console.WriteLiLine además de exportarlo a un fichero si está configrado
        /// </summary>
        /// <param name="text">Texto que se escribirá en la salida por defecto de la aplicación</param>
        public static void WriteError(string text)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Debug.WriteLine("Error: " + text);
        }

        /// <summary>
        /// Realiza un Console.WriteLiLine además de exportarlo a un fichero si está configrado
        /// </summary>
        /// <param name="text">Texto que se escribirá en la salida por defecto de la aplicación</param>
        public static void WriteWarn(string text)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Debug.WriteLine("Warning: " + text);
        }

        /// <summary>
        /// Realiza un Console.WriteLiLine además de exportarlo a un fichero si está configrado
        /// </summary>
        /// <param name="text">Texto que se escribirá en la salida por defecto de la aplicación</param>
        public static void WriteDebug(string text)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Debug.WriteLine("Debug: " + text, true);
        }

        /// <summary>
        /// Realiza un Console.WriteLiLine además de exportarlo a un fichero si está configrado
        /// </summary>
        /// <param name="text">Texto que se escribirá en la salida por defecto de la aplicación</param>
        public static void WriteInfo(string text)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Debug.WriteLine("Info: " + text);
        }

        /// <summary>
        /// Realiza un Console.WriteLiLine además de exportarlo a un fichero si está configrado
        /// </summary>
        /// <param name="ex">Excepción sobre la que se escribirá la información</param>
        public static void WriteLine(Exception ex)
        {
            Debug.WriteError(ex.GetType().FullName + Environment.NewLine + ex.Message + Environment.NewLine + ex.StackTrace);
        }

        /// <summary>
        /// Lee una linea escrita por el usuario de la consola
        /// </summary>
        /// <returns></returns>
        public static string ReadLine()
        {
            string command = Console.ReadLine();
            switch (command)
            {
                case "clear":
                    Console.Clear();
                    break;
                case "exit":
                    Application.Exit();
                    break;
                case "version":
                    MessageBox.Show(Credits, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case "info":
                    Console.Write(new Information().ToString());
                    break;
            }

            return command;
        }

        #region Application Console
        [DllImport("kernel32.dll")]
        private static extern Boolean AllocConsole();
        [DllImport("kernel32.dll")]
        private static extern Boolean FreeConsole();
        [DllImport("kernel32.dll")]
        private static extern bool AttachConsole(int pid);

        /// <summary>
        /// Abre la consola de debug de la aplicación
        /// </summary>
        /// <returns></returns>
        public static Boolean OpenConsole()
        {
            if (!AttachConsole(-1))
                AllocConsole();

            StreamWriter standardOutput = new StreamWriter(Console.OpenStandardOutput());
            standardOutput.AutoFlush = true;
            Console.SetOut(standardOutput);

            StreamReader standarInput = new StreamReader(Console.OpenStandardInput());
            standardOutput.AutoFlush = true;
            Console.SetIn(standarInput);

            Application.ApplicationExit -= Application_ApplicationExit;
            Application.ApplicationExit += Application_ApplicationExit;

            Information i = new Information();
            Console.WriteLine(i.OSVersion + " " + i.OSProcess);
            Console.WriteLine(i.ApplicationName + " " + i.AppProcess);
            return true;
        }

        /// <summary>
        /// Libera los recursos de la consola y la cierra
        /// </summary>
        /// <returns></returns>
        public static Boolean CloseConsole()
        {
            return FreeConsole();
        }

        static void Application_ApplicationExit(object sender, EventArgs e)
        {
            FreeConsole();
        }

        private static string Credits
        {
            get { return "Joseph Girón Flores - Debugger V. " + Environment.Version; }
        }

        #endregion
    }
}