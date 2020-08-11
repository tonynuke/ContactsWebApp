using System;
using CSharpFunctionalExtensions;

namespace Employee.Domain
{
    public class Name : IEquatable<Name>
    {
        public string Value { get; }

        public static Result<Name> Create(string name)
        {
            return string.IsNullOrWhiteSpace(name) ? 
                Result.Failure<Name>("Name can't be empty") : 
                Result.Success(new Name(name));
        }

        private Name(string value)
        {
            this.Value = value;
        }

        private Name()
        {
        }

        public static implicit operator string(Name value)
        {
            return value.Value;
        }

        public static implicit operator Name(string value)
        {
            return Create(value).Value;
        }

        public bool Equals(Name other)
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
            return Equals((Name)obj);
        }

        public override int GetHashCode()
        {
            return (this.Value != null ? this.Value.GetHashCode() : 0);
        }
    }
}