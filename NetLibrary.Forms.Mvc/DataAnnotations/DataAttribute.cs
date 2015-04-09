using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetLibrary.Reflection;
using System.Reflection;
using NetLibrary.Forms.Mvc;

namespace NetLibrary.Forms.MVC.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Property, Inherited=true)]
    public abstract class ValidationAttribute : Attribute
    {
        public string ErrorMessage { get; set; }
        public abstract bool IsValid(Object value, PropertyInfo property);
    }

    public class RequireAttribute : ValidationAttribute 
    {
        public bool AllowEmptyStrings {get; set;}

        public RequireAttribute()
        {
            this.ErrorMessage = "{0} no puede esta vacío";
        }

        public override bool IsValid(Object value, PropertyInfo property)
        {
            return (value != null);
        }
    }
    public class MaxLengthAttribute : ValidationAttribute 
    {
        public int Length { get; private set; }
        public MaxLengthAttribute(int length)
        {
            this.ErrorMessage = "{0} no puede ser mayor de " + this.Length;
            this.Length = length;
        }

        public override bool IsValid(Object value, PropertyInfo property)
        {
            return (value.ToString().Length <= this.Length);
        }
    }
    public class MinLengthAttribute : ValidationAttribute
    {
        public int Length { get; private set; }
        public MinLengthAttribute(int length)
        {
            this.ErrorMessage = "{0} no puede ser menor de " + this.Length;
            this.Length = length; 
        }

        public override bool IsValid(Object value, PropertyInfo property)
        {
            return (value.ToString().Length >= this.Length);
        }
    }
    public class RegularExpressionAttribute : ValidationAttribute
    {
        public string Pattern { get; private set; }
        public RegularExpressionAttribute(string pattern)
        {
            this.ErrorMessage = "{0} no tiene un formato válido";
            this.Pattern = pattern;
        }

        public override bool IsValid(Object value, PropertyInfo property)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(value.ToString(), this.Pattern, System.Text.RegularExpressions.RegexOptions.CultureInvariant);
        }
    }
    public class RangeAttribute: ValidationAttribute
    {
        public object Maximum { get; private set; }
        public object Minimum { get; private set; }
        public Type Type { get; private set; }

        public RangeAttribute(Type type, string maximun, string minimum)
        {
            this.Maximum = maximun;
            this.Minimum = minimum;
            this.Type = type;
            this.ErrorMessage = "{0} debe estar entre los valores " + this.Minimum + " y " + this.Maximum;
        }
        public RangeAttribute(string maximun, string minimum)
        {
            this.Maximum = maximun;
            this.Minimum = minimum;
            this.Type = typeof(string);
            this.ErrorMessage = "{0} debe estar entre los valores " + this.Minimum + " y " + this.Maximum;
        }
        public RangeAttribute(int maximun, int minimum)
        {
            this.Maximum = maximun;
            this.Minimum = minimum;
            this.Type = typeof(int);
            this.ErrorMessage = "{0} debe estar entre los valores " + this.Minimum + " y " + this.Maximum;
        }
        public RangeAttribute(double maximun, double minimum)
        {
            this.Maximum = maximun;
            this.Minimum = minimum;
            this.Type = typeof(double);
            this.ErrorMessage = "{0} debe estar entre los valores " + this.Minimum + " y " + this.Maximum;
        }

        public override bool IsValid(Object value, PropertyInfo property)
        {
            return (value.ToString().CompareTo(this.Minimum.ToString()) > -1 && value.ToString().CompareTo(this.Maximum.ToString()) < 1);
        }
    }
    public class UniqueAttribute : ValidationAttribute 
    {
        public override bool IsValid(Object value, PropertyInfo property)
        {
            return true;
        }
    }
    public class CustomValidationAttribute: ValidationAttribute
    {
        public string Method { get; private set; }
        public CustomValidationAttribute(string method)
        {
            this.ErrorMessage = "{0} no es válido";
            this.Method = method;
        }

        public override bool IsValid(Object value, PropertyInfo property)
        {
            return true;
        }
    }

    public class Validator
    {
        public static Dictionary<string, string> Validate(Object item)
        {
            Dictionary<string, string> results;
            if (Validator.TryValidate(item, out results))
            {
                return results;
            }

            return null;
        }

        public static bool TryValidate(Object item, out Dictionary<string, string> results)
        {
            results = new Dictionary<string, string>();
            try
            {
                foreach (PropertyInfo pi in item.GetType().GetProperties())
                {
                    object[] attributes = pi.GetCustomAttributes(typeof(ValidationAttribute), true);
                    if (attributes != null)
                    {
                        for (int x = 0; x < attributes.Length; x++)
                        {
                            ValidationAttribute attr = attributes[x] as ValidationAttribute;
                            bool isValid = attr.IsValid(pi.GetValue(item, null), pi);

                            if (!isValid)
                                results.Add(pi.Name, String.Format(attr.ErrorMessage, pi.Name));
                        }
                    }
                }
            }
            catch (Exception ex) 
            {
                Debugger.Debug.WriteLine(ex);
                return false;
            }

            return true;
        }
    }

    public class Generator
    {
        public IMvcControl Control(Object item)
        {
            return null;
        }
    }

    public enum InputType
    {
        Text, Number, Password, Date, DateTime, Time
    }

    [AttributeUsage(AttributeTargets.Property, Inherited = true)]
    public class DisplayAttriubute : Attribute
    {
        public InputType Type { get; set; }
        public String Title { get; set; }
        public String Description { get; set; }
        public String Format { get; set; }
        public String DefaultValue { get; set; }
        public DisplayAttriubute()
        {
        }
    }
    [AttributeUsage(AttributeTargets.Property, Inherited = true)]
    public class KeyAttribute : Attribute { }
    [AttributeUsage(AttributeTargets.Property, Inherited = true)]
    public class ReadonlyAttribute : Attribute { }
}
