using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetLibrary.Forms.Mvc
{
    public partial class View : UserControl, IView
    {
        public bool IsPartial { get; internal set; }
        public Object Model { get; internal set; }

        internal string ActionName { get; set; }
        internal string ControllerName { get; set; }
        public View()
        {
            InitializeComponent();
            Initialize();
        }

        public View(Object model) : this()
        {
            this.Model = model;
        }

        /// <summary>
        /// Se ejecuta al crear la clase
        /// </summary>
        protected virtual void Initialize() { }

        public void Action(string action)
        {
            Context.Execute(action, this.ControllerName);
        }
        public void Action(string action, string controller)
        {
            Context.Execute(action, controller);
        }
        public void Action(string action, string controller, object data)
        {
            Context.Execute(action, controller, data);
        }

        new protected virtual bool Validate()
        {
            base.Validate();
            return true;
        }

        private void initialize()
        {
            foreach (Control control in this.Controls)
            {
                if (control is IMvcControl)
                {
                    IMvcControl mvcControl = control as IMvcControl;
                    //mvcControl.Value = null;
                    //mvcControl.OnCreate();
                }
            }
        }
    }
}
