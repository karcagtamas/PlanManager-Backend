using System;
using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Shared.Annotations
{
    public class MaxNumberAttribute : ValidationAttribute
    {
        private int Max { get; set; }

        public MaxNumberAttribute(int max)
        {
            this.Max = max;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            try
            {
                var number = (int?) value;

                if (number == null)
                {
                    return ValidationResult.Success;
                }

                if (number > this.Max)
                {
                    return new ValidationResult($"Value is bigger than {this.Max}");
                }
            }
            catch (Exception)
            {
                return new ValidationResult("Field is not integer");
            }

            return ValidationResult.Success;
        }
    }
}