 using CleanArchMvc.Domain.Entities;
using MediatR;

namespace CleanArchMvcApplication.Products.Command
{
    public abstract class ProductCommand : IRequest<Product>
    {
        public string Nome { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string Image { get; set; }
        public int CategoryId { get; set; }
    }
}