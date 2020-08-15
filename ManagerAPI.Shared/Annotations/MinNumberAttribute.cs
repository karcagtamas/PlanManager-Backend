using System;
using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Shared.Annotations
{
    public class MinNumberAttribute : ValidationAttribute
    {
        private int Min { get; set; }

        public MinNumberAttribute(int min)
        {
            this.Min = min;
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

                if (number < this.Min)
                {
                    return new ValidationResult($"Value is less than {this.Min}");
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