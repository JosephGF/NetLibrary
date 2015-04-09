using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetLibrary
{ public static class ProgramManager
    {
        public static event EventHandler<CommandEventArgs> JumpListCommandReceived;
        public static event EventHandler<StartupEventArgs> StarupInstance;

        public static void Run(Type formType, string mainFormTitle)
        {
            WindowsMessageHelper.MainFormName = mainFormTitle;
            using (SingleProgramInstance spi = new SingleProgramInstance())
            {
                if (spi.IsSingleInstance)
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);

                    Form form = (Form)Activator.CreateInstance(formType);
                    //TaskBarJumpListForm mainForm = form as TaskBarJumpListForm;
                    //if (mainForm != null)
                    //{
                    //    var args = string.Join(" ", Environment.GetCommandLineArgs().Skip(1).ToArray());
                    //    mainForm.OnStartupInstance(new StartupEventArgs(true, args));
                    //}
                    var args = string.Join(" ", Environment.GetCommandLineArgs().Skip(1).ToArray());
                    if (StarupInstance != null)
                        StarupInstance(form, new StartupEventArgs(true, args));

                    Application.Run(form);
                }
                else
                {
                    // The program has already been started, so pass the arguments to it.
                    IntPtr handle = spi.RaiseOtherProcess();
                    HandleCommand(handle);
                }
            }
        }

        private static void HandleCommand(IntPtr handle)
        {
            var commandLineArgs = Environment.GetCommandLineArgs();
            if (commandLineArgs.Length > 1 && commandLineArgs[1].StartsWith(WindowsMessageHelper.COMMAND_PREFIX))
            {
                // It is a Jump List command.
                string temp = commandLineArgs[1].Split(':').LastOrDefault();
                int commandNumber;
                if (int.TryParse(temp, out commandNumber))
                    WindowsMessageHelper.SendMessage(handle, commandNumber);
            }
            else
            {
                var args = string.Join(" ", commandLineArgs.Skip(1).ToArray());
                WindowsMessageHelper.SendMessage(handle, args);
            }
        }
    }
}
