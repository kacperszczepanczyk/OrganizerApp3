
using OrganizerApp.Helpers;
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
            if(String.IsNullOrWhiteSpace(propertyName))
            {
                throw new ArgumentNullException("Właściwość PropertyName nie może być: nullem, składać się z białych znaków, być pustym stringiem .");
            }
            _propertyName = propertyName;
            _expectedPropertyValue = expectedPropertyValue;
        }

        public RequiredIfAttribute(string propertyName, object expectedPropertyValue, Type errorMessageResourceType , string errorMessageResourceName) : this(propertyName , expectedPropertyValue , null)
        {
            ErrorMessageResourceName = errorMessageResourceName;
            ErrorMessageResourceType = errorMessageResourceType;
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
                    if(ErrorMessageResourceType != null && !String.IsNullOrWhiteSpace(ErrorMessageResourceName))
                    {
                        string message = ResourceHelpers.GetResourceValue(ErrorMessageResourceType , ErrorMessageResourceName);
                        return new ValidationResult(message);
                    }
                    else if (!String.IsNullOrWhiteSpace(ErrorMessage))
                    {
                        return new ValidationResult(ErrorMessage);
                    }
                    else
                    {
                        throw new ArgumentException("Jeden z następujących zestawów właściwości musi spełniać następujące kryteria:\n" +
                                                    " 1) ErrorMessageResourceType (różny od null), ErrorMessageResourceName (różny od null, nie składający się tylko z białych znaków, nie będący pustym stringiem )\n" +
                                                    " 2) ErrorMessage (różny od null, nie składający się tylko z białych znaków, nie będący pustym stringiem )");
                    }
            }
            return ValidationResult.Success;
        }
    }
}
