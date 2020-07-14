using System;
using CSharpFunctionalExtensions;

namespace Employee.Domain
{
    public class String : IEquatable<String>
    {
        public string Value { get; }

        public static Result<String> Create(string value)
        {
            return string.IsNullOrWhiteSpace(value) ? 
                Result.Failure<String>("Value can't be empty") : 
                Result.Ok(new String(value));
        }

        private String(string value)
        {
            this.Value = value;
        }

        private String()
        {
        }

        public static implicit operator string(String value)
        {
            return value.Value;
        }

        public static implicit operator String(string value)
        {
            return Create(value).Value;
        }

        public bool Equals(String other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((String)obj);
        }

        public override int GetHashCode()
        {
            return (this.Value != null ? this.Value.GetHashCode() : 0);
        }
    }
}