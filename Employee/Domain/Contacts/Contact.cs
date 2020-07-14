using System;
using CSharpFunctionalExtensions;

namespace Employee.Domain.Contacts
{
    public class Contact : IEquatable<Contact>
    {
        public ContactType Type { get; }

        public string Value { get; }

        public static implicit operator string(Contact contact)
        {
            return contact.Value;
        }

        public static Result<Contact> Create(ContactType type, string value)
        {
            if (type == ContactType.Email)
            {
                return EmailValidator.IsValid(value) ?
                    Result.Ok(new Contact(type, value)) :
                    Result.Failure<Contact>("Email is not valid");
            }

            return string.IsNullOrWhiteSpace(value) ?
                Result.Failure<Contact>($"{type} contact can't be empty") :
                Result.Ok(new Contact(type, value));
        }

        public bool Equals(Contact other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Type == other.Type && Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Contact)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine((int)Type, Value);
        }

        private Contact() { }

        private Contact(ContactType type, string value)
        {
            this.Value = value;
            this.Type = type;
        }
    }
}