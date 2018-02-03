using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OrganizerApp.DalEntities.Helpers
{
    [AttributeUsage(AttributeTargets.Property , AllowMultiple = false)]
    public class RequiredIfAttribute : ValidationAttribute
    {
        private string _propertyName;
        private object _expectedPropertyValue;
        public override bool RequiresValidationContext { get { return true; } }


        public RequiredIfAttribute(string propertyName , object expectedPropertyValue , string errorMessage ) : base(errorMessage)
        {
            _propertyName = propertyName;
            _expectedPropertyValue = expectedPropertyValue;
        }


        public override string FormatErrorMessage(string name)
        {
            return string.Format(
                CultureInfo.CurrentCulture,
                base.ErrorMessageString,
                name);
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if(validationContext == null)
            {
                throw new ArgumentException("Validation context is required");
            }

            PropertyInfo realProperty = validationContext.ObjectType.GetProperty(_propertyName);

            if(realProperty == null)
            {
                throw new ArgumentException("Property named {0} doesn`t exist." , _propertyName);
            }

            object realPropertyValue = realProperty.GetValue(validationContext.ObjectInstance);

            bool isPropertysEqual = Object.Equals(realPropertyValue, _expectedPropertyValue);

            if (isPropertysEqual)
            {
                if (value == null)
                return new ValidationResult(ErrorMessage);
            }
            return ValidationResult.Success;
        }
    }
}
