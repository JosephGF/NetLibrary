using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NetLibrary.Forms
{
    public class AssemblyInfo
    {
        public string Name { get; set; }
        public string Version { get; set;}
        public string Description { get; set; }
        public string Author { get; set; }
        public string Copyright { get; set; }
        public Icon Icon { get; set;}

        public AssemblyInfo() { }

        public AssemblyInfo(Assembly assembly)
        {
            Initialize(assembly);
        }

        internal void Initialize(Assembly assembly)
        {
            this.Name = assembly.GetName().Name;
            this.Version = assembly.GetName().Version.ToString();

            AssemblyDescriptionAttribute descriptionAttribute = assembly.GetCustomAttributes<AssemblyDescriptionAttribute>().FirstOrDefault();
            this.Description = (descriptionAttribute != null) ? descriptionAttribute.Description : "";

            AssemblyCompanyAttribute companyAttribute = assembly.GetCustomAttributes<AssemblyCompanyAttribute>().FirstOrDefault();
            this.Author = (descriptionAttribute != null) ? companyAttribute.Company : "";

            AssemblyCopyrightAttribute copyrightAttribute = assembly.GetCustomAttributes<AssemblyCopyrightAttribute>().FirstOrDefault();
            this.Copyright = (copyrightAttribute != null) ? copyrightAttribute.Copyright : "";

            try
            {
                this.Icon = Icon.ExtractAssociatedIcon(assembly.Location);
            }
            catch (ArgumentException ex)
            {
                Debugger.Debug.WriteLine(ex);
            } 
        }
    }
}