using System;
using System.Windows.Forms;
using System.Drawing;

using NetLibrary.Forms.Taskbar;
using System.ComponentModel;

namespace NetLibrary.Forms.Taskbar.Controls
{
    public class TaskBarProgressBar : Component, IExtenderProvider
    {
        private ProgressBar _parent;

        public ProgressBar Parent
        {
            get { return this._parent; }
            set { 
                _parent = value;
                _parent.StyleChanged += _parent_StyleChanged;
//                UpdateTaskBarProgress();
            }
        }

        void _parent_StyleChanged(object sender, EventArgs e)
        {
            ChangeTaskBarState();
        }

        [DefaultValue(true)]
        public bool VisibleInTaskBar { get; set; }

        public bool Error { get; set; }
        public bool Paused { get; set; }

        public ProgressBarStyle Style
        {
            get { return this._parent.Style; }
            set { this._parent.Style = value; }
        }

        /// <summary>
        ///
        /// Resumen:
        ///     Obtiene o establece la posición actual de la barra de progreso.
        ///
        /// Devuelve:
        ///     Posición del intervalo de la barra de progreso. El valor predeterminado es
        ///     0.
        ///
        /// Excepciones:
        ///   System.ArgumentException:
        ///     El valor especificado es mayor que el valor de la propiedad System.Windows.Forms.ProgressBar.Maximum.
        ///     O bien El valor especificado es menor que el valor de la propiedad System.Windows.Forms.ProgressBar.Minimum.
        /// </summary>
        public int Value
        {
            get { return _parent.Value; }
            set { _parent.Value = value; UpdateTaskBarProgress(); }
        }
        
        /// <summary>
        ///
        /// Resumen:
        ///     Obtiene o establece el valor máximo del intervalo del control.
        ///
        /// Devuelve:
        ///     Valor máximo del intervalo. El valor predeterminado es 100.
        ///
        /// Excepciones:
        ///   System.ArgumentException:
        ///     El valor especificado es menor que 0.
        /// </summary>
        public int Maximum
        {
            get { return _parent.Maximum; }
            set { _parent.Maximum = value; UpdateTaskBarProgress(); }
        }

        /// <summary>
        /// Devuelve el porcentaje actual de la barra de progreso
        /// </summary>
        public decimal Percent
        {
            get { return Decimal.Divide(this.Maximum, this.Value) * 100; }
        }

        public TaskBarProgressBar()
        {
            this._parent = new ProgressBar();
        }

        public TaskBarProgressBar(ProgressBar progressBar)
        {
            this._parent = progressBar;
        }

        private void IUProgressBar_StyleChanged(object sender, EventArgs e)
        {
            ChangeTaskBarState();
        }

        private void UpdateTaskBarProgress()
        {
            if (this.VisibleInTaskBar)
                TaskBarProgress.SetValue(this.Value, this.Maximum);
            else
                TaskBarProgress.State = TaskBarProgress.TaskBarState.NoProgress;
        }

        private void ChangeTaskBarState()
        {
            if (this.VisibleInTaskBar)
                TaskBarProgress.State = TaskBarState();
            else
                TaskBarProgress.State = TaskBarProgress.TaskBarState.NoProgress;
        }

        private TaskBarProgress.TaskBarState TaskBarState()
        {
            TaskBarProgress.TaskBarState result;
            if (!this.VisibleInTaskBar)
            {
                result = TaskBarProgress.TaskBarState.NoProgress;
            }
            else
            {
                switch (this._parent.Style)
                {
                    case ProgressBarStyle.Blocks:
                    case ProgressBarStyle.Continuous:
                        result = TaskBarProgress.TaskBarState.Normal;
                        break;
                    case ProgressBarStyle.Marquee:
                        result = TaskBarProgress.TaskBarState.Indeterminate;
                        break;
                    default:
                        result = TaskBarProgress.TaskBarState.NoProgress;
                        break;
                }

                if (this.Paused)
                    result = TaskBarProgress.TaskBarState.Paused;

                if (this.Error)
                    result = TaskBarProgress.TaskBarState.Error;
            }

            return result;
        }

        public bool CanExtend(object extendee)
        {
            return (extendee is ProgressBar);
        }
    }
}
