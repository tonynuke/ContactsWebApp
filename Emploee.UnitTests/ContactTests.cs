using Employee.Domain.Contacts;
using NUnit.Framework;

namespace Employee.UnitTests
{
    [TestFixture]
    public class ContactTests
    {
        [Test]
        public void Contacts_SameValues_DifferentTypes_NotEquals()
        {
            Contact email = Contact.Create(ContactType.Email,"something@mail.ru").Value;
            Contact other = Contact.Create(ContactType.Other, "something@mail.ru").Value;

            bool isEquals = email == other;

            Assert.IsFalse(isEquals);
        }

        [Test]
        public void Contacts_SameValues_SameTypes_Equals()
        {
            var email1 = Contact.Create(ContactType.Email, "something@mail.ru").Value;
            var email2 = Contact.Create(ContactType.Email, "something@mail.ru").Value;

            bool isEquals = email1 == email2;

            Assert.IsTrue(isEquals);
        }

        [Test]
        [TestCase("my@mail.ru", true)]
        [TestCase("mymail.ru", false)]
        [TestCase("mymail@.ru", false)]
        [TestCase("", false)]
        public void CreateEmail_CreationResultEqualsToTemplate(string email, bool expectedResult)
        {
            bool actualResult = Contact.Create(ContactType.Email, email).IsSuccess;
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        [TestCase("my@mail.ru", false)]
        [TestCase("88005553535", true)]
        [TestCase("", false)]
        public void CreatePhone_CreationResultEqualsToTemplate(string phone, bool expectedResult)
        {
            bool actualResult = Contact.Create(ContactType.Phone, phone).IsSuccess;
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        [TestCase("my@mail.ru", true)]
        [TestCase("88005553535", true)]
        [TestCase("", false)]
        public void CreateSkype_CreationResultEqualsToTemplate(string skype, bool expectedResult)
        {
            bool actualResult = Contact.Create(ContactType.Skype, skype).IsSuccess;
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}