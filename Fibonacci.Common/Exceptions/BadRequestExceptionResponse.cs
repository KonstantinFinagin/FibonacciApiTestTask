using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Fibonacci.Common.Exceptions
{
    /// <summary>
    /// Defines a serializable container for storing ModelState information.
    /// This information is stored as key/value pairs.
    /// </summary>
    public sealed class BadRequestExceptionResponse : Dictionary<string, string[]>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BadRequestExceptionResponse"/> class.
        /// </summary>
        public BadRequestExceptionResponse()
            : base(StringComparer.OrdinalIgnoreCase)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BadRequestExceptionResponse"/> class.
        /// </summary>
        /// <param name="error">Error description.</param>
        public BadRequestExceptionResponse(string error)
            : this(CreateDefaultDictionary(error))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BadRequestExceptionResponse"/> class.
        /// </summary>
        /// <param name="modelState"><see cref="ModelStateDictionary"/> containing the validation errors.</param>
        public BadRequestExceptionResponse(ModelStateDictionary modelState)
            : this()
        {
            if (modelState == null)
            {
                throw new ArgumentNullException(nameof(modelState));
            }

            if (modelState.IsValid)
            {
                return;
            }

            foreach (var keyModelStatePair in modelState)
            {
                var key = keyModelStatePair.Key;
                var errors = keyModelStatePair.Value.Errors;
                if (errors == null || errors.Count <= 0)
                {
                    continue;
                }

                var errorMessages = errors.Select(error => string.IsNullOrEmpty(error.ErrorMessage) ? "Invalid field." : error.ErrorMessage).ToArray();
                Add(key, errorMessages);
            }
        }

        private static ModelStateDictionary CreateDefaultDictionary(string error)
        {
            var dictionary = new ModelStateDictionary();
            dictionary.AddModelError("general", error);

            return dictionary;
        }
    }
}
