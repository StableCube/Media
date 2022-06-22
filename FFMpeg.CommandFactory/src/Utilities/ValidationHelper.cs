using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StableCube.Media.FFMpeg.CommandFactory
{
    public static class ValidationHelper
    {
        public static bool TryValidate(object data, out ICollection<ValidationResult> results)
        {
            var context = new ValidationContext(@data, serviceProvider: null, items: null);
            results = new List<ValidationResult>();
            
            return Validator.TryValidateObject(
                @data, context, results, 
                validateAllProperties: true
            );
        }

        public static void EnsureValid(object data)
        {
            ICollection<ValidationResult> validationResults = new List<ValidationResult>();
            if(!TryValidate(data, out validationResults))
            {
                throw new ModelValidationFailedException(new ModelValidationError(data, validationResults));
            }
        }
    }

    public class ModelValidationError
    {
        public string ErrorMessage { get; private set; }

        public ModelValidationError(object data, ICollection<ValidationResult> validationResults)
        {
            string dataClass = data.GetType().ToString();
            if(validationResults == null)
            {
                ErrorMessage = $"Class: {dataClass}, ValidationResults: null";
                return;
            }

            List<string> errorMessages = new List<string>();
            foreach (ValidationResult validationResult in validationResults)
            {
                if(validationResult == null)
                {
                    errorMessages.Add("ValidationResult: null");
                    continue;
                }

                string validationErrorMsg = "";
                if(validationResult.ErrorMessage != null)
                {
                    validationErrorMsg += "{";
                    var memberList = new List<string>(validationResult.MemberNames);
                    if(memberList.Count > 0)
                        validationErrorMsg += $"Property: {memberList[0]}, ";

                    validationErrorMsg += $"ErrorMessage: {validationResult.ErrorMessage}";
                }

                validationErrorMsg += "}";
                errorMessages.Add(validationErrorMsg);
            }

            ErrorMessage = $"Class: {dataClass}, Error: {String.Join(" | ", errorMessages)}";
        }
    }

    public class ModelValidationFailedException : Exception
    {
        public ModelValidationFailedException(ModelValidationError error)
            : base(error.ErrorMessage)
        {

        }
    }
}