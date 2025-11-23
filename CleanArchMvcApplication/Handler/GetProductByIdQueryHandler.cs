using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using CleanArchMvcApplication.Queries;
using MediatR;

namespace CleanArchMvcApplication.Handler
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Product>
    {
        private readonly IProductRepository _productRepository;

        public GetProductByIdQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Product> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new ApplicationException($"Error, objetct invalid");

            return await _productRepository.GetByIdAsync(request.Id);
        }
    }
}
