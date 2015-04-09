using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NetLibrary.Reflection
{
    public class Reflector<T>
    {
        public Type Type { get; private set; }
        public object Instance { get; private set; }

        public Reflector()
        {
            this.Type = typeof(T);
        }

        public Reflector(object instance)
        {
            this.Instance = instance;
            this.Type = instance.GetType();
        }

        public object this[string property]
        {
            get { return Manager.GetPropertyValue(this.Instance, property); }
            set { Manager.SetPropertyValue(this.Instance, property, value); }
        }

        public IList<CustomAttributeData> GetDataAnotations(string property)
        {
            return Manager.GetAttributes(this.Type, property);
        }

        public CustomAttributeData GetDataAnotation(Type tipo, string property)
        {
            return null;
            //return Manager.getatt  
        }
    }
}
