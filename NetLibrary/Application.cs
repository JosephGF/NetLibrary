using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Windows.Forms;

namespace NetLibrary
{
    public class App
    {
        [DllImport("user32")]
        public static extern UInt32 SendMessage(IntPtr hWnd, UInt32 msg, UInt32 wParam, UInt32 lParam);

        internal const int BCM_FIRST = 0x1600; //Normal button
        internal const int BCM_SETSHIELD = (BCM_FIRST + 0x000C); //Elevated button

        /// <summary>
        /// Check if application is running width administrator role
        /// </summary>
        /// <summary xml:lang="es">
        /// Comprueba si la aplicación se esta ejecutando con permisos de administrador
        /// </summary>
        public static bool IsAdministrator { get { return CheckRole(WindowsBuiltInRole.Administrator); } }

        /// <summary>
        /// Reinica la aplicación ejecutandola con permisos de administrador
        /// </summary>
        public static void ExecuteAdministrator()
        {
            // Launch itself as administrator
            ProcessStartInfo proc = new ProcessStartInfo();
            proc.UseShellExecute = true;
            proc.WorkingDirectory = Environment.CurrentDirectory;
            proc.FileName = Application.ExecutablePath;
            proc.Verb = "runas";

            try
            {
                Process.Start(proc);
            }
            catch
            {
                // The user refused to allow privileges elevation.
                // Do nothing and return directly ...
                return;
            }

            Application.Exit();  // Quit itself
        }

        /// <summary>
        /// Comprueba si la aplicación tiene el rol especificado
        /// </summary>
        /// <param name="role">Rol que se checkeara</param>
        /// <returns>Indica si está disponible el rol indicado</returns>
        public static bool CheckRole(WindowsBuiltInRole role)
        {
            WindowsPrincipal pricipal = new WindowsPrincipal(WindowsIdentity.GetCurrent());
            return pricipal.IsInRole(role);
        }

        /// <summary>
        /// Añade el icono de "Permisos de administrador al botón indicado
        /// </summary>
        /// <param name="button">Boton al que se le incrustará el icono</param>
        public static void AddShieldToButton(Button button)
        {
            button.FlatStyle = FlatStyle.System;
            SendMessage(button.Handle, BCM_SETSHIELD, 0, 0xFFFFFFFF);
        }

        /// <summary>
        /// Obtiene el identificador público de la aplicación
        /// </summary>
        /// <returns>Identificador público de la aplicación</returns>
        public static string GetApplicationID()
        {
            System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
            var attribute = (System.Runtime.InteropServices.GuidAttribute)asm.GetCustomAttributes(typeof(System.Runtime.InteropServices.GuidAttribute), true)[0];
            string id = attribute.Value;
            return id;
        }
    }
}
