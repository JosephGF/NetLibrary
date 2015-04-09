using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NetLibrary.Forms.Mvc
{
    public class ActionResult
    {
        internal string ActionName { get; set; }
        public string ViewName { get; internal set; }
        public string ContorllerName { get; internal set; }
        public object Model { get; set; }

        public ActionResult(string action, string controller, object model)
        {
            this.ViewName = this.ActionName = action;
            this.ContorllerName = controller;
            this.Model = model;
        }

        internal View GetView()
        {
            string ns = Configuration.RouteViews + "." + this.ContorllerName + "." + this.ViewName;
            View view = (View)ReflectionUtils.createObject(ns);
            view.ControllerName = this.ContorllerName;
            view.ActionName = this.ActionName;
            view.Model = this.Model;
            return view;
        }
    }
}
