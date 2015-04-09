using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NetLibrary.Forms;
using System.Reflection;
using System.Windows.Forms;

namespace NetLibrary.Forms.Mvc
{
    internal static class Context
    {
        internal enum ShowType { Default, Dialog }
        internal static FormMVC Current { get; private set; }
        internal static FormMVC Parent { get; private set; }

        static Context()
        {
            Context.Parent = new FormMVC();
            Context.Parent.Show();
            Context.Current = Context.Parent;
            NetLibrary.Forms.Animations.OnAnimationEnd += Animations_OnAnimationEnd;
        }

        static void Animations_OnAnimationEnd(object sender, EventArgs e)
        {
            Current.Controls.Remove((Control)sender);
        }

        internal static void Execute(string strAction, string strController, object data)
        {
            string nsPartial = Configuration.RouteControllers + "." + strController + "Controller";
            ActionResult actionResult = (ActionResult)ReflectionUtils.invokeMethod(nsPartial, strAction);
            View view = actionResult.GetView();
            Context.ShowView(view);
        }

        internal static void Execute(string strAction, string strController)
        {
            Execute(strAction, strController, new { });
        }

        internal static void ShowView (View view) {
            Context.ShowView(view, ShowType.Default);
        }

        internal static void ShowView(View view, ShowType show)
        {
            switch (show)
            {
                case ShowType.Dialog:
                    Context.Current = new FormMVC();
                    break; 
                default:
                    if (Context.Current.IsDisposed)
                    {
                        Context.Parent = new FormMVC();
                    }
                    Context.Current = Context.Parent;
                    view.Dock = DockStyle.Fill;
                    Context.Current.View = view;
                    break;
            }

            Context.Current.Controls.Add(view);
            Context.ClearViews(Context.Current, view);

            Context.Current.Show();
        }

        private static void ClearViews(FormMVC form, Control exclude)
        {
            foreach (Control c in form.Controls)
            {
                if (c is View && !c.Equals(exclude))
                    NetLibrary.Forms.Animations.Animate(c, Animations.Effect.Center, 250, 0);
            }
        }
    }
}
