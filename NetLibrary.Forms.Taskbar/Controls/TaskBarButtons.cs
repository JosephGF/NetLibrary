using Microsoft.WindowsAPICodePack.Taskbar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetLibrary.Forms.Taskbar.Controls
{
    [DesignerSerializer(typeof(TaskBarButtonsSerializer), typeof(CodeDomSerializer))]
    public partial class TaskBarButtons : Component
    {
        public event EventHandler<TaskBarButtonsClickedEventArgs> OnButtonClick;


        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public TaskBarButton[] TaskButtons { get; set; }

        public TaskBarButton this[string key]
        {
            get
            {
                if (TaskButtons != null)
                {
                    for (int x = 0; x < this.TaskButtons.Length; x++)
                    {
                        if (key == this.TaskButtons[x].Name)
                            return this.TaskButtons[x];
                    }
                }

                return null;
            }
        }

        public TaskBarButton this[int index]
        {
            get
            {
                return this.TaskButtons[index];
            }
        }

        public TaskBarButtons()
        {
            InitializeComponent();
        }

        public TaskBarButtons(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
        }

        public void AddToWindow(IntPtr windowHandle)
        {
            if (this.TaskButtons == null) return;

            List<ThumbnailToolBarButton> buttons = new List<ThumbnailToolBarButton>();
            foreach (TaskBarButton btn in this.TaskButtons)
            {
                btn.Click += btn_Click;
                buttons.Add(btn.GetButton());
            }

            TaskbarManager.Instance.ThumbnailToolBars.AddButtons(windowHandle, buttons.ToArray());
        }

        private void btn_Click(object sender, TaskBarButtonsClickedEventArgs e)
        {
            if (this.OnButtonClick != null)
                this.OnButtonClick(sender, e);
        }
    }
}
