using CleanArchMvc.Domain.Entities;
using MediatR;

namespace CleanArchMvcApplication.Products.Command
{
    public class ProductRemoveCommand : IRequest<Product>
    {
        public int Id { get; set; }
        public ProductRemoveCommand(int id)
        {
            Id = id;
        }
    }
}
