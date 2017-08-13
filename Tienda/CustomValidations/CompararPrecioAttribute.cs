using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Tienda.CustomValidations
{
    public enum GenericCompareOperator
    {
        GreaterThan,
        GreaterThanOrEqual,
        LessThan,
        LessThanOrEqual
    }
    public class GenericCompareAttribute : ValidationAttribute, IClientValidatable
    {
        private GenericCompareOperator operatorname = GenericCompareOperator.GreaterThanOrEqual;
        public string CompareToPropertyName { get; set; }
        public GenericCompareOperator OperatorName { get { return operatorname; } set { operatorname = value; } }

        private readonly string _errorMessage;
        public GenericCompareAttribute() : base(){}
        
        protected override ValidationResult IsValid(
            object value, ValidationContext validationContext)
        {
            string operstring = 
                (OperatorName == GenericCompareOperator.GreaterThan ? "Mayor que " 
                : (OperatorName == GenericCompareOperator.GreaterThanOrEqual ? "Mayor o igual que " 
                    : (OperatorName == GenericCompareOperator.LessThan ? "Menor que " 
                        : (OperatorName == GenericCompareOperator.LessThanOrEqual ? "Monor o igual que " : ""))));

            var basePropertyInfo = validationContext.ObjectType.GetProperty(CompareToPropertyName);
            if (basePropertyInfo == null)
                return new ValidationResult(String.Format("La propiedad {0} no existe", CompareToPropertyName));

            var valOther = (IComparable)basePropertyInfo.GetValue(validationContext.ObjectInstance, null);
            var valThis = (IComparable)value;

            if ((operatorname == GenericCompareOperator.GreaterThan && valThis.CompareTo(valOther) <= 0) ||
                (operatorname == GenericCompareOperator.GreaterThanOrEqual && valThis.CompareTo(valOther) < 0) ||
                (operatorname == GenericCompareOperator.LessThan && valThis.CompareTo(valOther) >= 0) ||
                (operatorname == GenericCompareOperator.LessThanOrEqual && valThis.CompareTo(valOther) > 0))
                return new ValidationResult(base.ErrorMessage);
            return ValidationResult.Success;
            
        }
        public override string FormatErrorMessage(string name)
        {
            return string.Format(ErrorMessageString, name, CompareToPropertyName);
        }
        public IEnumerable<ModelClientValidationRule>
        GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            string errorMessage = this.FormatErrorMessage(metadata.DisplayName);
            ModelClientValidationRule compareRule = new ModelClientValidationRule();
            compareRule.ErrorMessage = errorMessage;
            compareRule.ValidationType = "genericcompare";
            compareRule.ValidationParameters.Add("comparetopropertyname", CompareToPropertyName);
            compareRule.ValidationParameters.Add("operatorname", OperatorName.ToString());
            yield return compareRule;
        }
    }
}