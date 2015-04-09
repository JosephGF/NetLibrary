using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetLibrary.Extensions
{
    public static class EnumExtension
    {
        public static object Parse (this Enum that, int value) {
            return Enum.ToObject(that.GetType(), value);
        }
        public static object Parse(this Enum that, string value)
        {
            return Enum.Parse(that.GetType(), value, true);
        }
    }
} 
