using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetLibrary.Reflection;

namespace NetLibrary.Extensions
{
    public static class ReflectionExtension
    {
        public static T GetAttributeFrom<T>(this object instance, string propertyName) where T : Attribute
        {
            return Manager.GetAttribute<T>(instance.GetType(), propertyName);
        }
        public static T CallMethod<T>(this object instance, string name)
        {
            return (T)Manager.CallMethod(instance, name);
        }
        public static T CallMethod<T>(this object instance, string name, object[] args)
        {
            return (T)Manager.CallMethod(instance, name, args);
        }
        public static object CallMethod(this object instance, string name)
        {
            return Manager.CallMethod(instance, name);
        }
        public static object CallMethod(this object instance, string name, object[] args)
        {
            return Manager.CallMethod(instance, name, args);
        }
        public static T GetProperty<T>(this object instance, string propertyName)
        {
            return Manager.GetPropertyValue<T>(instance, propertyName);
        }
        public static void SetProperty(this object instance, string propertyName, object value)
        {
            Manager.SetPropertyValue(instance, propertyName, value);
        }

        /// <summary>
        /// Check if current object implemments, inherent or is specifict type
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="implementsType"></param>
        /// <returns></returns>
        public static bool Implemets(this object instance, Type implementsType)
        {
            Type objType = instance.GetType();
            return implementsType.IsAssignableFrom(objType) || objType.IsSubclassOf(implementsType) || implementsType.IsInstanceOfType(instance);

        }

        public static bool IsNumeric(this object instance)
        {
            switch (Type.GetTypeCode(instance.GetType()))
            {
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                    return true;
                default:
                    return false;
            }
        }
    }
}
