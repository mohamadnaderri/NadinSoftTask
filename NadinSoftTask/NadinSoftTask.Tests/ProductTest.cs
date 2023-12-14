using NadinSoftTask.DomainModel.Product;
using NadinSoftTask.Infrastructure;

namespace NadinSoftTask.Tests
{
    public class ProductTest
    {
        [Fact]
        public void ProductModel_Should_ValidateManufactureEmail()
        {
            // Arrange
            string name = "Sample Product";
            string manufacturerEmail = "example@email.com";
            string manufacturerPhoneNumber = "02155446325";
            bool isAvailable = true;

            // Act
            var product = new Product(name, DateTime.Now.AddYears(-1), manufacturerPhoneNumber, manufacturerEmail, isAvailable, Guid.NewGuid());

            // Assert
            Assert.True(product.ManufacturerEmail.IsValidEmail());
        }

        [Fact]
        public void ProductModel_Should_ValidateManufacturePhoneNumber()
        {
            // Arrange
            string name = "Sample Product";
            string invalidEmail = "example@email.com"; 
            string manufacturerPhoneNumber = "02155446325";
            bool isAvailable = true;

            // Act
            var product = new Product(name, DateTime.Now.AddDays(-1), manufacturerPhoneNumber, invalidEmail, isAvailable, Guid.NewGuid());

            // Assert
            Assert.True(product.ManufacturerPhoneNumber.IsValidPhoneNumber());
        }

        [Fact]
        public void ProductModel_Should_ProductMustIsAvailable()
        {
            // Arrange
            string name = "Sample Product";
            string invalidEmail = "example@email.com";
            string manufacturerPhoneNumber = "02155446325";
            bool isAvailable = true;

            // Act
            var product = new Product(name, DateTime.Now.AddDays(-1), manufacturerPhoneNumber, invalidEmail, isAvailable, Guid.NewGuid());

            // Assert
            Assert.True(product.IsAvailable);
        }

        [Fact]
        public void ProductModel_Should_ProduceDateMustPast()
        {
            // Arrange
            string name = "Sample Product";
            string invalidEmail = "example@email.com";
            string manufacturerPhoneNumber = "02155446325";
            bool isAvailable = true;

            // Act
            var product = new Product(name, DateTime.Now, manufacturerPhoneNumber, invalidEmail, isAvailable, Guid.NewGuid());

            // Assert
            Assert.True(product.ProduceDate.Date == DateTime.Now.Date);
        }
    }
}