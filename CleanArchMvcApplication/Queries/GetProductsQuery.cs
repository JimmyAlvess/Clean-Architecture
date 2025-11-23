using CleanArchMvc.Domain.Entities;
using MediatR;

namespace CleanArchMvcApplication.Queries
{
    public class GetProductsQuery : IRequest<IEnumerable<Product>>
    {

    }
}
