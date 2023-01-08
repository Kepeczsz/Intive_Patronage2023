using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntivePatronage.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]

        public void TestSurname()
        {
            // Arrange
            Author author = new Author();
            author.Surname = "Smith";

            // Act
            bool isValid = author.IsValidSurname();

            // Assert
            Assert.IsTrue(isValid);
        }

    }
}
