using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Tienda.CustomValidations
{
    public class ExcludeCharAttribute : ValidationAttribute, IClientValidatable
    {
        private readonly string _chars;
        public ExcludeCharAttribute(string chars)
            : base("{0} continen caracteres invalidos.")
        {
            _chars = chars;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var valueAsString = value.ToString();
                for (int i = 0; i < _chars.Length; i++)
                {
                    if (valueAsString.Contains(_chars[i]))
                    {
                        var errorMessage = FormatErrorMessage(validationContext.DisplayName);
                        return new ValidationResult(errorMessage);
                    }
                }
            }
            return ValidationResult.Success;
        }
        //new method  
        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule();
            rule.ErrorMessage = FormatErrorMessage(metadata.GetDisplayName());
            rule.ValidationParameters.Add("chars", _chars);
            rule.ValidationType = "exclude";
            yield return rule;
        }
    }
}