using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CreepCollector.Attributes
{
    public class GreaterThanZeroAttribute : ValidationAttribute, IClientValidatable
    {
        public GreaterThanZeroAttribute()
        {
        }

        public override bool IsValid(object value)
        {
            if (value == null) return false;
            if (value.GetType() != typeof(int) && value.GetType() != typeof(long) && value.GetType() != typeof(double) && value.GetType() != typeof(float)) throw new InvalidOperationException("can only be used on int, long, double, float properties.");
            return (Convert.ToDouble(value) > 0);
        }

        public override string FormatErrorMessage(string name)
        {
            return "The " + name + " field must contain a minimum value of 1 in order to continue.";
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            yield return new ModelClientValidationRule
            {
                ErrorMessage = String.IsNullOrEmpty(ErrorMessage) ? FormatErrorMessage(metadata.DisplayName) : ErrorMessage,
                ValidationType = "greaterthanzero"
            };
        }
    }
}