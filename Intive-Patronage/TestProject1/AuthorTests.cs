using Intive_Patronage.Entities;
using Intive_Patronage.Tests;
namespace TestProject1
{
    [TestClass]
    public class AuthorTests
    {
        [TestMethod]
        public void CheckFirstName_ReturnsTrue()
        {
            AuthorTestsMethods authorTests= new AuthorTestsMethods();
            Author author = new Author();

            author.FirstName = "Andrzej";

            Assert.IsTrue(authorTests.checkFirstName(author));
        }

        [TestMethod]
        public void CheckLastName_ReturnsTrue()
        {
            AuthorTestsMethods authorTests = new AuthorTestsMethods();
            Author author = new Author();

            author.LastName = "Slimak";

            Assert.IsTrue(authorTests.checkLastName(author));
        }
    }
}