using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Tienda.CustomValidations
{
    public class CompararPrecioAttribute : ValidationAttribute
    {
        private readonly string _propertyName;
        public CompararPrecioAttribute(string propertyName, string errorMessage) 
            : base(errorMessage)
        {
            _propertyName = propertyName;
        }
        protected override ValidationResult IsValid(
            object firstValue, ValidationContext validationContext)
        {
            var propertyInfo = validationContext.ObjectType.GetProperty(_propertyName);
            if (propertyInfo == null)
                return new ValidationResult(String.Format("La propiedad {0} no existe", _propertyName));

            var propertyValue = propertyInfo.GetValue(validationContext.ObjectInstance, null);
            if ((float)firstValue >= (float)propertyValue)
            {
                return ValidationResult.Success;
            }
            
            return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
        }
    }
}