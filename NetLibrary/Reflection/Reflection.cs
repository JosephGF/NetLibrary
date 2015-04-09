using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetLibrary.Reflection
{
    public class Manager
    {
        internal delegate bool InvokeControl(object obj, string pName, object value);

        /// <summary>
        /// Crea una nueva istancia del tipo especificado
        /// </summary>
        /// <param name="type">Tipo del objeto</param>
        /// <returns>Objeto que representa la nueva instancia</returns>
        public static object CreateInstance(Type type)
        {
            return Activator.CreateInstance(type);
        }

        /// <summary>
        /// Crea una nueva istancia del tipo especificado
        /// </summary>
        /// <typeparam name="T">Tipo del objeto</typeparam>
        /// <returns>Objeto que representa la nueva instancia</returns>
        public static T CreateInstance<T>() where T : class
        {
            return Manager.CreateInstance(typeof(T)) as T;
        }

        /// <summary>
        /// Realiza una llamada a invoke necesaria para cambiar el valor de una propiedad desde un thread esterno
        /// </summary>
        /// <param name="control">Control sobre el que se cambiará la propiedad</param>
        /// <param name="pName">Nombre de la propiedad que se desea actualizar</param>
        /// <param name="value">Nuevo valor de la propiedad</param>
        public static void Invoke(Control control, string pName, object value)
        {
            control.Invoke((InvokeControl)SetPropertyValue, new object[] { control, pName, value });
        }

        /// <summary>
        /// Actualiza el valor de la propiedad especificada, si no es posible No Lanza Excepción
        /// </summary>
        /// <param name="obj">Objeto sobre el que se trabajará</param>
        /// <param name="pName">Nombre de la propiedad que se desea actualizar</param>
        /// <param name="value">Nuevo valor de la propiedad</param>
        /// <returns>Devuelve un booleano indicando si se ha podido actualizar el valor</returns>
        public static bool SetPropertyValue(object obj, string pName, object value)
        {
            /*try
            {
                PropertyInfo propertyInfo = obj.GetType().GetProperty(pName);
                propertyInfo.SetValue(obj, Convert.ChangeType(value, propertyInfo.PropertyType, null));

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }*/
            return SetPropertyValue(obj, pName, value, null);
        }

        /// <summary>
        /// Actualiza el valor de la propiedad especificada, si no es posible No Lanza Excepción
        /// </summary>
        /// <param name="obj">Objeto sobre el que se trabajará</param>
        /// <param name="pName">Nombre de la propiedad que se desea actualizar</param>
        /// <param name="value">Nuevo valor de la propiedad</param>
        /// <param name="index">Valores de índice para los valores indizados</param>
        /// <returns>Devuelve un booleano indicando si se ha podido actualizar el valor</returns>
        public static bool SetPropertyValue(object obj, string pName, object value, object[] index)
        {
            try
            {
                PropertyInfo property = obj.GetType().GetProperty(pName);
                Type valueType = property.PropertyType;
                if (valueType.GenericTypeArguments.Length > 0)
                    valueType = valueType.GenericTypeArguments[0];


                if (valueType.IsArray)
                {
                    Type elementType = valueType.GetElementType();
                    string[] valueArray = value.ToString().Split(",".ToCharArray());
                    Array valueArrayAux = Array.CreateInstance(elementType, valueArray.Length);
                    for (int idx = 0; idx < valueArray.Length; idx++)
                    {
                        object val = null;
                        try
                        {
                            val = Convert.ChangeType(valueArray[idx], elementType);
                        }
                        catch (Exception ex)
                        {
                            //Debugger.Log(0, "Error ", ex.Message);
                        }

                        if (val != null)
                        {
                            valueArrayAux.SetValue(val, idx);
                        }
                    }

                    value = valueArrayAux;
                }
                else
                {
                    if (value != null)
                    {
                        string strValue = value.ToString();
                        switch (valueType.Name.ToUpper())
                        {
                            case "TIMESPAN":
                                value = TimeSpan.Parse(strValue);
                                break;
                            case "DATETIME":
                                value = DateTime.Parse(strValue);
                                break;
                            case "DECIMAL":
                            case "DOUBLE":
                                strValue = strValue.Replace(".", ",");
                                value = Convert.ChangeType(strValue, valueType);
                                break;
                            default:
                                value = Convert.ChangeType(strValue, valueType);
                                break;
                        }
                    }
                }

                property.SetValue(obj, value, index);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Obtiene el valor de la propiedad especificada
        /// </summary>
        /// <typeparam name="TValue">Tipo de propiedad esperada</typeparam>
        /// <param name="obj">Objeto sobre el que se trabajará</param>
        /// <param name="pName">Nombre de la propiedad que se desea actualizar</param>
        /// <returns>Valor obtenido</returns>
        public static TValue GetPropertyValue<TValue>(object obj, string pName)
        {
            return (TValue)Manager.GetPropertyValue(obj, pName);
        }

        /// <summary>
        /// Obtiene el valor de la propiedad especificada
        /// </summary>
        /// <param name="obj">Objeto sobre el que se trabajará</param>
        /// <param name="pName">Nombre de la propiedad que se desea actualizar</param>
        /// <returns>Valor obtenido</returns>
        public static object GetPropertyValue(object obj, string pName)
        {
            return Manager.GetPropertyValue(obj, pName, null);
        }

        /// <summary>
        /// Obtiene el valor de la propiedad especificada
        /// </summary>
        /// <param name="obj">Objeto sobre el que se trabajará</param>
        /// <param name="pName">Nombre de la propiedad que se desea actualizar</param>
        /// <param name="index">Valores de índice para los valores indizados</param>
        /// <returns>Valor obtenido</returns>
        public static object GetPropertyValue(object obj, string pName, object[] index)
        {
            object value = (string)obj.GetType().GetProperty(pName).GetValue(obj, index);
            return value;
        }

        public static IDictionary<string, object> GetProperties(object obj)
        {
            Dictionary<string, object> results = new Dictionary<string, object>();

            string[] propertyNames = obj.GetType().GetProperties().Select(p => p.Name).ToArray();

            foreach (var name in propertyNames)
            {
                object value = obj.GetType().GetProperty(name).GetValue(obj, null);
                results.Add(name, value);
            }

            return results;
        }

        /// <summary>
        /// Ejecuta el método especificado de un objeto y devuelve el valor resultado
        /// </summary>
        /// <param name="obj">Objeto sobre el que se trabajará</param>
        /// <param name="mName">Nombre del metodo que se ejecutará</param>
        /// <param name="args">Parametros del método</param>
        /// <returns>Objeto devuelto por el método</returns>
        public static object CallMethod(object obj, string mName, object[] args)
        {
            List<Type> types = new List<Type>();
            foreach(object ob in args)
                types.Add(ob.GetType());

            MethodInfo mInfo = obj.GetType().GetMethod(mName, types.ToArray());
            object result = mInfo.Invoke(obj, args);
            return result;
        }

        /// <summary>
        /// Ejecuta el método especificado de un objeto y devuelve el valor resultado
        /// </summary>
        /// <param name="obj">Objeto sobre el que se trabajará</param>
        /// <param name="mName">Nombre del metodo que se ejecutará</param>
        /// <returns>Objeto devuelto por el método</returns>
        public static object CallMethod(object obj, string mName)
        {
            return CallMethod(obj, mName, new object[0]);
        }

        /// <summary>
        /// Ejecuta el método estático especificado devuelve el valor resultado
        /// </summary>
        /// <param name="type">Tipo donde se puede encontrar el método</param>
        /// <param name="mName">Nombre del metodo que se ejecutará</param>
        /// <returns>Objeto devuelto por el método</returns>
        public static object CallMethod(Type type, string mName)
        {
            MethodInfo mInfo = type.GetMethod(mName);
            object result = mInfo.Invoke(null, null);
            return result;
        }

        /// <summary>
        /// Obtiene todos los métodos públicos para el tipo especificado
        /// </summary>
        /// <param name="type">Tipo sobre el que se realizará la busqueda</param>
        /// <returns>Métodos públicos encontrados</returns>
        public static MethodInfo[] GetAllMethods(Type type)
        {
            // Display information for all methods. 
            MethodInfo[] myArrayMethodInfo = type.GetMethods();
            return myArrayMethodInfo;
        }

        /// <summary>
        /// Obtiene todas las propiedades públicas para el tipo especificado
        /// </summary>
        /// <param name="type">Tipo sobre el que se realizará la busqueda</param>
        /// <returns>Propiedades públicas encontradas</returns>
        public static PropertyInfo[] GetAllProperties(Type type)
        {
            // Display information for all methods. 
            PropertyInfo[] myArrayPropertyInfo = type.GetProperties();
            return myArrayPropertyInfo;
        }

        /// <summary>
        /// Devuelve el PropertyInfo según el tipo y nombre de la propiedad especificadas
        /// </summary>
        /// <param name="type">Tipo sobre el que se buscará la propiedad</param>
        /// <param name="name">Nombre de la propiedad</param>
        /// <returns>Objeto que representa la propiedad</returns>
        public static PropertyInfo GetPropertyFromAttribute(Type type, string name)
        {
            // Display information for all methods. 
            PropertyInfo propertyInfo = type.GetProperty(name);
            return propertyInfo;
        }

        /// <summary>
        /// Devuelve un objeto que respresenta los atributos personalizados según la propiedad especificada
        /// </summary>
        /// <param name="propertyInfo">Propiedad sobre la que se buscaránlos atributos</param>
        /// <param name="attributeType">Tipo de atributo personalizado que se buscará</param>
        /// <param name="inherint">Busca también en los elementos heredados</param>
        /// <returns>Array que representa cada uno de los atributos personalizados</returns>
        public static object[] GetCustomAttributes(PropertyInfo propertyInfo, Type attributeType, bool inherint)
        {
            object[] result = propertyInfo.GetCustomAttributes(attributeType, inherint);
            return result;
        }

        /// <summary>
        /// Devuelve una lista que respresenta los atributos personalizados
        /// </summary>
        /// <param name="propertyInfo">Propiedad sobre la que se buscaránlos atributos</param>
        /// <returns>Devuelve una lista con los atributos encontrados para esa propiedad</returns>
        public static IList<CustomAttributeData> GetAttributes(PropertyInfo propertyInfo)
        {
            return propertyInfo.GetCustomAttributesData();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objType"></param>
        /// <param name="pName"></param>
        /// <returns></returns>
        public static IList<CustomAttributeData> GetAttributes(Type objType, string pName)
        {
            return GetAttributes(objType.GetProperty(pName));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyInfo"></param>
        /// <param name="attrType"></param>
        /// <param name="inherent"></param>
        /// <returns></returns>
        public static Attribute GetAttribute(MemberInfo propertyInfo, Type attrType, bool inherent)
        {
            return propertyInfo.GetCustomAttribute(attrType, inherent);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyInfo"></param>
        /// <param name="attrType"></param>
        /// <param name="inherent"></param>
        /// <returns></returns>
        public static Attribute GetAttribute(MemberInfo propertyInfo, Type attrType)
        {
            return GetAttribute(propertyInfo, attrType, true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objType"></param>
        /// <param name="attrType"></param>
        /// <returns></returns>
        public static Attribute GetAttribute(Type objType,  Type attrType)
        {
            return GetAttribute(objType as MemberInfo, attrType);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objType"></param>
        /// <param name="pName"></param>
        /// <param name="attrType"></param>
        /// <returns></returns>
        public static Attribute GetAttribute(Type objType,string pName, Type attrType)
        {
            PropertyInfo propertyInfo = objType.GetProperty(pName);
            return GetAttribute(propertyInfo, attrType);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objType"></param>
        /// <param name="propertyName"></param>
        /// <param name="attrType"></param>
        /// <returns></returns>
        public static Attribute GetAttribute(Type objType, string propertyName, Type attrType, bool inherent)
        {
            MemberInfo propertyInfo = objType.GetProperty(propertyName);
            return GetAttribute(propertyInfo, attrType, inherent);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TAttribute"></typeparam>
        /// <param name="objType"></param>
        /// <returns></returns>
        public static TAttribute GetAttribute<TAttribute>(Type objType) where TAttribute : Attribute
        {
            return GetAttribute(objType as MemberInfo, typeof(TAttribute)) as TAttribute;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TAttribute"></typeparam>
        /// <param name="objType"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static TAttribute GetAttribute<TAttribute>(Type objType, string propertyName) where TAttribute : Attribute
        {
            return GetAttribute(objType, propertyName, typeof(TAttribute), true) as TAttribute;
        } 

        /// <summary>
        /// Carga el ensamblado especificado
        /// </summary>
        /// <param name="dllPath">Ruta de la dll</param>
        /// <param name="dllName">Nombre de la dll</param>
        /// <returns>Objeto que representa el ensamblado</returns>
        public static Assembly LoadAssambly(string dllPath, string dllName)
        {
            Assembly a = Assembly.LoadFrom(Path.Combine(dllPath, dllName));
            return a;
        }
    }
}
