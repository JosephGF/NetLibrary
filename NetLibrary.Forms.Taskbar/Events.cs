using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetLibrary.Forms.Taskbar
{
    public class CommandEventArgs : EventArgs
    {
        public string CommandName { get; internal set; }

        public CommandEventArgs(string commandName)
        {
            CommandName = commandName;
        }
    }

    public class StartupEventArgs : EventArgs
    {
        public string CommandLineArgs { get; internal set; }
        public bool IsFirstInstance { get; internal set; }

        public StartupEventArgs(bool isFirstInstance, string commandLineArgs)
        {
            IsFirstInstance = isFirstInstance;
            CommandLineArgs = commandLineArgs;
        }
    }

    public class ButtonClickEventArgs : EventArgs
    {
        public Icon icon { get; set; }
        public string ToolTip { get; set; }

        public ButtonClickEventArgs(string tooltip, Icon icon)
        {
            this.icon = icon;
            this.ToolTip = tooltip;
        }
    }
}