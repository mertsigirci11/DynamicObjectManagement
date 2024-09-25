using DynamicObjectManagement.Core.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DynamicObjectManagement.Service.Validators
{
    public class ObjectValidator<TEntity> where TEntity : class
    {
        private List<ValidationResult> ValidationResults { get; set; } = new();

        private ValidationContext ValidationContext { get; set; }

        public bool ValidateObject(TEntity entity, out List<string> errorMessages)
        {
            ValidationContext = ValidationContext ?? new ValidationContext(entity, serviceProvider: null, items: null);

            if (!Validator.TryValidateObject(entity, ValidationContext, ValidationResults,true))
            {
                errorMessages = ValidationResults.Select(x => x.ErrorMessage).ToList();
                return false;
            }
            errorMessages = null;
            return true;
        }
    }
}
