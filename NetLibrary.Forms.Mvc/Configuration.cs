using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetLibrary.Forms.Mvc
{
    public static class Configuration
    {
        public delegate void ConfigEvent(EventArgsConfiguration e);

        internal static string RouteControllers {get; set;}
        internal static string RouteViews {get; set;}
        public static event EventHandler<EventArgsConfiguration> OnConfigLoading;

        public static void initialize()
        {
            Context.Execute("Index", "Home");
        }
        public static void initialize(string action, string controller)
        {
            Context.Execute(action, controller);
        }

        static Configuration()
        {
            EventArgsConfiguration e = new EventArgsConfiguration();
            e.RouteControllers = "Controller";
            e.RouteViews = "View";
            if (OnConfigLoading != null)
                OnConfigLoading(null, e);
            Configuration.RouteControllers = e.RouteControllers;
            Configuration.RouteViews = e.RouteViews;

        }
    }

    public class EventArgsConfiguration : EventArgs
    {
        public string RouteControllers { get; set; }
        public string RouteViews { get; set; }
    }
}
