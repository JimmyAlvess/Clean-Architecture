using CleanArchMvc.Domain.Validation;

namespace CleanArchMvc.Domain.Entities
{
    public sealed class Product : Entity
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public int Stock { get; private set; }
        public string ImageUrl { get; private set; } 
        public int CategoryId { get; set; }
        public Category Category { get; set; } 

        // Construtor sem parâmetros para o Entity Framework
        public Product() { }

        public Product(string name, string description, decimal price, int stock, string image)
        {
            ValidateDomain(name, description, price, stock, image);
        }
        
        public Product(string name, string description, decimal price, int stock, string image, int id)
        {
            DomainExceptionValidation.When(id < 0, "Invalid id. Id must be greater than 0");
            Id = id;
        
            ValidateDomain(name, description, price, stock, image);
        }

        public void Update(string name, string description, decimal price, int stock, string image, int categoryId)
        {
            ValidateDomain(name, description, price, stock, image);
            CategoryId = categoryId;
        }

        public void ValidateDomain(string name, string description, decimal price, int stock, string image)
        {
            DomainExceptionValidation.When(string.IsNullOrWhiteSpace(name), "Invalid name. Name is required");
            DomainExceptionValidation.When(name.Length < 3, "Invalid name. Name must be at least 3 characters");
            DomainExceptionValidation.When(string.IsNullOrWhiteSpace(description), "Invalid description. Description is required");
            DomainExceptionValidation.When(price < 0, "Invalid price. Price must be greater than 0");
            DomainExceptionValidation.When(stock < 0, "Invalid stock. Stock must be greater than 0");
            DomainExceptionValidation.When(string.IsNullOrWhiteSpace(image), "Invalid image. Image is required");
            DomainExceptionValidation.When(image?.Length > 250, "Invalid image name, too long, maximum 250 characters");

            Name = name;
            Description = description;
            Price = price;
            Stock = stock;
            ImageUrl = image;
        }
    }
}