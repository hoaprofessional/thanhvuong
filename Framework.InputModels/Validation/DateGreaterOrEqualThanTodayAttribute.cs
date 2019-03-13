using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Framework.InputModels.Validation
{
    public class DateGreaterOrEqualThanTodayAttribute : ValidationAttribute
    {
        public DateGreaterOrEqualThanTodayAttribute()
        {
        }
        public override bool IsValid(object value)
        {
            if ((DateTime)value < DateTime.Today)
            {
                return false;
            }
            return base.IsValid(value);
        }
    }
}
