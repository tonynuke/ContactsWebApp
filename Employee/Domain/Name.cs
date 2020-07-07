using System;
using Shared;

namespace Employee.Domain
{
    public class Name : IEquatable<Name>
    {
        public string Value { get; }

        public static bool IsNotEmpty(string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }

        public Name(string value)
        {
            Contract.Requires(IsNotEmpty(value), "value can't be empty");
            this.Value = value;
        }

        private Name()
        {
        }

        public static implicit operator Name(string value)
        {
            return new Name(value);
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