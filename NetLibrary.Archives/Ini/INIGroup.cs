using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetLibrary.Archives.INI
{
    public class INIGroup
    {
        public string Key { get; set; }
        public List<INIValues> Values { get; set; }

        public INIValues this[string key] 
        {
            get {
                return Values.FirstOrDefault(v => v.Key.Equals(key));
            }
            set
            {
                INIValues val = Values.FirstOrDefault(v => v.Key.Equals(key));
                if (val == null)
                {
                    val.Key = key;
                    val.Value = value.Value;
                }
                else
                {
                    val.Value = value.Value;
                }
            }
        }
    }
}
