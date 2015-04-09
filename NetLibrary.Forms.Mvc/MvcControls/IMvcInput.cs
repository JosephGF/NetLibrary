using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetLibrary.Forms.Mvc.MvcControls
{
    public interface IMvcInput : IMvcControl
    {
        bool Validate();
        string Value { get; set; }
    }
}
