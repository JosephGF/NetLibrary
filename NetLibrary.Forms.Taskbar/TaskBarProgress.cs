using Microsoft.WindowsAPICodePack.Shell;
using Microsoft.WindowsAPICodePack;
using Microsoft.WindowsAPICodePack.Taskbar;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetLibrary.Forms.Taskbar
{
    public class TaskBarProgress
    {
        public enum TaskBarState
        {
            NoProgress = 0,
            Indeterminate = 1,
            Normal = 2,
            Error = 4,
            Paused = 8,
        }

        public static bool IsAviable { get { return TaskbarManager.IsPlatformSupported; } }
        public static TaskBarState State { set { TaskbarManager.Instance.SetProgressState(ConvertState(value)); } }
        public static ProgressBarStyle Style { set { TaskbarManager.Instance.SetProgressState(ConvertState(value)); } }
        public static Icon Icon { set { TaskbarManager.Instance.SetOverlayIcon(value, TaskbarManager.Instance.ApplicationId); } }

        public static void SetPercentaje(int percentaje)
        {
            TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.Normal);
            TaskbarManager.Instance.SetProgressValue(percentaje, 100);
        }

        public static void SetValue(int value, int maximun)
        {
            TaskbarManager.Instance.SetProgressValue(value, maximun);
        }

        internal static TaskbarProgressBarState ConvertState(TaskBarState state)
        {
            TaskbarProgressBarState result = (TaskbarProgressBarState)state;
            return result;
        }

        internal static TaskbarProgressBarState ConvertState(ProgressBarStyle state)
        {
            TaskbarProgressBarState result = TaskbarProgressBarState.NoProgress;
            switch (state)
            {
                case ProgressBarStyle.Blocks:
                case ProgressBarStyle.Continuous:
                    result = TaskbarProgressBarState.Normal;
                    break;
                case ProgressBarStyle.Marquee:
                    result = TaskbarProgressBarState.Indeterminate;
                    break;
            }

            return result;
        }
    }
}
