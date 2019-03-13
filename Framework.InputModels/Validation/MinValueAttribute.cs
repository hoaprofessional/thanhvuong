using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Framework.InputModels.Validation
{
    public class MinValueAttribute : ValidationAttribute
    {
        int minValue;
        public MinValueAttribute(int minValue)
        {
            this.minValue = minValue;
        }
        public override bool IsValid(object value)
        {
            return ((int)value > minValue);
        }
    }
}
