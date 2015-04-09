using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetLibrary.Forms.Mvc
{
    public class Form<T>
    {
        protected class Input<TProperty>
        {
            public TProperty MaxValue { get; set; }
            public TProperty MinValue { get; set; }
            public String Name { get; set; }
            public String Descripcion { get; set; }
            public String Required { get; set; }
            public Boolean IsValid { get { return true; } }
        }

        public Control ControlFrom(Expression<Func<T, Object>> property) 
        {
            return null;
        }
    }
}
