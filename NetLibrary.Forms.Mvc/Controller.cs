using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetLibrary.Forms.Mvc
{
    public class Controller
    {
        protected dynamic ViewBag { get; set; }
        protected dynamic VieData { get; set; }
        public Dictionary<string, string> ModelState { get { return null; } }

        protected string ControllerName { get; set; }

        protected ActionResult View()
        {
            return View(null, null,null);
        }

        protected ActionResult View(object model)
        {
            return View(null, null, model);
        }

        protected ActionResult View(string action)
        {
            return View(action, null, null);
        }

        protected ActionResult View(string action, object model)
        {
            return View(action, null, model);
        }

        protected ActionResult View(string action, string controller, object model)
        {
            controller = controller ?? this.GetType().Name.Replace("Controller", "");
            action = action ?? ReflectionUtils.getCallerMethodName();
            return new ActionResult(action, controller, model);
        }
    }
}
