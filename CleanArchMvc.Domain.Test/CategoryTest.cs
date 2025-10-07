using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Validation;
using Xunit;

namespace CleanArchMvc.Domain.Test
{
    public class CategoryTest
    {
        [Fact]
        public void CreateCategory_WithValidName_CreateSuccessfully()
        {
            string validName = "Electronics";

            var category = new Category(validName);

            Assert.Equal(validName, category.Name);
            Assert.NotNull(category.Products);
        }

        [Fact]
        public void CreateCategory_WithValidNameAndId_CreateSuccessfully()
        {
            string validName = "Electronics";
            int validId = 1;

            var category = new Category(validName, validId);

            Assert.Equal(validName, category.Name);
            Assert.Equal(validId, category.Id);
            Assert.NotNull(category.Products);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void CreateCategory_WithInvalidName_ThrowDomainExceptionValidation(string invalidName)
        {
            var exception = Assert.Throws<DomainExceptionValidation>(() => new Category(invalidName));
            Assert.Equal("Invalid name. Name is required", exception.Message);
        }

        [Theory]
        [InlineData("A")]
        [InlineData("AB")]
        public void CreateCategory_WithShortName_ThrowDomainExceptionValidation(string name)
        {
            var exception = Assert.Throws<DomainExceptionValidation>(() => new Category(name));
            Assert.Equal("Invalid name. Name must be at least 3 characters", exception.Message);
        }

        [Fact]
        public void CreateCategory_WithNegativeId_ThrowDomainExceptionValidation()
        {
            string validName = "Electronics";
            int negativeId = -1;

            var exception = Assert.Throws<DomainExceptionValidation>(() => new Category(validName, negativeId));
            Assert.Equal("Invalid id. Id must be greater than 0", exception.Message);
        }

        [Fact]
        public void UpdateCategory_WithValidName_UpdateSuccessfully()
        {
            var category = new Category("Electronics");
            string newName = "Computer Science";

            category.Update(newName);

            Assert.Equal(newName, category.Name);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void UpdateCategory_WithInvalidName_ThrowDomainExceptionValidation(string invalidName)
        {
            var category = new Category("Electronics");

            var exception = Assert.Throws<DomainExceptionValidation>(() => category.Update(invalidName));
            Assert.Equal("Invalid name. Name is required", exception.Message);
        }

        [Theory]
        [InlineData("A")]
        [InlineData("AB")]
        public void UpdateCategory_WithShortName_ThrowDomainExceptionValidation(string shortName)
        {
            var category = new Category("Electronics");

            var exception = Assert.Throws<DomainExceptionValidation>(() => category.Update(shortName));
            Assert.Equal("Invalid name. Name must be at least 3 characters", exception.Message);
        }
    }
}