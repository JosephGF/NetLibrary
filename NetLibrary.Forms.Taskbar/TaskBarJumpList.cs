using Microsoft.WindowsAPICodePack.Taskbar;
using System;
using System.Collections.Generic;
using System.Reflection;
using NetLibrary;

namespace NetLibrary.App
{
    public class TaskBarJumpList
    {
        private static JumpList list;
        private static Dictionary<string, JumpListCustomCategory> categories;

        public static bool AutoRefresh { get; set; }
        private static IntPtr _WindowsId;
        public static IntPtr WindowsId
        {
            get { return _WindowsId; }
            set
            {
                _WindowsId = value;
                list = JumpList.CreateJumpListForIndividualWindow(TaskbarManager.Instance.ApplicationId, WindowsId);
            }
        }
        static TaskBarJumpList()
        {
            if (WindowsId.ToInt32() == 0)
                list = JumpList.CreateJumpList();
            else
                list = JumpList.CreateJumpListForIndividualWindow(TaskbarManager.Instance.ApplicationId, WindowsId);

            list.ClearAllUserTasks();
            list.Refresh();
            categories = new Dictionary<string, JumpListCustomCategory>();
        }

        #region AddUserTask overloads

        public static void AddTaskLink(string title, string path)
        {
            AddTaskLink(title, path, null, null, 0);
        }

        public static void AddTaskLink(string title, string path, string arguments, string iconPath)
        {
            AddTaskLink(title, path, arguments, iconPath, 0);
        }

        public static void AddTaskLink(string title, string path, string iconPath, int iconNumber)
        {
            AddTaskLink(title, path, null, iconPath, iconNumber);
        }

        public static void AddTaskLink(string title, string path, string iconPath)
        {
            AddTaskLink(title, path, null, iconPath, 0);
        }

        public static void AddTaskLink(string title, string path, string arguments, string iconPath, int iconNumber)
        {
            var icon = CreateIconReference(iconPath, iconNumber);
            AddTaskLink(title, path, arguments, icon);
        }

        private static void AddTaskLink(string title, string path, string arguments, Microsoft.WindowsAPICodePack.Shell.IconReference? icon)
        {
            var task = CreateJumpListLink(title, path, arguments, icon);
            list.AddUserTasks(task);

            if (AutoRefresh)
                list.Refresh();
        }

        #endregion

        #region Add Custom Links
        public static void RemoveJumList()
        {
            list.ClearAllUserTasks();
            list.Refresh();
        }

        public static void AddCategoryLink(string categoryName, string title, string path)
        {
            AddCategoryLink(categoryName, title, path, null, null, 0);
        }

        public static void AddCategoryLink(string categoryName, string title, string path, string arguments, string iconPath)
        {
            AddCategoryLink(categoryName, title, path, arguments, iconPath, 0);
        }

        public static void AddCategoryLink(string categoryName, string title, string path, string iconPath, int iconNumber)
        {
            AddCategoryLink(categoryName, title, path, null, iconPath, iconNumber);
        }

        public static void AddCategoryLink(string categoryName, string title, string path, string iconPath)
        {
            AddCategoryLink(categoryName, title, path, null, iconPath, 0);
        }

        public static void AddCategoryLink(string categoryName, string title, string path, string arguments, string iconPath, int iconNumber)
        {
            var icon = CreateIconReference(iconPath, iconNumber);
            AddCategoryLink(categoryName, title, path, arguments, icon);
        }

        private static void AddCategoryLink(string categoryName, string title, string path, string arguments, Microsoft.WindowsAPICodePack.Shell.IconReference? icon)
        {
            var task = CreateJumpListLink(title, path, arguments, icon);
            JumpListCustomCategory category;
            if (!categories.TryGetValue(categoryName, out category))
            {
                category = new JumpListCustomCategory(categoryName);
                categories[categoryName] = category;
                list.AddCustomCategories(category);
            }

            category.AddJumpListItems(task);

            if (AutoRefresh)
                list.Refresh();
        }

        #endregion

        #region Add Category Self Links

        public static void AddCategorySelfLink(string categoryName, string title, string commandName)
        {
            AddCategorySelfLink(categoryName, title, commandName, null, 0);
        }

        public static void AddCategorySelfLink(string categoryName, string title, string commandName, string iconPath)
        {
            AddCategorySelfLink(categoryName, title, commandName, iconPath, 0);
        }

        public static void AddCategorySelfLink(string categoryName, string title, string commandName, string iconPath, int iconNumber)
        {
            if (string.IsNullOrWhiteSpace(commandName))
                throw new ArgumentNullException("commandName");

            var icon = CreateIconReference(iconPath, iconNumber);

            // Register the command and get the associated number.
            int command = WindowsMessageHelper.RegisterCommand(commandName);
            AddCategoryLink(categoryName, title, Assembly.GetEntryAssembly().Location, WindowsMessageHelper.COMMAND_PREFIX + command.ToString(), icon);
        }

        #endregion

        #region Add Task Self Links

        public static void AddTaskSelfLink(string title, string commandName)
        {
            AddTaskSelfLink(title, commandName, null, 0);
        }

        public static void AddTaskSelfLink(string title, string commandName, string iconPath)
        {
            AddTaskSelfLink(title, commandName, iconPath, 0);
        }

        public static void AddTaskSelfLink(string title, string commandName, string iconPath, int iconNumber)
        {
            if (string.IsNullOrWhiteSpace(commandName))
                throw new ArgumentNullException("commandName");

            var icon = CreateIconReference(iconPath, iconNumber);

            // Register the command and get the associated number.
            int command = WindowsMessageHelper.RegisterCommand(commandName);
            AddTaskLink(title, Assembly.GetEntryAssembly().Location, WindowsMessageHelper.COMMAND_PREFIX + command.ToString(), icon);
        }

        #endregion

        public static void AddTaskSeparator()
        {
            list.AddUserTasks(new JumpListSeparator());
        }

        private static JumpListLink CreateJumpListLink(string title, string path, string arguments, Microsoft.WindowsAPICodePack.Shell.IconReference? icon)
        {
            var task = new JumpListLink(path, title);
            if (icon.HasValue)
                task.IconReference = icon.Value;
            task.Arguments = arguments;

            return task;
        }

        private static Microsoft.WindowsAPICodePack.Shell.IconReference? CreateIconReference(string iconPath, int iconNumber)
        {
            Microsoft.WindowsAPICodePack.Shell.IconReference? icon = null;
            if (!string.IsNullOrEmpty(iconPath))
                icon = new Microsoft.WindowsAPICodePack.Shell.IconReference(iconPath, iconNumber);

            return icon;
        }

        public static void Refresh()
        {
            list.Refresh();
        }

        public static void AddToRecent(string path)
        {
            JumpList.AddToRecent(path);
        }

        public static bool ShowRecentFiles
        {
            get
            {
                if (list.KnownCategoryToDisplay == JumpListKnownCategoryType.Recent)
                    return true;

                return false;
            }
            set
            {
                if (value)
                    list.KnownCategoryToDisplay = JumpListKnownCategoryType.Recent;
                else
                    list.KnownCategoryToDisplay = JumpListKnownCategoryType.Neither;
            }
        }

        public static bool ShowFrequentFiles
        {
            get
            {
                if (list.KnownCategoryToDisplay == JumpListKnownCategoryType.Frequent)
                    return true;

                return false;
            }
            set
            {
                if (value)
                    list.KnownCategoryToDisplay = JumpListKnownCategoryType.Frequent;
                else
                    list.KnownCategoryToDisplay = JumpListKnownCategoryType.Neither;
            }
        }
    }
}
