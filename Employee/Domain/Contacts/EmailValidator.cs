using System.Text.RegularExpressions;

namespace Employee.Domain.Contacts
{
    public static class EmailValidator
    {
        private const string EmailPattern = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
        private static readonly Regex EmailRegex = new Regex(EmailPattern);

        public static bool IsValid(string email)
        {
            Match match = EmailRegex.Match(email);
            return match.Success;
        }
    }
}