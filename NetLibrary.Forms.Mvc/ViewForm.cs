using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace NetLibrary.Forms.Mvc
{
    public class ViewForm<T> : View where T : class, new()
    {
        private Dictionary<string, string> _modelState = new Dictionary<string, string>();
        private Dictionary<string, string> _userModelErrors = new Dictionary<string, string>();

        public Dictionary<string, string> ModelState { get { return _modelState; } }
        new public T Model { 
            get { return (T)base.Model; } 
            set { base.Model = value; } 
        }

        public ViewForm(T model)
        {
            base.Model = model;
        }

        public void AddModelError(string key, string message)
        {
            _userModelErrors.Add(key, message);
        }

        public void AddModelError(Exception ex)
        {
            _userModelErrors.Add("Exception", ex.Message);
        }

        protected override bool Validate()
        {
            if (!base.Validate()) return false;
            return ValidateModel().Count == 0;
        }

        public Dictionary<string, string> ValidateModel()
        {
            _modelState = new Dictionary<string, string>();
            ValidationContext valContext = new ValidationContext(this.Model);
            List<ValidationResult> validationResult = new List<ValidationResult>();
            if (Validator.TryValidateObject(this.Model, valContext, validationResult))
                foreach (ValidationResult validation in validationResult)
                    foreach (string attr in validation.MemberNames)
                        _modelState.Add(attr, validation.ErrorMessage);

            foreach (var item in _userModelErrors)
                _modelState.Add(item.Key, item.Value);

            return this.ModelState;
        }

        protected override void Initialize()
        {
            base.Initialize();

            if (base.Model != null)
            {

            }
        }
    }
}
