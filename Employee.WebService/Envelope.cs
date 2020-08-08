using System;
using System.Collections.Generic;

namespace Employee.WebService
{
    public class ValidationResult
    {
        public string FieldName { get; }
        public IEnumerable<string> Errors { get; }

        public ValidationResult(string fieldName, IEnumerable<string> errors)
        {
            this.FieldName = fieldName ?? throw new ArgumentNullException(nameof(fieldName));
            this.Errors = errors ?? throw new ArgumentNullException(nameof(errors));
        }
    }

    public class Envelope<T>
    {
        public T Result { get; }
        public string ErrorMessage { get; }
        public DateTime TimeGenerated { get; }
        public IEnumerable<ValidationResult> ValidationErrors { get; } = new List<ValidationResult>();

        protected internal Envelope(T result, string errorMessage)
        {
            this.Result = result;
            this.ErrorMessage = errorMessage;
            this.TimeGenerated = DateTime.UtcNow;
        }

        protected internal Envelope(IEnumerable<ValidationResult> validationResults)
        {
            TimeGenerated = DateTime.UtcNow;
            ValidationErrors = validationResults;
        }
    }

    public class Envelope : Envelope<string>
    {
        protected Envelope(string errorMessage)
            : base(string.Empty, errorMessage)
        {
        }

        protected Envelope(IEnumerable<ValidationResult> validationResults)
            : base(validationResults)
        {
        }

        public static Envelope<T> Ok<T>(T result)
        {
            return new Envelope<T>(result, string.Empty);
        }

        public static Envelope Ok()
        {
            return new Envelope(string.Empty);
        }

        public static Envelope Error(string errorMessage)
        {
            return new Envelope(errorMessage);
        }

        public static Envelope Error(IEnumerable<ValidationResult> validationResults)
        {
            return new Envelope(validationResults);
        }
    }
}