using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Validation;
using Xunit;

namespace CleanArchMvc.Domain.Test
{
    public class ProductTest
    {
        private const string ValidName = "Macbook";
        private const string ValidDescription = "Macbook M1";
        private const decimal ValidPrice = 100.00m;
        private const int ValidStock = 5;
        private const string ValidImage = "image.jpg";
        private const int ValidId = 1;
        private const int categoryId = 1;

        private Product CreateValidProduct() => new Product(ValidName, ValidDescription, ValidPrice, ValidStock, ValidImage);

        private Product CreateValidProductWithId(int id) => new Product(ValidName, ValidDescription, ValidPrice, ValidStock, ValidImage, id);

        private void AssertProductProperties(Product product, string name, string description, decimal price, int stock, string image)
        {
            Assert.Equal(name, product.Name);
            Assert.Equal(description, product.Description);
            Assert.Equal(price, product.Price);
            Assert.Equal(stock, product.Stock);
            Assert.Equal(image, product.ImageUrl);
        }

        private void AssertValidationException<T>(Action action, string expectedMessage) where T : Exception
        {
            var exception = Assert.Throws<T>(action);
            Assert.Equal(expectedMessage, exception.Message);
        }

        private void AssertValidationForCreate(string name, string description, decimal price, int stock, string image, string expectedMessage)
        {
            AssertValidationException<DomainExceptionValidation>(() => new Product(name, description, price, stock, image), expectedMessage);
        }

        private void AssertValidationForUpdate(string name, string description, decimal price, int stock, string image, int categoryId, string expectedMessage)
        {
            var product = CreateValidProduct();
            AssertValidationException<DomainExceptionValidation>(() => product.Update(name, description, price, stock, image, categoryId), expectedMessage);
        }

        [Fact]
        public void CreateProduct_WithValidData_CreateSuccessfully()
        {
            var product = CreateValidProduct();
            AssertProductProperties(product, ValidName, ValidDescription, ValidPrice, ValidStock, ValidImage);
        }

        [Fact]
        public void CreateProduct_WithValidDataAndId_CreateSuccessfully()
        {
            var product = CreateValidProductWithId(ValidId);

            AssertProductProperties(product, ValidName, ValidDescription, ValidPrice, ValidStock, ValidImage);
            Assert.Equal(ValidId, product.Id);
        }

        [Theory]
        [InlineData("", "Macbook M1", 100.00, 5, "image.jpg")]
        [InlineData(" ", "Macbook M1", 100.00, 5, "image.jpg")]
        [InlineData(null, "Macbook M1", 100.00, 5, "image.jpg")]
        public void CreateProduct_WithInvalidName_ThrowDomainExceptionValidation(string name, string description, decimal price, int stock, string image)
        {
            AssertValidationForCreate(name, description, price, stock, image, "Invalid name. Name is required");
        }

        [Theory]
        [InlineData("A", "Macbook M1", 100.00, 5, "image.jpg")]
        [InlineData("AB", "Macbook M1", 100.00, 5, "image.jpg")]
        public void CreateProduct_WithShortName_ThrowDomainExceptionValidation(string name, string description, decimal price, int stock, string image)
        {
            AssertValidationForCreate(name, description, price, stock, image, "Invalid name. Name must be at least 3 characters");
        }

        [Theory]
        [InlineData("Macbook", "", 100.00, 5, "image.jpg")]
        [InlineData("Macbook", " ", 100.00, 5, "image.jpg")]
        [InlineData("Macbook", null, 100.00, 5, "image.jpg")]
        public void CreateProduct_WithInvalidDescription_ThrowDomainExceptionValidation(string name, string description, decimal price, int stock, string image)
        {
            AssertValidationForCreate(name, description, price, stock, image, "Invalid description. Description is required");
        }

        [Theory]
        [InlineData("Macbook", "Macbook M1", -100.00, 5, "image.jpg")]
        [InlineData("Macbook", "Macbook M1", -0.01, 5, "image.jpg")]
        public void CreateProduct_WithNegativePrice_ThrowDomainExceptionValidation(string name, string description, decimal price, int stock, string image)
        {
            AssertValidationForCreate(name, description, price, stock, image, "Invalid price. Price must be greater than 0");
        }

        [Theory]
        [InlineData("Macbook", "Macbook M1", 100.00, -5, "image.jpg")]
        [InlineData("Macbook", "Macbook M1", 100.00, -1, "image.jpg")]
        public void CreateProduct_WithNegativeStock_ThrowDomainExceptionValidation(string name, string description, decimal price, int stock, string image)
        {
            AssertValidationForCreate(name, description, price, stock, image, "Invalid stock. Stock must be greater than 0");
        }

        [Theory]
        [InlineData("Macbook", "Macbook M1", 100.00, 5, "")]
        [InlineData("Macbook", "Macbook M1", 100.00, 5, " ")]
        [InlineData("Macbook", "Macbook M1", 100.00, 5, null)]
        public void CreateProduct_WithInvalidImage_ThrowDomainExceptionValidation(string name, string description, decimal price, int stock, string image)
        {
            AssertValidationForCreate(name, description, price, stock, image, "Invalid image. ImageUrl is required");
        }

        [Fact]
        public void CreateProduct_WithTooLongImage_ThrowDomainExceptionValidation()
        {
            string longImage = new string('a', 251);
            AssertValidationException<DomainExceptionValidation>(() => new Product("Macbook", "Macbook M1", 100.00m, 5, longImage), 
                "Invalid image name, too long, maximum 250 characters");
        }

        [Fact]
        public void CreateProduct_WithNegativeId_ThrowDomainExceptionValidation()
        {
            AssertValidationException<DomainExceptionValidation>(() => CreateValidProductWithId(-1), 
                "Invalid id. Id must be greater than 0");
        }

        [Fact]
        public void UpdateProduct_WithValidData_UpdateSuccessfully()
        {
            var product = CreateValidProduct();

            product.Update(ValidName, ValidDescription, ValidPrice, ValidStock, ValidImage, categoryId);

            AssertProductProperties(product, ValidName, ValidDescription, ValidPrice, ValidStock, ValidImage);
            Assert.Equal(categoryId, product.CategoryId);
        }

        [Theory]
        [InlineData("", "Macbook M1", 100.00, 5, "image.jpg", 1)]
        [InlineData(" ", "Macbook M1", 100.00, 5, "image.jpg", 1)]
        [InlineData(null, "Macbook M1", 100.00, 5, "image.jpg", 1)]
        public void UpdateProduct_WithInvalidName_ThrowDomainExceptionValidation(string name, string description, decimal price, int stock, string image, int categoryId)
        {
            AssertValidationForUpdate(name, description, price, stock, image, categoryId, "Invalid name. Name is required");
        }

        [Theory]
        [InlineData("A", "Macbook M1", 100.00, 5, "image.jpg", 1)]
        [InlineData("AB", "Macbook M1", 100.00, 5, "image.jpg", 1)]
        public void UpdateProduct_WithShortName_ThrowDomainExceptionValidation(string name, string description, decimal price, int stock, string image, int categoryId)
        {
            AssertValidationForUpdate(name, description, price, stock, image, categoryId, "Invalid name. Name must be at least 3 characters");
        }

        [Theory]
        [InlineData("Macbook", "", 100.00, 5, "image.jpg", 1)]
        [InlineData("Macbook", " ", 100.00, 5, "image.jpg", 1)]
        [InlineData("Macbook", null, 100.00, 5, "image.jpg", 1)]
        public void UpdateProduct_WithInvalidDescription_ThrowDomainExceptionValidation(string name, string description, decimal price, int stock, string image, int categoryId)
        {
            AssertValidationForUpdate(name, description, price, stock, image, categoryId, "Invalid description. Description is required");
        }

        [Theory]
        [InlineData("Macbook", "Macbook M1", -100.00, 5, "image.jpg", 1)]
        [InlineData("Macbook", "Macbook M1", -0.01, 5, "image.jpg", 1)]
        public void UpdateProduct_WithNegativePrice_ThrowDomainExceptionValidation(string name, string description, decimal price, int stock, string image, int categoryId)
        {
            AssertValidationForUpdate(name, description, price, stock, image, categoryId, "Invalid price. Price must be greater than 0");
        }

        [Theory]
        [InlineData("Macbook", "Macbook M1", 100.00, -5, "image.jpg", 1)]
        [InlineData("Macbook", "Macbook M1", 100.00, -1, "image.jpg", 1)]
        public void UpdateProduct_WithNegativeStock_ThrowDomainExceptionValidation(string name, string description, decimal price, int stock, string image, int categoryId)
        {
            AssertValidationForUpdate(name, description, price, stock, image, categoryId, "Invalid stock. Stock must be greater than 0");
        }

        [Theory]
        [InlineData("Macbook", "Macbook M1", 100.00, 5, "", 1)]
        [InlineData("Macbook", "Macbook M1", 100.00, 5, " ", 1)]
        [InlineData("Macbook", "Macbook M1", 100.00, 5, null, 1)]
        public void UpdateProduct_WithInvalidImage_ThrowDomainExceptionValidation(string name, string description, decimal price, int stock, string image, int categoryId)
        {
            AssertValidationForUpdate(name, description, price, stock, image, categoryId, "Invalid image. ImageUrl is required");
        }

        [Fact]
        public void UpdateProduct_WithTooLongImage_ThrowDomainExceptionValidation()
        {
            string longImage = new string('a', 251); 
            AssertValidationForUpdate("Macbook", "Macbook M1", 100.00m, 5, longImage, 1, "Invalid image name, too long, maximum 250 characters");
        }
    }
}

