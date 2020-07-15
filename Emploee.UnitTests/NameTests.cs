using CSharpFunctionalExtensions;
using Employee.Domain;
using NUnit.Framework;

namespace Employee.UnitTests
{
    public class Tests
    {
        [Test]
        public void CreateName_ImplicitCastFromEmptyString_ThrowsException()
        {
            Assert.Throws<ResultFailureException>(() =>
            {
                Name name = string.Empty;
            });
        }

        [Test]
        public void CreateName_FromEmptyString_ResultIsFailure()
        {
            Result creationResult = Name.Create(string.Empty);
            Assert.IsTrue(creationResult.IsFailure);
        }
    }
}