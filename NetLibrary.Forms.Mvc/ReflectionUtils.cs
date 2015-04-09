using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NetLibrary.Forms.Mvc
{
    internal class ReflectionUtils
    {
        internal static string getCallerMethodName()
        {
            StackTrace stackTrace = new System.Diagnostics.StackTrace();
            StackFrame frame = stackTrace.GetFrames()[3];
            MethodBase method = frame.GetMethod();
            string methodName = method.Name;
            return methodName;
        }

        internal static object createObject(string @namespace)
        {
            return invokeMethod(@namespace, null);
        }

        internal static object invokeMethod(string @namespace, string method)
        {
            Assembly assembly = System.Reflection.Assembly.GetEntryAssembly();
            Type controllerType = assembly.ExportedTypes.FirstOrDefault(t => t.FullName.EndsWith(@namespace));
            Object reference = Reflection.Manager.CreateInstance(controllerType);

            if (!string.IsNullOrEmpty(method))
            {
                return Reflection.Manager.CallMethod(reference, method);
            }

            return reference;
        }
    }
}
