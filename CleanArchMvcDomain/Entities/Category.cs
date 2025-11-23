using CleanArchMvc.Domain.Validation;

namespace CleanArchMvc.Domain.Entities
{
    public sealed class Category : Entity
    {
        public string Name { get; private set; }
        public ICollection<Product> Products { get; private set; } = new List<Product>();

        public Category() { }

        public Category(string name)
        {
            ValidateDomain(name);
        }

        public Category(string name, int id)
        {
            ValidateDomain(name);
            DomainExceptionValidation.When(id < 0, "Invalid id. Id must be greater than 0");
            Id = id;
        }

        public void Update(string name)
        {
            ValidateDomain(name);
        }

        public void ValidateDomain(string name)
        {
            DomainExceptionValidation.When(string.IsNullOrWhiteSpace(name),
             "Invalid name. Name is required");

            DomainExceptionValidation.When(name.Length < 3,
            "Invalid name. Name must be at least 3 characters");
            Name = name;
        }
    }
}