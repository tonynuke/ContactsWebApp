using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;

namespace Employee.Domain.Contacts
{
    public static class ContactValidator
    {
        private const string EmailPattern = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";

        private const string PhonePattern = @"^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$";

        private static readonly Regex EmailRegex = new Regex(EmailPattern);

        private static readonly Regex PhoneRegex = new Regex(PhonePattern);

        public static Result IsValid(ContactType type, string value)
        {
            switch (type)
            {
                case ContactType.Email:
                    return EmailRegex.Match(value).Success ?
                        Result.Ok() :
                        Result.Failure($"{value} is not valid Email");
                case ContactType.Phone:
                    return PhoneRegex.Match(value).Success ?
                        Result.Ok() :
                        Result.Failure($"{value} is not valid phone");
                default:
                    return !string.IsNullOrWhiteSpace(value) ?
                        Result.Ok() :
                        Result.Failure($"{value} can't be empty");
            }
        }
    }
}