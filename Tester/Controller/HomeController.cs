using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NetLibrary.Forms.Mvc;

namespace Tester.Controller
{
    public class HomeController : NetLibrary.Forms.Mvc.Controller
    {
        public NetLibrary.Forms.Mvc.ActionResult Index()
        {
            return View();
        }
        public ActionResult Manager()
        {
            return View();
        }
    }
}
